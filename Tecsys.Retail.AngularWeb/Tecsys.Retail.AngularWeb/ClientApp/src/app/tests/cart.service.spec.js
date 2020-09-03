//import { async, ComponentFixture, TestBed } from '@angular/core/testing';
//import { CartService } from '../services/cart.service';
//import { } from 'jasmine';
//import { HttpClientModule } from '@angular/common/http';
//import { HttpTestingController } from '@angular/common/http/testing';
//import { ICartItemModel } from '../models/interfaces';
//describe('CartService', () => {
//  let component: CartService;
//  let fixture: ComponentFixture<CartService>;
//  let httpMock: HttpTestingController;
//  let service: CartService;
//  beforeEach(async(() => {
//    TestBed.configureTestingModule({
//      declarations: [CartService]
//    })
//      .compileComponents();
//  }));
//  describe('CartService', () => {
//    let service: CartService;
//    beforeEach(() => {
//      TestBed.configureTestingModule({
//        imports: [HttpClientModule],
//        providers: [CartService]
//      });
//      service = TestBed.get(CartService);
//      httpMock = TestBed.get(HttpTestingController);
//    });
//  beforeEach(() => {
//    TestBed.configureTestingModule({});
//    fixture = TestBed.createComponent(CartService);
//    component = fixture.componentInstance;
//    fixture.detectChanges();
//  });
//  it('should create', () => {
//    expect(component).toBeTruthy();
//  });
//  it('should be created', () => {
//    const service: CartService = TestBed.get(CartService);
//    expect(service).toBeTruthy();
//  });
//    it('be able to retrieve CartItemModels from the API bia GET', () => {
//      const dummyPosts: Post[] = [{
//        userId: '1',
//        id: 1,
//        body: 'Hello World',
//        title: 'testing Angular'
//      }, {
//        userId: '2',
//        id: 2,
//        body: 'Hello World2',
//        title: 'testing Angular2'
//      }];
//      service.addToCart().subscribe(posts => {
//        expect(posts.length).toBe(2);
//        expect(posts).toEqual(dummyPosts);
//      });
//      const request = httpMock.expectOne(`${service.ROOT_URl}/posts`);
//      expect(request.request.method).toBe('GET');
//      request.flush(dummyPosts);
//    });
//});
//# sourceMappingURL=cart.service.spec.js.map