// defaultRecipe.ts
import Recipe from '../data/recipe';
import IngredientToRecipe from '../data/IngredientToRecipe';

const defaultRecipe: Recipe = {
  
  Name: '',
  NrOfPortions: 0,
  Minutes: 0,
  Difficulty:0,
  Instructions: '',
  Ingredients: [] as IngredientToRecipe[], // Assuming Ingredients is an array of IngredientToRecipe
};

export default defaultRecipe;
