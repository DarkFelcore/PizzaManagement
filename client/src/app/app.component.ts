import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpErrorResponse } from '@angular/common/http';
import { NgxSpinnerModule } from "ngx-spinner";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { AuthService } from './auth/auth.service';
import { IUser } from './shared/types/user';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule, 
    RouterOutlet, 
    FormsModule, 
    HttpClientModule, 
    NgxSpinnerModule,
    FontAwesomeModule,
    NavbarComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  // Services
  authService: AuthService = inject(AuthService);

  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser(): void {
    var token: string = localStorage.getItem('token') as string;
    this.authService.loadCurrentUser(token).subscribe({
      next: (user: IUser | null) => {},
      error: (err: HttpErrorResponse) => this.authService.clearUser()
    })
  }

}
