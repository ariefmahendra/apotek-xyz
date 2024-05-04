import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { TransactionRoutingModule } from './transaction-routing.module';
import { TransactionComponent } from './transaction.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { TransactionHeaderComponent } from './component/transaction-header/transaction-header.component';
import { TransactionProductFormComponent } from './component/transaction-product-form/transaction-product-form.component';
import { TransactionListComponent } from './component/transaction-list/transaction-list.component';
import { TransactionService } from './service/transaction.service';


@NgModule({
  declarations: [
    TransactionComponent,
    TransactionHeaderComponent,
    TransactionProductFormComponent,
    TransactionListComponent
  ],
  imports: [
    CommonModule,
    TransactionRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    TransactionService,
    DatePipe
  ]
})
export class TransactionModule { }
