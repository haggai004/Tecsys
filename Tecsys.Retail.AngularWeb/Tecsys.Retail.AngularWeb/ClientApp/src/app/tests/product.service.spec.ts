import { async, TestBed, inject } from '@angular/core/testing';
import { ProductService } from '../services/product.service';
import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';
import { } from 'jasmine';
import { IProductModel } from '../models/interfaces';

describe('ProductService', () => {
  let service: ProductService;
  let httpMock: HttpTestingController;
  let dummyProductModels: IProductModel[];

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProductService]
    }).compileComponents();

    service = TestBed.get(ProductService);
    httpMock = TestBed.get(HttpTestingController);
  }));

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
    expect(service).toBeDefined();
  });

  it('getCars test', async( () => {
      dummyProductModels = [{
      ProductId: 1,
      ProductName: 'Fast Car',
      UnitPrice: 100.5,
      ImagePath: 'c:\fastcar.img'
    },
    {
      ProductId: 2,
      ProductName: 'Slow Car',
      UnitPrice: 1.5,
      ImagePath: 'c:\slowcar.img'
      }];

    service.getCars().subscribe(cars => {
      expect(cars.length).toBe(2);
      expect(cars).toEqual(dummyProductModels);
      expect(cars[0].ProductName).toEqual('Fast Car');
    });

    const req = httpMock.expectOne(`${service.REST_API_SERVER}api/product/`);
    expect(req.request.url.endsWith('api/product/')).toEqual(true);
    expect(req.request.method).toBe('GET');
    req.flush(dummyProductModels);

  }));
});

 
