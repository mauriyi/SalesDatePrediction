export class NewOrderDetailDto {
  constructor(
    public productId: number,
    public unitPrice: number,
    public qty: number,
    public discount: number
  ) {}
}
