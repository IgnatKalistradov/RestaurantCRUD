import "./App.css";
import { Routes, Route } from "react-router-dom";
import Ingredients from "./pages/ingredients/ingredients";
import Categories from "./pages/categories/categories";
import Navbar from "./components/navbar";
import AddDish from "./pages/dishes/add-dish";
import Orders from "./pages/orders";
import NewOrder from "./pages/new-order";
import AddIngredient from "./pages/ingredients/addIngredient";
import AddCategory from "./pages/categories/addCategory";
import DetailsCategory from "./pages/categories/detailsCategory";
import DetailsIngredient from "./pages/ingredients/detailsIngredients";
import EditCategory from "./pages/categories/editCategory";
import EditIngredient from "./pages/ingredients/editIngredient";
import Dishes from "./pages/dishes/dishes";
import EditDish from "./pages/dishes/editDish";
import DetailsDish from "./pages/dishes/detailsDish";

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
          <Route path="/details-category/:id" element={<DetailsCategory />} />
          <Route
            path="/details-ingredient/:id"
            element={<DetailsIngredient />}
          />
          <Route path="/edit-category/:id" element={<EditCategory />} />
          <Route path="/edit-ingredient/:id" element={<EditIngredient />} />
          <Route path="/add-dish" element={<AddDish />} />
          <Route path="/dish/edit/:id" element={<EditDish />} />
          <Route path="/dish/:id" element={<DetailsDish />} />
          <Route path="/orders" element={<Orders />} />
          <Route path="new-order" element={<NewOrder />} />
        </Routes>
      </main>
    </>
  );
}

export default App;
