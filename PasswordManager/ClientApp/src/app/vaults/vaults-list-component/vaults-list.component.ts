import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialogComponent } from 'src/app/confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogService } from 'src/app/confirmation-dialog/confirmation-dialog.service';
import { VaultItem } from 'src/app/model/vaults/vault-item';
import { EncryptService } from 'src/app/services/encrypt.service';
import { VaultService } from 'src/app/services/vault.service';

@Component({
    selector: 'app-vaults-list',
    templateUrl: './vaults-list.component.html'
})
export class VaultsListComponent implements OnInit {
    vaults: VaultItem[];

    constructor(private vaultService: VaultService,
        private toastrService: ToastrService,
        private confirmDialogService:ConfirmationDialogService
        ) {
    }

    ngOnInit(): void {
        this.populateList();
    }
    populateList() :void {
        
        this.vaultService.GetVaults()
            .subscribe(res => {
                this.vaults = res;
            }, err => {
                this.toastrService.error(err);
            });
    }

    removeVault(vId:number):void {
        this.confirmDialogService.confirm('Usunięcie','Czy na pewno chcesz usunąć sejf i wszystkie w nim znajdujące się hasła?')
        .then(res => {
            if (res) {
                this.vaultService.RemoveVault({vaultId:vId})
                .subscribe(item => {
                    this.toastrService.success('Sejf usunięty');
                    this.populateList();
                },err => {
                    this.toastrService.error(err);
                })
            }
        })
    }



}
