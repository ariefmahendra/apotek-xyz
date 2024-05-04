import { Component,OnInit } from '@angular/core';
import { ProductService } from '../product/service/product.service';
import { ApiResponse } from '../../shared/model/response.model';
import { Product } from '../product/models/product';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {TransactionService} from "./service/transaction.service";
import {TransactionRequestModel} from "./model/transaction-request.model";

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrl: './transaction.component.scss'
})
export class TransactionComponent {

}
