import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IUser } from 'src/app/shared/models/iuser';


@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    
    private currentUserSubject: BehaviorSubject<IUser>;
    public currentUser: Observable<IUser>;

    constructor(private http: HttpClient) {
        
        let user: IUser = {} as IUser;

        if (localStorage.getItem('currentUser')) {                        
            user = JSON.parse(localStorage.getItem('currentUser') || '');
            this.currentUserSubject = new BehaviorSubject<IUser>(user);
        } else  {
            this.currentUserSubject = new BehaviorSubject<IUser>(user);
        }
        
        this.currentUser = this.currentUserSubject.asObservable();
        
    }

    public get currentUserValue(): IUser {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<any>(`/users/authenticate`, { username, password })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    this.currentUserSubject.next(user);
                }

                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next({} as IUser);
    }
}

