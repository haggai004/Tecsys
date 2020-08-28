import { Component, Input, OnInit, OnDestroy, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs'
import { ProductService } from '../services/product.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-products-component',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
 
})

@Injectable()
export class SearchProductsComponent implements OnInit, OnDestroy {
  @Input() SearchText: String = 'aaaa';
  public productModels: IProductModel[];
  destroy$: Subject<boolean> = new Subject<boolean>();

  message: String;

  constructor(private productsService: ProductService, private dataService: DataService) { }

  ngOnInit() {

    this.dataService.currentMessage.subscribe(message => {

      this.message = message;

      this.SearchProducts(this.message);
    })
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    // Unsubscribe from the subject
    this.destroy$.unsubscribe();
  }

  SearchProducts(SearchText) {

      window.alert(this.message);

    this.productsService.searchProducts(SearchText).pipe(takeUntil(this.destroy$)).subscribe((data) => {
      console.log(data);
      this.productModels = data;
      //window.alert(this.productModels);
    },
      error => console.error(error));
  }

  onRowClick(entity) {
    var val = 100;
  }
}


//import { Component, OnInit } from '@angular/core';
//import { DataService } from "../data.service";

//@Component({
//  selector: 'app-sibling',
//  template: `
//    {{message}}
//    <button (click)="newMessage()">New Message</button>
//  `,
//  styleUrls: ['./sibling.component.css']
//})
//export class SiblingComponent implements OnInit {

//  message: string;

//  constructor(private data: DataService) { }

//  ngOnInit() {
//    this.data.currentMessage.subscribe(message => this.message = message)
//  }

//  newMessage() {
//    this.data.changeMessage("Hello from Sibling")
//  }

//}


