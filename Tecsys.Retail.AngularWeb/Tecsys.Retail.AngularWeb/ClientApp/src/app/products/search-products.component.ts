import { Component, Input, OnInit, OnDestroy, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs'
import { ProductService } from '../services/product.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DataService } from '../services/data.service';
import { CartService } from '../services/cart.service';
import { Guid } from "guid-typescript";
import { ICartItemModel } from '../models/interfaces';
import { IProductModel } from '../models/interfaces';
import { CartItemModel } from '../models/cart-item-model';
import { CartItemsService } from '../services/cart.items.service';

@Component({
  selector: 'app-products-component',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})

export class SearchProductsComponent implements OnInit, OnDestroy {
  public productModels: IProductModel[];
  destroy$: Subject<boolean> = new Subject<boolean>();
  private cartId: string;
  message: string;
  public searchText: string;

  constructor(private productsService: ProductService, private dataService: DataService,
    private cartService: CartService, private cartItemsService: CartItemsService) {
  }

  ngOnInit() {

    this.dataService.currentMessage.subscribe(message => {
      this.searchText = message;
      this.SearchProducts(this.searchText);
    })

  }

  ngOnDestroy() {
    this.destroy$.next(true);
    // Unsubscribe from the subject
    this.destroy$.unsubscribe();
  }


  fetchCars() {

    this.productModels = null;

    this.productsService.getCars().pipe(takeUntil(this.destroy$)).subscribe((data) => {
      console.log(data);
      this.productModels = data;
    },
      error => console.error(error));
  }


  SearchProducts(searchText:string) {
    this.productModels = null;
    if (searchText.length < 2)
      return;

    this.productsService.searchProducts(searchText).pipe(takeUntil(this.destroy$)).subscribe((data) => {
      console.log(data);
      this.productModels = data;
    },
      error => console.error(error));
  }

  onAddToCartClick(productId: number) {

    if (sessionStorage["cartId"])
      this.cartId = sessionStorage["cartId"];
    else {
      this.cartId = Guid.create().toString();
      sessionStorage.setItem("cartId",this.cartId)
    }

    let cartItemModel: ICartItemModel = new CartItemModel();
    cartItemModel.ItemId = Guid.create().toString();
    cartItemModel.CartId = this.cartId;
    cartItemModel.ProductId = productId;
    cartItemModel.Quantity = 1;
    cartItemModel.DateCreated = new Date();

    this.cartService.addToCart(cartItemModel).pipe(takeUntil(this.destroy$)).subscribe((data) => {
      console.log(data);
      this.cartItemsService.onItemAdded(1);
    },
      error => console.error(error));
  }
}
