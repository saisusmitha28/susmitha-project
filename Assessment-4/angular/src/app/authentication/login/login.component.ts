import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { HttpService } from 'src/app/services/http.service';
import { ILogin } from './login-model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  loginObj = {} as ILogin;
  subscription!: Subscription;
  returnUrl!: string | null;

  constructor(private _authenticationService: AuthenticationService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  public login(f: NgForm) {
    this.subscription = this._authenticationService.postData("Accounts/LoginUser", this.loginObj).subscribe({
      next: (data: any) => {
        if (data.statusCode == 200 && data.data != null) {
          console.log(data);
          localStorage.setItem("token", data.data);
          if (this.returnUrl)
            this.router.navigate([this.returnUrl]);
          else
            this.router.navigate(["/admin/dashboard"]);
        }
        else {
          console.log(data.message);
        }
      },
      error: reason => console.log(reason)
    });
  }
  ngOnDestroy(): void {
    if (this.subscription)
      this.subscription.unsubscribe();
  }
 
}
