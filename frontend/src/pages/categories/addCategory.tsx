import IngredientCategoryForm from "../../components/ingredientCategoryForm";
import { useNavigate } from "react-router-dom";
import { addCategory } from "../../services/categoriesApi";
import useForm from "../../hooks/useForm";
import type { ItemFormData } from "../../types/item";

function AddCategory() {
  const navigate = useNavigate();
  const validateForm = (values: ItemFormData) => {
    if (values.name.trim() === "") {
      throw new Error("Name is required");
    }
    if (values.description.trim() === "") {
      throw new Error("Description is required");
    }
  };
  const { submitForm, isSubmitting, error } = useForm<ItemFormData>({
    formSubmit: async (values) => {
      const status = await addCategory(values.name, values.description);

      if (status != 201) throw new Error("Failed to add category");
    },
    formValidation: validateForm,
    onSuccess: () => {
      navigate("/categories");
    },
  });

  const handleFormSubmit = async (name: string, description: string) => {
    submitForm({ name, description });
  };

  return (
    <div>
      <h2>Add category</h2>
      {error && <div className="alert alert-danger">{error.message}</div>}
      <IngredientCategoryForm
        onSubmit={handleFormSubmit}
        isButtonDisabled={isSubmitting}
      />
    </div>
  );
}

export default AddCategory;
