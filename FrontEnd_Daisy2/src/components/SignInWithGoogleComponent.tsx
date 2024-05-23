import React from "react";
import { useDispatch } from "react-redux";
import { GoogleLogin } from "@react-oauth/google";
import { fetchBearerTokenWithGoogle } from "../api/login";
import { setAspNetToken } from '../redux/authSlice';
import { useNavigate } from "react-router-dom";

export default function SignInWithGoogleComponent (){
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const signUpWithGoogle = async (credential: string|undefined) => {
   
        if (credential === undefined) {
          console.log("Error")
        }
        else{
          
      const response = await fetchBearerTokenWithGoogle(credential);
          if (response != undefined) {
            dispatch(setAspNetToken(response));
            navigate('/offers')
          }
      }
      };
    
return(<div>
         <GoogleLogin
            onSuccess={(credentialResponse) => {
              signUpWithGoogle(credentialResponse.credential)
            }}
            onError={() => {
              console.log("Login Failed");
            }}
          />
</div>)
 
} 