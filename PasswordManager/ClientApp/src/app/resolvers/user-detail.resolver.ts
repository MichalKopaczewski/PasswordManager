import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable, of, forkJoin } from "rxjs";
import { catchError } from "rxjs/operators";
import { UserDetail } from "../model/users/user-detail";
import { UserService } from "../services/users.service";

@Injectable()
export class UserDetailResolver implements Resolve<UserDetail> {
    constructor(private userService: UserService, private router: Router, private toastr: ToastrService) {

    }

    resolve(route: ActivatedRouteSnapshot):Observable<UserDetail> {
        return this.userService.GetUser(route.params['id'])
            .pipe(
                catchError(error => {
                    this.toastr.error('Problem retrieving data');
                    this.router.navigate(['/users']);
                    return of(null);
                })
            )

    }
}
