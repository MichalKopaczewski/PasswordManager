import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { VaultCreate } from "../model/vaults/vault-create";
import { VaultDelete } from "../model/vaults/vault-delete";
import { VaultDetail } from "../model/vaults/vault-detail";
import { VaultEntryItem } from "../model/vaults/vault-entry-item";
import { VaultItem } from "../model/vaults/vault-item";

@Injectable({
    providedIn: 'root'
})
export class VaultService {

    private baseUrl: string = environment.apiUrl + 'Vault';

    constructor(private http: HttpClient) {
    }
    public GetVaults(): Observable<VaultItem[]> {
        return this.http.get<VaultItem[]>(this.baseUrl + '/GetVaults' );
    } 
    public GetVault(vaultId:number): Observable<VaultDetail> {
        return this.http.get<VaultDetail>(this.baseUrl + '/GetVault/' + vaultId );
    } 
    public GetEntriesList(vaultId:number): Observable<VaultEntryItem[]> {
        return this.http.get<VaultEntryItem[]>(this.baseUrl + '/GetEntriesList/' + vaultId);
    }
    public CreateVault(vaultCreate: VaultCreate) {
        return this.http.post(this.baseUrl + '/CreateVault', vaultCreate);
    }
    public RemoveVault(vaultDelete: VaultDelete) {
        return this.http.post(this.baseUrl + '/RemoveVault', vaultDelete);
    }
}