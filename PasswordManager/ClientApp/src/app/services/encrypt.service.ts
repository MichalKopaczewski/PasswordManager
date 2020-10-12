import { Injectable } from "@angular/core";
import * as CryptoJS from 'crypto-js';  
import { LocalStorageService } from "./local-storage-service";
@Injectable({
    providedIn: 'root'
})
export class EncryptService {
    

    constructor(private storageService:LocalStorageService){

    }

    public savePassword(pass:string) {
        var key = this.getRandomKey();
        //TODO coś tu nie działa
        this.storageService.setOnLocalStorage<string>(LocalStorageService.PASSWORD_KEY,key);
        this.storageService.setOnLocalStorage<string>(LocalStorageService.PASSWORD,this.encryptString(pass,key));
    }
    public getPassword():string {
        var key = this.storageService.getFromLocalStorage(LocalStorageService.PASSWORD_KEY,undefined);
        if (key === undefined) {
            this.storageService.removeItem(LocalStorageService.PASSWORD);
            return '';
        }
        return this.decryptString(this.storageService.getFromLocalStorage(LocalStorageService.PASSWORD,undefined),key);
    }

    private getRandomKey():string {
        return CryptoJS.lib.WordArray.random(128 / 8).toString(CryptoJS.enc.Base64);
    }
    
    public encryptString(text: string,key?:string) {
        if (key===undefined) {
            
            if (this.getPassword()==='') {
                throw new Error('Nie podano klucza głównego');
            }
            key = this.getPassword();
        }
        return CryptoJS.AES.encrypt(text, key).toString();  
    }
    public decryptString(cipherText: string,key?:string) {
        if (key===undefined) {
            
            if (this.getPassword()==='') {
                throw new Error('Nie podano klucza głównego');
            }
            key = this.getPassword();
        }
        return CryptoJS.AES.decrypt(cipherText, key).toString(CryptoJS.enc.Utf8);  
    }
    public hashString(text:string,key:string) {
        return CryptoJS.HmacSHA512(text, key).toString(CryptoJS.enc.Base64);
    }
}