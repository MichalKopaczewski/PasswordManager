import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxPermissionsService } from 'ngx-permissions';
import { AuthService } from '../services/auth.service';
import { LocalStorageService } from '../services/local-storage-service';
@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent  implements OnInit{
    isExpanded = false;

    constructor(public authService: AuthService,
        private ngxPermission :NgxPermissionsService,
        private router:Router,
        private localStorageService:LocalStorageService
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
        this.localStorageService.clearLocalStorage();
        this.router.navigate(['/login']);
    }
}
