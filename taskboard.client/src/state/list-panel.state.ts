import { createAction, props } from "@ngrx/store";
import { BoardDto } from "src/Dtos/BoardDto";
import { CardDto } from "src/Dtos/CardDto";
import { CardListDto } from "src/Dtos/CardListDto";

export interface ListPanelState {
    board: BoardDto | null;
    cards: CardDto[];
    lists: CardListDto[];
    loading: boolean;
  }
  
  export const loadBoard = createAction('[List Panel] Load Board', props<{ boardId: number }>());
  export const loadBoardSuccess = createAction('[List Panel] Load Board Success', props<{ board: BoardDto }>());
  export const loadBoardFail = createAction('[List Panel] Load Board Fail', props<{ error: any }>());
  
  export const loadCards = createAction('[List Panel] Load Cards');
  export const loadCardsSuccess = createAction('[List Panel] Load Cards Success', props<{ cards: CardDto[] }>());
  export const loadCardsFail = createAction('[List Panel] Load Cards Fail', props<{ error: any }>());
  
  export const loadLists = createAction('[List Panel] Load Lists');
  export const loadListsSuccess = createAction('[List Panel] Load Lists Success', props<{ lists: CardListDto[] }>());
  export const loadListsFail = createAction('[List Panel] Load Lists Fail', props<{ error: any }>());
  
  export const addList = createAction('[List Panel] Add List', props<{ list: CardListDto }>());
  