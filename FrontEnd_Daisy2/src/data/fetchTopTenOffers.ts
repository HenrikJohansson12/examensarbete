import ProductRecord from "./ProductRecord";


export const fetchTopTenOffers = async (token: string): Promise<ProductRecord[]> => {
    const response = await fetch(`https://localhost:7027/api/toptendiscountedproducts`, {  
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
    
    return data.products.map((item: ProductRecord) => ({
        id: item.id,
        countryOfOrigin: item.countryOfOrigin,
        offerType: item.offerType,
        name: item.name,
        brand: item.brand,
        description: item.description,
        price: item.price,
        discountedPrice: item.discountedPrice,
        size: item.size,
        minItems: item.minItems,
        maxItems: item.maxItems,
        isMemberOffer: item.isMemberOffer,
        storeName: item.storeName,
        category: item.category,
        discountPercent: item.discountPercent
    }));
};
