import { useNavigate } from 'react-router-dom';
import image from '../../assets/background.webp';

export default function LandingPage(){
  const navigate = useNavigate();
  const joinButtonClicked = () =>{
    navigate('/login')
  }
    return(
<div className="hero min-h-screen" style={{backgroundImage: `url(${image})`}}>
  <div className="hero-overlay bg-opacity-60"></div>
  <div className="hero-content text-center text-neutral-content">
    <div className="max-w-md">
      <h1 className="mb-5 text-5xl font-bold">Studentmat</h1>
      <p className="mb-5">Din tjänst för att se extrapriser från matbutikerna i din närhet och som ger dig förslag på vad du kan laga.</p>
      <button className="btn btn-primary" onClick={joinButtonClicked}>Gå med</button>
    </div>
  </div>
</div>
    )
}