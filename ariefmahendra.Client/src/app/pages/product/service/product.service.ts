import { Injectable } from '@angular/core';
import {catchError, Observable} from "rxjs";
import {ApiResponse} from "../../../shared/model/response.model";
import {Product} from "../models/product";
import {CodeService} from "../../../shared/types/enum.types";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment.development";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseurl = environment.url;

  constructor(
    private readonly http: HttpClient
  ) { }

  public listProducts(): Observable<ApiResponse<Product[]>> {
    return this.http.get<ApiResponse<Product[]>>(this.baseurl + CodeService.GET_ALL_PRODUCT)
      .pipe(
        catchError((err: Error) => {
          throw err;
        })
      );
  }

  public getProductById (id: string): Observable<ApiResponse<Product>> {
    return this.http.get<ApiResponse<Product>>(this.baseurl + CodeService.GET_PRODUCT_BY_ID + id)
      .pipe(
        catchError((err: Error) => {
          throw err;
        })
      );
  }

  public createNewProduct(product: Product): Observable<ApiResponse<Product>> {
    return this.http.post<ApiResponse<Product>>(this.baseurl + CodeService.CREATE_PRODUCT, product)
      .pipe(
        catchError((err: Error) => {
          throw err;
        })
      );
  }

  public deleteProduct(id: string): Observable<ApiResponse<string>> {
    return this.http.delete<ApiResponse<any>>(this.baseurl + CodeService.DELETE_PRODUCT + id)
      .pipe(
        catchError((err: Error) => {
          throw err;
        })
      );
  }

  public updateProduct(product: Product): Observable<ApiResponse<Product>>{
    return this.http.put<ApiResponse<Product>>( this.baseurl + CodeService.UPDATE_PRODUCT + `{id}`, product)
      .pipe(
        catchError((err: Error) => {
          throw err;
        })
      );
  }
}
