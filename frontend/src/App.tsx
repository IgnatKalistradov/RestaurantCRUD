import "./App.css";
import { Routes, Route } from "react-router-dom";
import Ingredients from "./pages/ingredients";
import Dishes from "./pages/dishes";
import Categories from "./pages/categories";
import Navbar from "./components/navbar";
import AddEditPage from "./pages/add-edit-page";
import addIngredient from "./api/ingredients";
import addCategory from "./api/categories";
import AddDish from "./pages/add-dish";
import Orders from "./pages/orders";
import NewOrder from "./pages/new-order";

function App() {
  return (
    <>
      <Navbar />
      <main className="main-content">
        <Routes>
          <Route path="/" element={<Dishes />} />
          <Route path="/ingredients" element={<Ingredients />} />
          <Route path="/categories" element={<Categories />} />
          <Route
            path="/add-ingredient"
            element={<AddEditPage type="ingredient" onSubmit={addIngredient} />}
          />
          <Route
            path="/add-category"
            element={<AddEditPage type="category" onSubmit={addCategory} />}
          />
          <Route
            path="/edit-ingredient/:id"
            element={<AddEditPage type="ingredient" onSubmit={addIngredient} />}
          />
          <Route path="/add-dish" element={<AddDish />} />
          <Route path="/orders" element={<Orders />} />
          <Route path="new-order" element={<NewOrder />} />
        </Routes>
      </main>
    </>
  );
}

export default App;
