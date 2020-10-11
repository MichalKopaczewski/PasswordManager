import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxPermissionsService } from 'ngx-permissions';
import { AuthService } from '../services/auth.service';
@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent  implements OnInit{
    isExpanded = false;

    constructor(public authService: AuthService,
        private ngxPermission :NgxPermissionsService,
        private router:Router
        ) {

    }
    
    ngOnInit(): void {
        console.log('x');
        this.ngxPermission.permissions$.subscribe(item => {
            console.log(JSON.stringify(item));
        });
    }
    collapse(): void {
        this.isExpanded = false;
    }

    toggle(): void {
        this.isExpanded = !this.isExpanded;
    }
   
    goToHomePage(): void {
        console.log('x');
        window.location.reload(true);
    }
    loggedIn() {
        return this.authService.loggedIn();
    }
    logout() {
        localStorage.removeItem("token");
        this.router.navigate(['/login']);
    }
}
