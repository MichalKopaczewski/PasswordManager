import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NgxPermissionsService } from "ngx-permissions/lib/service/permissions.service";
import { environment } from "src/environments/environment";
import { AuthUserToken } from "../model/auth/auth-user-token";
import { LocalStorageService } from "./local-storage-service";
import { map, catchError } from 'rxjs/operators'
import { Observable, Subject } from "rxjs";
import { PostUserLogin } from "../model/auth/post-user-login";
import { JwtHelperService } from "@auth0/angular-jwt";

export function tokenGetter(): string {
    //return localStorageService.getFromLocalStorage<string>(LocalStorageService.TOKEN,'');
    const token = localStorage.getItem("token");
    if (token === null) return "";
    return token.substring(1, token.length - 1);
}

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
        if (this.getUser().role !== undefined) {
            if (!Array.isArray(this.getUser().role)) {
                const roles = [this.getUser().role.toString()];
                this.permissionsService.loadPermissions(roles);
            } else {
                this.permissionsService.loadPermissions(this.getUser().role);
            }
        }
    }



    login(model: PostUserLogin) {
        return this.http
            .post(this.accessPointUrl + 'Login', model)
            .pipe(
                map((response: any) => {
                    const user = response;
                    if (user) {
                        this.localStorageService.setOnLocalStorage<string>(LocalStorageService.TOKEN, user.token);
                        //localStorage.setItem('token', user.token);
                        this.decodedToken = this.jwtHelper.decodeToken(user.token);
                        this.loadPermissions();
                        this.userLoggedIn.next(true);
                    }
                })
            )
    }
    logout() {
        //localStorage.removeItem("token");
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
