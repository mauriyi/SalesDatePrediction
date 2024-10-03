export interface Order {
  orderId: number;
  requiredDate: Date;
  shippedDate: Date | null;
  shipName: string;
  shipAddress: string;
  shipCity: string;
}
