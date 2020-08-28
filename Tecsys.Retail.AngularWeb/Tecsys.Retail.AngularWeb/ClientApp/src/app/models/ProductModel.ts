
import { IProductModel } from '../models/interfaces';

export class ProductModel implements IProductModel {
  ProductId: number;
  ProductName: string;
  UnitPrice: number;
  ImagePath: string;
  Description: string;

  constructor() {}

}
