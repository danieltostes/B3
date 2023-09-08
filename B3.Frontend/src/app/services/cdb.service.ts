import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CalculoRendimento } from '../models/CalculoRendimento';
import { Observable } from 'rxjs';
import { RentabilidadeCdb } from '../models/RentabilidadeCdb';

@Injectable()
export class CdbService {
    baseUrl : string = "https://localhost:7004/api/cdb";

    constructor(private http: HttpClient) { }

    public rendimentoCdb(calculoRendimento: CalculoRendimento) : Observable<RentabilidadeCdb>
    {
        return this.http.post<RentabilidadeCdb>(this.baseUrl, calculoRendimento);
    }
}
