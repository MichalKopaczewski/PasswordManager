import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { UserNew } from 'src/app/model/users/user-new';
import { UserService } from 'src/app/services/users.service';

@Component({
    selector: 'app-user-new',
    templateUrl: './user-new.component.html'
})
export class UserNewComponent implements OnInit {

    userForm: FormGroup;
    user: UserNew;
    constructor(private userService: UserService, private fb: FormBuilder
        , private toast: ToastrService, private router: Router) {
    }
    ngOnInit(): void {
        this.createUserForm();
    }
    createUserForm() {
        this.userForm = this.fb.group({
            username: ['', [Validators.required, Validators.minLength(4)]],
            password: ['', Validators.required]
        });
    }
    saveUser() {
        if (this.userForm.valid) {
            this.user = Object.assign({}, this.userForm.value);
            this.userService.CreateUser(this.user).subscribe(() => {
                this.toast.success('Użytkownik utworzony');
            }, (error: string) => {
                this.toast.error(error);
            }, () => {
                this.router.navigate(['/users']);
            });
        }
        console.log(this.userForm.value);
    }
}