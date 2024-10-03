import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NewOrderDto } from '../../models/newOrderDto';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = `${environment.apiUrl}Orders/`;

  constructor(private http: HttpClient) {}

  // addNewOrder(order: NewOrderDto): Observable<any> {
  //   return this.http.post(this.apiUrl, order);
  // }

  addNewOrder(orderData: NewOrderDto): Observable<number> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.post<number>(`${this.apiUrl}AddNewOrder`, orderData, { headers });
  }
}
