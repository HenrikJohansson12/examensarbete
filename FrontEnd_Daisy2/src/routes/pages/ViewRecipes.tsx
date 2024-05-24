import React from "react";
//import { productRecords } from "../../data/testData";
import { useNavigate } from "react-router-dom";

export default function ViewRecipes() {
    const navigate = useNavigate();
   const addRecipeButtonClicked = () =>{
        navigate("new");
    }
  return (/*
    <div className="flex flex-col flex-1 relative">
      <div className="flex flex-col items-center">
        <h1 className="text-center text-3xl mb-4">
          Veckans recept baserat på erbjudanden nära dig
        </h1>
      </div>
      <div className="grid grid-cols-2 gap-x-2 gap-y-10 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 xl:gap-x-8 mt-4">
        {productRecords.map((productRecord) => (
          <div key={productRecord.Id} className="card w-56 bg-base-100 shadow-xl">
            <div className="card-body items-center text-center">
              <h2 className="card-title">{productRecord.Name}</h2>
              <p>{productRecord.Description}</p>
              <p>Brand: {productRecord.Brand}</p>
              <p>Price: {productRecord.Price}</p>
              <p>Discounted Price: {productRecord.DiscountedPrice}</p>
              <div className="card-actions justify-center w-full">
                <button className="btn btn-primary btn-sm">
                  Skapa shoppinglista
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    strokeWidth={1.5}
                    stroke="currentColor"
                    className="w-6 h-6 ml-2"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="M15.75 10.5V6a3.75 3.75 0 1 0-7.5 0v4.5m11.356-1.993 1.263 12c.07.665-.45 1.243-1.119 1.243H4.25a1.125 1.125 0 0 1-1.12-1.243l1.264-12A1.125 1.125 0 0 1 5.513 7.5h12.974c.576 0 1.059.435 1.119 1.007ZM8.625 10.5a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm7.5 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Z"
                    />
                  </svg>
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
      <button onClick={addRecipeButtonClicked} className="fixed bottom-16 right-4 btn btn-circle btn-lg">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          strokeWidth={1.5}
          stroke="currentColor"
          className="w-6 h-6"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            d="M12 4.5v15m7.5-7.5h-15"
          />
        </svg>
      </button>
    </div>
    */
 <div>Apa</div> );
}
