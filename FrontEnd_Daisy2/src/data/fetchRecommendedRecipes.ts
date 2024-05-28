import { RecommendedRecipeDto } from "./RecommendedRecipe";

export const fetchRecommendedRecipes = async (token: string): Promise<RecommendedRecipeDto[]> => {
    const response = await fetch(`https://localhost:7027/api/getrecommendedrecipes`, {  
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`,
        },
    });

    if (!response.ok) {
        throw new Error('Failed to fetch data');
    }

    const data = await response.json();
    
    return data.recipes.map((item: RecommendedRecipeDto) => ({
        id: item.id,
        name: item.name,
        minutes: item.minutes,
        nrOfPortions: item.nrOfPortions,
        difficulty: item.difficulty,
        instructions: item.instructions,
        ingredients: item.ingredients.map(ingredient => ({
            name: ingredient.name,
            amount: ingredient.amount,
            unit: ingredient.unit,
            offer: ingredient.offer ? {
                id: ingredient.offer.id,
                countryOfOrigin: ingredient.offer.countryOfOrigin,
                offerType: ingredient.offer.offerType,
                name: ingredient.offer.name,
                brand: ingredient.offer.brand,
                description: ingredient.offer.description,
                price: ingredient.offer.price,
                discountedPrice: ingredient.offer.discountedPrice,
                size: ingredient.offer.size,
                minItems: ingredient.offer.minItems,
                maxItems: ingredient.offer.maxItems,
                isMemberOffer: ingredient.offer.isMemberOffer,
                storeName: ingredient.offer.storeName,
                category: ingredient.offer.category,
                discountPercent: ingredient.offer.discountPercent,
            } : null
        }))
    }));
};
