import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  jwtHelepr = new JwtHelperService();
  
  constructor(
    private authService: AuthService
    ) {
    }
  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
        this.authService.decodedToken = this.jwtHelepr.decodeToken(token);
        this.authService.loadPermissions();
    }
}
}
