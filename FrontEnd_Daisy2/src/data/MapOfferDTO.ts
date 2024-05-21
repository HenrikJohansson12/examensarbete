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

export interface PostMappedOffer{
    ProductRecordId: number,
    CategoryId: number,
    IngredientId: number
}

export const mapToPostMappedOffer = (mappedOffer: MappedOffer): PostMappedOffer => {
    return {
        ProductRecordId: mappedOffer.id,
        CategoryId: mappedOffer.Category?.Id ? mappedOffer.Category.Id : 0, // eller en lämplig standardvärde
        IngredientId: mappedOffer.Ingredient?.id ? mappedOffer.Ingredient.id : 0 // eller en lämplig standardvärde
    };
}

// data.ts
