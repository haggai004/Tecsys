import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductService } from '../services/product.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Guid } from "guid-typescript";
import { ICartItemModel } from '../models/interfaces';
import { IProductModel } from '../models/interfaces';
import { CartItemModel } from '../models/cart-item-model';
import { CartService } from '../services/cart.service';
import { CartItemsService } from '../services/cart.items.service';

@Component({
  selector: 'app-cars-component',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})

export class FetchCarsComponent implements OnInit, OnDestroy {
  public productModels: IProductModel[];
  private cartId: string;

  destroy$: Subject<boolean> = new Subject<boolean>();

  constructor(private carsService: ProductService, private cartService: CartService, private cartItemsService: CartItemsService) { }

  ngOnInit() {
    this.carsService.getCars().pipe(takeUntil(this.destroy$)).subscribe((data) => {
        console.log(data);
      this.productModels = data;
      }, 
      error => console.error(error));
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    // Unsubscribe from the subject
    this.destroy$.unsubscribe();
  }

  onAddToCartClick(productId: number) {

    if (sessionStorage["cartId"])
      this.cartId = sessionStorage["cartId"];
    else {
      this.cartId = Guid.create().toString();
      sessionStorage.setItem("cartId", this.cartId)
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



