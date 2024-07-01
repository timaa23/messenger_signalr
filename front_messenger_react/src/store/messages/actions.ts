import { Dispatch } from "react";
import {
  IMessageItem,
  IMessageSendItem,
  MessageActionTypes,
  MessageActions,
} from "./types";
import ServiceResponse from "../../ServiceResponse";
import http from "../../http_common";
import chatServiceConnector from "../../helpers/chatServiceConnector";

export const SetMessagePage =
  (conversationGuid: string) => async (dispatch: Dispatch<MessageActions>) => {
    try {
      const resp = await http.get<ServiceResponse<Array<IMessageItem>>>(
        `/api/Message/get/conversationGuid/${conversationGuid}`
      );

      const { data } = resp;

      dispatch({
        type: MessageActionTypes.SET_MESSAGE_PAGE,
        payload: {
          messages: data.payload,
          conversationGuid: conversationGuid,
          // loading: false,
        },
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

export const ReceiveMessage =
  (model: ServiceResponse<IMessageItem>) =>
  async (dispatch: Dispatch<MessageActions>) => {
    try {
      const { payload } = model;

      dispatch({
        type: MessageActionTypes.RECEIVE_MESSAGE,
        payload: {
          message: payload,
        },
      });

      return Promise.resolve(payload);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  };

export const SendMessage =
  (model: IMessageSendItem) => async (dispatch: Dispatch<MessageActions>) => {
    try {
      const { sendMessage } = chatServiceConnector();

      // generate temp guid
      const tempGuid = Math.random().toString(36).slice(2, 9);

      var message: IMessageItem = {
        ...model,
        guid: tempGuid,
        dateTime: new Date(),
        isPending: true,
      };

      dispatch({
        type: MessageActionTypes.SEND_MESSAGE_PENDING,
        payload: {
          message: message,
        },
      });

      await sendMessage(model);

      dispatch({
        type: MessageActionTypes.SEND_MESSAGE_SUCCESS,
        payload: {
          tempMessageGuid: tempGuid,
        },
      });

      return Promise.resolve();
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  };
