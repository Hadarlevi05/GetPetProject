import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IUser } from 'src/app/shared/models/iuser';
import { BaseService } from './base-service';
import { IUserLogin } from '../models/user-login';
import { ILoginResponse } from '../models/ilogin-response';


@Injectable({ providedIn: 'root' })
export class AuthenticationService extends BaseService {

    private currentUserSubject: BehaviorSubject<IUser>;
    private tokenSubject: BehaviorSubject<string>;

    public currentUser: Observable<IUser>;
    public token: Observable<string>;

    constructor(protected http: HttpClient) {
        super(http);
        let user: IUser = {} as IUser;
        let token = '';

        if (localStorage.getItem('currentUser')) {
            user = JSON.parse(localStorage.getItem('currentUser') || '');
        }
        this.currentUserSubject = new BehaviorSubject<IUser>(user);

        if (localStorage.getItem('token')) {
            token = localStorage.getItem('token') || '';
        }

        this.tokenSubject = new BehaviorSubject<string>(token);

        this.currentUser = this.currentUserSubject.asObservable();
        this.token = this.tokenSubject.asObservable();

    }

    public get currentUserValue(): IUser {
        return this.currentUserSubject.value;
    }

    public get tokenValue(): string {
        return this.tokenSubject.value;
    }

    login(email: string, password: string) {
        return this.http.post<ILoginResponse>(`${this.BASE_URL}users/login`, { email, password })
            .pipe(map(loginResponse => {
                // login successful if there's a jwt token in the response
                if (loginResponse && loginResponse.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(loginResponse.user));
                    localStorage.setItem('token', JSON.stringify(loginResponse.token));

                    this.currentUserSubject.next(loginResponse.user);
                    this.tokenSubject.next(loginResponse.token);
                }
                return loginResponse;
            }));
    }

    register(user: IUser) {

        return this.http.post<ILoginResponse>(`${this.BASE_URL}users/register`, user)
            .pipe(map(userResponse => this.login(user.email, user.password)));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        localStorage.removeItem('token');

        this.currentUserSubject.next({} as IUser);
        this.tokenSubject.next('');

    }
}

