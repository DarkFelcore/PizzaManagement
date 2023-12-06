import { Injectable, inject } from '@angular/core';
import { Observable, ReplaySubject, map, of } from 'rxjs';
import { IUser } from '../shared/types/user';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoginRequest } from '../shared/types/requests/login-request';
import { environment } from '../../environments/environment.development';
import { RegisterRequest } from '../shared/types/requests/register-request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSource: ReplaySubject<IUser | null> = new ReplaySubject<IUser | null>(1);
  currentUser$ : Observable<IUser | null> = this.currentUserSource.asObservable();

  // Sevices
  private readonly http : HttpClient = inject(HttpClient);
  private readonly router: Router = inject(Router);

  loadCurrentUser(token: string): Observable<IUser | null> {
    if(token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }
    
    return this.http.get<IUser | null>(environment.baseUrl + 'auth').pipe(
      map((user: IUser | null) => {
        if(user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
        return user;
      })
    )
  }

  login(values: LoginRequest): Observable<IUser | null> {
    return this.http.post<IUser | null>(environment.baseUrl + 'auth/login', values).pipe(
      map((user: IUser | null) => {
        if(user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
        return user;
      })
    )
  }

  register(values: RegisterRequest): Observable<IUser | null> {
    return this.http.post<IUser | null>(environment.baseUrl + 'auth/register', values).pipe(
      map((user: IUser | null) => {
        if(user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
        return user;
      })
    )
  }

  logout(): void {
    this.clearUser();
    this.router.navigateByUrl('/login');
  }

  clearUser(): void {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }

}
