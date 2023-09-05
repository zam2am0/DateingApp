import { HttpClient } from '@angular/common/http';
import { Component ,OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;

  constructor(private http: HttpClient){}

  ngOnInit():void{
    this.getUsers();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  getUsers(){
    this.http.get("https://localhost:5001/api/users").subscribe({ //method sends an HTTP GET request to the specified URL
    next: response =>  this.users = response,  //assigns the response data (list of users) to the users variable.
    error: error => console.log(error), //callback function that gets executed if an error occurs during the HTTP request.
    complete:() => console.log("Request has completed")//callback function that gets executed when the request is completed
  });
  }

  cancelRegisterMode(event: boolean)
  {
    this.registerMode=event;

  }
}
