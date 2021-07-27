import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseFilter } from '../models/base-filter';

export class BaseService {

    protected BASE_URL = 'https://localhost:44345/api/';

    protected IMAGE_BASE_URL = 'https://localhost:44345/api/';

    constructor(
        protected http: HttpClient) { }

    protected getHttpParams(filter: BaseFilter) {
        let params: HttpParams = new HttpParams();
        params = params.append('page', `${filter.page}`);
        params = params.append('perPage', `${filter.perPage}`);

        return params;
    }
}