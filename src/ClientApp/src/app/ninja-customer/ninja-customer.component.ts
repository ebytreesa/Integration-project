import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild, Pipe } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { map } from 'rxjs/operators';
import { Customer } from '../models/Customer';
import { CustomerService } from '../services/customer.service';




@Component({
  selector: 'app-ninja-customer',
  templateUrl: './ninja-customer.component.html',
  styleUrls: ['./ninja-customer.component.css']
})
export class NinjaCustomerComponent implements OnInit {
  customers : Customer[];
  displayedColumns: string[] = ['select', 'name', 'email', 'status'];
  dataSource :  MatTableDataSource<Customer>;
  selection = new SelectionModel<Customer>(true, []);

  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;

  constructor(private service: CustomerService) {
   // this.dataSource = new MatTableDataSource(this.customers);
   }

  ngOnInit(): void {
    this.get();
  }
  private getCustomers() {
    this.service.getCustomers().subscribe(result => {
     // this.dataSource.data = result as Customer[] ;
     this.customers = result.filter(x => x.customerSource == "Invoice Ninja");
     this.dataSource = new MatTableDataSource<any>(this.customers);
     this.dataSource.paginator = this.paginator;
    });
  }
private get(){
  this.service.getNinjaCustomers().subscribe(r =>{
    this.customers = r;
    this.customers.forEach(element => {
      console.log(element)
      if (element.economicCustomerId != 0) {
      element.status = "overført"
    } else {
      element.status = "ikke overført"
    }    
  });
    this.dataSource = new MatTableDataSource<any>(this.customers);
     this.dataSource.paginator = this.paginator;
  })
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
    debugger;  
    const numSelected = this.selection.selected;  
    if (numSelected.length > 0) {  
        if (confirm("Are you sure to transfer items ")) {  
            this.service.addCustomerToEconomic(numSelected).subscribe(
              result => {  
                console.log(result);
              alert("Data blev overført");     
              this.ngOnInit();  
              this.selection.clear() ;       
              }),
              err => {
                alert("Fejl ved syncronisering data");
              console.log("Error");
              }
        }  
    } else {  
        alert("Select at least one row");  
    }  
}  

}
