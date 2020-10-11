import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { VaultCreate } from 'src/app/model/vaults/vault-create';
import { VaultService } from 'src/app/services/vault.service';

@Component({
    selector: 'app-vault-new',
    templateUrl: './vault-new.component.html'
})
export class VaultNewComponent implements OnInit {


    vaultForm: FormGroup;
    vault: VaultCreate;
    constructor(private vaultService: VaultService,
        private fb: FormBuilder,
        private toast: ToastrService,
        private router: Router) {
    }
    ngOnInit(): void {
        this.createForm();
    }
    createForm() {
        this.vaultForm = this.fb.group({
            name: ['', [Validators.required, Validators.minLength(4)]],
            masterPassword: ['', Validators.required]
        });
    }
    saveVault() {
        if (this.vaultForm.valid) {
            this.vault = Object.assign({}, this.vaultForm.value);
            this.vaultService.CreateVault(this.vault).subscribe(() => {
                this.toast.success('Sejf utworzony');
            }, (error: string) => {
                this.toast.error(error);
            }, () => {
                this.router.navigate(['/vaults']);
            });
        }
    }

}
