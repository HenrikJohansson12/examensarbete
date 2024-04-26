import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import { createBrowserRouter,RouterProvider } from 'react-router-dom';
import Root from './routes/Root.tsx';
import LandingPage from './routes/pages/LandingPage.tsx';
import SignUpPage from './routes/pages/SignUpPage.tsx';
import LoginPage from './routes/pages/LoginPage.tsx';
import OfferPage from './routes/pages/Offers.tsx';
import RecipePage from './routes/pages/RecipePage.tsx';

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
        element: <LoginPage />
      },
      {
        path: "/offers",
        element: <OfferPage />
      },
      {
        path: "/recipes",
        element: <RecipePage />
      },
    ],
  },
]);
ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
