import { Inject, Injectable } from '@angular/core';
import { LOCAL_STORAGE, StorageService } from 'angular-webstorage-service';
@Injectable()
export class LocalStorageService {

    static readonly TOKEN = 'token';
    constructor(@Inject(LOCAL_STORAGE) private storage: StorageService) {

    }
    public setOnLocalStorage<T>(key: string, value: T): void {
        // insert updated array to local storage
        this.storage.set(key, value);
    }

    public getFromLocalStorage<T>(key: string, defaultValue: T): T {
        const val: T = this.storage.get(key);
        if (val === null) return defaultValue;

        return val;
    }
    public removeItem(key: string): void {
        this.storage.remove(key);
    }
    public clearLocalStorage(): void {
        this.storage.remove(LocalStorageService.TOKEN);
        //TODO dodać usuwanie po wylogowaniu z aplikacji
    }
}