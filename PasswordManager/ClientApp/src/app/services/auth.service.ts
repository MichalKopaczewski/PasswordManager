import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthUserToken } from "../model/auth/auth-user-token";
import { LocalStorageService } from "./local-storage-service";
import { map, catchError } from 'rxjs/operators'
import { Observable, Subject } from "rxjs";
import { PostUserLogin } from "../model/auth/post-user-login";
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from "src/environments/environment";
import { NgxPermissionsService } from "ngx-permissions";


@Injectable()
export class AuthService {
    private accessPointUrl: string = environment.apiUrl + 'Auth/';
    jwtHelper = new JwtHelperService();
    decodedToken: AuthUserToken;
    private userLoggedIn = new Subject<boolean>();
    constructor(private http: HttpClient
        , private permissionsService: NgxPermissionsService
        , private localStorageService: LocalStorageService) {
    }
    public getUser(): AuthUserToken {
        if (this.loggedIn()) {
            const token = this.localStorageService.getFromLocalStorage<string>(LocalStorageService.TOKEN, '');// localStorage.getItem('token');
            return this.jwtHelper.decodeToken(token);
        }
        return null;
    }

    public loggedIn() {
        const token = this.localStorageService.getFromLocalStorage<string>(LocalStorageService.TOKEN, '');// localStorage.getItem('token');
        return !this.jwtHelper.isTokenExpired(token);
    }

    public userInRole(role: string): boolean {
        if (this.loggedIn() === false) return false;

        if (this.getUser() === null || this.getUser() === undefined) {
            return false;
        }
        if (!Array.isArray(this.getUser().role)) {
            return role.localeCompare(role, this.getUser().role) === 0;
        } else {
            const x = this.getUser().role.find(item => item == role);
            if (x !== undefined) return true;
        }
        return false;
    }

    public loadPermissions() {
        this.permissionsService.flushPermissions();
        if (this.getUser() == null) {
            return;
        }
        console.log(JSON.stringify(this.getUser()));
        if (this.getUser().role !== undefined) {
            if (!Array.isArray(this.getUser().role)) {
                const roles = [this.getUser().role.toString()];
                console.log(JSON.stringify(roles));
                this.permissionsService.loadPermissions(roles);
            } else {
                console.log(JSON.stringify(this.getUser().role));
                this.permissionsService.loadPermissions(this.getUser().role);
            }
            this.permissionsService.permissions$.subscribe(item => {
                console.log(JSON.stringify(item));
            });
        }
    }



    login(model: PostUserLogin) {
        return this.http
            .post(this.accessPointUrl + 'Login', model)
            .pipe(
                map((response: any) => {
                    const user = response;
                    console.log(user);
                    if (user) {
                        this.localStorageService.setOnLocalStorage<string>(LocalStorageService.TOKEN, user.token);
                        this.decodedToken = this.jwtHelper.decodeToken(user.token);
                        this.loadPermissions();
                        this.userLoggedIn.next(true);
                    }
                })
            )
    }
    logout() {
        this.localStorageService.clearLocalStorage();
        this.permissionsService.flushPermissions();
    }
    getUserLoggedIn(): Observable<boolean> {
        return this.userLoggedIn.asObservable();
    }
    setUserLoggedIn(userLoggedIn: boolean) {
        this.userLoggedIn.next(userLoggedIn);
    }

}
