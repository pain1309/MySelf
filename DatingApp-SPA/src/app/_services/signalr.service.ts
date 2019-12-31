import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Subject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/Message';
import { Chat } from '../_models/Chat';
import { ChatModel } from '../_models/ChatModel';

@Injectable({
    providedIn: 'root'
})
export class SignalrService {
    public contentMess: Chat;
    private message$: Subject<Message>;
    private chat$: ChatModel;
    private connection: signalR.HubConnection;

    constructor() {
        this.message$ = new Subject<Message>();
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(environment.hubUrl)
            .build();

        this.connect();
    }

    private connect() {
        this.connection.start().catch(err => console.log(err));
        // định nghĩa các phương thức treen client mà hub (trên server) có thể gọi sử dụng connection.on, sau khi build nhưng trước khi start
        this.connection.on('SendMessage', message => {
            this.message$.next(message);
        });
        this.connection.on('ReceiveMessage', message => {
            this.chat$.message = message;
        });
    }

    public getMessage(): Observable<Message> {
        return this.message$;
    }

    public getChat(): ChatModel {
        return this.chat$;
    }

    public sendChatToHub(message: string): void {
        this.connection.invoke('SendMessageToAll', message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    

    public disconnect() {
        this.connection.stop();
    }
}
