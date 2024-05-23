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
import { convertToUnMappedOffers, offerData, UnMappedOffer } from './data/MapOfferDTO.ts';
import MappedOfferList from './routes/pages/MapOffers.tsx';
import CreateRecipe from './routes/pages/CreateRecipe.tsx';
import TestIngredient from './components/TestIngredient.tsx';
import Recipe from './routes/pages/Recipe.tsx';
import ViewRecipes from './routes/pages/ViewRecipes.tsx';
import { Provider } from 'react-redux';
import store from './redux/store.ts';
import MyProfile from './routes/pages/MyProfile.tsx';


const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    // Lägg till error element om det behövs
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
        path: "/profile",
        element: <MyProfile />
      },
      {
        path: "/offers",
        element: <OfferPage />
      },
      {
        path: "/mapoffers",
        element: <MappedOfferList />
      },
      {
        path: "/recipes",
        element: <Recipe />,
        children: [
          {
            path: "",
            element: <ViewRecipes />
          },
          {
            path: "new",
            element: <CreateRecipe />
          }
        ]
      },
    ],
  },
]);
ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <GoogleOAuthProvider clientId='485776855476-pqbc2gcckp64u0ugjjt9hflc0cgmedpn.apps.googleusercontent.com'> 
    <Provider store={store}>
    <RecipeProvider>
    <RouterProvider router={router} />
    </RecipeProvider>
    </Provider>
    </GoogleOAuthProvider>
  </React.StrictMode>
);
