import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'password-modal',
    templateUrl: './password-modal.component.html'
})
export class PasswordModalComponent {
    password:string;
    constructor(private spinner: NgxSpinnerService
        , private toastr: ToastrService
        , public activeModal: NgbActiveModal
        , public fb: FormBuilder
    ) {

    }

    ngOnInit(): void {
        
    }
    passwordCloseModal(): void {
        this.activeModal.close(this.password);
    }
}