import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, map, switchMap } from 'rxjs';
import { BoardDto } from 'src/Dtos/BoardDto';
import { CardDto } from 'src/Dtos/CardDto';
import { CardListDto } from 'src/Dtos/CardListDto';
import { BoardsService } from 'src/services/boards.service';
import { CardsService } from 'src/services/cards.service';
import { ListsService } from 'src/services/lists.service';

@Component({
  selector: 'app-list-panel',
  templateUrl: './list-panel.component.html',
  styleUrls: ['./list-panel.component.css']
})
export class ListPanelComponent {
  board!: BoardDto;
  cards$: Observable<CardDto[]> | null = null;
  lists$: Observable<CardListDto[]> | null = null;
  listsWithCards$: Observable<{ list: CardListDto, cards: CardDto[] }[]> | null = null;

  listForm: FormGroup | null = null;
  isSidebarOpen = false;
  boardId: number | null = null;
  
  constructor(
    private listsService: ListsService, 
    private cardsService: CardsService,
    private boardService: BoardsService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
  ) { }
  
  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }
  
  private getLists(): void{
    this.lists$ = this.listsService.getByBoardId(this.boardId!);
    this.listsService.getByBoardId(this.boardId!).subscribe(
      lwc=> console.log(lwc)
    );
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

    this.route.params.subscribe(params=>{
      this.boardId = +params['id'];
      this.boardService.getById(this.boardId).subscribe(
        board => this.board = board
      );
    });

    this.listForm = this.fb.group({
      name: ['', Validators.required]
    });

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

    onSubmit(){
      if(this.listForm?.valid){
        
        const list: CardListDto ={
          id: 0,
          name: this.listForm?.value.name,
          boardId: this.boardId!
        } 
        console.log(list);
        this.listsService.addList(list);
      }
    }
}