import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Group } from '../_models/Group';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class GroupService {
    baseUrl = environment.apiUrl;
    constructor(private http: HttpClient) {}
    getGroups(): Observable<Group[]> {
        return this.http.get<Group[]>(this.baseUrl + 'News/GetAllGroups');
    }
    getGroup(id): Observable<Group> {
        return this.http.get<Group>(this.baseUrl + 'News/' + id);
    }
}
