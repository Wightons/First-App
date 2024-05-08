import { Component } from '@angular/core';
import { Observable, map, switchMap } from 'rxjs';
import { CardDto } from 'src/Dtos/CardDto';
import { CardListDto } from 'src/Dtos/CardListDto';
import { CardsService } from 'src/services/cards.service';
import { ListsService } from 'src/services/lists.service';

@Component({
  selector: 'app-list-panel',
  templateUrl: './list-panel.component.html',
  styleUrls: ['./list-panel.component.css']
})
export class ListPanelComponent {
  cards$: Observable<CardDto[]> | null = null;
  lists$: Observable<CardListDto[]> | null = null;
  listsWithCards$: Observable<{ list: CardListDto, cards: CardDto[] }[]> | null = null;

  constructor(
    private listsService: ListsService, 
    private cardsService: CardsService
  ) { }

  private getLists(): void{
    this.lists$ = this.listsService.getLists()
  }

  private getCards(): void{
    this.cards$ = this.cardsService.getCards()
  }

  private getSorted(): void{
    this.listsWithCards$ = this.lists$!.pipe(
      switchMap(lists =>
       this.cards$!.pipe(
         map(cards =>
            lists.map(list => ({
              list: list,
             cards: cards.filter(card => card.listId === list.id)
           }))
         )
       )
     )
    );
  }

  ngOnInit() {

    this.listsService.refreshNeeded
        .subscribe(() => {
          this.getLists();
          this.getSorted();
        });

    this.cardsService.refreshNeeded
        .subscribe(() => {
          this.getCards();
          this.getSorted();
        });

    this.getLists();
    this.getCards();
    this.getSorted();

    }
  
}