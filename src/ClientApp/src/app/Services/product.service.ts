import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/Product';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl: string = environment.baseUrl + 'api/';

  constructor(private http: HttpClient) { }

  public getProducts() : Observable<Product[]>{
    return this.http.get<Product[]>(this.baseUrl + `EconProduct`);   
  }

  public getNinjaProducts() {
    return this.http.get<Product[]>(this.baseUrl + `NinjaProduct`); 

  }

  public getEconProducts() {
    return this.http.get<Product[]>(this.baseUrl + `EconProduct`); 
  }

  public addProductsToNinja(products: Product[]) {
    console.log(this.baseUrl);
    return this.http.post<Product[]>(this.baseUrl + `NinjaProduct`, products);
}

public addProductsToEconomic(products: Product[]) {
  console.log(this.baseUrl);

  return this.http.post<Product[]>(this.baseUrl + `EconProduct`, products);
}
}


