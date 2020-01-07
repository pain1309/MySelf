import { JwtHelperService } from '@auth0/angular-jwt';
import { ActivatedRoute } from '@angular/router';
import { Validators, FormBuilder } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Message } from '../_models/Message';
import { Subscription } from 'rxjs';
import { MessageService } from '../_services/Message.service';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';

@Component({
    selector: 'app-messages',
    templateUrl: './messages.component.html',
    styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
    messages: Message[] = [];
    user: User;
    messageForm = this.fb.group({
        message: ['', Validators.required]
    });
    notificationSubscription: Subscription;

    constructor(
        private fb: FormBuilder,
        private notification: MessageService,
        private route: ActivatedRoute,
        private authService: AuthService
    ) {}

    ngOnInit() {
        this.notificationSubscription = this.notification.message.subscribe(
            msg => {
                this.messages.push(msg);
            }
        );
        
        this.route.data.subscribe(data => {
            this.user = data['user'];
        });
    }

    onSubmit() {
        this.notification.send(this.authService.decodedToken.unique_name, this.messageForm.getRawValue().message);
    }
}
