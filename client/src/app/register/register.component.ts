import { Component ,EventEmitter,Input,OnInit, Output} from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {} 

  constructor(private accountService: AccountService){}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  register(){
    this.accountService.register(this.model).subscribe({
      next: () => {
        //console.log(response);
        this.cancle();
      },
      error: error => console.log(error)
    })
  }

  cancle()
  {
    this.cancelRegister.emit(false);
  }
}
