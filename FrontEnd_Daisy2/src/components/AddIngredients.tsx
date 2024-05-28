import { useContext, useState, useEffect } from "react";
import { fetchIngredients } from "../data/FetchIngredients";
import { IngredientDto } from "../data/Ingredient";
import IngredientToRecipe from "../data/IngredientToRecipe";
import { RecipeContext } from "../contexts/RecipeContext";
import { useSelector } from "react-redux";
import { RootState } from "../redux/store";

export default function AddIngredients() {
  const context = useContext(RecipeContext);

  if (!context) {
    throw new Error("AddIngredients must be used within a RecipeProvider");
  }
  const aspNetToken = useSelector((state: RootState) => state.auth.aspNetToken);
  const { addIngredient } = context;
  const [ingredientQuery, setIngredientQuery] = useState<string>('');
  const [ingredientResults, setIngredientResults] = useState<IngredientDto[]>([]);
  const [amounts, setAmounts] = useState<{ [key: number]: number }>({});
  const [units, setUnits] = useState<{ [key: number]: string }>({});

  const addIngredientsButtonClicked = (ingredient: IngredientDto, index: number) => {
    const ingredientToRecipe: IngredientToRecipe = {
      Ingredient: ingredient,
      Amount: amounts[index] || 0,
      Unit: units[index] || "g",
    };

    addIngredient(ingredientToRecipe);
    console.log(ingredientToRecipe);
  };

  useEffect(() => {
    const delayDebounceFn = setTimeout(async () => {
      if (ingredientQuery.length > 2 && aspNetToken) {
        const results = await fetchIngredients(ingredientQuery, aspNetToken);
        setIngredientResults(results);
      } else {
        setIngredientResults([]);
      }
    }, 500); // 500ms debounce

    return () => clearTimeout(delayDebounceFn);
  }, [ingredientQuery, aspNetToken]);

  const handleIngredientSearch = (query: string) => {
    setIngredientQuery(query);
  };

  const handleAmountInputChange = (index: number, value: number) => {
    setAmounts((prevAmounts) => ({
      ...prevAmounts,
      [index]: value,
    }));
  };

  const handleUnitInputChange = (index: number, value: string) => {
    setUnits((prevUnits) => ({
      ...prevUnits,
      [index]: value,
    }));
  };

  return (
    <div className="flex flex-col items-center h-screen">
      <div className="flex flex-row">
        <input
          type="text"
          placeholder="Sök ingrediens"
          className="input input-bordered w-full max-w-xs"
          value={ingredientQuery}
          onChange={(e) => handleIngredientSearch(e.target.value)}
        />
      </div>
      <ul className="menu bg-base-500 rounded-box mt-4">
        {ingredientResults.map((result, index) => (
          <li key={index} className="flex flex-col items-start">
            <a className="w-full">
              {result.name}
              <div className="mt-2">
                <label className="block text-sm font-medium text-gray-700">Mängd</label>
                <input
                  value={amounts[index] || ''}
                  onChange={(e) => handleAmountInputChange(index, parseInt(e.target.value))}
                  type="number"
                  placeholder=""
                  className="input input-bordered w-20"
                />
              </div>
              <div className="mt-2">
                <label className="block text-sm font-medium text-gray-700">Enhet</label>
                <select
                  value={units[index] || "g"}
                  className="select select-bordered w-full max-w-xs"
                  onChange={(e) => handleUnitInputChange(index, e.target.value)}
                >
                  <option value="g">g</option>
                  <option value="dl">dl</option>
                  <option value="msk">msk</option>
                  <option value="tsk">tsk</option>
                  <option value="krm">krm</option>
                </select>
              </div>
              <button
                className="btn btn-square mt-2"
                onClick={() => addIngredientsButtonClicked(result, index)}
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
    </div>
  );
}
