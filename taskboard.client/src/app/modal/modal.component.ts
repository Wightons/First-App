import { Component, Input } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ModalService } from "ngx-modal-ease";
import { CardDto } from "src/Dtos/CardDto";
import { ModalType } from "src/Types/ModalType";
import { CardsService } from "src/services/cards.service";

@Component({
  selector: 'modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css'],
})
export class ModalComponent{
  
  @Input() modalType: ModalType | null = null;
  @Input() modalData: any;
  @Input() lists: any;

  ModalType = ModalType;
  InitialName = "";

  cardForm: FormGroup | null = null; 

  constructor(
    private fb: FormBuilder, 
    private modalService: ModalService,
    private cardsService: CardsService,
  ){}

  ngOnInit(){
    switch (this.modalType) {
      case ModalType.Create:
        this.cardForm = this.fb.group({
          name: ['', [Validators.required, Validators.minLength(1)]],
          description: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(1000)]],
          dueDate: [new Date, Validators.required],
          priority: [1, [Validators.min(1), Validators.max(3)]],
        });
        console.log("cf", this.cardForm);
        
        break;
      case ModalType.Edit:
        const typedModalData: CardDto = <CardDto>this.modalData; 
        this.InitialName = typedModalData.name;
        this.cardForm = this.fb.group({
          id: [typedModalData.id, Validators.required],
          name: [typedModalData.name, [Validators.required, Validators.minLength(1)]],
          listId: [typedModalData.listId, Validators.required],
          description: [typedModalData.description, [Validators.required, Validators.minLength(1), Validators.maxLength(1000)]],
          dueDate: [typedModalData.dueDate, Validators.required],
          priority: [typedModalData.priority, [Validators.min(1), Validators.max(3)]],
        });
        break;

      default:
        break;
    }
  }

  onSubmit(){
    this.CreateSubmit();
    this.UpdateSubmit();
  }

  onClose(){
    this.modalService.close();
  }

  private CreateSubmit(): void{
    if(
      this.modalType === Number(ModalType.Create) &&
      this.cardForm && 
      this.cardForm.valid
    ){
      const card: CardDto = {
        id: 0,
        name: this.cardForm.value.name!,
        description: this.cardForm.value.description!,
        dueDate: this.cardForm.value.dueDate!,
        priority: this.cardForm.value.priority!,
        listId: this.modalData
      };
      this.cardsService.addCard(card);
      this.modalService.close(this.cardForm.value);
    }
  }

  private UpdateSubmit(): void{
    if(
      this.modalType === Number(ModalType.Edit) &&
      this.cardForm && 
      this.cardForm.valid
    ){
      const card: CardDto = {
        id: this.cardForm.value.id,
        name: this.cardForm.value.name!,
        description: this.cardForm.value.description!,
        dueDate: this.cardForm.value.dueDate!,
        priority: this.cardForm.value.priority!,
        listId: this.cardForm.value.listId
      };
      this.cardsService.updateCard(card.id, card);
      this.modalService.close();
    }
  }

}