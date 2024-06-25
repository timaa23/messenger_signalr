import { Outlet, useNavigate } from "react-router-dom";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { useEffect } from "react";

const AuthLayout = () => {
  const { isAuth } = useTypedSelector((store) => store.auth);

  const navigator = useNavigate();

  useEffect(() => {
    if (isAuth) navigator("/");
  }, [isAuth]);

  return (
    <>
      <Outlet />
    </>
  );
};

export default AuthLayout;
