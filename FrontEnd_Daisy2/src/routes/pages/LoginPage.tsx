import React, { useState } from "react";
import { NavLink, useNavigate } from "react-router-dom";
import logo from "../../assets/studentmatlogo.webp";
import { loginWithEmailAndPassword } from "../../api/login";
import { useDispatch } from "react-redux";
import { setAspNetToken } from "../../redux/authSlice";
import SignInWithGoogleComponent from "../../components/SignInWithGoogleComponent";

export default function LoginPage() {
  const [emailAdress, setEmailAdress] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [validationError, setValidationError] = useState<string | null>(null);
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const registerButtonClicked = () => {
    navigate('/offers');
  };

  const signInWithUserNameAndPassword = async () => {
    const response = await loginWithEmailAndPassword(emailAdress, password);
    if (response === undefined) {
      setValidationError("Inloggning misslyckades");
    } else {
      dispatch(setAspNetToken(response.accessToken));
      setValidationError(null);
      navigate('/offers');
    }
  };

  return (
    <div className="flex items-center justify-center h-screen">
      <div className="min-w-fit flex-col border bg-white px-6 py-14 shadow-md rounded-[4px] ">
        <div className="mb-8 flex justify-center">
          <img className="w-24" src={logo} alt="Studentmat logo" />
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
            <input
              type="text"
              value={emailAdress}
              onChange={(e) => setEmailAdress(e.target.value)}
              className="grow"
              placeholder="Emailadress"
            />
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
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="grow"
              placeholder="Lösenord"
            />
          </label>
        </div>
        <button
          className="btn btn-primary mt-5 w-full border p-2 bg-gradient-to-r text-white rounded-[4px]"
          type="submit"
          onClick={signInWithUserNameAndPassword}
        >
          Logga in
        </button>
        <div className="mt-5 flex justify-between text-gray-600">
          <p className="align-middle">Inte medlem? </p>
          <button className="btn btn-secondary" onClick={registerButtonClicked}>
            Registrera dig
          </button>
        </div>
        <div className="flex justify-center mt-5 text-sm">
          <p className="text-gray-400">Eller</p>
        </div>
        <div className="place-self-center">
          <SignInWithGoogleComponent />
        </div>
        <div className="mt-5 flex text-center text-sm text-gray-400">
          
        </div>
        {validationError && (
          <div className="mt-4">
            <div role="alert" className="alert alert-warning">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="stroke-current shrink-0 h-6 w-6"
                fill="none"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z"
                />
              </svg>
              <span>{validationError}</span>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
