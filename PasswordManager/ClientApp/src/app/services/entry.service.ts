import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { EntryCreate } from "../model/entries/entry-create";
import { EntryDelete } from "../model/entries/entry-delete";
import { EntryDetail } from "../model/entries/entry-details";
import { EntryUpdate } from "../model/entries/entry-update";

@Injectable({
    providedIn: 'root'
})
export class EntryService {

    private baseUrl: string = environment.apiUrl + 'Entry';

    constructor(private http: HttpClient) {
    }
    public GetEntry(entryId:number): Observable<EntryDetail> {
        return this.http.get<EntryDetail>(this.baseUrl + '/GetEntry/' +entryId);
    } 
    public CreateEntry(createEntry: EntryCreate) {
        return this.http.post(this.baseUrl + '/CreateEntry', createEntry);
    }
    public UpdateEntry(updateEntry: EntryUpdate) {
        return this.http.post(this.baseUrl + '/UpdateEntry', updateEntry);
    }
    public RemoveEntry(deleteEntry: EntryDelete) {
        return this.http.post(this.baseUrl + '/DeleteEntry', deleteEntry);
    }
}