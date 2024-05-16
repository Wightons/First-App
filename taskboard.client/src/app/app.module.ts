import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CardComponent } from './card/card.component';
import { ListPanelComponent } from './list-panel/list-panel.component';

import { HttpClientModule } from '@angular/common/http';
import { ModalComponent } from './modal/modal.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ListComponent } from './list/list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RouterModule, Routes } from '@angular/router';
import { BoardComponent } from './board/board.component';
import { BoardPanelComponent } from './board-panel/board-panel.component';
import { StoreModule } from '@ngrx/store';

const routes: Routes = [
  { path: "boards", component: BoardPanelComponent },
  { path: "", pathMatch: "full", redirectTo: "/boards" },
  { path: "boards/:id", component: ListPanelComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    CardComponent,
    ListPanelComponent,
    ListComponent,
    ModalComponent,
    SidebarComponent,
    BoardComponent,
    BoardPanelComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    StoreModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
