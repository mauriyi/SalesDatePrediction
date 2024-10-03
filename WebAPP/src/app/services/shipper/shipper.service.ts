import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Shipper } from '../../models/shipper';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShipperService {

  private apiUrl = `${environment.apiUrl}Shippers/GetShippers`;

  constructor(private http: HttpClient) {}

  getShippers(): Observable<Shipper[]> {
    return this.http.get<Shipper[]>(this.apiUrl);
  }

}
