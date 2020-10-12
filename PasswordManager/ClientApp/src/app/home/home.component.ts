import { Component } from '@angular/core';
import { EncryptService } from '../services/encrypt.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent {
    encrypted:string;
    constructor(private encryptService:EncryptService) {

    }

    public encryptString() {
    //    this.encrypted =  this.encryptService.encryptString("ala ma kota",'aasfasffase2tw');
        console.log(this.encrypted);
    }
    public decryptString() {
     //   console.log(this.encryptService.decryptString(this.encrypted,'aasfasffase2tw'));
    }
    public hashString() {
   //     console.log(this.encryptService.hashString("ala ma kota",'aasfasffase2tw'));
    }
}
