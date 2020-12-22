import { CustomerContact } from "./CustomerContact";

export class Customer {
    customerId: number;
    customerName: string;
    customerSource : string;
    customerSourceId :number;
    invoiceNinjaCustomerId : number;
    economicCustomerId : number;
    address :string;
    city : string;
    country :string;
    email : string;
    contacts : CustomerContact[];
    deliveryLocations : any[];
     status : string ;
    
}
