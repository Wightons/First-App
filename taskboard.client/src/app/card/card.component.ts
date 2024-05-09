import { Component, Input} from '@angular/core';
import { ModalService } from 'ngx-modal-ease';
import { CardDto, Priority } from 'src/Dtos/CardDto';
import { CardListDto } from 'src/Dtos/CardListDto';
import { CardsService } from 'src/services/cards.service';
import { ModalComponent } from '../modal/modal.component';
import { ModalType } from 'src/Types/ModalType';

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
  formattedDate: string = "";

  constructor(
    private cardsService: CardsService,
    private modalService: ModalService,
  ){}

  ngOnInit(){
    this.lists = this.lists.filter(list => list.id !== this.card.listId);
    this.priorName = Priority[this.card.priority];
    this.formattedDate = this.formatDate(this.card.dueDate.toString());
  }

  formatDate(dateStr: string): string {
    const dateObj = new Date(dateStr);
    const options: Intl.DateTimeFormatOptions = { weekday: 'short', day: 'numeric', month: 'short' };
    return dateObj.toLocaleDateString('en-US', options);
  }

  deleteHandler(){
    this.cardsService.deleteCard(this.card.id);
  }

  editHandler(){
    this.modalService.open(ModalComponent, 
      {
        data: 
        {
          modalData: this.card,
          modalType: ModalType.Edit,
          lists: this.lists
        },
        size:{
          width: "50%",
        }
      }).subscribe((data)=>{
        console.log("data from form", data);
      });
  }

  onListSelect(event: Event){
    const selectedId = (event.target as HTMLSelectElement).value;
    const card: CardDto = {
      id: this.card.id,
      name: this.card.name,
      description: this.card.description,
      dueDate: this.card.dueDate,
      priority: this.card.priority,
      listId: Number(selectedId)
    };
    this.cardsService.updateCard(card.id, card);
  }

}
