import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { CardDto } from 'src/Dtos/CardDto';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class CardsService {

  constructor(private http: HttpClient) {}
  
  private _refreshNeeded$ = new Subject<void>();

  get refreshNeeded(){
    return this._refreshNeeded$;
  }

  getCards(): Observable<CardDto[]> {
    return this.http.get<CardDto[]>(environment.baseApiUrl + "cards");
  }

  addCard(card: CardDto){
    this.http
    .post<CardDto>(environment.baseApiUrl + 'cards', card)
    .subscribe(() => {
      this._refreshNeeded$.next();
    });
  }

}
