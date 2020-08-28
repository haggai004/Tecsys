import { Injectable } from '@angular/core'
import { BehaviorSubject } from 'rxjs'

//@Injectable()
//export class DataSharingService {

//  private messageSource = new BehaviorSubject('default message');
//  currentMessage = this.messageSource.asObservable();

//  constructor() { }

//  OnNext(message: string) {
//    this.messageSource.next(message)
//  }


//}


@Injectable()
export class DataSharingService {

  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();

  constructor() { }

  changeMessage(message: string) {
    this.messageSource.next(message)
  }

}
