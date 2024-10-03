import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NewOrderDto } from '../../models/newOrderDto';
import { Order } from '../../models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = `${environment.apiUrl}Orders/`;

  constructor(private http: HttpClient) {}

  getOrders(idUser: number): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}GetClientOrders/${idUser}`);
  }

  addNewOrder(orderData: NewOrderDto): Observable<number> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.post<number>(`${this.apiUrl}AddNewOrder`, orderData, { headers });
  }
}
