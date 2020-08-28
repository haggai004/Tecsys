import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { IProductModel } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})

export class ProductService {

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

  public getCars() {
    return this.httpClient.get<IProductModel[]>(this.REST_API_SERVER + 'api/product/').pipe(catchError(this.handleError));
  }

  public searchProducts(productName:String) {
    return this.httpClient.get<IProductModel[]>(this.REST_API_SERVER + 'api/product/'+productName).pipe(catchError(this.handleError));
  }
}

