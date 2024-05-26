import Category from "./Category";
import { IngredientDto } from "./Ingredient";
import { MappedOffer } from "./MapOfferDTO";

// data.ts
export const fetchIngredients = async (query: string, token : string): Promise<IngredientDto[]> => {
    const response = await fetch(`https://localhost:7027/api/searchIngredients?searchWord=${query}`, {
        method: "GET",
        headers: {
          'Authorization': `Bearer ${token}`,
        },
      });
    const data = await response.json();
    console.log(query)
    return data.ingredients.map((item: IngredientDto) => ({
        id: item.id,
        slvIngredientId: item.slvIngredientId,
        name: item.name
    }));
};

export const fetchUnmappedOffers = async (token: string): Promise<MappedOffer[]> => {
    const response = await fetch(`https://localhost:7027/api/getunmappedoffers`,{
        method: "GET",
        headers:{
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`,
        },    
    });
    const data = await response.json();
    return data.offers.map((item: MappedOffer) => ({
        id: item.id,
        name: item.name,
        brand: item.brand,
        description: item.description,
        Ingredient: {}, // Fyll på med faktiska värden om de finns
        Category: {} // Fyll på med faktiska värden om de finns
    }));
};

export const fetchCategories = async (token: string): Promise<Category[]> => {
    const response = await fetch(`https://localhost:7027/api/getcategories`,
    {
        method: "GET",
        headers:{
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`,
        },    
    });
    const data = await response.json();
    return data.categories.map((item: Category) => ({
        id: item.id,
        name: item.name,
    }));
};
