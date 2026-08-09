import { useState } from "react";
import DishCard from "../../components/dishCard";
import { deleteDish, getDishes } from "../../services/dishesApi";
import DeleteForm from "../../components/deleteForm";
import { useCart } from "../../hooks/useCart";
import type { DishInfo } from "../../types/dish";
import useFetch from "../../hooks/useFetch";
import useForm from "../../hooks/useForm";

function Dishes() {
  const [isModalVisible, setModalVisibility] = useState(false);
  const [itemToDelete, setItemToDelete] = useState<DishInfo | null>(null);
  const cart = useCart();

  const {
    data: dishes,
    isLoading,
    error,
    refetch,
  } = useFetch<DishInfo[]>({ fetchFunction: getDishes });

  const { submitForm } = useForm<number>({
    formSubmit: async (params) => {
      const status = await deleteDish(params);
      if (status != 204) throw new Error("Failed to delete dish");
    },
    onSuccess: refetch,
    formValidation: (values: number) => {
      if (values <= 0) {
        throw new Error("Invalid dish id");
      }
    },
  });

  const handleDishDelete = async () => {
    setModalVisibility(false);

    if (!itemToDelete) return;

    submitForm(itemToDelete.id);
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
        ) : error ? (
          <div className="alert alert-danger">{error.message}</div>
        ) : dishes && dishes.length > 0 ? (
          <div className="row g-4">
            {dishes.map((dish) => (
              <div key={dish.id} className="col-12 col-sm-6 col-lg-4 col-xl-3">
                <DishCard
                  key={dish.id}
                  id={dish.id}
                  name={dish.name}
                  description={dish.description}
                  price={dish.price}
                  stock={dish.stock}
                  imageUrl={dish.imageUrl}
                  onDelete={() => {
                    setItemToDelete(dish);
                    setModalVisibility(true);
                  }}
                  onAddToCart={cart.addItem}
                />
              </div>
            ))}
          </div>
        ) : (
          <p>No dishes found.</p>
        )}
      </div>
    </>
  );
}

export default Dishes;
