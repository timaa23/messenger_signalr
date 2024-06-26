import {
  ConversationActionTypes,
  ConversationActions,
  IConversationState,
} from "./types";

const initialState: IConversationState = {
  conversations: [],
  // currentConversation: "",
};

export const ConversationReducer = (
  state = initialState,
  action: ConversationActions
): IConversationState => {
  switch (action.type) {
    case ConversationActionTypes.GET_USER_CONVERSATIONS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    default:
      return state;
  }
};
