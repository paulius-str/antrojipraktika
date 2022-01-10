import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: string = "";
  password: string = "";

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  loginAsStudent()
  {
    console.log(this.username);
    this.accountService.login({username: this.username, password: this.password, usertype: "student"}).subscribe(x => {
      console.log("success");
      this.router.navigate(['/student-view']);
    }, error => {
      console.log(error);
    });
  }

  loginAsLecturer()
  {
    console.log(this.username);
    this.accountService.login({username: this.username, password: this.password, usertype: "lecturer"}).subscribe(x => {
      console.log("success");
      this.router.navigate(['/lecturer-view']);
    }, error => {
      console.log(error);
    });
  }

  loginAsAdmin()
  {
    console.log(this.username);
    this.accountService.login({username: this.username, password: this.password, usertype: "admin"}).subscribe(x => {
      console.log("success");
      this.router.navigate(['/admin-view']);
    }, error => {
      console.log(error);
    });
  }

}
