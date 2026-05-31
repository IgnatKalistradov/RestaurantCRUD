import "./App.css";
import { Routes, Route } from "react-router-dom";
import Ingredients from "./pages/ingredients/ingredients";
import Dishes from "./pages/dishes";
import Categories from "./pages/categories/categories";
import Navbar from "./components/navbar";
import AddEditPage from "./pages/add-edit-page";
import addIngredient from "./services/ingredientsApi";
import AddDish from "./pages/add-dish";
import Orders from "./pages/orders";
import NewOrder from "./pages/new-order";
import AddIngredient from "./pages/ingredients/addIngredient";
import AddCategory from "./pages/categories/addCategory";

function App() {
  return (
    <>
      <Navbar />
      <main className="main-content">
        <Routes>
          <Route path="/" element={<Dishes />} />
          <Route path="/ingredients" element={<Ingredients />} />
          <Route path="/categories" element={<Categories />} />
          <Route path="/add-ingredient" element={<AddIngredient />} />
          <Route path="/add-category" element={<AddCategory />} />
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
