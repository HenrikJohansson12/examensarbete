
import { productRecords } from "../../data/testData";

export default function OfferPage(){

  return (
    <div className="grid grid-cols-2 gap-x-2 gap-y-10 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 xl:gap-x-8">
   
        {productRecords.map((productRecord) => (
            <div key={productRecord.Id} className='card w-50 bg-base-100 shadow-xl'>
                <div className='card-body items-center text-center'>
                    <h2 className='card-title'>{productRecord.Name}</h2>
                    <p>{productRecord.Description}</p>
                    <p>Brand: {productRecord.Brand}</p>
                    <p>Price: {productRecord.Price}</p>
                    <p>Discounted Price: {productRecord.DiscountedPrice}</p>
                    {/* Render other fields as needed */}
                    <div className='card-actions'>
                        <button className='btn btn-primary btn-sm'>
                            Save to shopping list
                        </button>
                    </div>
                </div>
            </div>
        ))}
       
    </div>
);
}
