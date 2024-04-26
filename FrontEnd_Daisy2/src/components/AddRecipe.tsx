export default function AddRecipe() {
  return (
    <div className="flex items-row justify-center h-screen">
        <div>
     <input type="text" placeholder="Name" className="input input-bordered w-full max-w-xs" />
     <input type="text" placeholder="Time" className="input input-bordered w-full max-w-xs" />
     <input type="text" placeholder="Portions" className="input input-bordered w-full max-w-xs" />
     <div className="rating">
        <div>
    <h2> Difficulty</h2>
  <input type="radio" name="rating-1" className="mask mask-star" />
  <input type="radio" name="rating-1" className="mask mask-star" />
  <input type="radio" name="rating-1" className="mask mask-star" checked />
  <input type="radio" name="rating-1" className="mask mask-star" />
  <input type="radio" name="rating-1" className="mask mask-star" />
  </div>
</div>
<input type="text" placeholder="Type here" className="input input-bordered w-full max-w-xs" />
<div>
<ul className="menu bg-base-200 w-56 rounded-box">
  <li><a>Här ska resultaten komma</a></li>
  <li><a>Man väljer ingrediens </a></li>
  <li><a>Sen måste vi in antal  och enhet</a></li>
</ul>
</div>
     </div> 
      <textarea
        placeholder='Instructions'
        className='textarea textarea-bordered textarea-lg w-full max-w-xs'
      ></textarea>
    </div>
  );
}
