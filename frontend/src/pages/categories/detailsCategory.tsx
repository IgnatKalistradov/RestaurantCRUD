import { useEffect, useState } from "react";
import { getCategory } from "../../services/categoriesApi";
import ItemDetails from "../../components/itemDetails";
import type { Item } from "../../types/item";
import type { DishBase } from "../../types/product";
import { useParams } from "react-router-dom";

function DetailsCategory() {
  const [category, setCategory] = useState<Item | null>(null);
  const [dishes, setDishes] = useState<DishBase[]>([]);
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const id = Number(useParams().id);

  useEffect(() => {
    const fetchCategory = async () => {
      try {
        const result = await getCategory(id);

        setCategory(result.item);
        setDishes(result.dishes);
      } catch {
        setError("Failed to load category.");
      } finally {
        setLoading(false);
      }
    };
    fetchCategory();
  }, []);

  return (
    <div>
      {isLoading ? (
        <p>Loading category...</p>
      ) : error === "" && category ? (
        <ItemDetails item={category} dishes={dishes} />
      ) : (
        <p>{error}</p>
      )}
    </div>
  );
}

export default DetailsCategory;
