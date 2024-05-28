import React, { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { RootState } from "../../redux/store";
import { fetchRecipes, selectAllRecipes, getRecipesStatus, getRecipesError } from "../../redux/recommendedRecipesSlice"
import { RecommendedRecipeDto } from "../../data/RecommendedRecipe";

export default function ViewRecipes() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const aspNetToken = useSelector((state: RootState) => state.auth.aspNetToken);
  const recipes = useSelector(selectAllRecipes);
  const recipesStatus = useSelector(getRecipesStatus);
  const error = useSelector(getRecipesError);

  const addRecipeButtonClicked = () => {
    navigate("new");
  };

  useEffect(() => {
    if (recipesStatus === 'idle' && aspNetToken) {
      dispatch(fetchRecipes(aspNetToken));
    }
  }, [recipesStatus, aspNetToken, dispatch]);

  if (recipesStatus === 'loading') {
    return <div>Loading...</div>;
  }

  if (recipesStatus === 'failed') {
    return <div>Error: {error}</div>;
  }

  return (
    <div className="flex flex-col flex-1 relative">
      <div className="flex flex-col items-center">
        <h1 className="text-center text-3xl mb-4">
          Veckans recept baserat på erbjudanden nära dig
        </h1>
      </div>
      <div className="grid grid-cols-2 gap-x-2 gap-y-10 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 xl:gap-x-8 mt-4">
        {recipes.map((recipe: RecommendedRecipeDto) => {
          const totalIngredients = recipe.ingredients.length;
          const ingredientsWithOfferCount = recipe.ingredients.filter(ingredient => ingredient.offer != null).length;

          return (
            <div key={recipe.id} className="card w-56 bg-base-100 shadow-xl">
              <div className="card-body items-center text-center">
                <h2 className="card-title">{recipe.name}</h2>
                <p>{recipe.nrOfPortions} portioner</p>
                <p>ca {recipe.minutes} minuter</p>
                <p>{ingredientsWithOfferCount} av {totalIngredients} ingredienser på extrapris</p>

                <div className="card-actions justify-center w-full">
                  <button className="btn btn-primary btn-sm"
                    onClick={() => navigate(`recipe/${recipe.id}`)}>
                    Visa recept
                  </button>
                </div>
              </div>
            </div>
          );
        })}
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
  );
}
