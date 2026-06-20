import { useNavigate, useParams } from "react-router-dom";
import { useCallback, useEffect, useState } from "react";
import { getIngredient, updateIngredient } from "../../services/ingredientsApi";
import type { Item } from "../../types/item";
import IngredientCategoryForm from "../../components/ingredientCategoryForm";
import useFetch from "../../hooks/useFetch";
import type { ItemDetailsProps } from "../../components/itemDetails";
import useForm from "../../hooks/useForm";

function EditIngredient() {
  const id = Number(useParams().id);
  const navigate = useNavigate();

  const fetchIngredient = useCallback(
    async () => await getIngredient(id),
    [id],
  );

  const {
    data: ingredient,
    isLoading,
    error: fetchError,
  } = useFetch<ItemDetailsProps>({ fetchFunction: fetchIngredient });

  const validateForm = (values: Item) => {
    if (values.name.trim() === "") {
      throw new Error("Name is required");
    }
    if (values.description.trim() === "") {
      throw new Error("Description is required");
    }
  };

  const {
    submitForm,
    isSubmitting,
    error: submitError,
  } = useForm<Item>({
    formSubmit: async (values: Item) => {
      const status = await updateIngredient(
        values.id,
        values.name,
        values.description,
      );
      if (status !== 204) throw new Error("Failed to update ingredient");
    },
    onSuccess: () => {
      navigate("/ingredients");
    },
    formValidation: validateForm,
  });

  const handleFormSubmit = async (name: string, description: string) => {
    submitForm({ id, name, description });
  };

  return (
    <>
      <h2>Edit ingredient</h2>
      {submitError && (
        <div className="alert alert-danger">{submitError.message}</div>
      )}
      {isLoading ? (
        <p>Loading ingredient...</p>
      ) : fetchError ? (
        <div className="alert alert-danger">{fetchError.message}</div>
      ) : (
        ingredient && (
          <IngredientCategoryForm
            name={ingredient.item.name}
            description={ingredient.item.description}
            onSubmit={handleFormSubmit}
            isButtonDisabled={isSubmitting}
          />
        )
      )}
    </>
  );
}

export default EditIngredient;
