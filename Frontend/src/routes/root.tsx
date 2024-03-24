import { Outlet } from "react-router-dom";
import BottomAppBar from "../components/BottomAppBar";


export default function Root() {
  return (
    <div>
      <Outlet />
      <BottomAppBar />
    </div>
  );
}
