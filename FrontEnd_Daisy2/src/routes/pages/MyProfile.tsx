import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { RootState } from "../../redux/store"; // Justera sökvägen till din store-fil

export default function MyProfile() {
  const [zipcode, setZipcode] = useState<string>("");
  const [displayName, setDisplayName] = useState<string>("");
  const navigate = useNavigate();
  const aspNetToken = useSelector((state: RootState) => state.auth.aspNetToken);

  useEffect(() => {
    if (!aspNetToken) {
      navigate("/login");
    }
  }, [aspNetToken, navigate]);

  const updateProfile = async () => {
    try {
      const response = await fetch('https://localhost:7027/api/updateuserinformation', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${aspNetToken}`,
        },
        body: JSON.stringify({ zipcode, displayName }),
      });

      if (!response.ok) {
        throw new Error('Failed to update profile');
      }

    } catch (error) {
      console.error('Error updating profile:', error);
    }
  };

  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <h1 className="text-3xl mb-4">Uppdatera profil</h1>
      <input
        type="text"
        value={zipcode}
        onChange={e => setZipcode(e.target.value)}
        placeholder="Postnummer"
        className="input input-bordered input-primary w-full max-w-xs mb-4"
      />
      <input
        type="text"
        value={displayName}
        onChange={e => setDisplayName(e.target.value)}
        placeholder="Visningsnamn"
        className="input input-bordered input-primary w-full max-w-xs mb-4"
      />
      <button className="btn btn-primary" onClick={updateProfile}>Spara ändringar</button>
    </div>
  );
}
