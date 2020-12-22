import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../models/Customer';
import { Observable } from 'rxjs';
// import { environment } from 'src/environments/environment';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private baseUrl: string = environment.baseUrl + 'api/';

  constructor(private http: HttpClient) { }

  public getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.baseUrl + `customers`);    
  }

  public getNinjaCustomers() {
    return this.getCustomers().pipe(map(data => data.filter(p => p.customerSource == "Invoice Ninja")));
  }

  public getEconCustomers() {
    return this.getCustomers().pipe(map(data => data.filter(p => p.customerSource == "E-conomic")));
  }

  public addCustomerToNinja(customers: Customer[]) {
    return this.http.post<Customer[]>(this.baseUrl + `NinjaCustomer`, customers);
}

public addCustomerToEconomic(customers: Customer[]) {
  return this.http.post<Customer[]>(this.baseUrl + `EconCustomer`, customers);
}


}
