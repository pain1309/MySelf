import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { UserService } from './../_services/user.service';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Injectable } from "@angular/core";
import { AlertifyService } from '../_services/alertify.service';
import { User } from '../_models/user';


@Injectable()
export class MessResolver implements Resolve<User> {
    constructor(
        private userService: UserService,
        private route: Router,
        private alertify: AlertifyService
    ) {}
    resolve(route: ActivatedRouteSnapshot) : Observable<User> {
        return this.userService.getUser(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/members']);
                return of(null);
            })
        );
    }
}