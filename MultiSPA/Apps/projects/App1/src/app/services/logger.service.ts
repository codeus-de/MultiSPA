import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class LoggerService {

    constructor() { }

    public LogException(ex): void {
        try {
            console.log(ex);

            var text = 'Unknown Exception';
            if (ex.Message)
                text = ex.Message;

            alert(text);

        } catch (ex1) {
            console.log(ex1);
            alert(ex1);
        }
    }
}
