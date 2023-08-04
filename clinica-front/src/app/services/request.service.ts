import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(private http:HttpClient) { }
  postData(endpoint: any, body?: any): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .append('Access-Control-Allow-Origin', '*')
      .append('Accept', '*/*')

    return this.http.post<any>(environment.ApiUrl+endpoint, body, { headers })
  }

  getData(endpoint: string, parameters?: any): Observable<any> {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    let params = new HttpParams();
    if (parameters) {
      Object.keys(parameters).forEach(key => {
        params = params.set(key, parameters[key]);
      });
    }
    const options = { headers: headers, params: params };
    return this.http.get<any>(`${environment.ApiUrl}${endpoint}`, options);
  }
}
