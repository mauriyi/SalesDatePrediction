import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../../models/customer';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = `${environment.apiUrl}Customer/SalesDatePrediction`;

  constructor(private http: HttpClient) {}

  getSalesDatePrediction(searchTerm: string): Observable<Customer[]> {
    if (searchTerm.trim() === '') {
      // Si el término de búsqueda está vacío
      return this.http.get<Customer[]>(`${this.apiUrl}`);
    }
    // Si hay un término de búsqueda, realiza la búsqueda
    return this.http.get<Customer[]>(`${this.apiUrl}?search=${searchTerm}`);
  }
}
