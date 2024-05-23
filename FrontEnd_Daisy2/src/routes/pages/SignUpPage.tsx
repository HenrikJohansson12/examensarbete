import { useState } from "react";
import SignInWithGoogleComponent from "../../components/SignInWithGoogleComponent";
import { loginWithEmailAndPassword, registerWithEmailAndPassword } from "../../api/login";
import { useDispatch } from "react-redux";
import { setAspNetToken } from "../../redux/authSlice";

export default function SignUpPage() {
  const dispatch = useDispatch();
  const [emailAdress, setEmailAdress] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [verifyPassword, setVerifyPassword] = useState<string>('');
  const [validationError, setValidationError] = useState<string | null>(null);

  const registerButtonClicked = async () => {
    if (password !== verifyPassword) {
      setValidationError('Lösenorden matchar inte');
    }
    else{
      setValidationError(null);

     if (await registerWithEmailAndPassword(emailAdress, password)){
      const response = await loginWithEmailAndPassword(emailAdress, password);
          if (response != undefined) {
            dispatch(setAspNetToken(response.accessToken));
            console.log(response)
          }
     }
    }
   
  };

  return (
    <div className="py-6">
      <div className="flex bg-white rounded-lg shadow-lg overflow-hidden mx-auto max-w-sm lg:max-w-4xl">
        <div className="hidden lg:block lg:w-1/2 bg-cover"></div>
        <div className="w-full p-8 lg:w-1/2">
          <h2 className="text-2xl font-semibold text-gray-700 text-center">
            Studentmat
          </h2>
          <p className="text-xl text-gray-600 text-center">Registrera ny användare</p>

          <div className="mt-4">
            <label className="block text-gray-700 text-sm font-bold mb-2 text-left">
              Epostadress
            </label>
            <input
              value={emailAdress}
              onChange={(e) => setEmailAdress(e.target.value)}
              className="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none"
              type="email"
            />
          </div>
          <div className="mt-4">
            <div className="flex justify-between">
              <label className="block text-gray-700 text-sm font-bold mb-2">
                Lösenord
              </label>
            </div>
            <input
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none"
              type="password"
            />
          </div>
          <div className="mt-4">
            <div className="flex justify-between">
              <label className="block text-gray-700 text-sm font-bold mb-2">
                Verifiera password
              </label>
            </div>
            <input
              className="bg-gray-200 text-gray-700 focus:outline-none focus:shadow-outline border border-gray-300 rounded py-2 px-4 block w-full appearance-none"
              type="password"
              value={verifyPassword}
              onChange={(e) => setVerifyPassword(e.target.value)}
            />
          </div>
          <div className="mt-8">
            <button
              onClick={registerButtonClicked}
              className="bg-gray-700 text-white font-bold py-2 px-4 w-full rounded hover:bg-gray-600"
            >
              Registrera
            </button>
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
          <div className="mt-4 flex items-center justify-between">
            <span className="border-b w-1/5 md:w-1/4"></span>
            <a href="#" className="text-xs text-gray-500 uppercase">
              ELLER
            </a>
            <span className="border-b w-1/5 md:w-1/4"></span>
          </div>
          <SignInWithGoogleComponent />
        </div>
      </div>
    </div>
  );
}
