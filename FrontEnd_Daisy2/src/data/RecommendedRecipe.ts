export interface RecommendedRecipeDto {
    id: number;
    name: string;
    minutes: number;
    nrOfPortions: number;
    difficulty: number;
    instructions: string;
    ingredients: RecommendedRecipeIngredient[];
  }
  
 export interface RecommendedRecipeIngredient {
    name: string;
    amount: number;
    unit: string;
    offer: Offer | null;
  }
  
export  interface Offer {
    id: number;
    countryOfOrigin: string;
    offerType: string;
    name: string;
    brand: string;
    description: string;
    price: number;
    discountedPrice: number;
    size: string;
    minItems: number;
    maxItems: number;
    isMemberOffer: boolean;
    storeName: string;
    category: string;
    discountPercent: number;
  }

  
 
  