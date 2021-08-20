import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseFilter } from '../models/base-filter';
import { environment } from '../../../environments/environment';

export class BaseService {

    protected BASE_URL = environment.apiUrl;
    protected IMAGE_BASE_URL = environment.baseImageUrl;

    constructor(
        protected http: HttpClient) { }

    protected getHttpParams(filter: BaseFilter) {
        let params: HttpParams = new HttpParams();
        params = params.append('page', `${filter.page}`);
        params = params.append('perPage', `${filter.perPage}`);

        return params;
    }
}