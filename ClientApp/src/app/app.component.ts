import { Component } from '@angular/core';
import { AccountService } from './account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(private accountService: AccountService) {}

  ngOnInit() {
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    if(token) {
      this.accountService.loadCurrentUser(token).subscribe(() => {
        console.log("Success");
      }, error => {
        console.log(error);
      });
    }
  }
}
