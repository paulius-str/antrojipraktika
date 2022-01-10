import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from '../account.service';
import { IUser } from '../models/user';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  currentUser$: Observable<IUser>;
  
  constructor(private accountService: AccountService) {}

  ngOnInit(){
    this.currentUser$ = this.accountService.currentUser$;
  }

  logOut() {
    this.accountService.logout();
  }
}
