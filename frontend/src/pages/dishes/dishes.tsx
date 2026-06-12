import { useEffect, useState } from "react";
import DishCard from "../../components/dishCard";
import type { DishInfo } from "../../types/product";
import { deleteDish, getDishes } from "../../services/dishesApi";
import DeleteForm from "../../components/deleteForm";
import { useCart } from "../../hooks/useCart";

function Dishes() {
  const [dishes, setDishes] = useState<DishInfo[]>();
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [isModalVisible, setModalVisibility] = useState(false);
  const [itemToDelete, setItemToDelete] = useState<DishInfo | null>(null);
  const cart = useCart();

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
  }, [isLoading]);

  const handleDishDelete = async () => {
    setModalVisibility(false);

    if (!itemToDelete) return;

    try {
      const status = await deleteDish(itemToDelete.id);

      if (status != 204) throw new Error();

      setLoading(true);
    } catch {
      setError("Failed to delete dish.");
    }
  };

  return (
    <>
      <DeleteForm
        isShown={isModalVisible}
        itemName={itemToDelete ? itemToDelete.name : ""}
        onConfirm={handleDishDelete}
        onClose={() => {
          setModalVisibility(false);
        }}
      />
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
              onDelete={() => {
                setItemToDelete(dish);
                setModalVisibility(true);
              }}
              onAddToCart={cart.addItem}
            />
          ))
        )}
      </div>
    </>
  );
}

export default Dishes;
