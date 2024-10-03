import { NewOrderDetailDto } from "./newOrderDetailDto";

export class NewOrderDto {
  constructor(
    public empId: number,
    public custId: number,
    public shipperId: number,
    public shipName: string,
    public shipAddress: string,
    public shipCity: string,
    public orderdate: Date,
    public requiredDate: Date,
    public shippedDate: Date,
    public freight: number,
    public shipCountry: string,
    public orderDetails: NewOrderDetailDto
  ) {}
}
