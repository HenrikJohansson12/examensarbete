import { useEffect, useState } from "react";
import { useSelector } from "react-redux";

import { RootState } from "../../redux/store";
import ProductRecord from "../../data/ProductRecord";
import { fetchTopTenOffers } from "../../data/fetchTopTenOffers";

export default function OfferPage() {
    const aspNetToken = useSelector((state: RootState) => state.auth.aspNetToken);
    const [productRecords, setProductRecords] = useState<ProductRecord[]>([]);

    useEffect(() => {
        const fetchOffers = async () => {
            if(aspNetToken != null)
            try {
                const data = await fetchTopTenOffers(aspNetToken);
                setProductRecords(data);
            } catch (error) {
                console.error('Failed to fetch product records:', error);
            }
        };

        fetchOffers();
    }, [aspNetToken]);

    return (
        <div className="grid grid-cols-2 gap-x-2 gap-y-10 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 xl:gap-x-8">
            {productRecords.map((productRecord) => (
                <div key={productRecord.id} className='card w-50 bg-base-100 shadow-xl'>
                    <div className='card-body items-center text-center'>
                        <h2 className='card-title'>{productRecord.name}</h2>
                        <p>{productRecord.storeName}</p>
                        {productRecord.offerType === "MultiBuyOffer" && (
                            <p>Vid köp av {productRecord.minItems} eller fler</p>
                        )}
                           {productRecord.offerType === "PerKiloGram" && (
                            <p>Pris per kilo</p>
                        )}  
                        <p>Pris: {productRecord.discountedPrice} kr ord. pris {productRecord.price} kr</p>
                        <p>{productRecord.discountPercent.toPrecision(2)}% rabatt</p>
                        {productRecord.maxItems>0 && (
                            <p>Max {productRecord.maxItems} per hushåll</p>
                        )}
                         {productRecord.isMemberOffer && (
                            <p>Medlemspris</p>
                        )}
                        <div className='card-actions'>
                            <button className='btn btn-primary btn-sm'>
                                Spara i Shoppinglista
                            </button>
                        </div>
                    </div>
                </div>
            ))}
        </div>
    );
}
