import { Dispatch } from "react";
import {
  IMessageItem,
  IMessageSendItem,
  MessageActionTypes,
  MessageActions,
} from "./types";
import ServiceResponse from "../../ServiceResponse";
import http from "../../http_common";

export const SetMessagePage =
  (conversationGuid: string) => async (dispatch: Dispatch<MessageActions>) => {
    try {
      const resp = await http.get<ServiceResponse<Array<IMessageItem>>>(
        `/api/Message/get/conversationGuid/${conversationGuid}`
      );

      const { data } = resp;

      dispatch({
        type: MessageActionTypes.SET_MESSAGE_PAGE,
        payload: { messages: data.payload, loading: false },
      });

      console.log("MESSAGES ACTION", data);

      return Promise.resolve(data);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  };

export const SendMessageHTTP =
  (model: IMessageSendItem) => async (dispatch: Dispatch<MessageActions>) => {
    try {
      const resp = await http.post<ServiceResponse<IMessageItem>>(
        `/api/Message/send`,
        model
      );

      const { data } = resp;

      dispatch({
        type: MessageActionTypes.SEND_MESSAGE_HTTP,
        payload: {
          message: data.payload,
        },
      });

      console.log("SEND MESSAGES ACTION", data);

      return Promise.resolve(data);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  };

// IN PROGRESS
export const SendMessage =
  (model: IMessageSendItem) => async (dispatch: Dispatch<MessageActions>) => {
    try {
      const resp = await http.post<ServiceResponse<IMessageItem>>(
        `/api/Message/send`,
        model
      );

      const { data } = resp;

      dispatch({
        type: MessageActionTypes.SEND_MESSAGE_HTTP,
        payload: {
          message: data.payload,
        },
      });

      console.log("SEND MESSAGES ACTION", data);

      return Promise.resolve(data);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  };
