import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { CardListDto } from 'src/Dtos/CardListDto';

@Component({
  selector: 'modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css'],
})
export class ModalComponent{
  @Input() size? = 'md';
  @Input() title? = 'Modal title';
  @Input() data?: any;
  @Input() submitData?: any;
  

  @Output() closeEvent = new EventEmitter();
  @Output() submitEvent = new EventEmitter();

  constructor(private elementRef: ElementRef, private fb: FormBuilder) {}

  ngOnInit(){
    (<Observable<CardListDto[]>>this.data).subscribe(
      data => {
        console.log(data)
      }   
    )
  }

  close(): void {
    this.elementRef.nativeElement.remove();
    this.closeEvent.emit();
  }

  submit(): void {
    console.log("submitted", this.submitData);
    this.elementRef.nativeElement.remove();
    this.submitEvent.emit();
  }
}