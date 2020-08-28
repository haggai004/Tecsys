import { Component, Injectable } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { DataService } from '../services/data.service';
import { BehaviorSubject } from 'rxjs'

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],

})

export class NavMenuComponent {
  public searchInputText: string = 'aaaaaaaaaaaaa';
  constructor(private dataService: DataService) {
    this.dataService.currentMessage.subscribe(value => {
      if (this.searchInputText == value)
        return;
      this.searchInputText = value;
      this.Search(this.searchInputText);
    })
  }


  countChange(event) {
    var aaa = 10;
  }


  Search(message) {
    var aa = 100;

    this.dataService.OnNext(this.searchInputText);
  }

}



