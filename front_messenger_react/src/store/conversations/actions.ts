import { Dispatch } from "react";
import { ConversationActionTypes, ConversationActions, IConversationItem } from "./types";
import http from "../../http_common";
import ServiceResponse from "../../ServiceResponse";

export const GetConversations = () => async (dispatch: Dispatch<ConversationActions>) => {
  try {
    const resp = await http.get<ServiceResponse<Array<IConversationItem>>>(
      `/api/Conversation/get/user`
    );

    const { data } = resp;

    dispatch({
      type: ConversationActionTypes.GET_USER_CONVERSATIONS,
      payload: { conversations: data.payload },
    });

    console.log("CONVERSATION ACTION", data);

    return Promise.resolve(data);
  } catch (error: any) {
    return Promise.reject(error.response);
  }
};
