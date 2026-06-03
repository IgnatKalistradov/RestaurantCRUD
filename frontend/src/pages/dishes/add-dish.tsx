import { useEffect, useState } from "react";
import DishForm from "../../components/dishForm";
import type { Item } from "../../types/item";
import { getCategories } from "../../services/categoriesApi";
import { getIngredients } from "../../services/ingredientsApi";
import { addDish } from "../../services/dishesApi";
import { useNavigate } from "react-router-dom";

function AddDish() {
  const navigate = useNavigate();

  const [categories, setCategories] = useState<Item[]>();
  const [ingredients, setIngredients] = useState<Item[]>();
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const result = await getCategories();

        setCategories(result);
      } catch {
        setError("Failed to load categories");
      }
    };

    const fetchIngredients = async () => {
      try {
        const result = await getIngredients();

        setIngredients(result);
      } catch {
        setError("Failed to load ingredients");
      }
    };

    fetchCategories();
    if (error != "") {
      setLoading(false);
      return;
    }

    fetchIngredients();
    setLoading(false);
  }, []);

  const handleFormSubmit = async (
    name: string,
    description: string,
    price: number,
    stock: number,
    categoryId: number,
    ingredientIds: number[],
  ) => {
    try {
      const status = await addDish(
        name,
        description,
        price,
        stock,
        categoryId,
        ingredientIds,
      );

      if (status != 204) {
        throw new Error();
      }

      navigate("/");
    } catch {
      setError("Failed to add dish");
    }
  };
  return (
    <>
      <h2>Create dish</h2>
      {isLoading ? (
        <p>Loading categories and ingredients...</p>
      ) : error != "" ? (
        <p>{error}</p>
      ) : (
        categories &&
        ingredients && (
          <DishForm
            categories={categories}
            ingredients={ingredients}
            onSubmit={handleFormSubmit}
          />
        )
      )}
    </>
  );
}

export default AddDish;
