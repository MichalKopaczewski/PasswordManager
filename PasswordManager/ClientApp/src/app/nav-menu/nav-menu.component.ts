import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { version } from 'process';
import { PostUserLogin } from '../model/auth/post-user-login';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
    public version: string = version;
    isExpanded = false;
    model: PostUserLogin = { login: '', password: '' };
    isLoginDisabled: boolean;

    constructor(
        public authService: AuthService,
        private toastr: ToastrService,
        private spinner: NgxSpinnerService,
        private router: Router) {

    }
    collapse(): void {
        this.isExpanded = false;
    }

    toggle(): void {
        this.isExpanded = !this.isExpanded;
    }
    login(): void {

        globalCacheBusterNotifier.next();
        this.isLoginDisabled = true;
        this.spinner.show();
        this.authService.login(this.model)
            .subscribe(() => {
                this.model.password = '';
                this.model.login = '';
                this.isLoginDisabled = false;
                this.spinner.hide();
            }, error => {
                this.spinner.hide();
                console.log(error);
                this.toastr.error('Wystapił błąd, spróbuj ponownie! ' + error);
                this.isLoginDisabled = false;
            }
            )
    }
    loggedIn(): boolean {
        return this.authService.loggedIn();
    }
    logout(): void {
        this.model.password = '';
        this.model.login = '';
        this.authService.logout();
        console.log("logged out");
        this.router.navigate(['/home']);
    }
    goToHomePage(): void {
        window.location.reload(true);
    }
}
