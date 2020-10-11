import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialogService } from 'src/app/confirmation-dialog/confirmation-dialog.service';
import { VaultDetail } from 'src/app/model/vaults/vault-detail';
import { VaultEntryItem } from 'src/app/model/vaults/vault-entry-item';
import { EntryService } from 'src/app/services/entry.service';
import { VaultService } from 'src/app/services/vault.service';

@Component({
    selector: 'app-vault-entries',
    templateUrl: './vault-entries.component.html',
    styles: []
})
export class VaultEntriesComponent implements OnInit {

    vaultEntries: VaultEntryItem[];
    vaultId: number;
    vault :VaultDetail;

    constructor(private vaultService: VaultService,
        private toastrService: ToastrService,
        private confirmDialogService: ConfirmationDialogService,
        private route: ActivatedRoute,
        private entryService:EntryService,
        private router: Router) {
    }

    ngOnInit(): void {
        this.vaultId = parseInt(this.route.snapshot.paramMap.get('vaultId'));
        this.populateList();
    }
    populateList(): void {
        this.vaultService.GetVault(this.vaultId)
        .subscribe(res => {
            this.vault = res;
        },err=>{
            this.toastrService.error('Błąd w pobieraniu danych o sejfie');
        })
        this.vaultService.GetEntriesList(this.vaultId)
            .subscribe(res => {
                this.vaultEntries = res;

            }, err => {
                this.toastrService.error(err);
            });
    }

    removeEntry(eId: number): void {
        this.confirmDialogService.confirm('Usunięcie', 'Czy na pewno chcesz usunąć wpis?')
            .then(res => {
                if (res) {
                    this.entryService.RemoveEntry({entryId:eId})
                        .subscribe(item => {
                            this.toastrService.success('Wpis usunięty');
                            this.populateList();
                        }, err => {
                            this.toastrService.error(err);
                        })
                }
            })
    }

}
