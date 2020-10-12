import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialogService } from 'src/app/confirmation-dialog/confirmation-dialog.service';
import { EntryCreate } from 'src/app/model/entries/entry-create';
import { EncryptService } from 'src/app/services/encrypt.service';
import { EntryService } from 'src/app/services/entry.service';

@Component({
    selector: 'app-entry-new-update',
    templateUrl: './entry-new-update.component.html',
    styles: []
})
export class EntryNewUpdateComponent implements OnInit {

    entryForm: FormGroup;
    vaultId: number;
    forCreate: boolean;
    entryId:number;
    constructor(
        private toastrService: ToastrService,
        private confirmDialogService: ConfirmationDialogService,
        private route: ActivatedRoute,
        private entryService: EntryService,
        private fb: FormBuilder,
        private encryptService:EncryptService,
        private router: Router) {
    }

    ngOnInit() {
        this.createForm();
        if (this.route.snapshot.paramMap.has('entryId')) {
            this.forCreate = false;
            this.entryId = parseInt(this.route.snapshot.paramMap.get('entryId'));
            this.fillForm(this.entryId);
        } else {
            this.vaultId = parseInt(this.route.snapshot.paramMap.get('vaultId'));
            this.forCreate = true;
        }
    }

    createForm() {
        this.entryForm = this.fb.group({
            portal: ['', [Validators.required, Validators.minLength(4)]],
            password: ['', [Validators.required, Validators.minLength(4)]],
            login: ['', [Validators.required, Validators.minLength(4)]],
            email: ['', [Validators.required, Validators.minLength(4)]]
        });
    }
    fillForm(entryId: number): void {
        this.entryService.GetEntry(entryId)
            .subscribe(res => {
                this.entryForm.setValue({
                    portal: this.encryptService.decryptString(res.portal),
                    password: this.encryptService.decryptString(res.password),
                    login: this.encryptService.decryptString(res.login),
                    email: this.encryptService.decryptString(res.email)
                });
                this.vaultId = res.vaultId;
            }, err => {
                this.toastrService.error(err);
            });
    }
    saveForm(): void {
        if (this.forCreate) {
            this.entryService.CreateEntry(
                {
                    email: this.encryptService.encryptString(this.entryForm.get('email').value),
                    login: this.encryptService.encryptString(this.entryForm.get('login').value),
                    password: this.encryptService.encryptString(this.entryForm.get('password').value),
                    portal: this.encryptService.encryptString(this.entryForm.get('portal').value),
                    vaultId: this.vaultId
                }).subscribe(x => {
                    this.toastrService.success('Utworzono wpis');
                    this.router.navigate(['/vault-entries',this.vaultId]);
                },err => {
                    this.toastrService.error(err);
                });
        } else {
            this.entryService.UpdateEntry(
                {
                    email: this.encryptService.encryptString(this.entryForm.get('email').value),
                    login: this.encryptService.encryptString(this.entryForm.get('login').value),
                    password: this.encryptService.encryptString(this.entryForm.get('password').value),
                    portal:  this.encryptService.encryptString(this.entryForm.get('portal').value),
                    id:this.entryId
                }).subscribe(x => {
                    this.toastrService.success('Zaktualizowano wpis');
                    this.router.navigate(['/vault-entries',this.vaultId]);
                },err => {
                    this.toastrService.error(err);
                });
        }
    }

}
