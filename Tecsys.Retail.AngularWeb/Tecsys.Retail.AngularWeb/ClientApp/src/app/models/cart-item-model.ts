
import { ICartItemModel } from '../models/interfaces';

export class CartItemModel implements ICartItemModel {
  ItemId: string;
  CartId: string;
  Quantity: number;
  DateCreated: Date;
  ProductId: number;

  constructor() {}

}
