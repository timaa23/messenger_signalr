import { BrowserRouter } from "react-router-dom";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.scss";
import "./icons.scss";
import "./_fonts.scss";
import { Provider } from "react-redux";
import { store } from "./store/index.ts";
import { setAuthUserByToken } from "./store/auth/actions.ts";

if (localStorage.getItem("token")) {
  const { token } = localStorage;
  setAuthUserByToken(token, store.dispatch);
  console.log("TOKEN", token);
}

ReactDOM.createRoot(document.getElementById("root")!).render(
  <Provider store={store}>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </Provider>
);
