import { TransactionDetail } from "./transaction-detail.model";

export interface Transaction {
    id: string,
    noInvoice: string,
    transaction: Date,
    total: number,
    transactionDetails: TransactionDetail[]
}
