
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { } from 'jasmine';

import { FetchCarsComponent } from '../cars/fetch-cars.component';
import { of } from 'rxjs';
import { HttpTestingController } from '@angular/common/http/testing';
import { HttpClient, HttpClientModule, HttpHandler } from '@angular/common/http';
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
import { Console } from 'console';

describe('FetchCarsComponent', () => {
  let fixture: ComponentFixture<FetchCarsComponent>;
  let component: FetchCarsComponent;
  let productService: ProductService;
  let productModels: IProductModel[];
  let el: HTMLElement;
  let mockProductService = jasmine.createSpyObj<ProductService>('ProductService', ['getCars']);

  mockProductService.getCars.and.returnValues(
    of<IProductModel[]>(
      [
        {
          ProductId: 1,
          ProductName: 'Fast Car',
          UnitPrice: 100.5,
          ImagePath: 'c:\image1.img'
        },
        {
          ProductId: 2,
          ProductName: 'Slow Car',
          UnitPrice: 1.5,
          ImagePath: 'c:\image2.img'
        }
      ]
    ));

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
      ],
      declarations: [
        FetchCarsComponent
      ],
      providers: [
        HttpClient,
        HttpHandler,
        HttpClientModule,
        { provide: ProductService, useValue: mockProductService }
      ]
    }).compileComponents();
  }));

    // synchronous beforeEach(): fixtures and components setup
    beforeEach(() => {
      fixture = TestBed.createComponent(FetchCarsComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('create component test', () => {
      expect(component).toBeTruthy();
      expect(component).toBeDefined();

    });

  it('should display a "CARS (AngularWeb)" title',
      async(() => {
        let title = fixture.nativeElement.querySelector('h1');
        expect(title.textContent).toEqual('CARS (AngularWeb)');
      }));

    it('should contain a table with a list of one or more Cars',
      async(() => {
        let table = fixture.nativeElement.querySelector('.table-striped');
        let tableRows = table.querySelectorAll('.body-row');
        expect(tableRows.length).toBeGreaterThan(0);
        expect(tableRows.length).toBe(2);
      }));

  })
