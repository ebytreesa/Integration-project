import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { CustomerListComponent } from './customer/customer-list/customer-list.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatTableModule} from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';

 import { MatPaginatorModule } from '@angular/material/paginator';
 import { MatSidenavModule } from '@angular/material/sidenav';
 import { MatListModule } from '@angular/material/list';






import { CustomerService } from './services/customer.service';
import { EconCustomerComponent } from './econ-customer/econ-customer.component';
import { NinjaCustomerComponent } from './ninja-customer/ninja-customer.component';
import { EconProductsComponent } from './econ-products/econ-products.component';
import { NinjaProductsComponent } from './ninja-products/ninja-products.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    CustomerListComponent,    
    EconCustomerComponent,    
    NinjaCustomerComponent, EconProductsComponent,  NinjaProductsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatTableModule,
    HttpClientModule,
    FormsModule,
    MatCheckboxModule,
    MatPaginatorModule,
    MatSidenavModule,
    MatListModule
    
    
   
  ],
  providers: [
    CustomerService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
