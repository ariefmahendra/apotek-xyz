import {Component, OnInit} from '@angular/core';
import {Product} from "../models/product";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ProductService} from "../service/product.service";
import {CommonService} from "../../../shared/service/common.service";

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent implements OnInit{

  product: Product
  isAdded: boolean = false;
  message: string = "";
  isShow: boolean = false;

  constructor(
    private readonly productService: ProductService,
    private readonly commonService: CommonService
  ) {
  }

  productForm: FormGroup = new FormGroup({
    productName: new FormControl(null, Validators.required),
    productPrice: new FormControl(null, Validators.required),
    productCode: new FormControl(null, Validators.required),
    stock: new FormControl(null, Validators.required),
  })

  ngOnInit(): void {
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
          this.message = "Gagal Membuat Produk karena";
          this.isAdded = false;
          this.isShow = true;
          setTimeout(() => {
            this.isShow = false;
          }, 1500);
        },
        complete: () => {
          this.commonService.sendUpdate("update component product list")
          this.productForm.reset();
        }
      });
  }
}
