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
    .subscribe(() => {
      this._refreshNeeded$.next();
    });
  }

  deleteList(id: number){
    this.http
    .delete(environment.baseApiUrl + `lists/${id}`)
    .subscribe(() => {
      this._refreshNeeded$.next();
    });
  }

  updateList(id: number, list: CardListDto){
    this.http
    .patch(environment.baseApiUrl + `lists/${id}`, list)
    .subscribe(() => {
      this._refreshNeeded$.next();
    });
  }

}
