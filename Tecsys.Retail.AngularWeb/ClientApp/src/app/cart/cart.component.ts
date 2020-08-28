import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CartService } from '../services/cart.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@Component({
  selector: 'app-cart-component',
  templateUrl: './cart-items.component.html',
  styleUrls: ['./cart-items.component.css']
})

export class CartComponent implements OnInit, OnDestroy {
  public productModels: IProductModel[];
  destroy$: Subject<boolean> = new Subject<boolean>();

  constructor(private cartService: CartService) { }

  ngOnInit() {

  }

  ngOnDestroy() {
    this.destroy$.next(true);
    // Unsubscribe from the subject
    this.destroy$.unsubscribe();
  }

  addToCart(productModel: IProductModel) {
    var str = 'aaaaaaaaaaa';

    this.cartService.addToCart(productModel).pipe(takeUntil(this.destroy$)).subscribe((data) => {
      console.log(data);
      //this.productModels = data;
      window.alert(data);
    },
      error => console.error(error));
  }

  getItemsInCart() {
    this.cartService.getItemsInCart().pipe(takeUntil(this.destroy$)).subscribe((data) => {
      console.log(data);
      let productModels: IProductModel = data;
      //window.alert(this.productModels);
    },
      error => console.error(error));
  }
}



