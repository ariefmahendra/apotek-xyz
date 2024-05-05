import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../../service/transaction.service';
import { TransactionProductForm } from '../../model/transaction-product-form.model';
import { CommonService } from '../../../../shared/service/common.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.scss'
})
export class TransactionListComponent implements OnInit {

  productListTx: TransactionProductForm[] = [];
  totalPrice: number;

  constructor(
    private readonly txService: TransactionService,
    private readonly commonService: CommonService
  ) { }

  calculateForm: FormGroup = new FormGroup ({
    total: new FormControl({value: null, disabled: true }, [Validators.required]),
    cash: new FormControl({value: null, disabled: false }, [Validators.required]),
    returnCash: new FormControl({value: null, disabled: true }, [Validators.required]),
  })

  ngOnInit(): void {
    this.commonService.getUpdate()
      .subscribe({
        next: () => {
          this.getTransactionList(); 
          this.getTotalPrice();
        }
      });
  }

  getTransactionList(): void {  
    this.productListTx = this.txService.getListProductTx();
  }

  clear(): void {
    this.totalPrice = 0;
    this.txService.clearTotalTranscationPrice();
    this.productListTx = [];
    this.txService.clearProductTx();
    this.calculateForm.reset();
  }

  getTotalPrice(): void {
   this.totalPrice = this.txService.getTotalTransactionPrice();
   this.calculateForm.patchValue({
    total: this.totalPrice
   })
  }

  change(): void{
    let returnCash = this.calculateForm.value.cash - this.txService.getTotalTransactionPrice();
    if (returnCash < 0) {
      returnCash = 0;
    }
    
    this.calculateForm.patchValue({
      returnCash: returnCash 
    })
  }

  creteNewTransaction(): void {
    if (this.productListTx.length == 0) {
      this.txService.clearTotalTranscationPrice();
      return alert("tidak ada transaksi yang dibuat, silahkan tambahkan transaksi terlebih dahulu");
    }

    if (this.calculateForm.invalid) {
      this.txService.clearTotalTranscationPrice();
      return alert("gagal membuat transaksi, silahkan bayar transaksi terlebih dahulu");
    }

    this.txService.createTransaction()
      .subscribe({
        next: () => {
          alert("sukses membuat transaksi");
          this.clear();
          this.txService.clearTotalTranscationPrice();
        }, 
        error: (err: HttpErrorResponse) => {                 
          alert(err.error.message);
          this.clear();
          this.txService.clearTotalTranscationPrice();
        }
      })
  }

}
