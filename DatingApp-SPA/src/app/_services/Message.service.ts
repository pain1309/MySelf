import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Message } from './../_models/Message';
import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Subject, Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Conversation } from '../_models/Conversation';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class MessageService {
    jwtHelper = new JwtHelperService();
    private connection: signalR.HubConnection;
    message = new Subject<Message>();
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient, private authService: AuthService) {}

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

    send(user, user_counter, message) {
        this.connection.invoke('SendMessage', message, user, user_counter)
            .catch(err => console.log(err));
    }

    loadMess(user_counter): Observable<Conversation[]> {
        return this.http.get<Conversation[]>(this.baseUrl + 'conversation/GetConversation?user=' 
                + this.authService.decodedToken.unique_name + '&user_counter=' + user_counter);
    }

}
