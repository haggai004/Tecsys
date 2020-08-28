import { Component, OnInit, OnDestroy, Injectable } from '@angular/core';
import { DataService } from '../services/data.service';
import { CartItemsService } from '../services/cart.items.service';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})

export class NavMenuComponent implements OnInit, OnDestroy{
  public searchInputText: string = '';
  public cartItemsCount: number = 0;
  initialized: boolean = false;
  destroy$: Subject<boolean> = new Subject<boolean>();

  constructor(private dataService: DataService, private cartItemsService: CartItemsService, private router: Router) {
  }

  ngOnInit() {

    this.cartItemsService.currentMessage.subscribe(count => {
      this.cartItemsCount += count;
    })
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    // Unsubscribe from the subject
    this.destroy$.unsubscribe();
  }

  Search(searchText: string) {
    if (!(searchText && searchText.length >= 2))
      return;

    if (!this.initialized)
      this.router.navigate(['/products']);

    this.dataService.OnNext(searchText);

    this.initialized = true;
  }

}



