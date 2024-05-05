import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import { TransactionService } from '../../service/transaction.service';
import { DatePipe } from '@angular/common';
import { CommonService } from './../../../../shared/service/common.service';

@Component({
  selector: 'app-transaction-header',
  templateUrl: './transaction-header.component.html',
  styleUrl: './transaction-header.component.scss'
})
export class TransactionHeaderComponent implements OnInit{

  isDisabled: boolean =  true;
  date: string;

  constructor(
    private readonly txService: TransactionService,
    private readonly datePipe: DatePipe,
    private readonly commonService: CommonService
  ) { }


  ngOnInit(): void {  
    this.sendTransactionHeader();
  }

  transactionForm: FormGroup = new FormGroup({
    noInvoice: new FormControl({value: this.commonService.generateInvoiceNumber(), disabled: this.isDisabled }, [Validators.required]),
    fakturDate: new FormControl({value: this.datePipe.transform(Date.now(), "yyyy-MM-dd"), disabled: this.isDisabled }, [Validators.required]),
    operator: new FormControl({value: 'Tes', disabled: this.isDisabled }, [Validators.required]),
  })

  sendTransactionHeader(): void {
    this.txService.sendTransactionHeader({
      invoiceDate: this.date = Date.now().toString(),
      noInvoice: this.transactionForm.value.noInvoice,
      operator: this.transactionForm.value.operator
    });
  }
}
