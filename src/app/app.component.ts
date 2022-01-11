import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'client';
  users : any;

  constructor(private http:HttpClient , private accoutService: AccountService){}


  ngOnInit() {
     this.setCurrentUseruser();
    }  


    setCurrentUseruser()
    {
      const user:User =JSON.parse(localStorage.getItem('user'));
      this.accoutService.setCurrentUser(user);
    }
}





