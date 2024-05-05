import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Product} from "../../../product/models/product";
import { ProductService } from '../../../product/service/product.service';
import { ApiResponse } from '../../../../shared/model/response.model';
import { TransactionService } from '../../service/transaction.service';
import { CommonService } from '../../../../shared/service/common.service';

@Component({
  selector: 'app-transaction-product-form',
  templateUrl: './transaction-product-form.component.html',
  styleUrl: './transaction-product-form.component.scss'
})
export class TransactionProductFormComponent implements OnInit {
  
  isDisabled: boolean =  true;
  listProduct: Product[] = [];
  selectedProduct: Product;
  listProductAdded: Product[] =[];
  alertIsShow: boolean = false;
  message: string = '';
  btnIsShow: boolean = true;
  
  constructor(
    private readonly productService: ProductService,
    private readonly txService: TransactionService,
    private readonly updated: CommonService
  ) { }

  ngOnInit(): void {
    this.getAllProduct();
  }

  transactionProductForm: FormGroup = new FormGroup({
    productName: new FormControl({value: '', disabled: this.isDisabled }, [Validators.required]),
    productPrice: new FormControl({value: null, disabled: this.isDisabled }, [Validators.required]),
    quantity: new FormControl({value: null, disabled: false }, [Validators.required,  Validators.pattern("^[0-9]*$"), Validators.min(1)]),
    total: new FormControl({value: null, disabled: this.isDisabled }, [Validators.required]),
  })

  onAddProduct(): void {
    if (this.transactionProductForm.valid) {
      this.txService.addProduct(this.selectedProduct);
      this.txService.addProductTx(
        {
          id: this.selectedProduct.id,
          productName: this.selectedProduct.productName,
          productCode: this.selectedProduct.productCode,
          productPrice: this.selectedProduct.productPrice,
          quantity: this.transactionProductForm.value.quantity,
          subTotal: this.transactionProductForm.value.quantity * this.selectedProduct.productPrice
        }
      );
      this.transactionProductForm.patchValue({
        total: this.transactionProductForm.value.quantity * this.selectedProduct.productPrice
      })
      this.updated.sendUpdate("render component");  
    } else {
      this.message = "gagal membuat transaksi karena data tidak lengkap"
      this.alertIsShow = true
      setTimeout(() => {
        this.alertIsShow = false
      }, 1500)
    }
  }
  
  getAllProduct(): void {
    this.productService.listProducts()
      .subscribe({
        next: (res: ApiResponse<Product[]>) => {
          this.listProduct = res.data;
        }
      });
  }

  onSelectedProduct(value: any): void {
    this.selectedProduct = this.listProduct.find(product => product.productCode == value);
    this.transactionProductForm.patchValue({
      productName: this.selectedProduct.productName,
      productPrice: this.selectedProduct.productPrice
    })
  }

  onChangeQuantity(): void {
    if (this.selectedProduct) {
      this.transactionProductForm.patchValue({
        total: this.transactionProductForm.value.quantity * this.selectedProduct.productPrice
      });

      if (this.selectedProduct.stock < this.transactionProductForm.value.quantity) {
        this.message = "gagal membuat transaksi, stok tidak mencukupi"
        this.alertIsShow = true;
        this.btnIsShow = false;
      } else if (!this.transactionProductForm.valid) {
        this.message = "gagal membuat transaksi, input tidak valid"
        this.alertIsShow = true;
        this.btnIsShow = false;
      } else {
        this.alertIsShow = false;
        this.btnIsShow = true;
      }
    }
  }
}
