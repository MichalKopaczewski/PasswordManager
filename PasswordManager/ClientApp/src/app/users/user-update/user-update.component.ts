import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserDetail } from 'src/app/model/users/user-detail';
import { UserUpdate } from 'src/app/model/users/user-update';
import { UserService } from 'src/app/services/users.service';
import { ConfirmationDialogService } from 'src/app/confirmation-dialog/confirmation-dialog.service';

@Component({
    selector: 'app-user-update',
    templateUrl: './user-update.component.html'
})
export class UserUpdateComponent implements OnInit {
    user: UserDetail;
    userForm: FormGroup;

    constructor(private route: ActivatedRoute
        , private fb: FormBuilder
        , private toast: ToastrService
        , private router: Router
        , private userService: UserService
        , private confirmationDialogService: ConfirmationDialogService
    ) {
    }
    ngOnInit(): void {
        this.userForm = this.fb.group({
            username: ['', [Validators.required, Validators.maxLength(20)]],
            password: ['', [Validators.required, Validators.maxLength(20)]]
        });
        this.route.data.subscribe(data => {

            this.user = data['user'];
            console.log(this.user);
            this.userForm.setValue({
                username: this.user.username,
                password: this.user.password
            });
        });
    }
    public removeUser() {
        this.confirmationDialogService.confirm('Potwierdź usuwanie projektu', 'Czy na pewno chcesz usunąć użytkownika?')
            .then((confirmed) => {
                if (!confirmed) { return; }
                this.userService.DeleteUser({username: this.user.username})
                    .subscribe(() => {
                        this.toast.success('Uzytkownik usunięty');
                        this.router.navigate(['/users']);
                    }, (error: string) => {
                        this.toast.error(error);
                    }, () => {
                    });
                ;
            });
    }
    public saveUser() {
        const userUpdate: UserUpdate = {
            username: this.user.username,
            password: this.userForm.get('password').value,
        };
        this.userService.UpdateUser(userUpdate)
        .subscribe(() => {
            this.toast.success('Użytkownik zaktualizowany');
            this.router.navigateByUrl('blank').then(() => {
                this.router.navigate(['/users-details', this.user.username]);
            });
        }, (error: string) => {
            this.toast.error(error);
        }, () => {
        });
    ;

    }
}