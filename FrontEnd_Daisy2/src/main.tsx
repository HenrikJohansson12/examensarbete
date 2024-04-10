import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import { createBrowserRouter,RouterProvider } from 'react-router-dom';
import Root from './routes/Root.tsx';
import LandingPage from './routes/pages/LandingPage.tsx';
import SignUpPage from './routes/pages/SignUpPage.tsx';

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
        element: <SignUpPage />
      },
      {
        path: "/login",
        element: <LandingPage />
      },
    ],
  },
]);
ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
