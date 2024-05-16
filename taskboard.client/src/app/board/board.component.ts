import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { BoardDto } from 'src/Dtos/BoardDto';
import { CardListDto } from 'src/Dtos/CardListDto';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent {
  @Input() board: BoardDto | null = null;
  @Input() lists: CardListDto[] | null = null;

  constructor(private router: Router){}

  routeOnBoard(){
    this.router.navigate([`/boards/${Number(this.board?.id)}`]);
  }
}
