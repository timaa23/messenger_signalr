import { IMessageItem, IMessageState, MessageActionTypes, MessageActions } from "./types";

const initialState: IMessageState = {
  // loading: false,
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
    case MessageActionTypes.RECEIVE_MESSAGE: {
      const { messages } = state;
      const { message } = action.payload;

      setReceivedMessage(messages, message);

      return { ...state };
    }
    case MessageActionTypes.SEND_MESSAGE_PENDING: {
      return {
        ...state,
        messages: [action.payload.message, ...state.messages],
      };
    }
    case MessageActionTypes.SEND_MESSAGE_SUCCESS: {
      const { messages } = state;
      const { message, oldGuid } = action.payload;

      setReceivedMessage(messages, message);
      removePendingMessage(messages, oldGuid);

      return {
        ...state,
      };
    }
    default:
      return state;
  }
};

const setReceivedMessage = (messages: Array<IMessageItem>, message: IMessageItem) => {
  var lastSuccessId = messages.findIndex((m) => !m.isPending);
  if (lastSuccessId == -1) lastSuccessId = messages.length;
  console.log("LAST SUCCESS", { m: messages[lastSuccessId], id: lastSuccessId });

  // set message after last success
  messages.splice(lastSuccessId, 0, message);
};

const removePendingMessage = (messages: Array<IMessageItem>, messageGuid: string) => {
  var oldMessageId = messages.findIndex((m) => m.guid === messageGuid && m.isPending);
  if (oldMessageId == -1) return;

  // remove old pending
  messages.splice(oldMessageId, 1);
};
