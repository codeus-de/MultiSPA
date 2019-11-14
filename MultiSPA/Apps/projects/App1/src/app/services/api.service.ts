import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { App1DemoData } from '../data/models/app1-demo-data';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    public GetApp1DemoData(): Promise<App1DemoData[]> {
        return this.http.get<App1DemoData[]>(this.baseUrl + 'api/App1DemoData').toPromise();
    }
}
