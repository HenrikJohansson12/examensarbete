export default function SignUpPage() {
  return (
    <div className="min-h-screen flex items-center justify-center w-full">
        <div className=  "shadow-md rounded-lg px-8 py-6 max-w-md">
      <label className="input input-bordered flex items-center gap-2">
        Name
        <input type="text" className="grow" placeholder="Daisy" />
      </label>
      <label className="input input-bordered flex items-center gap-2">
        Email
        <input type="text" className="grow" placeholder="daisy@site.com" />
      </label>
     
      </div>
    </div>
    
  );
}
