import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title  = 'Dating App';

  constructor(private accountService: AccountService){} 

  ngOnInit():void{ //angular method
   this.setCurrentUser();
  }
  /*
  getUsers(){
    this.http.get("https://localhost:5001/api/users").subscribe({ //method sends an HTTP GET request to the specified URL
    next: response =>  this.users = response,  //assigns the response data (list of users) to the users variable.
    error: error => console.log(error), //callback function that gets executed if an error occurs during the HTTP request.
    complete:() => console.log("Request has completed")//callback function that gets executed when the request is completed
  });
  */

  

  setCurrentUser()
  {
    const userString = localStorage.getItem('user');
    if(!userString) return;
    const user : User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);  
  }

}


