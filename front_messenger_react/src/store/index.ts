import { Tuple, configureStore, createSelector } from "@reduxjs/toolkit";
import { combineReducers } from "redux";
import { MessageReducer } from "./reducers/messages";
import { ConversationReducer } from "./reducers/conversations";
import { AuthReducer } from "./reducers/auth";
import { thunk } from "redux-thunk";

//Reducers
export const rootReducer = combineReducers({
  message: MessageReducer,
  auth: AuthReducer,
  conversation: ConversationReducer,
});

export const store = configureStore({
  reducer: rootReducer,
  devTools: true,
  middleware: () => new Tuple(thunk),
  // middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(thunk),
});

//Root state
export type rootState = ReturnType<typeof rootReducer>;

//Create typed selector
export const createTypedSelector = createSelector.withTypes<rootState>();
