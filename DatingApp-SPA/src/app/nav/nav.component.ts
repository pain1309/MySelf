import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { MessageService } from '../_services/Message.service';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
    model: any = {};
    constructor(
        public authService: AuthService,
        private alertify: AlertifyService,
        private router: Router,
        private notification: MessageService
    ) {}

    ngOnInit() {}

    login() {
        this.authService.login(this.model).subscribe(
            next => {
                this.alertify.success('Logged in successfully!');
            },
            error => {
                this.alertify.error(error);
            },
            () => {
                this.router.navigate(['/members']);
                this.notification.connect(localStorage.getItem('token'));
            }
        );
    }
    loggedIn() {
        return this.authService.loggedIn();
    }
    logout() {
        localStorage.removeItem('token');
        this.alertify.message('logout');
        this.router.navigate(['/home']);
    }
}