import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import type { ProductDetails } from "../../types/product";
import DishForm from "../../components/dishForm";
import { getDish, updateDish } from "../../services/dishesApi";
import { getCategories } from "../../services/categoriesApi";
import type { Item } from "../../types/item";
import { getIngredients } from "../../services/ingredientsApi";

function EditDish() {
  const id = Number(useParams().id);
  const navigate = useNavigate();

  const [dish, setDish] = useState<ProductDetails>();
  const [categories, setCategories] = useState<Item[]>([]);
  const [ingredients, setIngredients] = useState<Item[]>([]);

  useEffect(() => {
    const fetchDish = async () => {
      try {
        const result = await getDish(id);

        setDish(result);
      } catch {
        console.log("Failed to load dish");
      }
    };
    const fetchCategories = async () => {
      try {
        const result = await getCategories();

        setCategories(result);
      } catch {
        console.log("Failed to load categories");
      }
    };
    const fetchIngredients = async () => {
      try {
        const result = await getIngredients();

        setIngredients(result);
      } catch {
        console.log("Failed to load ingredients");
      }
    };

    fetchDish();
    fetchCategories();
    fetchIngredients();
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
      const status = await updateDish(
        id,
        name,
        description,
        price,
        stock,
        categoryId,
        ingredientIds,
      );

      if (status != 200) {
        throw new Error();
      }

      navigate("/");
    } catch {
      console.log("Failed to edit dish");
    }
  };

  return (
    <>
      <h2>Edit dish</h2>

      {dish && (
        <DishForm
          categories={categories}
          ingredients={ingredients}
          onSubmit={handleFormSubmit}
          name={dish.name}
          description={dish.description}
          price={dish.price}
          stock={dish.stock}
          dishCategoryId={dish.category.id}
          dishIngredientIds={dish.ingredients.map(
            (ingredient) => ingredient.id,
          )}
        />
      )}
    </>
  );
}

export default EditDish;
