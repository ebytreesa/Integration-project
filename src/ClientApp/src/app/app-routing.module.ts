import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { CustomerListComponent } from './customer/customer-list/customer-list.component';
import { NinjaCustomerComponent } from './ninja-customer/ninja-customer.component';
import { NinjaProductsComponent } from './ninja-products/ninja-products.component';

import { EconCustomerComponent } from './econ-customer/econ-customer.component';
import { EconProductsComponent } from './econ-products/econ-products.component';


const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'customers', component: CustomerListComponent },
  { path: 'ninjaCustomers', component: NinjaCustomerComponent },
  { path: 'ninjaProducts', component: NinjaProductsComponent },

  { path: 'econCustomers', component: EconCustomerComponent },
  { path: 'econProducts', component: EconProductsComponent },


  { path: '**', redirectTo: 'home', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
