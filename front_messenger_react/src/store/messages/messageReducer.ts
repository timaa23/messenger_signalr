import { IReceiveMessageState, MessageActionTypes, MessageActions } from "./types";

const initialState: IReceiveMessageState = {
  last_message: {
    message: "",
    user_name: "",
    date_time: "",
  },
};

export const MessageReducer = (
  state = initialState,
  action: MessageActions
): IReceiveMessageState => {
  switch (action.type) {
    case MessageActionTypes.SET_LAST_MESSAGE: {
      return {
        ...state,
        ...action.payload,
      };
    }
    default:
      return state;
  }
};
