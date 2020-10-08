import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { AddUserToRole } from 'src/app/model/users/add-user-to-role';
import { RemoveUserFromRole } from 'src/app/model/users/remove-user-from-role';
import { UserDetail } from 'src/app/model/users/user-detail';
import { UserService } from 'src/app/services/users.service';
import { ConfirmationDialogService } from 'src/app/confirmation-dialog/confirmation-dialog.service';

@Component({
    selector: 'app-user-roles',
    templateUrl: './user-roles.component.html'
})
export class UserRolesComponent implements OnInit {
    user: UserDetail;
    constructor(private route: ActivatedRoute
        , private fb: FormBuilder
        , private toast: ToastrService
        , private router: Router
        , private userService: UserService
        , private confirmationDialogService: ConfirmationDialogService
    ) {
    }
    ngOnInit(): void {
        this.route.data.subscribe(data => {
            this.user = data['user'];

        });
    }
    public removeAssignment( rolename: string) {
        const userRole: RemoveUserFromRole = {
            rolename: rolename,
            username: this.user.username
        }
        console.log(userRole);
        this.userService.RemoveUserFromRole(userRole).subscribe(() => {
            this.toast.success('Użytkownik zaktualizowany');
            this.router.navigateByUrl('blank').then(() => {
                this.router.navigate(['/users-details', this.user.username]);
            });
        }, (error: string) => {
            this.toast.error(error);
        }, () => {
        });
    }
    public addAssignment( rolename: string) {
        const userRole: AddUserToRole = {
            rolename: rolename,
            username: this.user.username
        }
        console.log(userRole);
        this.userService.AddUserToRole(userRole).subscribe(() => {
            this.toast.success('Użytkownik zaktualizowany');
            this.router.navigateByUrl('blank').then(() => {
                this.router.navigate(['/users-details', this.user.username]);
            });
        }, (error: string) => {
            this.toast.error(error);
        }, () => {
        });
    }
}