import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { loadBoard, loadCards, loadLists, addList } from '../../state/list-panel.state';
import { ListPanelState } from '../../state/list-panel.state';
import { CardListDto } from 'src/Dtos/CardListDto';
import { BoardDto } from 'src/Dtos/BoardDto';
import { Observable } from 'rxjs';
import { CardDto } from 'src/Dtos/CardDto';

@Component({
  selector: 'app-list-panel',
  templateUrl: './list-panel.component.html',
  styleUrls: ['./list-panel.component.css'],
})
export class ListPanelComponent implements OnInit {
  board!: BoardDto;
  board$ = this.store.pipe(select((state: ListPanelState) => state.board));
  cards$ = this.store.pipe(select((state: ListPanelState) => state.cards));
  lists$ = this.store.pipe(select((state: ListPanelState) => state.lists));
  loading$ = this.store.pipe(select((state: ListPanelState) => state.loading));

  listsWithCards$: Observable<{ list: CardListDto, cards: CardDto[] }[]> | null = null;

  boardId: number| null = null;
  listForm: FormGroup | null = null;
  isSidebarOpen: boolean = false;

  constructor(private store: Store<ListPanelState>, private fb: FormBuilder, private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.params.subscribe((params) => {
      const boardId = +params['id'];
      this.store.dispatch(loadBoard({ boardId }));
    });
    this.store.dispatch(loadCards());
    this.store.dispatch(loadLists());
  }

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  onSubmit() {
    if (this.listForm?.valid) {
      const list: CardListDto = {
        id: 0,
        name: this.listForm.value.name,
        boardId: this.boardId!,
      };
      this.store.dispatch(addList({ list }));
    }
  }
}
