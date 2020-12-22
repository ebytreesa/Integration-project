import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerService } from 'src/app/services/customer.service';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { MatTableDataSource } from '@angular/material/table';
import { Customer } from '../models/Customer';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  customers : Customer[];
  displayedColumns: string[] = ['select', 'name', 'email'];
  dataSource :  MatTableDataSource<Customer>;
  selection = new SelectionModel<Customer>(true, []);
  loading: boolean = true;

  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;

  constructor(private service: CustomerService) {
    this.dataSource = new MatTableDataSource(this.customers);
   }

  ngOnInit(): void {
     this.getCustomers();
  }

  private getCustomers() {
    this.service.getCustomers().subscribe(result => {
      this.loading = false;
      this.dataSource.data = result as Customer[] ;
    // this.users = response.users;
    // this.dataSource = new MatTableDataSource<any>(this.users);
     this.dataSource.paginator = this.paginator;
    });
  }


   /** Whether the number of selected elements matches the total number of rows. */
   isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = !!this.dataSource && this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: Customer): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.customerName + 1}`;
  }

  TransferData() {  
    
}  

}
