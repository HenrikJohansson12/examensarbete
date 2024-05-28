import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import { useSelector } from 'react-redux';
//import { RootState } from '../redux/store';
import { RecommendedRecipeDto } from '../data/RecommendedRecipe';
import { selectAllRecipes } from '../../src/redux/recommendedRecipesSlice';

const RecommendedRecipePage: React.FC = () => {
    const { recipeId } = useParams<{ recipeId: string }>();
    const recipes = useSelector(selectAllRecipes);
    const recipe = recipes.find((r: RecommendedRecipeDto) => r.id === parseInt(recipeId!));
    const [showOffers, setShowOffers] = useState(false);

    if (!recipe) {
        return <div>Recipe not found</div>;
    }

    const ingredientsWithOffers = recipe.ingredients.filter(ingredient => ingredient.offer);

    const totalSavings = ingredientsWithOffers.reduce((acc, ingredient) => {
        if (ingredient.offer) {
            return acc + (ingredient.offer.price - ingredient.offer.discountedPrice);
        }
        return acc;
    }, 0);

    return (
        <div className="flex justify-center items-center h-screen">
            <div className="card w-96 bg-base-100 shadow-xl">
                <div className="card-body">
                    <h2 className="card-title text-lg font-bold">{recipe.name}</h2>
                    <p><strong>Tid:</strong> {recipe.minutes} minuter</p>
                    <p><strong>Portioner:</strong> {recipe.nrOfPortions}</p>
                    <p><strong>Svårighetsgrad: </strong> {recipe.difficulty}/5</p>
                    <div className="my-4">
                        <h3 className="text-md font-semibold">Ingredienser:</h3>
                        <ul>
                            {recipe.ingredients.map((ingredientToRecipe, index) => (
                                <li key={index} className="flex justify-between">
                                    <span className={ingredientToRecipe.offer ? 'font-bold' : ''}>{ingredientToRecipe.name}</span>
                                    <span>{ingredientToRecipe.amount} {ingredientToRecipe.unit}</span>
                                </li>
                            ))}
                        </ul>
                    </div>
                    <button className='btn btn-primary' onClick={() => setShowOffers(true)}>Visa erbjudanden</button>
                    
                    {showOffers && (
                        <dialog open className="modal">
                            <div className="modal-box flex flex-col justify-center items-center">
                                <h3 className="text-lg font-bold">Erbjudanden</h3>
                                <ul>
                                    {ingredientsWithOffers.map((ingredient, index) => (
                                        <li key={index} className="my-2">
                                            <p><strong>{ingredient.name}</strong></p>
                                            <p>Butik: {ingredient.offer!.storeName}</p>
                                            <p>Ordinarie pris: {ingredient.offer!.price} kr</p>
                                            <p>Rabatterat pris: {ingredient.offer!.discountedPrice} kr</p>
                                        </li>
                                    ))}
                                </ul>
                                <p className="mt-4 font-bold">Total besparing: {totalSavings.toFixed(2)} kr</p>
                                <button className="btn btn-secondary mt-4" onClick={() => setShowOffers(false)}>Stäng</button>
                            </div>
                        </dialog>
                    )}
                    
                    <div className="my-4">
                        <h3 className="text-md font-semibold">Instruktioner:</h3>
                        <p>{recipe.instructions}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default RecommendedRecipePage;
