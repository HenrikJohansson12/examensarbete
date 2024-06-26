import { useState, useEffect, useRef } from "react";
import { MappedOffer, PostMappedOffer, mapToPostMappedOffer } from "../../data/MapOfferDTO";
import {
  fetchCategories,
  fetchUnmappedOffers,
  fetchIngredients,
} from "../../data/FetchIngredients";
import Category from "../../data/Category";
import { IngredientDto } from "../../data/Ingredient";
import { RootState } from "../../redux/store";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

export default function MappedOfferList() {
  const navigate = useNavigate();
  const [mappedOffers, setMappedOffers] = useState<MappedOffer[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [ingredientQueries, setIngredientQueries] = useState<{ [key: number]: string }>({});
  const [ingredientResults, setIngredientResults] = useState<IngredientDto[]>([]);
  const [activeOfferId, setActiveOfferId] = useState<number | null>(null);
  const aspNetToken = useSelector((state: RootState) => state.auth.aspNetToken);

  // Ref to store timeout ID for debouncing
  const debounceTimeout = useRef<{ [key: number]: NodeJS.Timeout }>({});

  useEffect(() => {
    if (aspNetToken === null) {
      navigate("/login");
    } else {
      const loadMappedOffers = async () => {
        try {
          setLoading(true);
          const offers = await fetchUnmappedOffers(aspNetToken);
          setMappedOffers(offers);
          const fetchedCategories = await fetchCategories(aspNetToken);
          setCategories(fetchedCategories);
        } catch (error) {
          setError("Failed to fetch offers");
        } finally {
          setLoading(false);
        }
      };

      loadMappedOffers();
    }
  }, [aspNetToken, navigate]);

  const handleSave = async () => {
    try {
      const postMappedOffers: PostMappedOffer[] = mappedOffers.map(mapToPostMappedOffer);
      const payload = { offers: postMappedOffers };
      const response = await fetch('https://localhost:7027/api/updateOffers', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${aspNetToken}`,
        },
        body: JSON.stringify(payload)
      });
      if (response.ok) {
        alert("Offers saved successfully!");
      } else {
        alert("Failed to save offers.");
      }
    } catch (error) {
      console.error("Error saving offers:", error);
      alert("An error occurred while saving the offers.");
    }
  };

  const handleCategoryChange = (event: React.ChangeEvent<HTMLSelectElement>, offerId: number) => {
    const selectedCategoryId = parseInt(event.target.value);
    const selectedCategory = categories.find(category => category.id === selectedCategoryId);

    setMappedOffers((prevOffers) =>
      prevOffers.map((offer) =>
        offer.id === offerId ? { ...offer, Category: selectedCategory } : offer
      )
    );
  };

  const handleIngredientSearch = (query: string, offerId: number) => {
    setIngredientQueries(prevQueries => ({ ...prevQueries, [offerId]: query }));
    setActiveOfferId(offerId);

    // Clear previous timeout if it exists
    if (debounceTimeout.current[offerId]) {
      clearTimeout(debounceTimeout.current[offerId]);
    }

    // Set a new timeout for the API call
    debounceTimeout.current[offerId] = setTimeout(async () => {
      if (query.length > 2 && aspNetToken != null) {
        const results = await fetchIngredients(query, aspNetToken);
        setIngredientResults(results);
      } else {
        setIngredientResults([]);
      }
    }, 500); // 0.5 seconds delay
  };

  const handleIngredientSelect = (ingredient: IngredientDto, offerId: number) => {
    setMappedOffers((prevOffers) =>
      prevOffers.map((offer) =>
        offer.id === offerId ? { ...offer, Ingredient: ingredient } : offer
      )
    );
    setIngredientQueries(prevQueries => ({ ...prevQueries, [offerId]: ingredient.name }));
    setIngredientResults([]);
    setActiveOfferId(null);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div className="p-4 bg-white shadow-md rounded-md">
      <button 
        onClick={handleSave} 
        className="mt-4 p-2 bg-blue-500 text-white rounded-md"
      >
        SPARA
      </button>
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
            <label className="block text-gray-700" htmlFor={`CategoryId-${offer.id}`}>Category:</label>
            <select
              name="CategoryId"
              id={`CategoryId-${offer.id}`}
              value={offer.Category?.id || ""}
              onChange={(e) => handleCategoryChange(e, offer.id)}
              className="mt-1 p-2 block w-full border border-gray-300 rounded-md"
            >
              <option value="">Select a category</option>
              {categories.map((category) => (
                <option key={category.id} value={category.id}>
                  {category.name}
                </option>
              ))}
            </select>
          </div>
          <div className="mb-2 relative">
            <label className="block text-gray-700">Ingredient:</label>
            <input
              type="text"
              value={ingredientQueries[offer.id] || ''}
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
