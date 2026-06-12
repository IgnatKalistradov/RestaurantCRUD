import { useEffect, useState } from "react";
import { getIngredient } from "../../services/ingredientsApi";
import ItemDetails from "../../components/itemDetails";
import type { Item } from "../../types/item";
import type { DishBase } from "../../types/product";
import { useParams } from "react-router-dom";

function DetailsIngredient() {
  const [ingredient, setIngredient] = useState<Item | null>(null);
  const [dishes, setDishes] = useState<DishBase[]>([]);
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const id = Number(useParams().id);

  useEffect(() => {
    const fetchIngredient = async () => {
      try {
        const result = await getIngredient(id);

        setIngredient(result.item);
        setDishes(result.dishes);
      } catch {
        setError("Failed to load ingredient.");
      } finally {
        setLoading(false);
      }
    };
    fetchIngredient();
  }, []);

  return (
    <div>
      {isLoading ? (
        <p>Loading ingredient...</p>
      ) : error === "" && ingredient ? (
        <ItemDetails item={ingredient} dishes={dishes} />
      ) : (
        <p>{error}</p>
      )}
    </div>
  );
}

export default DetailsIngredient;
