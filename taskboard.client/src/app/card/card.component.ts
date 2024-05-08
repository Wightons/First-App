import { Component, Input} from '@angular/core';
import { CardDto, Priority } from 'src/Dtos/CardDto';
import { CardListDto } from 'src/Dtos/CardListDto';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent {
  @Input()
  card: CardDto = new CardDto();
  @Input()
  lists: CardListDto[] = [];

  priorName: string = "";

  ngOnInit(){
    this.priorName = Priority[this.card.priority];
  }
}
