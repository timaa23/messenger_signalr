import { Route, Routes } from "react-router-dom";
import ChatPage from "./pages/Chat/ChatPage";
import DefaultLayout from "./components/containers/default";
import AuthLayout from "./components/containers/auth";
import AuthPage from "./pages/Auth/AuthPage";

function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<DefaultLayout />}>
          <Route path=":guid?" element={<ChatPage />} />
        </Route>
        <Route path="/auth" element={<AuthLayout />}>
          <Route index element={<AuthPage />} />
        </Route>
      </Routes>
    </>
  );
}

export default App;
