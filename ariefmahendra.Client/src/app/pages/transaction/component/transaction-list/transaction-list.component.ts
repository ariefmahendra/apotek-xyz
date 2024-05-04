import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../../service/transaction.service';
import { TransactionProductForm } from '../../model/transaction-product-form.model';
import { CommonService } from '../../../../shared/service/common.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.scss'
})
export class TransactionListComponent implements OnInit {

  productListTx: TransactionProductForm[] = [];
  cash: number;
  totalPrice: number;

  constructor(
    private readonly txService: TransactionService,
    private readonly commonService: CommonService
  ) { }

  calculateForm: FormGroup = new FormGroup ({
    total: new FormControl({value: null, disabled: true }, [Validators.required]),
    cash: new FormControl({value: null, disabled: false }, [Validators.required]),
    kembalian: new FormControl({value: null, disabled: true }, [Validators.required]),
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
    this.calculateForm.patchValue({
      cash: this.cash,
      kembalian: this.txService.getTotalTransactionPrice() - this.cash
    })
  }

}
