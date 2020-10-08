import { Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { CounterComponent } from "./counter/counter.component";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { UserDetailResolver } from "./resolvers/user-detail.resolver";
import { UserListComponent } from "./users/user-list/user-list.component";
import { UserNewComponent } from "./users/user-new/user-new.component";
import { UsersDetailsComponent } from "./users/users-details/users-details.component";

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '', runGuardsAndResolvers: 'always',
        children: [
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'login', component: LoginComponent },
            {
                path: 'users', component: UserListComponent,
                canActivate: [NgxPermissionsGuard],
                data: {
                    permissions: {
                        only: ['Admin']
                    }
                }
            },
            {
                path: 'users-details/:id', component: UsersDetailsComponent, resolve: { user: UserDetailResolver },
                canActivate: [NgxPermissionsGuard],
                data: {
                    permissions: {
                        only: ['Admin']
                    }
                }
            },
            {
                path: 'user-new', component: UserNewComponent,
                canActivate: [NgxPermissionsGuard],
                data: {
                    permissions: {
                        only: ['Admin']
                    }
                }
            },
        ]
    },
    
    { path: '**', redirectTo: '', pathMatch: 'full' }
]