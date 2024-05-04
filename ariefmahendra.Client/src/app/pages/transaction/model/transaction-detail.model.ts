import { Product } from "../../product/models/product";

export interface TransactionDetail {
    id: string, 
    quantity: number,
    product: Product
}
