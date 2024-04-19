
import ProductRecord from './ProductRecord'; // Import your interface definition
import { jsonData } from './testProductJson';
// Read the JSON file

console.log(jsonData);
// Parse the JSON string
const parsedData = JSON.parse(jsonData);

// Extract the "result" array from the parsed data
const results = parsedData.result;

// Map each object in the "result" array to the ProductRecord interface
export const productRecords: ProductRecord[] = results.map((item: any) => ({
    Id: item.id,
    CountryOfOrigin: item.countryOfOrigin,
    OfferType: item.offerType,
    Name: item.name,
    Brand: item.brand,
    Description: item.description,
    Price: item.price,
    DiscountedPrice: item.discountedPrice,
    Size: item.size,
    MinItems: item.minItems,
    MaxItems: item.maxItems,
    IsMemberOffer: item.isMemberOffer
}));

// Now you have an array of ProductRecord objects


