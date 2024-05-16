import { createReducer, on } from '@ngrx/store';
import * as ListPanelActions from '../state/list-panel.state';
import { ListPanelState } from 'src/state/list-panel.state';

const initialState: ListPanelState = {
  board: null,
  cards: [],
  lists: [],
  loading: false,
};

const listPanelReducer = createReducer(
  initialState,
  on(ListPanelActions.loadBoard, (state) => ({ ...state, loading: true })),
  on(ListPanelActions.loadBoardSuccess, (state, { board }) => ({ ...state, board, loading: false })),
  on(ListPanelActions.loadBoardFail, (state, { error }) => ({ ...state, loading: false, error })),
  on(ListPanelActions.addList, (state, { list }) => ({ ...state, lists: [...state.lists, list] })),
);

export default listPanelReducer;
