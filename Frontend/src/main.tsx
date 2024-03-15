import React from "react";
import ReactDOM from "react-dom/client";
import Root from "./routes/root.tsx";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import LandingPage from "./routes/components/LandingPage.tsx";
import SignupPage from "./routes/components/SignupPage.tsx";

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
    ],
  },
]);
ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
