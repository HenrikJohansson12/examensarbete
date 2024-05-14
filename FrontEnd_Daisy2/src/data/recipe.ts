import IngredientToRecipe from "./IngredientToRecipe";

export default interface Recipe{

   Name: string;
   Minutes: number;
   NrOfPortions: number;
   Difficulty: number;
   Instructions: string;
   Ingredients: IngredientToRecipe[]
}