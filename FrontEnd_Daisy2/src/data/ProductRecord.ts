export default interface ProductRecord {
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
