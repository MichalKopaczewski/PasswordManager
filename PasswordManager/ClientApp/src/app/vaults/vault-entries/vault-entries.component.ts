import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialogService } from 'src/app/confirmation-dialog/confirmation-dialog.service';
import { PasswordModalComponent } from 'src/app/modals/password-modal.component';
import { VaultDetail } from 'src/app/model/vaults/vault-detail';
import { VaultEntryItem } from 'src/app/model/vaults/vault-entry-item';
import { EncryptService } from 'src/app/services/encrypt.service';
import { EntryService } from 'src/app/services/entry.service';
import { LocalStorageService } from 'src/app/services/local-storage-service';
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
        private modalService:NgbModal,
        private encryptService:EncryptService,
        private localStorageService:LocalStorageService,
        private router: Router) {
    }

    ngOnInit(): void {
        this.vaultId = parseInt(this.route.snapshot.paramMap.get('vaultId'));
        if (this.vaultId !== this.localStorageService.getFromLocalStorage<number>(LocalStorageService.ACTIVE_VAULT,-1)) {
            const modalRef = this.modalService.open(PasswordModalComponent, { size: 'lg' });
            modalRef.result.then((result) => {
                console.log(result);
                if (result) {
                    this.vaultService.ValidateVaultPassword({vaultId:this.vaultId,masterPassword:result})
                    .subscribe(res => {
                        if (res) {
                            this.localStorageService.setOnLocalStorage<number>(LocalStorageService.ACTIVE_VAULT,this.vaultId);
                            this.encryptService.savePassword(result);
                            this.populateList();
                        } else {
                            this.toastrService.error("Podano błędne hasło główne");
                            this.router.navigate(['/vaults']);
                        }
                    },err => {
                        this.toastrService.error("Podano błędne hasło główne");
                        this.router.navigate(['/vaults']);
                    });
                }
            });
        } else {
            this.populateList();
        }
       
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
                this.vaultEntries.forEach(element => {
                    element.email = this.encryptService.decryptString(element.email);
                    element.portal = this.encryptService.decryptString(element.portal);
                    element.password = this.encryptService.decryptString(element.password);
                    element.login = this.encryptService.decryptString(element.login);
                });
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
