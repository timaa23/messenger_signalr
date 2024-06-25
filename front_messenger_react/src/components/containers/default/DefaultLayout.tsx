import { Outlet, useNavigate } from "react-router-dom";
import * as signalR from "@microsoft/signalr";
import { useChatService } from "../../../hooks/useChatService";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { useEffect } from "react";

export const connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5010/chat", { accessTokenFactory: () => localStorage.token })
  .withAutomaticReconnect()
  .configureLogging(signalR.LogLevel.Information)
  .build();

const DefaultLayout = () => {
  const { isAuth } = useTypedSelector((store) => store.auth);
  const navigator = useNavigate();
  const { start, disconnectSocket } = useChatService();

  useEffect(() => {
    if (!isAuth) {
      navigator("/auth");
      return;
    }

    // start();

    return () => {
      disconnectSocket();
    };
  }, [isAuth]);

  return (
    <>
      <Outlet />
    </>
  );
};

export default DefaultLayout;
