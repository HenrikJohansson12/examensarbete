import React from "react";
import AddIngredients from "./AddIngredients";

export default function AddRecipe() {
  
  
  return (
    
    <div className="flex items-row justify-center h-screen">
      <div>
        <input
          type="text"
          placeholder="Name"
          className="input input-bordered w-full max-w-xs"
        />
        <input
          type="text"
          placeholder="Time (m)"
          className="input input-bordered w-full max-w-xs"
        />
        <input
          type="text"
          placeholder="Portions"
          className="input input-bordered w-full max-w-xs"
        />
        <div className="rating">
          <div>
            <h2> Difficulty</h2>
            <input type="radio" name="rating-1" className="mask mask-star" />
            <input type="radio" name="rating-1" className="mask mask-star" />
            <input type="radio" name="rating-1" className="mask mask-star" />
            <input type="radio" name="rating-1" className="mask mask-star" />
            <input type="radio" name="rating-1" className="mask mask-star" />
          </div>
        </div>
        <AddIngredients/>
      </div>
     
      <div className="join grid grid-cols-2">
  <button className="join-item btn btn-outline">Previous page</button>
  <button className="join-item btn btn-outline">Next</button>
</div>
    </div>

  );
}
