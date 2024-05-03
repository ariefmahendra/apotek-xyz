import {Component, OnInit} from '@angular/core';
import {Product} from "../models/product";
import {ProductService} from "../service/product.service";
import {ApiResponse} from "../../../shared/model/response.model";
import {CommonService} from "../../../shared/service/common.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit{
  products: Product[] = [];
  private subscription: Subscription;

  constructor(
    private readonly productService: ProductService,
    private readonly commonService: CommonService
  ) {
  }

  ngOnInit(): void {
    this.getAllProducts();
    this.subscription = this.commonService.getUpdate()
      .subscribe({
        next: () => this.getAllProducts()
      })
  }

  getAllProducts(): void {
    this.productService.listProducts()
      .subscribe({
        next: (response: ApiResponse<Product[]>) => {
          this.products = response.data;
        },
        error: (err: Error) => {
          console.error(err);
        }
      });
  }

  onDeleteProduct(id: string): void {
    this.productService.deleteProduct(id)
      .subscribe({
        error: (err: Error) => {
          console.error(err);
        },
        complete: (()=> {
          this.ngOnInit();
        })
      });
  }
}
