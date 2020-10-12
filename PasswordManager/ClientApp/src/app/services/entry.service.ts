import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { EntryCreate } from "../model/entries/entry-create";
import { EntryDelete } from "../model/entries/entry-delete";
import { EntryDetail } from "../model/entries/entry-details";
import { EntryUpdate } from "../model/entries/entry-update";
import { EncryptService } from "./encrypt.service";

@Injectable({
    providedIn: 'root'
})
export class EntryService {

    private baseUrl: string = environment.apiUrl + 'Entry';

    constructor(private http: HttpClient,
        private encrypService:EncryptService
        ) {
    }
    public GetEntry(entryId:number): Observable<EntryDetail> {
        return this.http.get<EntryDetail>(this.baseUrl + '/GetEntry/' +entryId,{headers: {'masterPassword':this.encrypService.getPassword()}});
    } 
    public CreateEntry(createEntry: EntryCreate) {
        return this.http.post(this.baseUrl + '/CreateEntry', createEntry,{headers: {'masterPassword':this.encrypService.getPassword()}});
    }
    public UpdateEntry(updateEntry: EntryUpdate) {
        return this.http.post(this.baseUrl + '/UpdateEntry', updateEntry,{headers: {'masterPassword':this.encrypService.getPassword()}});
    }
    public RemoveEntry(deleteEntry: EntryDelete) {
        return this.http.post(this.baseUrl + '/DeleteEntry', deleteEntry,{headers: {'masterPassword':this.encrypService.getPassword()}});
    }
}