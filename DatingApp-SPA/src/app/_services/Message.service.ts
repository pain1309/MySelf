import { Message } from './../_models/Message';
import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Subject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn: 'root'
})
export class MessageService {
    jwtHelper = new JwtHelperService();
    private connection: signalR.HubConnection;
    message = new Subject<Message>();

    connect(token) {
        // let tokenModel: TokenResponse = {
        //     access_token: token,
        //     expires_in: 36000,
        //     token_type: "Bearer"
        // }
        if (!this.connection) {
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl('http://localhost:5000/chathub',
                  {
                    accessTokenFactory: () => token
                })
                .build();
            this.connection.on('receive', (user, content) => {
                this.message.next({ user, content });
            });
            this.connection.start().catch(err => console.log(err));
        }
    }

    send(nickname, message) {
        console.log();
        this.connection.invoke('SendMessage', message, nickname)
            .catch(err => console.log(err));
    }

}
