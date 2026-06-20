import { useNavigate, useParams } from "react-router-dom";
import IngredientCategoryForm from "../../components/ingredientCategoryForm";
import { getCategory, updateCategory } from "../../services/categoriesApi";
import type { ItemDetailsProps } from "../../components/itemDetails";
import useFetch from "../../hooks/useFetch";
import useForm from "../../hooks/useForm";
import { useCallback } from "react";

interface FormData {
  id: number;
  name: string;
  description: string;
}

function EditCategory() {
  const id = Number(useParams().id);
  const navigate = useNavigate();

  const fetchCategory = useCallback(() => getCategory(id), [id]);

  const {
    data: details,
    isLoading,
    error: fetchError,
  } = useFetch<ItemDetailsProps>({ fetchFunction: fetchCategory });

  const validateForm = (values: FormData) => {
    if (values.name.trim() === "") {
      throw new Error("Name is required");
    }
    if (values.description.trim() === "") {
      throw new Error("Description is required");
    }
  };

  const {
    submitForm,
    error: submitError,
    isSubmitting,
  } = useForm<FormData>({
    formSubmit: async (values) => {
      const status = await updateCategory(
        values.id,
        values.name,
        values.description,
      );
      if (status !== 204) throw new Error("Failed to update category");
    },
    onSuccess: () => {
      navigate("/categories");
    },
    formValidation: validateForm,
  });

  const handleFormSubmit = (name: string, description: string) => {
    submitForm({ id, name, description });
  };

  return (
    <>
      <h2>Edit category</h2>
      {submitError && (
        <div className="alert alert-danger">{submitError.message}</div>
      )}
      {isLoading ? (
        <p>Loading category...</p>
      ) : fetchError ? (
        <div className="alert alert-danger">{fetchError.message}</div>
      ) : (
        details && (
          <IngredientCategoryForm
            name={details.item.name}
            description={details.item.description}
            onSubmit={handleFormSubmit}
            isButtonDisabled={isSubmitting}
          />
        )
      )}
    </>
  );
}

export default EditCategory;
