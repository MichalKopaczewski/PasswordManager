import { Component, OnInit } from "@angular/core";
import { BaseListComponent } from "src/app/helpers/base-list.component";
import { Column } from "src/app/helpers/column";
import { UserListItem } from "src/app/model/users/user-list-item";
import { UserService } from "src/app/services/users.service";

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html'
})
export class UserListComponent extends BaseListComponent implements OnInit {

    columns: Column[] = [
        { title: 'Username', key: 'username', isSortable: true, filterValue: '', isFilterable: true },
        { title: '', key: '', isSortable: false, filterValue: '', isFilterable: false }
    ];

    constructor(private userService: UserService) {
        super();
    }

    ngOnInit() {
        this.onInit();
    }
    protected populateList() {
        this.userService.GetUsers()
            .subscribe((result: UserListItem[]) => {
                this.queryResult.items = result;
                this.queryResult.totalItems = result.length;
            }, err => {
                console.log(err);
            });
    }
}
