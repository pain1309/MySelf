import { Component, OnInit } from "@angular/core";
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Group } from '../_models/Group';
import { GroupService } from '../_services/group.service';

@Component({
    selector: "app-group",
    templateUrl: "./group.component.html",
    styleUrls: ["./group.component.css"]
})
export class GroupComponent implements OnInit {
    listGroups: Group[] = [];
    constructor(private authService: AuthService, private alertify: AlertifyService, private groupService: GroupService) {}

    ngOnInit() {
        this.groupService.getGroups().subscribe(res => {
            this.listGroups = res;
        });
    }

}
