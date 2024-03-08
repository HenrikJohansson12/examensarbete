import React from 'react'
import ReactDOM from 'react-dom/client';
import Root from './routes/root.tsx';
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import "./index.css";
import './index.css'


const router = createBrowserRouter([
  {
    path: "/",
    element: <Root/>,
  },
]);
ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
   <RouterProvider router={router} />
  </React.StrictMode>,
)
