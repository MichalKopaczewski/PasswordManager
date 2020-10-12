import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { ValidateVaultPassword } from "../model/vaults/validate-vault-password";
import { VaultCreate } from "../model/vaults/vault-create";
import { VaultDelete } from "../model/vaults/vault-delete";
import { VaultDetail } from "../model/vaults/vault-detail";
import { VaultEntryItem } from "../model/vaults/vault-entry-item";
import { VaultItem } from "../model/vaults/vault-item";
import { EncryptService } from "./encrypt.service";

@Injectable({
    providedIn: 'root'
})
export class VaultService {

    private baseUrl: string = environment.apiUrl + 'Vault';

    constructor(private http: HttpClient,
        private encrypService:EncryptService
        ) {
    }
    public GetVaults(): Observable<VaultItem[]> {
        return this.http.get<VaultItem[]>(this.baseUrl + '/GetVaults' );
    } 
    public GetVault(vaultId:number): Observable<VaultDetail> {
        return this.http.get<VaultDetail>(this.baseUrl + '/GetVault/' + vaultId );
    } 
    public GetEntriesList(vaultId:number): Observable<VaultEntryItem[]> {
        return this.http.get<VaultEntryItem[]>(this.baseUrl + '/GetEntriesList/' + vaultId,{headers: {'masterPassword':this.encrypService.getPassword()}});
    }
    public CreateVault(vaultCreate: VaultCreate) {
        return this.http.post(this.baseUrl + '/CreateVault', vaultCreate);
    }
    public RemoveVault(vaultDelete: VaultDelete) {
        return this.http.post(this.baseUrl + '/RemoveVault', vaultDelete,{headers: {'masterPassword':this.encrypService.getPassword()}});
    }
    public ValidateVaultPassword(validate:ValidateVaultPassword):Observable<boolean> {
        return this.http.post<boolean>(this.baseUrl + '/ValidateVaultPassword', validate);
    }
}