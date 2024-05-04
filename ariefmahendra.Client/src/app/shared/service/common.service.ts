import { Injectable } from '@angular/core';
import {Observable, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  private subjectName= new Subject<any>();

  constructor() { }

  sendUpdate(message: string){
    this.subjectName.next({message})
  }

  getUpdate(): Observable<any> {
    return this.subjectName.asObservable();
  }

  generateInvoiceNumber(): string {
    const randValue = Math.floor(100000 + Math.random() * 900000).toString();
    return `INV-${randValue}`
  }
}
