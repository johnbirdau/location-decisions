import { Injectable } from '@angular/core';
import { Constants } from '../config/constants';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { DataValueEnvelope } from './data-value-api';
import { DataValue } from './data-value';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private constants: Constants, private http: HttpClient) { }

  getDataValues () : Observable<DataValue[]> {
    return this.http.get<DataValueEnvelope>(this.constants.DATA_ENDPOINT)
      .pipe(
        map<DataValueEnvelope, DataValue[]>(
          envelope => envelope.data
        )
      );
  }}
