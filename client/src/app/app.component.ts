import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title  = 'Dating App';

  users : any; //can be any type
  constructor(private http: HttpClient){} //  It initializes the component and injects an instance of the HttpClient service, which is used to make HTTP requests.

  ngOnInit():void{ //angular method
    this.http.get("https://localhost:5001/api/user").subscribe({ //method sends an HTTP GET request to the specified URL
      next: response =>  this.users = response,  //assigns the response data (list of users) to the users variable.
      error: error => console.log(error), //callback function that gets executed if an error occurs during the HTTP request.
      complete:() => console.log("Request has completed")//callback function that gets executed when the request is completed

    });
  }

}
