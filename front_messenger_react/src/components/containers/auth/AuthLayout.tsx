import { Navigate, Outlet } from "react-router-dom";
import { useTypedSelector } from "../../../hooks/useTypedSelector";

const AuthLayout = () => {
  const { isAuth } = useTypedSelector((store) => store.auth);

  if (isAuth) return <Navigate to="/" replace />;

  return <Outlet />;
};

export default AuthLayout;
