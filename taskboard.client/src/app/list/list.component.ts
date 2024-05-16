import { Component, Input } from '@angular/core';
import { CardDto } from 'src/Dtos/CardDto';
import { CardListDto } from 'src/Dtos/CardListDto';
import { Observable } from 'rxjs';
import { ListsService } from 'src/services/lists.service';
import { ModalService } from 'ngx-modal-ease';
import { ModalComponent } from '../modal/modal.component';
import { ModalType } from 'src/Types/ModalType';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent {
  @Input()
  list: CardListDto = new CardListDto;
  @Input()
  cards: CardDto[] = [];
  @Input()
  boardId: number | null = null;
  lists$: Observable<CardListDto[]> | null = null;

  showEditForm: boolean = false;

  constructor(
    private listsService: ListsService,
    private modalService: ModalService,
    private formBuilder: FormBuilder
  ){}
  
  openEditForm(event: Event) {
    this.showEditForm = !this.showEditForm;
    event.stopPropagation();
  }

  editForm: FormGroup = this.formBuilder.group({
    name: ['', Validators.required],
    boardId: ['', Validators.required],
  });

  submitEdit(event: Event){
    event.preventDefault();
    if(this.editForm.valid){
      const list: CardListDto = {
        id: this.list.id,
        name: this.editForm.value.name,
        boardId: this.boardId!
      }
      this.listsService.updateList(list.id,list);
    }
    this.showEditForm = false;
  }

  openModal() {
    this.modalService.open(ModalComponent, 
      {
        data: 
        {
          modalType: ModalType.Create,
          modalData: this.list.id
        },
        size:{
          width: "50%",
        },
      }).subscribe((data)=>{
        console.log("data from form", data);
      });
  }

  ngOnInit(){
    this.lists$ = this.listsService.getLists();
  }

  deleteHandler(){
    this.listsService.deleteList(this.list.id);
  }

}
