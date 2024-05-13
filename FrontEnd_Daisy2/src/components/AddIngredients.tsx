import { useState } from "react";
import React from "react";
import { ingredientDB } from "../data/ingredientArray";
import Ingredient from "../data/Ingredient";
import IngredientToRecipe from "../data/IngredientToRecipe";

export default function AddIngredients() {
  const [searchTerm, setSearchTerm] = useState("");
  const [searchResults, setSearchResults] = useState<Ingredient[]>([]);
  const [amount, setAmount] = useState(0);
  const [unit, setUnit] = useState("g");
  const [selectedIngredients, setSelectedIngredients] = useState<
    IngredientToRecipe[]
  >([]);

  const addIngredient = (ingredient: Ingredient) => {
    const ingredientToRecipe: IngredientToRecipe ={
        Ingredient: ingredient,
        Amount: amount,
        Unit: unit
    };
    setSelectedIngredients((prevIngredients) => [
      ...prevIngredients,
      ingredientToRecipe,
    ]);
    console.log(selectedIngredients);
  };

  const clearSearch = () => {
    const emptySearchResults : Ingredient[] = [];
    setSearchResults(emptySearchResults);
  }

  const handleSearchInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setSearchTerm(value);
    // Filter items based on search term
    const filteredResults = ingredientDB.filter((ingredient) =>
      ingredient.Name.toLowerCase().includes(value.toLowerCase())
    );
    setSearchResults(filteredResults);
  };

  const handleAmountInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    const integerValue = parseInt(value);
    setAmount(integerValue);
  };

  const handleUnitInputChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setUnit(e.target.value);
  };

  return (
    <div className="flex flex-col items-center h-screen">
        <div className=" flex flex-row">
      <input
        type="text"
        placeholder="Type here"
        className="input input-bordered w-full max-w-xs"
        value={searchTerm}
        onChange={handleSearchInputChange}
      />
      <button
                className="btn btn-square"
                onClick={clearSearch}
              >
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
      <ul className="menu bg-base-500 rounded-box">
        {searchResults.map((result, index) => (
          <li key={index}>
            <a>
              {result.Name}
              <input
                value={amount}
                onChange={handleAmountInputChange}
                type="number"
                placeholder="Amount"
                className="input input-bordered w-16 min-w-m"
              />
              <select
                className="select select-bordered w-full max-w-xs"
                onChange={handleUnitInputChange}
              > 
                <option value="g">g</option>
                <option value="dl">dl</option>
                <option value="msk">msk</option>
                <option value="tsk">tsk</option>
                <option value="krm">krm</option>
              </select>
              <button
                className="btn btn-square"
                onClick={() => addIngredient(result)}
              >
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
            </a>
          </li>
        ))}
      </ul>

      <ul className="menu bg-base-200 w-56 rounded-box">
        {selectedIngredients.map((result, index) => (
          <li key={index}>
            <a>{result.Ingredient.Name + result.Amount + result.Unit}</a>
          </li>
        ))}
      </ul>
    </div>
  );
}
