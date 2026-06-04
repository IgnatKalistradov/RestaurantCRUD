import { useEffect, useState } from "react";
import type { ProductDetails } from "../../types/product";
import { useParams } from "react-router-dom";
import { getDish } from "../../services/dishesApi";
import DishDescription from "../../components/DishDescription";

function DetailsDish() {
  const id = Number(useParams().id);
  const [dish, setDish] = useState<ProductDetails | null>(null);
  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    const fetchDish = async () => {
      try {
        const result = await getDish(id);

        setDish(result);
      } catch {
        console.log("Failed to load dish.");
      } finally {
        setLoading(false);
      }
    };

    fetchDish();
  }, []);

  return (
    <>
      {isLoading && <p>Loading dish...</p>}
      {dish && <DishDescription dish={dish} />}
    </>
  );
}

export default DetailsDish;
