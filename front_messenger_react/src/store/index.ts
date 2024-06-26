import { configureStore } from "@reduxjs/toolkit";
import { combineReducers } from "redux";
import { MessageReducer } from "./messages/messageReducer";
import { AuthReducer } from "./auth/authReducer";
import { ConversationReducer } from "./conversations/conversationReducer";
// import thunk from "redux-thunk";
//Reducers

export const rootReducer = combineReducers({
  message: MessageReducer,
  auth: AuthReducer,
  conversation: ConversationReducer,
});

export const store = configureStore({
  reducer: rootReducer,
  devTools: true,
});
