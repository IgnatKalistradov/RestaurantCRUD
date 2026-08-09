import { useCallback } from "react";
import { useNavigate, useParams } from "react-router-dom";
import type { DishDetails } from "../../types/dish";
import DishForm from "../../components/dishForm";
import { getDish, updateDish } from "../../services/dishesApi";
import { getCategories } from "../../services/categoriesApi";
import type { Item } from "../../types/item";
import { getIngredients } from "../../services/ingredientsApi";
import useFetch from "../../hooks/useFetch";
import useForm from "../../hooks/useForm";

interface UpdateFormParams {
  id: number;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: number;
  ingredientIds: number[];
  image: File | null;
}

function EditDish() {
  const id = Number(useParams().id);
  const navigate = useNavigate();

  const fetchDish = useCallback(async () => {
    return await getDish(id);
  }, [id]);
  const { data: dish } = useFetch<DishDetails>({ fetchFunction: fetchDish });

  const { data: categories } = useFetch<Item[]>({
    fetchFunction: getCategories,
  });
  const { data: ingredients } = useFetch<Item[]>({
    fetchFunction: getIngredients,
  });

  const validateForm = (values: UpdateFormParams) => {
    if (values.id <= 0) {
      throw new Error("Invalid dish id");
    }
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
    error: updateError,
  } = useForm<UpdateFormParams>({
    formSubmit: async (params) => {
      const status = await updateDish(
        params.id,
        params.name,
        params.description,
        params.price,
        params.stock,
        params.categoryId,
        params.ingredientIds,
        params.image,
      );
      if (status !== 200) {
        throw new Error("Failed to update dish");
      }
    },
    onSuccess: () => {
      navigate("/");
    },
    formValidation: validateForm,
  });

  const handleFormSubmit = async (
    name: string,
    description: string,
    price: number,
    stock: number,
    categoryId: number,
    ingredientIds: number[],
    image: File | null,
  ) => {
    submitForm({
      id,
      name,
      description,
      price,
      stock,
      categoryId,
      ingredientIds,
      image,
    });
  };

  return (
    <>
      <h2>Edit dish</h2>
      {updateError && (
        <div className="alert alert-danger">{updateError.message}</div>
      )}
      {dish && categories && ingredients && (
        <DishForm
          categories={categories}
          ingredients={ingredients}
          onSubmit={handleFormSubmit}
          name={dish.name}
          description={dish.description}
          price={dish.price}
          stock={dish.stock}
          dishCategoryId={dish.category.id}
          isSubmitting={isSubmitting}
          dishIngredientIds={dish.ingredients.map(
            (ingredient) => ingredient.id,
          )}
        />
      )}
    </>
  );
}

export default EditDish;
