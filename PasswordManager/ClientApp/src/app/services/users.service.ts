import { HttpHeaders, HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { AddUserToRole } from "../model/users/add-user-to-role";
import { RemoveUserFromRole } from "../model/users/remove-user-from-role";
import { RoleListItem } from "../model/users/role-list-item";
import { UserDelete } from "../model/users/user-delete";
import { UserDetail } from "../model/users/user-detail";
import { UserListItem } from "../model/users/user-list-item";
import { UserNew } from "../model/users/user-new";
import { UserUpdate } from "../model/users/user-update";

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private headers: HttpHeaders;
    private baseUrl: string = environment.apiUrl + 'User';

    constructor(private http: HttpClient) {
    }
    //TODo zmienić id na stringi
    public GetUser(username: string): Observable<UserDetail> {
        return this.http.get<UserDetail>(this.baseUrl + '/GetUser/' + username);
    }
    public DeleteUser(userDelete: UserDelete) {
        return this.http.post(this.baseUrl + '/RemoveUser', userDelete);
    }
    public UpdateUser(user: UserUpdate) {
        return this.http.post(this.baseUrl + '/UpdateUser', user);
    }
    public CreateUser(userNew: UserNew) {
        return this.http.post(this.baseUrl + '/CreateUser', userNew);
    }
    public AddUserToRole(addUserToRole: AddUserToRole) {
        return this.http.post(this.baseUrl + '/AddUserToRole', addUserToRole);
    }
    public RemoveUserFromRole(removeUserFromRole: RemoveUserFromRole) {
        return this.http.post(this.baseUrl + '/RemoveUserFromRole', removeUserFromRole);
    }
    public GetUsers(): Observable<UserListItem[]> {
        return this.http.get<UserListItem[]>(this.baseUrl + '/GetUsers');
    }
    public GetRoles(): Observable<RoleListItem[]> {

        return this.http.get<RoleListItem[]>(this.baseUrl + '/GetRoles');
           
    }
    public GetRolesNames(): Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + '/GetRolesNames');
    }
}