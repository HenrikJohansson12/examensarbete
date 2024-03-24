import React from "react";
import ReactDOM from "react-dom/client";
import Root from "./routes/root.tsx";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import LandingPage from "./routes/pages/LandingPage.tsx";
import SignupPage from "./routes/pages/SignupPage.tsx";
import LogInPage from "./routes/pages/LogInPage.tsx";


const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    //lägg till error element
    children: [
      {
        path: "/",
        element: <LandingPage />,
      },
      {
        path: "/signup",
        element: <SignupPage />
      },
      {
        path: "/login",
        element: <LogInPage />
      },
    ],
  },
]);
ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
