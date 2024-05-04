import { Injectable } from '@angular/core';
import {Observable, Subject} from "rxjs";
import {ApiResponse} from "../../../shared/model/response.model";
import {Transaction} from "../model/transaction.model";
import {TransactionRequestModel} from "../model/transaction-request.model";
import {HttpClient} from "@angular/common/http";
import {CodeService} from "../../../shared/types/enum.types";
import {environment} from "../../../../environments/environment.development";
import { Product } from '../../product/models/product';
import { TransactionHeader } from '../model/transaction-header.model';
import { TransactionProductForm } from '../model/transaction-product-form.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private _baseUrl: string = environment.url;
  private _productList: Product[] = [];
  private _transactionHeader: TransactionHeader;
  private _listProductTx: TransactionProductForm[] = [];
  private _totalTransactionPrice: number = 0;
  

  constructor(
    private readonly http: HttpClient
  ) { }

  createTransaction(payload: TransactionRequestModel): Observable<ApiResponse<Transaction>>{
    return this.http.post<ApiResponse<Transaction>>(this._baseUrl + CodeService.CREATE_TRANSACTION, payload);
  }

  addProduct(product: Product): void{
    this._productList.push(product);
  }

  getProductList(): Product[]{
    return this._productList;
  }

  sendTransactionHeader(TransactionHeader: TransactionHeader): void {
    this._transactionHeader = TransactionHeader;
  }

  getTransactionHeader(): TransactionHeader{
    return this._transactionHeader
  }

  addProductTx(product: TransactionProductForm): void{
    this._listProductTx.push(product);
    this._totalTransactionPrice += product.subTotal;
  }

  getListProductTx(): TransactionProductForm[]{
    return this._listProductTx
  }

  clearProductTx(): void {
    this._listProductTx = [];
  }

  getTotalTransactionPrice(): number{
    return this._totalTransactionPrice
  }

}
