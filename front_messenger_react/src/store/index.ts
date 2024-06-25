import { configureStore } from "@reduxjs/toolkit";
import { combineReducers } from "redux";
import { MessageReducer } from "./messages/messageReducer";
import { AuthReducer } from "./auth/authReducer";
// import thunk, { ThunkMiddleware } from "redux-thunk";
//Reducers

export const rootReducer = combineReducers({
  message: MessageReducer,
  auth: AuthReducer,
});

export const store = configureStore({
  reducer: rootReducer,
  devTools: true,
});
