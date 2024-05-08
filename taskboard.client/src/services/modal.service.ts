import { DOCUMENT } from '@angular/common';
import {
  Inject,
  Injectable,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { Subject, of } from 'rxjs';
import { ModalComponent } from 'src/app/modal/modal.component';

@Injectable({
  providedIn: 'root',
})
export class ModalService {
  private modalNotifier?: Subject<string>;
  constructor(
    @Inject(DOCUMENT) private document: Document
  ) {}

  open(
    vcr: ViewContainerRef,
    content: TemplateRef<any>,
    options?: { size?: string; title?: string; data?: any; submitData?: any }
  ) {
    const contentViewRef = vcr.createEmbeddedView(
      content,
      { lists: options!.data }
    );
    const modalComponent = vcr.createComponent(ModalComponent, {
      projectableNodes: [contentViewRef.rootNodes],
    });
    modalComponent.setInput('size', options?.size);
    modalComponent.setInput('title', options?.title);
    modalComponent.setInput('data', options?.data);
    modalComponent.setInput('submitData', options?.submitData)
    modalComponent.instance.closeEvent.subscribe(() => this.closeModal());
    modalComponent.instance.submitEvent.subscribe(() => this.submitModal());

    modalComponent.hostView.detectChanges();

    this.document.body.appendChild(modalComponent.location.nativeElement);
    this.modalNotifier = new Subject();
    return this.modalNotifier?.asObservable();
  }

  closeModal() {
    this.modalNotifier?.complete();
  }

  submitModal() {
    this.modalNotifier?.next('confirm');
    this.closeModal();
  }
}