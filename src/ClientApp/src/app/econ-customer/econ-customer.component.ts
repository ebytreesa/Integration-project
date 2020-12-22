import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild, Pipe } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { map } from 'rxjs/operators';
import { Customer } from '../models/Customer';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-econ-customer',
  templateUrl: './econ-customer.component.html',
  styleUrls: ['./econ-customer.component.css']
})
export class EconCustomerComponent implements OnInit {

  customers : Customer[];
  displayedColumns: string[] = ['select', 'name', 'email','status'];
  dataSource :  MatTableDataSource<Customer>;
  selection = new SelectionModel<Customer>(true, []);

  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  router: any;


  constructor(private service: CustomerService) { }

  ngOnInit(): void {
    this.get();
  }

  
private get(){
  this.service.getEconCustomers().subscribe(r =>{
    this.customers = r;
    this.customers.forEach(element => {
      console.log(element)
      if (element.invoiceNinjaCustomerId !== 0) {
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
            this.service.addCustomerToNinja(numSelected).subscribe(
              result => {  
                console.log((result))
                alert("Data blev overført");
                this.selection.clear() ;
                this.ngOnInit();  
            }) ,            
            err => {
              alert("Fejl ved synchronisering data");
            console.log("Error");
      }   
        }  
    } else {  
        alert("Select at least one row");  
    }  
}  

reloadComponent() {
  let currentUrl = this.router.url;
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate([currentUrl]);
  }

}
