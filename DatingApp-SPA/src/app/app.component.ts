import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subscription } from 'rxjs';
import { Message } from './_models/Message';
import { SignalrService } from './_services/signalr.service';
import { Chat } from './_models/Chat';
import { ChatModel } from './_models/ChatModel';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    jwtHelper = new JwtHelperService();
    // Subscription:  là kết quả có được sau khi thực hiện một Observable, nó thường dùng cho việc hủy việc tiếp tục xử lý.
    private signalRSubscription: Subscription;

    public content: Message;
    public contentMess: Chat;
    public model: ChatModel;
    public entity: any;
    constructor(
        private authService: AuthService,
        private signalrService: SignalrService
    ) {
        this.signalRSubscription = this.signalrService
            .getMessage()
            .subscribe(message => {
                this.content = message;
            });
        this.model = this.signalrService.getChat();
    }

    ngOnInit() {
        const token = localStorage.getItem('token');
        if (token) {
            this.authService.decodedToken = this.jwtHelper.decodeToken(token);
        }
    }

    onSubmit() {
        this.signalrService.sendChatToHub(this.model.message);
    }
    ngOnDestroy(): void {
        this.signalrService.disconnect();
        this.signalRSubscription.unsubscribe();
    }
}
