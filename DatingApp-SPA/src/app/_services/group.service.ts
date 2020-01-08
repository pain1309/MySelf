import { AuthService } from './auth.service';
import { Message } from './../_models/Message';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Group } from '../_models/Group';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { NewsItem } from '../_models/NewsItem';

@Injectable({
    providedIn: 'root'
})
export class GroupService {
    private connection: signalR.HubConnection;
    messGroup: any;
    baseUrl = environment.apiUrl;
    constructor(private http: HttpClient, private authService: AuthService) {}
    
    connect(token) {
        if(!this.connection) {
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl('http://localhost:5000/looney',
                    {
                        accessTokenFactory: () => token
                    }
                ).build();
            this.connection.start().catch(err => console.log(err));
        }
    }

    sendMess(message, group)
    {
        this.messGroup = new NewsItem();
        this.messGroup.AddData('header of message', 
            message, 
            this.authService.decodedToken.unique_name,
            group
        )
        console.log(this.messGroup);
        this.connection.invoke('Send', this.messGroup)
            .catch(err => console.log(err));
    }

    getGroups(): Observable<Group[]> {
        return this.http.get<Group[]>(this.baseUrl + 'News');
    }

    getGroup(id): Observable<Group> {
        return this.http.get<Group>(this.baseUrl + 'News/' + id);
    }
}
