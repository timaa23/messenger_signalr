import { Dispatch } from "react";
import {
  AuthActionTypes,
  AuthActions,
  ILoginCredentials,
  IRegisterCredentials,
  IUser,
} from "../types/auth";
import http from "../../http_common";
import ServiceResponse from "../../ServiceResponse";
import { jwtDecode } from "jwt-decode";
import setAuthToken from "../../helpers/setAuthToken";

export const Login =
  (user: ILoginCredentials) => async (dispatch: Dispatch<AuthActions>) => {
    try {
      const resp = await http.post<ServiceResponse<string>>("api/Auth/login", user);

      const { data } = resp;

      console.log("LOGIN ACTION", resp);

      setAuthUserByToken(data.payload, dispatch);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  };

export const Register =
  (user: IRegisterCredentials) => async (dispatch: Dispatch<AuthActions>) => {
    try {
      const resp = await http.post<ServiceResponse<string>>("api/Auth/register", user);

      const { data } = resp;

      dispatch({
        type: AuthActionTypes.REGISTER_USER,
      });

      setAuthUserByToken(data.payload, dispatch);
    } catch (error: any) {
      return Promise.reject(error.response);
    }
  };

export const Logout = () => async (dispatch: Dispatch<AuthActions>) => {
  try {
    dispatch({
      type: AuthActionTypes.LOGOUT_USER,
    });

    localStorage.removeItem("token");
  } catch (error: any) {
    return Promise.reject(error.response);
  }
};

export const setAuthUserByToken = (token: string, dispatch: Dispatch<AuthActions>) => {
  const decoded: any = jwtDecode(token);

  const timeStamp = new Date(decoded.exp * 1000); // множимо на 1000, оскільки JavaScript використовує мілісекунди

  if (!checkTimeStamp(timeStamp, dispatch)) return;

  const userInfo: IUser = { ...decoded };

  setAuthToken(token);

  dispatch({
    type: AuthActionTypes.LOGIN_USER,
    payload: {
      isAuth: true,
      user: userInfo,
    },
  });
};

const checkTimeStamp = (timeStamp: Date, dispatch: Dispatch<AuthActions>) => {
  if (timeStamp > new Date()) return true;

  dispatch({
    type: AuthActionTypes.LOGOUT_USER,
  });

  localStorage.removeItem("token");
  return false;
};
