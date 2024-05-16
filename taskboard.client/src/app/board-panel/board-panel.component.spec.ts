import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoardPanelComponent } from './board-panel.component';

describe('BoardPanelComponent', () => {
  let component: BoardPanelComponent;
  let fixture: ComponentFixture<BoardPanelComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BoardPanelComponent]
    });
    fixture = TestBed.createComponent(BoardPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
