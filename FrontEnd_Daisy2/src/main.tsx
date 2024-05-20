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
import { RecipeProvider } from './contexts/RecipeContext.tsx';
import { GoogleOAuthProvider } from '@react-oauth/google';
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
    <GoogleOAuthProvider clientId='485776855476-pqbc2gcckp64u0ugjjt9hflc0cgmedpn.apps.googleusercontent.com'> 
    <RecipeProvider>
    <RouterProvider router={router} />
    </RecipeProvider>
    </GoogleOAuthProvider>
  </React.StrictMode>
);
