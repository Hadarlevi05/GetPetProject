import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseFilter } from '../models/base-filter';

export class BaseService {

    protected BASE_URL = 'https://localhost:44345/';

    constructor(protected http: HttpClient) { }

    protected GetHttpParams(filter: BaseFilter) {
        let params: HttpParams = new HttpParams();
        params = params.append('page', `${filter.page}`);
        params = params.append('perPage', `${filter.perPage}`);

        return params;
    }
}