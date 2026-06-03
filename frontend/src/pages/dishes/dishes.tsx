import { useEffect, useState } from "react";
import DishCard from "../../components/dishCard";
import type { ProductInfo } from "../../types/product";
import { getDishes } from "../../services/dishesApi";

function Dishes() {
  const [dishes, setDishes] = useState<ProductInfo[]>();
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchDishes = async () => {
      try {
        const result = await getDishes();
        setDishes(result);
      } catch {
        setError("Failed to load dishes.");
      } finally {
        setLoading(false);
      }
    };

    fetchDishes();
  }, []);

  return (
    <>
      <h2>Menu page</h2>
      <a href="/add-dish">Add dish</a>
      <div className="container text-center">
        {isLoading ? (
          <p>Loading dishes...</p>
        ) : error != "" ? (
          <p>{error}</p>
        ) : (
          dishes &&
          dishes.map((dish) => (
            <DishCard
              key={dish.id}
              id={dish.id}
              name={dish.name}
              description={dish.description}
              price={dish.price}
              stock={dish.stock}
            />
          ))
        )}
      </div>
    </>
  );
}

export default Dishes;
