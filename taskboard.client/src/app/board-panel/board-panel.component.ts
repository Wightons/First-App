import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, map, switchMap } from 'rxjs';
import { BoardDto } from 'src/Dtos/BoardDto';
import { CardListDto } from 'src/Dtos/CardListDto';
import { BoardsService } from 'src/services/boards.service';
import { ListsService } from 'src/services/lists.service';

@Component({
  selector: 'app-board-panel',
  templateUrl: './board-panel.component.html',
  styleUrls: ['./board-panel.component.css']
})
export class BoardPanelComponent {

  boards$: Observable<BoardDto[]> | null = null;
  lists$: Observable<CardListDto[]> | null = null;
  boardsWithLists$: Observable<{ board: BoardDto, lists: CardListDto[] }[]> | null = null;

  constructor(
    private boardsService: BoardsService,
    private listsService: ListsService,
    private fb: FormBuilder,
  ){}

  createBoard: FormGroup = this.fb.group({
    name: ['', Validators.required]
  });

  onSubmit(){
    if(this.createBoard.valid){
      const board: BoardDto = {
        id: 0,
        name: this.createBoard.value.name
      }
      this.boardsService.addBoard(board);
    }
  }

  private getLists(): void{
    this.lists$ = this.listsService.getLists();
  }

  private getBoards(): void{
    this.boards$ = this.boardsService.getBoards();
  }

  private getSorted(): void{
    this.boardsWithLists$ = this.boards$!.pipe(
      switchMap(boards =>
       this.lists$!.pipe(
         map(lists =>
            boards.map(board => ({
              board: board,
              lists: lists.filter(list => list.boardId === board.id)
           }))
         )
       )
     )
    );
  }

  ngOnInit(){
     this.boardsService.refreshNeeded
     .subscribe(()=>{
      this.getBoards();
      this.getSorted();
     });

     this.listsService.refreshNeeded
     .subscribe(()=>{
      this.getLists();
      this.getSorted();
     });

     this.getBoards();
     this.getLists();
     this.getSorted();
  }
}
