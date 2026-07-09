import type { Item } from "../../types/item";
import { getCategories } from "../../services/categoriesApi";
import { getIngredients } from "../../services/ingredientsApi";
import { addDish } from "../../services/dishesApi";
import { useNavigate } from "react-router-dom";
import DishForm from "../../components/dishForm";
import useFetch from "../../hooks/useFetch";
import useForm from "../../hooks/useForm";

interface AddDishFormValues {
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: number;
  ingredientIds: number[];
  image: File | null;
}

function AddDish() {
  const navigate = useNavigate();

  const {
    data: categories,
    error: categoriesError,
    isLoading: loadingCategories,
  } = useFetch<Item[]>({ fetchFunction: getCategories });
  const {
    data: ingredients,
    error: ingredientsError,
    isLoading: loadingIngredients,
  } = useFetch<Item[]>({ fetchFunction: getIngredients });

  const validateForm = (values: AddDishFormValues) => {
    if (values.name.trim() === "") {
      throw new Error("Name is required");
    }
    if (values.description.trim() === "") {
      throw new Error("Description is required");
    }
    if (values.price <= 0) {
      throw new Error("Price must be greater than 0");
    }
    if (values.stock < 0) {
      throw new Error("Stock cannot be negative");
    }
    if (values.categoryId <= 0) {
      throw new Error("Category is required");
    }
    if (values.ingredientIds.length === 0) {
      throw new Error("At least one ingredient is required");
    }
    if (values.ingredientIds.some((id) => id <= 0)) {
      throw new Error("Invalid ingredient id");
    }
  };

  const {
    submitForm,
    isSubmitting,
    error: formError,
  } = useForm<AddDishFormValues>({
    formSubmit: async (values) => {
      const status = await addDish(
        values.name,
        values.description,
        values.price,
        values.stock,
        values.categoryId,
        values.ingredientIds,
        values.image,
      );
      if (status != 204) throw new Error("Failed to add dish");
    },
    onSuccess: () => navigate("/"),
    formValidation: validateForm,
  });

  return (
    <>
      <h2>Create dish</h2>
      {formError && (
        <div className="alert alert-danger">{formError.message}</div>
      )}
      {loadingCategories || loadingIngredients ? (
        <p>Loading categories and ingredients...</p>
      ) : categoriesError || ingredientsError ? (
        <div className="alert alert-danger">
          {categoriesError?.message || ingredientsError?.message}
        </div>
      ) : (
        categories &&
        ingredients && (
          <DishForm
            categories={categories}
            ingredients={ingredients}
            isSubmitting={isSubmitting}
            onSubmit={(
              name: string,
              description: string,
              price: number,
              stock: number,
              categoryId: number,
              ingredientIds: number[],
              image: File | null,
            ) => {
              submitForm({
                name,
                description,
                price,
                stock,
                categoryId,
                ingredientIds,
                image,
              });
            }}
          />
        )
      )}
    </>
  );
}

export default AddDish;
