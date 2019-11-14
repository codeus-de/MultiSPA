import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { App1DemoData } from '../../data/models/app1-demo-data';
import { LoggerService } from '../../services/logger.service';

@Component({
    selector: 'app-page1',
    templateUrl: './page1.component.html',
    styleUrls: ['./page1.component.css']
})
export class Page1Component implements OnInit {

    constructor(private api: ApiService, private logger: LoggerService) { }

    public data: App1DemoData[];

    ngOnInit() {
        this.LoadData();
    }

    private async LoadData() {
        try {
            this.data = await this.api.GetApp1DemoData();
        } catch (e) {
            this.logger.LogException(e);
        }
    }
}
