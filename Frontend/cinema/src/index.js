import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import AuthenticationContextProvider from "./context/AuthenticationContextProvider";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <AuthenticationContextProvider>
      <App />
    </AuthenticationContextProvider>
  </React.StrictMode>
);