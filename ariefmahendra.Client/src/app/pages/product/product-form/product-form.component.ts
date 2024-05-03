import { ApiResponse } from './../../../shared/model/response.model';
import {Component, OnInit} from '@angular/core';
import {Product} from "../models/product";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ProductService} from "../service/product.service";
import {CommonService} from "../../../shared/service/common.service";
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';


@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent implements OnInit{

  product: Product;
  isAdded: boolean = false;
  message: string = "";
  isShow: boolean = false;
  isUpdated: boolean = false;

  constructor(
    private readonly productService: ProductService,
    private readonly commonService: CommonService,
    private readonly activeRoute: ActivatedRoute,
    private readonly router: Router
  ) {
  }

  productForm: FormGroup = new FormGroup({
    productName: new FormControl(null, Validators.required),
    productPrice: new FormControl(null, Validators.required),
    productCode: new FormControl(null, Validators.required),
    stock: new FormControl(null, Validators.required),
  })

  ngOnInit(): void {
    this.activeRoute.params.pipe(
      map((params) => params['id'])
    ).subscribe(id => {
      if (id) {
        this.getProductById(id);
        this.isUpdated = true;
      }
    });
  }

  onSubmit(): void {
    this.product = this.productForm.value;
    this.productService.createNewProduct(this.product)
      .subscribe({
        next: () => {
          this.message = "Produk Berhasil dibuat";
          this.isAdded = true;
          this.isShow = true;
          setTimeout(() => {
            this.isShow = false;
          }, 1500);
        },
        error: (err) => {
          console.log(err);
          this.message = "Gagal Membuat Produk";
          this.isAdded = false;
          this.isShow = true;
          setTimeout(() => {
            this.isShow = false;
          }, 1500);
        },
        complete: () => {
          this.commonService.sendUpdate("update component product list")
          this.productForm.reset();
          this.router.navigateByUrl('product')
        }
      });
  }

  onUpdate(): void {
    this.activeRoute.params.pipe(
      map((params) => params['id'])
    )
    .subscribe({
      next: (id) => {
        this.product = this.productForm.value;
        this.productService.updateProduct(this.product, id)
        .subscribe({
          next: () => {
            this.message = "Produk Berhasil diupdate";
            this.isAdded = true;
            this.isShow = true;
            setTimeout(() => {
              this.isShow = false;
            }, 1500);
          },
          error: (err) => {
            this.message = "Gagal Mengupdate Produk";
            this.isAdded = false;
            this.isShow = true;
            setTimeout(() => {
              this.isShow = false;
            }, 1500);
          },
          complete: () => {
            this.commonService.sendUpdate("update component product list")
            this.productForm.reset();
            this.router.navigateByUrl('product')
          }
        })
      }, 
      complete: () => {
        this.isUpdated = false;
      }
    })
  }

  getProductById(id: string): void {
    this.productService.getProductById(id)
      .subscribe({
        next: (response: ApiResponse<Product>) => {
          this.product = response.data;
          this.setFormValue(this.product);
        }
      })
  }

  setFormValue(product: Product): void {
    this.productForm.setValue(
      {
        productName: product.productName,
        productPrice: product.productPrice,
        productCode: product.productCode,
        stock: product.stock
      }
    );
  }
}
