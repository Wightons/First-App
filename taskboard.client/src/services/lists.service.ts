import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { CardListDto } from 'src/Dtos/CardListDto';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class ListsService {

  constructor(private http: HttpClient) { }

  private _refreshNeeded$ = new Subject<void>();

  get refreshNeeded(){
    return this._refreshNeeded$;
  }

  getLists(): Observable<CardListDto[]> {
    return this.http.get<CardListDto[]>(environment.baseApiUrl + "lists");
  }

  addList(list: CardListDto){
    this.http
    .post<CardListDto>(environment.baseApiUrl + 'lists', list)
    .pipe(
      tap(()=>{
        this._refreshNeeded$.next();
      })
    );
  }
}
