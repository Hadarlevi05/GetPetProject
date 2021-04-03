import { HttpClient } from '@angular/common/http';

export class BaseService {

    protected BASE_URL = 'https://localhost:44345/';

    constructor(protected http: HttpClient) 
    { }
}