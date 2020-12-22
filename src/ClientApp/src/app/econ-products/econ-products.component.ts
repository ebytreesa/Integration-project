import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild, Pipe } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { map } from 'rxjs/operators';
import { Product } from '../models/Product';
import { ProductService } from '../services/product.service';


@Component({
  selector: 'app-econ-products',
  templateUrl: './econ-products.component.html',
  styleUrls: ['./econ-products.component.css']
})
export class EconProductsComponent implements OnInit {
  products : Product[];
  displayedColumns: string[] = ['select', 'name', 'price','status'];
  dataSource :  MatTableDataSource<Product>;
  selection = new SelectionModel<Product>(true, []);

  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  router: any;

  constructor(private service: ProductService) { }

  ngOnInit(): void {
    this.get();
  }

  private get(){
    this.service.getEconProducts().subscribe(r =>{
      this.products = r;
      this.products.forEach(element => {
        console.log(element)
        if (element.ninjaProductId !== 0) {
        element.status = "overført"
      } else {
        element.status = "ikke overført"
      }      
      });
      this.dataSource = new MatTableDataSource<any>(this.products);    
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
  checkboxLabel(row?: Product): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.name + 1}`;
  }

  TransferData() {  
    debugger;  
    const numSelected = this.selection.selected;  
    if (numSelected.length > 0) {  
        if (confirm("Are you sure to transfer items ")) {  
            this.service.addProductsToNinja(numSelected).subscribe(
              result => {  
                console.log((result))
                alert("Data blev overført");
                this.ngOnInit();
                this.selection.clear() ;

            }) ,            
            err => {
              alert("Fejl ved syncronisering data");
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
