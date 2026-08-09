import { useState } from "react";
import ItemsList from "../../components/itemsList";
import {
  deleteIngredient,
  getIngredients,
} from "../../services/ingredientsApi";
import type { Item } from "../../types/item";
import DeleteForm from "../../components/deleteForm";
import useFetch from "../../hooks/useFetch";
import useForm from "../../hooks/useForm";

function Ingredients() {
  const {
    data: ingredients,
    isLoading,
    error: fetchError,
    refetch,
  } = useFetch<Item[]>({ fetchFunction: getIngredients });
  const [showModal, setShowModal] = useState(false);
  const [ingredientToDelete, setIngredientToDelete] = useState<Item | null>(
    null,
  );

  const validateForm = (values: number) => {
    if (values === null) {
      throw new Error("Invalid ingredient id");
    }
    if (values <= 0) {
      throw new Error("Invalid ingredient id");
    }
  };

  const { submitForm, error: formError } = useForm<number>({
    formSubmit: async (params) => {
      const status = await deleteIngredient(params);
      if (status != 204) {
        throw new Error("Failed to delete ingredient");
      }
    },
    onSuccess: refetch,
    formValidation: validateForm,
  });

  const handleRemoveItemClick = (item: Item) => {
    setShowModal(true);
    setIngredientToDelete(item);
  };

  const handleRemoveItemConfirm = async () => {
    setShowModal(false);
    if (ingredientToDelete === null) return;

    submitForm(ingredientToDelete.id);

    if (formError) {
      console.log(formError.message);
    }
  };

  return (
    <div>
      {ingredientToDelete != null && (
        <DeleteForm
          isShown={showModal}
          onConfirm={handleRemoveItemConfirm}
          onClose={() => {
            setShowModal(false);
            setIngredientToDelete(null);
          }}
          itemName={ingredientToDelete.name}
        />
      )}

      <h2>Ingredients page</h2>
      <a href="/add-ingredient">Add ingredient</a>
      {isLoading ? (
        <p>Loading ingredients...</p>
      ) : fetchError ? (
        <div className="alert alert-danger">{fetchError.message}</div>
      ) : ingredients && ingredients.length > 0 ? (
        <ItemsList
          items={ingredients}
          detailsBaseUrl="/details-ingredient"
          editBaseUrl="/edit-ingredient"
          handleRemoveItem={handleRemoveItemClick}
        />
      ) : (
        <p>No ingredients found.</p>
      )}
    </div>
  );
}

export default Ingredients;
