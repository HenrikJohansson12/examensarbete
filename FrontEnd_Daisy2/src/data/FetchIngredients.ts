import Category from "./Category";
import { IngredientDto } from "./Ingredient";
import { MappedOffer } from "./MapOfferDTO";

// data.ts
export const fetchIngredients = async (query: string): Promise<IngredientDto[]> => {
    const response = await fetch(`https://localhost:7027/api/searchIngredients?searchWord=${query}`);
    const data = await response.json();
    return data.ingredients.map((item: IngredientDto) => ({
        id: item.id,
        slvIngredientId: item.slvIngredientId,
        name: item.name
    }));
};

export const fetchUnmappedOffers = async (): Promise<MappedOffer[]> => {
    const response = await fetch(`https://localhost:7027/api/getunmappedoffers`);
    const data = await response.json();
    return data.offers.map((item: any) => ({
        id: item.id,
        name: item.name,
        brand: item.brand,
        description: item.description,
        Ingredient: {}, // Fyll på med faktiska värden om de finns
        Category: {} // Fyll på med faktiska värden om de finns
    }));
};

export const fetchCategories = async (): Promise<Category[]> => {
    const response = await fetch(`https://localhost:7027/api/getcategories`);
    const data = await response.json();
    return data.categories.map((item: any) => ({
        Id: item.id,
        Name: item.name,
    }));
};
