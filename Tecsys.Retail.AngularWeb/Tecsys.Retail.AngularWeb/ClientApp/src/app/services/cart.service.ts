import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { ICartItemModel } from '../models/interfaces';
import { IProductModel } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})

export class CartService {

  private REST_API_SERVER = "https://localhost:44326/";

  constructor(private httpClient: HttpClient) { }
  
  handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = "Error: ${error.error.message}";
    } else {
      // Server-side errors
      errorMessage = "Error Code:" +error.statusText + "\nMessage:"+ error.message;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }

  public addToCart(cartItemModel:ICartItemModel) {
    return this.httpClient.post(this.REST_API_SERVER + 'api/cart/', cartItemModel).pipe(catchError(this.handleError));
  }
  
  public getItemsInCart() {
    return this.httpClient.get<IProductModel>(this.REST_API_SERVER + 'api/product/').pipe(catchError(this.handleError));
  }
}


