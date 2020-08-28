
export interface ICartItemModel {
  ItemId: string;
  CartId: string;
  Quantity: number;
  DateCreated: Date;
  ProductId: number;
}

export interface IProductModel {
  ProductId: number;
  ProductName: string;
  UnitPrice: number;
  ImagePath: string;
}
