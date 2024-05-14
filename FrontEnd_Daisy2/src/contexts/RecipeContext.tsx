// RecipeContext.tsx
import React, { createContext, useState, ReactNode } from 'react';
import IngredientToRecipe from '../data/IngredientToRecipe';
import defaultRecipe from '../data/defaultRecipe';
import Recipe from '../data/recipe';

interface RecipeContextProps {
  recipe: Recipe;
  setRecipe: React.Dispatch<React.SetStateAction<Recipe>>;
  addIngredient: (ingredient: IngredientToRecipe) => void;
  removeIngredient: (ingredient: IngredientToRecipe) => void;
  updateInstructions: (instructions: string) => void;
  updateName: (name: string) => void;
  updateMinutes: (minutes: number) => void;
  updatePortions: (portions: number) => void;
  updateDifficulty: (difficulty: number) => void;
}

export const RecipeContext = createContext<RecipeContextProps | undefined>(undefined);

export const RecipeProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [recipe, setRecipe] = useState<Recipe>(defaultRecipe);

  const addIngredient = (ingredient: IngredientToRecipe) => {
    setRecipe(prevRecipe => ({
      ...prevRecipe,
      Ingredients: [...prevRecipe.Ingredients, ingredient],
    }));
  };

  const removeIngredient = (ingredient: IngredientToRecipe) => {
    setRecipe(prevRecipe => ({
      ...prevRecipe,
      Ingredients: prevRecipe.Ingredients.filter(ing => ing !== ingredient),
    }));
  };

  const updateInstructions = (instructions: string) => {
    setRecipe(prevRecipe => ({
      ...prevRecipe,
      Instructions: instructions,
    }));
  };

  const updateName = (name: string) => {
    setRecipe(prevRecipe => ({
      ...prevRecipe,
      Name: name,
    }));
  };

  const updateMinutes = (minutes: number) => {
    setRecipe(prevRecipe => ({
      ...prevRecipe,
      Minutes: minutes,
    }));
  };

  const updatePortions = (portions: number) => {
    setRecipe(prevRecipe => ({
      ...prevRecipe,
      NrOfPortions: portions,
    }));
  };

  const updateDifficulty = (difficulty: number) => {
    setRecipe(prevRecipe => ({
      ...prevRecipe,
      Difficulty: difficulty,
    }));
  };

  return (
    <RecipeContext.Provider
      value={{ recipe, setRecipe, addIngredient, removeIngredient, updateInstructions, updateName, updateMinutes, updatePortions, updateDifficulty }}
    >
      {children}
    </RecipeContext.Provider>
  );
};
