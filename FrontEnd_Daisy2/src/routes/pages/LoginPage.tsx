import React from "react";
import { NavLink, useNavigate } from "react-router-dom";
import logo from "../../assets/studentmatlogo.webp";
import { GoogleLogin } from "@react-oauth/google";

import { useGoogleLogin } from '@react-oauth/google';

export default function LoginPage() {
  const navigate = useNavigate();
const registerButtonClicked = () => {
navigate('/signup')
}
  
  return (
    <div className="flex items-center justify-center h-screen">
      <div className="min-w-fit flex-col border bg-white px-6 py-14 shadow-md rounded-[4px] ">
        <div className="mb-8 flex justify-center">
          <img className="w-24" src={logo} alt="" />
        </div>
        <div className="flex flex-col text-sm rounded-md">
          <label className="input input-bordered flex items-center gap-2 mb-4">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 16 16"
              fill="currentColor"
              className="w-4 h-4 opacity-70"
            >
              <path d="M2.5 3A1.5 1.5 0 0 0 1 4.5v.793c.026.009.051.02.076.032L7.674 8.51c.206.1.446.1.652 0l6.598-3.185A.755.755 0 0 1 15 5.293V4.5A1.5 1.5 0 0 0 13.5 3h-11Z" />
              <path d="M15 6.954 8.978 9.86a2.25 2.25 0 0 1-1.956 0L1 6.954V11.5A1.5 1.5 0 0 0 2.5 13h11a1.5 1.5 0 0 0 1.5-1.5V6.954Z" />
            </svg>
            <input type="text" className="grow" placeholder="Emailadress" />
          </label>
          <label className="input input-bordered flex items-center gap-2">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 16 16"
              fill="currentColor"
              className="w-4 h-4 opacity-70"
            >
              <path
                fillRule="evenodd"
                d="M14 6a4 4 0 0 1-4.899 3.899l-1.955 1.955a.5.5 0 0 1-.353.146H5v1.5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1-.5-.5v-2.293a.5.5 0 0 1 .146-.353l3.955-3.955A4 4 0 1 1 14 6Zm-4-2a.75.75 0 0 0 0 1.5.5.5 0 0 1 .5.5.75.75 0 0 0 1.5 0 2 2 0 0 0-2-2Z"
                clipRule="evenodd"
              />
            </svg>
            <input type="password" className="grow" value="password" />
          </label>
        </div>
        <button
          className="btn btn-primary mt-5 w-full border p-2 bg-gradient-to-r text-white rounded-[4px]"
          type="submit"
        >
          Logga in
        </button>
        <div className="mt-5 flex justify-between  text-gray-600">
          <p className="align-middle">Inte medlem? </p>
          <button className="btn btn-secondary" onClick={registerButtonClicked}> Registrera dig </button>
        </div>
        <div className="flex justify-center mt-5 text-sm">
          <p className="text-gray-400">Eller</p>
        </div>
        <div className="place-self-center">
        <GoogleLogin
    text="signup_with"
    logo_alignment="left"

  onSuccess={credentialResponse => {
    console.log(credentialResponse);
  }}
  onError={() => {
    console.log('Login Failed');
  }}
/>
          </div>
        <div className="mt-5 flex text-center text-sm text-gray-400">
          <p>
            This site is protected by reCAPTCHA and the Google <br />
            <NavLink to="/signup" className="underline">
              Privacy Policy
            </NavLink>{" "}
            and{" "}
            <NavLink className="underline" to="/tos">
              Terms of Service
            </NavLink>{" "}
            apply.
          </p>
        </div>
      </div>
    </div>
  );
}
