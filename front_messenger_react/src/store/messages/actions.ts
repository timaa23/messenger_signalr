import { Dispatch } from "react";
import { IReceiveMessageType, MessageActionTypes, MessageActions } from "./types";

export const SetLastMessage =
  (message: IReceiveMessageType) => async (dispatch: Dispatch<MessageActions>) => {
    dispatch({
      type: MessageActionTypes.SET_LAST_MESSAGE,
      payload: { last_message: message },
    });
  };
