import { Component, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { CardDto, Priority } from 'src/Dtos/CardDto';
import { CardListDto } from 'src/Dtos/CardListDto';
import { Observable } from 'rxjs';
import { ListsService } from 'src/services/lists.service';
import { ModalService } from 'src/services/modal.service';
import { FormBuilder, Validators } from '@angular/forms';

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
  lists$: Observable<CardListDto[]> | null = null;

  constructor(
    private listsService: ListsService,
    private vcr: ViewContainerRef, 
    private modalService: ModalService,
    private fb: FormBuilder){}
  
  openModal(modalTemplate: TemplateRef<any>) {
    this.modalService
      .open(this.vcr, modalTemplate, { title: 'New Card', data: this.lists$})
      .subscribe((action) => {
        console.log('modalAction', action);
      });
  }

  createForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(1)]],
    description: ['', Validators.required],
    dueDate: [null],
    priority: [null, [Validators.required, Validators.min(1), Validators.max(3)]],
    listId: [null, Validators.required],
  });

  //ngx-modal-ease
  onSubmit(card: CardDto){
    this.listsService.addList(card);
  }

  ngOnInit(){
    this.lists$ = this.listsService.getLists();
    this.lists$.subscribe(lists => console.log(lists));
  }
}
