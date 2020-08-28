import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductService } from '../services/product.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@Component({
  selector: 'app-cars-component',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})

export class FetchCarsComponent implements OnInit, OnDestroy {
  public productModels: IProductModel[];
  destroy$: Subject<boolean> = new Subject<boolean>();

  constructor(private carsService: ProductService) { }

  ngOnInit() {
    this.carsService.getCars().pipe(takeUntil(this.destroy$)).subscribe((data) => {
        console.log(data);
      this.productModels = data;
      //window.alert(this.productModels);
      }, 
      error => console.error(error));
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    // Unsubscribe from the subject
    this.destroy$.unsubscribe();
  }
}



