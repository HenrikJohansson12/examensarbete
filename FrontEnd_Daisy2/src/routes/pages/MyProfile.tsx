import { useEffect } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { RootState } from "../../redux/store"; // Justera sökvägen till din store-fil

export default function MyProfile() {
  const navigate = useNavigate();
  const aspNetToken = useSelector((state: RootState) => state.auth.aspNetToken);

  useEffect(() => {
    if (!aspNetToken) {
      navigate("/login");
    }
  }, [aspNetToken, navigate]);

  return <div>Min profil</div>;
}
