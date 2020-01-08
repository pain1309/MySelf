import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MessResolver } from './_resolvers/mess.resolver';
import { GroupComponent } from './group/group.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members', component: MemberListComponent,
                resolve: {users: MemberListResolver}},
            { path: 'members/:id', component: MemberDetailComponent, 
                resolve: {user: MemberDetailResolver} },
            { path: 'messages/:id', component: MessagesComponent,
                resolve: {user: MessResolver} },
            { path: 'lists', component: ListsComponent },
            { path: 'groups', component: GroupComponent }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];