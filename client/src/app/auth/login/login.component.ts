import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EMAIL_REGEX } from '../../shared/constants/constants';
import { LoginRequest } from '../../shared/types/requests/login-request';
import { AuthService } from '../auth.service';
import { IUser } from '../../shared/types/user';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

  // Services
  authService: AuthService = inject(AuthService);
  fb: FormBuilder = inject(FormBuilder);
  router: Router = inject(Router);

  ngOnInit(): void {
    this.createForm();
  }

  createForm(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern(EMAIL_REGEX)]],
      password: ['', [Validators.required]]
    });
  }

  onSubmit(): void {
    if(this.loginForm.valid) {
      var request: LoginRequest = {
        email: this.loginForm.get('email')?.value,
        password: this.loginForm.get('password')?.value
      }
      this.authService.login(request).subscribe({
        next: (user: IUser | null) => this.router.navigateByUrl('/recipes'),
        error: (err: HttpErrorResponse) => console.log(err)
      })
    }
  }

}
