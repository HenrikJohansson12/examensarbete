import React, { useState, useEffect } from "react";
import { MappedOffer } from "../data/MapOfferDTO";
import {
  fetchCategories,
  fetchUnmappedOffers,
  fetchIngredients,
} from "../data/FetchIngredients";
import Category from "../data/Category";
import { IngredientDto } from "../data/Ingredient";

export default function MappedOfferList() {
  const [mappedOffers, setMappedOffers] = useState<MappedOffer[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [ingredientQuery, setIngredientQuery] = useState<string>('');
  const [ingredientResults, setIngredientResults] = useState<IngredientDto[]>([]);
  const [activeOfferId, setActiveOfferId] = useState<number | null>(null);

  useEffect(() => {
    const loadMappedOffers = async () => {
      try {
        setLoading(true);
        const offers = await fetchUnmappedOffers();
        setMappedOffers(offers);
        const fetchedCategories = await fetchCategories();
        setCategories(fetchedCategories);
      } catch (error) {
        setError("Failed to fetch offers");
      } finally {
        setLoading(false);
      }
    };

    loadMappedOffers();
  }, []); // Tom array betyder att effekten bara körs när komponenten laddas första gången

  const handleCategoryChange = (event: React.ChangeEvent<HTMLSelectElement>, offerId: number) => {
    const selectedCategoryId = parseInt(event.target.value);
    const selectedCategory = categories.find(category => category.Id === selectedCategoryId);

    setMappedOffers((prevOffers) =>
      prevOffers.map((offer) =>
        offer.id === offerId ? { ...offer, Category: selectedCategory } : offer
      )
    );
  };

  const handleIngredientSearch = async (query: string, offerId: number) => {
    setIngredientQuery(query);
    setActiveOfferId(offerId);
    if (query.length > 2) {
      const results = await fetchIngredients(query);
      setIngredientResults(results);
    } else {
      setIngredientResults([]);
    }
  };

  const handleIngredientSelect = (ingredient: IngredientDto, offerId: number) => {
    setMappedOffers((prevOffers) =>
      prevOffers.map((offer) =>
        offer.id === offerId ? { ...offer, Ingredient: ingredient } : offer
      )
    );
    setIngredientQuery('');
    setIngredientResults([]);
    setActiveOfferId(null);
  };
  const log = () =>{
    console.log(mappedOffers)
  }
  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div className="p-4 bg-white shadow-md rounded-md">
        <button onClick={log} >Console log</button>
      <h2 className="text-xl font-bold mb-4">Offers List</h2>
      {mappedOffers.map((offer) => (
        <div key={offer.id} className="mb-4 p-4 border rounded-md">
        
          <div className="mb-2">
            <label className="block text-gray-700">Name:</label>
            <p>{offer.name}</p>
          </div>
          <div className="mb-2">
            <label className="block text-gray-700">Brand:</label>
            <p>{offer.brand}</p>
          </div>
          <div className="mb-2">
            <label className="block text-gray-700">Description:</label>
            <p>{offer.description || "No description"}</p>
          </div>
          <div className="mb-2">
            <label
              className="block text-gray-700"
              htmlFor={`CategoryId-${offer.id}`}
            >
              Category:
            </label>
            <select
              name="CategoryId"
              id={`CategoryId-${offer.id}`}
              value={offer.Category?.Id || ""}
              onChange={(e) => handleCategoryChange(e, offer.id)}
              className="mt-1 p-2 block w-full border border-gray-300 rounded-md"
            >
              <option value="">Select a category</option>
              {categories.map((category) => (
                <option key={category.Id} value={category.Id}>
                  {category.Name}
                </option>
              ))}
            </select>
          </div>
          <div className="mb-2 relative">
            <label className="block text-gray-700">Ingredient:</label>
            <input
              type="text"
              value={activeOfferId === offer.id ? ingredientQuery : ''}
              onChange={(e) => handleIngredientSearch(e.target.value, offer.id)}
              className="mt-1 p-2 block w-full border border-gray-300 rounded-md"
              placeholder="Search for ingredient"
            />
            {activeOfferId === offer.id && ingredientResults.length > 0 && (
              <ul className="absolute z-10 bg-white border border-gray-300 rounded-md mt-1 w-full">
                {ingredientResults.map((ingredient) => (
                  <li
                    key={ingredient.id}
                    className="p-2 hover:bg-gray-200 cursor-pointer"
                    onClick={() => handleIngredientSelect(ingredient, offer.id)}
                  >
                    {ingredient.name}
                  </li>
                ))}
              </ul>
            )}
          </div>
        </div>
      ))}
    </div>
  );
}
