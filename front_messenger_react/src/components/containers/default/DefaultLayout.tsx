import { Navigate, Outlet } from "react-router-dom";
import { useTypedSelector } from "../../../hooks/useTypedSelector";

const DefaultLayout = () => {
  const { isAuth } = useTypedSelector((store) => store.auth);

  if (!isAuth) return <Navigate to="/auth" replace />;

  return <Outlet />;
};

export default DefaultLayout;
