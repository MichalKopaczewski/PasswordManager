import { Inject, Injectable } from '@angular/core';
@Injectable()
export class LocalStorageService {

    static readonly TOKEN = 'token';
    constructor(private localSt:LocalStorageService) {

    }
    public setOnLocalStorage<T>(key: string, value: T): void {
        this.localSt.setOnLocalStorage(key, value);
    }

    public getFromLocalStorage<T>(key: string,defaultValue: T): T {
        return this.localSt.getFromLocalStorage<T>(key,defaultValue);
    }
    public removeItem(key: string): void {
        this.localSt.removeItem(key);
    }
    public clearLocalStorage(): void {
        this.localSt.removeItem(LocalStorageService.TOKEN);
    }

}