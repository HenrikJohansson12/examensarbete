import Category from "./Category";
import { IngredientDto } from "./Ingredient";

export  interface UnMappedOffer{
    id: number;
    name: string;
    brand: string;
    description: string;
   
}

export  interface MappedOffer extends UnMappedOffer{
    Ingredient: IngredientDto | undefined;
    Category: Category | undefined;
  
}

// data.ts
