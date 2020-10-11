import { Component, OnInit } from "@angular/core";
import { UserListItem } from "src/app/model/users/user-list-item";
import { UserService } from "src/app/services/users.service";

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html'
})
export class UserListComponent  implements OnInit {
    users:UserListItem[] = [];

    constructor(private userService: UserService) {
    }

    ngOnInit() {
        this.userService.GetUsers()
            .subscribe((result: UserListItem[]) => {
                this.users = result;
            }, err => {
                console.log(err);
            });
    }
}
