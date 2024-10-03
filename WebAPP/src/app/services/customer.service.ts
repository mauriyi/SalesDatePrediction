import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = `${environment.apiUrl}Customer/SalesDatePrediction`;

  constructor(private http: HttpClient) {}

  getSalesDatePrediction(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiUrl);
  }
}
