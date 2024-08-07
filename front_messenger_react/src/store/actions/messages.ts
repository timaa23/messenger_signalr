import { Dispatch } from "react";
import {
  IMessageItem,
  IMessageSendItem,
  MessageActionTypes,
  MessageActions,
} from "../types/messages";
import ServiceResponse from "../../ServiceResponse";
import http from "../../http_common";
import chatServiceConnector from "../../helpers/chatServiceConnector";

export const FetchMessages =
  (conversationId: number) => async (dispatch: Dispatch<MessageActions>) => {
    try {
      dispatch({
        type: MessageActionTypes.FETCH_MESSAGES_PENDING,
        payload: {
          conversationId: conversationId,
        },
      });

      const resp = await http.get<ServiceResponse<Array<IMessageItem>>>(
        `/api/Message/get/conversationId/${conversationId}`
      );

      const { data } = resp;

      dispatch({
        type: MessageActionTypes.FETCH_MESSAGES_SUCCESS,
        payload: {
          messages: data.payload,
        },
      });

      console.log("MESSAGES ACTION", data);

      return Promise.resolve(data);
    } catch (error: any) {
      dispatch({
        type: MessageActionTypes.FETCH_MESSAGES_FAILURE,
      });
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

      const message = GetTempMessage(model);

      dispatch({
        type: MessageActionTypes.SEND_MESSAGE_PENDING,
        payload: {
          message: message,
        },
      });

      var resp = await sendMessage(model);

      dispatch({
        type: MessageActionTypes.SEND_MESSAGE_SUCCESS,
        payload: {
          message: resp.payload,
          tempMessageGuid: message.guid,
        },
      });

      return Promise.resolve();
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  };

const GetTempMessage = (model: IMessageSendItem): IMessageItem => {
  // generate temp guid
  const tempGuid = Math.random().toString(36).slice(2, 9);
  const tempDate = new Date();

  var message: IMessageItem = {
    ...model,
    guid: tempGuid,
    dateTime: tempDate,
    isPending: true,
  };

  return message;
};

// function sleep(time: number) {
//   return new Promise((resolve) => {
//     setTimeout(resolve, time || 1000);
//   });
// }
// function getRandomArbitrary(min: number, max: number) {
//   return Math.random() * (max - min) + min;
// }
