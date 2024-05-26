import React from 'react';
import Recipe from '../data/recipe';

interface RecipeCardProps {
    recipe: Recipe;
}

const RecipeCard: React.FC<RecipeCardProps> = ({ recipe }) => {
    return (
        <div className="card w-96 bg-base-100 shadow-xl">
            <div className="card-body">
                <h2 className="card-title text-lg font-bold">{recipe.Name}</h2>
                <p><strong>Tid:</strong> {recipe.Minutes} minuter</p>
                <p><strong>Portioner:</strong> {recipe.NrOfPortions}</p>
                <p><strong>Svårighetsgrad: </strong> {recipe.Difficulty}/5</p>
                <div className="my-4">
                    <h3 className="text-md font-semibold">Ingredients:</h3>
                    <ul>
                        {recipe.Ingredients.map((ingredientToRecipe, index) => (
                            <li key={index} className="flex justify-between">
                                <span>{ingredientToRecipe.Ingredient.name}</span>
                                <span>{ingredientToRecipe.Amount} {ingredientToRecipe.Unit}</span>
                            </li>
                        ))}
                    </ul>
                </div>
                <div className="my-4">
                    <h3 className="text-md font-semibold">Instruktioner:</h3>
                    <p>{recipe.Instructions}</p>
                </div>
            </div>
        </div>
    );
};

export default RecipeCard;
