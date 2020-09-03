"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var cart_service_1 = require("../services/cart.service");
describe('CartService', function () {
    var component;
    var fixture;
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            declarations: [cart_service_1.CartService]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({});
        fixture = testing_1.TestBed.createComponent(cart_service_1.CartService);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
    it('should be created', function () {
        var service = testing_1.TestBed.get(cart_service_1.CartService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=cart.service.spec.js.map