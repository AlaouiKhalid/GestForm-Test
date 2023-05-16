import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { Configuration } from '../models/configuration';
import { ItemQuery } from '../models/item-query';


@Injectable()
export class GestFormService {

    constructor(private HttpClient: HttpClient, private _configuration: Configuration) {        
    }


    public testerListe(liste: number[]): Observable<ItemQuery[]> {
        return this.HttpClient.post<ItemQuery[]>(this._configuration.ApiIP + ":" + this._configuration.ApiPort + this._configuration.GestFormLink, liste, { withCredentials: false });
    }
    

}