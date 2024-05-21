export default interface Ingredient{
    Id: number;
    IngredientId: number;
    type: string;
    Number: number;
    Version: Date;
    Name: string;
}

export  interface IngredientDto{
    id: number;
    slvIngredientId: number;
    name: string;
}
