import { Directive, Input, ViewContainerRef, TemplateRef, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Directive({
    selector: '[appHasRole]'
})
// structural directive: thay đổi layout (thêm hoặc xóa) phần tử trong DOM
// attribute directive: thay đổi thuộc tính của phần tử mà directive này gán cho
// đây là 1 structural directive
// TemplateRef và ViewContainerRef luôn cần cho structual directive
export class HasRoleDirective implements OnInit {
    @Input() appHasRole: string[];
    isVisible = false;

    constructor(
        private viewContainerRef: ViewContainerRef,
        private templateRef: TemplateRef<any>,
        private authService: AuthService
    ) {}

    ngOnInit() {
        const userRoles = this.authService.decodedToken.role as Array<string>;
        // if no roles clear the viewContainerRef
        if (!userRoles) {
            this.viewContainerRef.clear();
        }

        // if user has role need then render the element
        if (this.authService.roleMatch(this.appHasRole)) {
            if (!this.isVisible) {
                this.isVisible = true;
                this.viewContainerRef.createEmbeddedView(this.templateRef);
            } else {
                this.isVisible = false;
                this.viewContainerRef.clear();
            }
        }
    }
}
