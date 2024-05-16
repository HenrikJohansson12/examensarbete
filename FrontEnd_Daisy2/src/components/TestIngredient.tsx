import React, { useContext } from "react";
import AddIngredients from "./AddIngredients";
import { RecipeContext } from "../contexts/RecipeContext";
import IngredientsList from "./IngredientsList";
import RecipeCard from "./RecipeCard";

export default function TestIngredient() {
  const context = useContext(RecipeContext);

  if (!context) {
    throw new Error("RecipeComponent must be used within a RecipeProvider");
  }

  const { recipe, updateName, updateMinutes, updatePortions, updateDifficulty, updateInstructions } = context;

  const handleSave = async () => {
    try {
      console.log(recipe);
      const response = await fetch('http://localhost:5290/api/saverecipe', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(recipe)
      });
      if (response.ok) {
        alert("Recipe saved successfully!");
      } else {
        alert("Failed to save recipe.");
      }
    } catch (error) {
      console.error("Error saving recipe:", error);
      alert("An error occurred while saving the recipe.");
    }
  };

  return (
    <div className="flex flex-col justify-evenly p-4">
      <div className="flex flex-col items-center space-y-5 mb-5">
        <input
          type="text"
          placeholder="Name"
          value={recipe.Name}
          onChange={(e) => updateName(e.target.value)}
          className="input input-bordered w-full max-w-xs"
        />
        <input
          type="number"
          placeholder="Time (m)"
          value={recipe.Minutes}
          onChange={(e) => updateMinutes(Number(e.target.value))}
          className="input input-bordered w-full max-w-xs"
        />
        <input
          type="number"
          placeholder="Portions"
          value={recipe.NrOfPortions}
          onChange={(e) => updatePortions(Number(e.target.value))}
          className="input input-bordered w-full max-w-xs"
        />
        <div className="rating flex flex-col items-center justify-center">
          <h2 className="mb-2">Difficulty</h2>
          <div className="flex">
            {[1, 2, 3, 4, 5].map((rating) => (
              <input
                key={rating}
                type="radio"
                name="rating-1"
                className="mask mask-star"
                checked={recipe.Difficulty === rating}
                onChange={() => updateDifficulty(rating)}
              />
            ))}
          </div>
        </div>
      </div>

      <div className="flex flex-col items-center space-y-4">
        <button
          className="btn btn-primary w-full max-w-xs"
          onClick={() => document.getElementById("my_modal_2").showModal()}
        >
          Lägg till ingredienser
        </button>
        <button
          className="btn btn-primary w-full max-w-xs"
          onClick={() => document.getElementById("my_modal_3").showModal()}
        >
          Lägg till instruktioner
        </button>

        <dialog id="my_modal_2" className="modal">
          <div className="modal-box">
            <AddIngredients />
          </div>
          <form method="dialog" className="modal-backdrop">
            <button>close</button>
          </form>
        </dialog>

        <dialog id="my_modal_3" className="modal">
          <div className="modal-box flex flex-col justify-center items-center">
            <textarea
              value={recipe.Instructions}
              onChange={(e) => updateInstructions(e.target.value)}
              className="textarea textarea-bordered w-full h-64"
              placeholder="Instruktioner"
            ></textarea>
          </div>
          <form method="dialog" className="modal-backdrop">
            <button>close</button>
          </form>
        </dialog>

        <dialog id="my_modal_4" className="modal">
          <div className="modal-box flex flex-col justify-center items-center">
           <RecipeCard recipe={recipe}/>
           <button
          className="btn btn-primary w-full max-w-xs"
          onClick={handleSave}
        >
          Spara
        </button>
          </div>
          <form method="dialog" className="modal-backdrop">
            <button>close</button>
          </form>
        </dialog>
      </div>

      <IngredientsList />
      <div>
      <button
          className="btn btn-primary w-full max-w-xs"
          onClick={() => document.getElementById("my_modal_4").showModal()}
       
        >
          Förhandsgranska
        </button>

      </div>
    </div>
  );
}
