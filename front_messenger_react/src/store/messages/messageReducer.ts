import { IMessageState, MessageActionTypes, MessageActions } from "./types";

const initialState: IMessageState = {
  loading: false,
  messages: [],
};

export const MessageReducer = (
  state = initialState,
  action: MessageActions
): IMessageState => {
  switch (action.type) {
    case MessageActionTypes.SET_MESSAGE_PAGE: {
      return {
        ...state,
        ...action.payload,
      };
    }
    case MessageActionTypes.SEND_MESSAGE_HTTP: {
      return {
        ...state,
        messages: [action.payload.message, ...state.messages],
      };
    }
    default:
      return state;
  }
};
