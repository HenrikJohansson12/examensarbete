
import Ingredient from './Ingredient';
import { ingridientJson } from './testIngridients';
// Read the JSON file

const parsedData = JSON.parse(ingridientJson);

// Extract the "result" array from the parsed data
const results = parsedData.result;

// Map each object in the "result" array to the ProductRecord interface
export const ingredientDB: Ingredient[] = results.map((item: any) => ({
   // Id: item.id,
    IngredientId: item.ingredientId,
    Type: item.type,
    Number: item.number,
   // Version: item.version,
    Name: item.name
}));

// Now you have an array of ProductRecord objects


