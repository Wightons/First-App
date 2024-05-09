import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { LogDto } from 'src/Dtos/LogDto';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class LogsService {

  constructor(private http: HttpClient) { }

  getLogs(): Observable<LogDto[]> {
    return this.http.get<LogDto[]>(environment.baseApiUrl + "logs");
  }
}
