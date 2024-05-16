import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { BoardDto } from 'src/Dtos/BoardDto';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class BoardsService {

  constructor(private http: HttpClient) { }

  private _refreshNeeded$ = new Subject<void>();

  get refreshNeeded(){
    return this._refreshNeeded$;
  }

  getBoards() : Observable<BoardDto[]>{
    return this.http.get<BoardDto[]>(environment.baseApiUrl + "boards");
  }

  getById(id: number): Observable<BoardDto>{
    return this.http.get<BoardDto>(environment.baseApiUrl + `boards/${id}`);
  }

  addBoard(board: BoardDto){
    this.http
    .post<BoardDto>(environment.baseApiUrl + 'boards', board)
    .subscribe(() => {
      this._refreshNeeded$.next();
    });
  }

  updateBoard(id: number, card: BoardDto){
    this.http
    .patch(environment.baseApiUrl + `boards/${id}`, card)
    .subscribe(() => {
      this._refreshNeeded$.next();
    });
  }

  deleteBoard(id: number){
    this.http
    .delete(environment.baseApiUrl + `boards/${id}`)
    .subscribe(() => {
      this._refreshNeeded$.next();
    });
  }
}
