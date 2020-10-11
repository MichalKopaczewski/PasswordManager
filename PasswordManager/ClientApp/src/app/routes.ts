import { Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { UserDetailResolver } from "./resolvers/user-detail.resolver";
import { UserListComponent } from "./users/user-list/user-list.component";
import { UserNewComponent } from "./users/user-new/user-new.component";
import { UsersDetailsComponent } from "./users/users-details/users-details.component";
import { EntryNewUpdateComponent } from "./vaults/entry-new-update/entry-new-update.component";
import { VaultEntriesComponent } from "./vaults/vault-entries/vault-entries.component";
import { VaultNewComponent } from "./vaults/vault-new/vault-new.component";
import { VaultsListComponent } from "./vaults/vaults-list-component/vaults-list.component";

export const appRoutes: Routes = [
    { path: '', component: VaultsListComponent },
    {
        path: '', runGuardsAndResolvers: 'always',
        children: [
            { path: '', component: VaultsListComponent, pathMatch: 'full' },
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
            {
                path: 'vaults', component: VaultsListComponent,
                canActivate: [NgxPermissionsGuard],
                data: {
                    permissions: {
                        only: ['Admin','SystemUser']
                    }
                }
            },
            {
                path: 'vault-new', component: VaultNewComponent,
                canActivate: [NgxPermissionsGuard],
                data: {
                    permissions: {
                        only: ['Admin','SystemUser']
                    }
                }
            },
            {
                path: 'vault-entries/:vaultId', component: VaultEntriesComponent,
                canActivate: [NgxPermissionsGuard],
                data: {
                    permissions: {
                        only: ['Admin','SystemUser']
                    }
                }
            },
            {
                path: 'entry-update/:entryId', component: EntryNewUpdateComponent,
                canActivate: [NgxPermissionsGuard],
                data: {
                    permissions: {
                        only: ['Admin','SystemUser']
                    }
                }
            },
            {
                path: 'entry-new/:vaultId', component: EntryNewUpdateComponent,
                canActivate: [NgxPermissionsGuard],
                data: {
                    permissions: {
                        only: ['Admin','SystemUser']
                    }
                }
            },
        ]
    },
    
    { path: '**', redirectTo: '', pathMatch: 'full' }
]