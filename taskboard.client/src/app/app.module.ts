import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CardComponent } from './card/card.component';
import { ListPanelComponent } from './list-panel/list-panel.component';
import { ListComponent } from './list/list.component';
import { HttpClientModule } from '@angular/common/http';
import { ModalComponent } from './modal/modal.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ModalService } from 'src/services/modal.service';

@NgModule({
  declarations: [
    AppComponent,
    CardComponent,
    ListPanelComponent,
    ListComponent,
    ModalComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [ModalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
