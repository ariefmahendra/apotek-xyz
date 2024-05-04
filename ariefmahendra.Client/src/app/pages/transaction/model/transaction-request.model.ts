import {TransactionDetails} from "./transaction-detail-request.model";

export interface TransactionRequestModel {
  noInvoice: string,
  transactionDate: Date,
  transactionDetails: TransactionDetails[]
}
