import { useNavigate } from "react-router-dom";
import IngredientCategoryForm from "../../components/ingredientCategoryForm";
import { addIngredient } from "../../services/ingredientsApi";
import useForm from "../../hooks/useForm";

function AddIngredient() {
  const validateForm = (values: { name: string; description: string }) => {
    if (values.name.trim() === "") {
      throw new Error("Name is required");
    }
    if (values.description.trim() === "") {
      throw new Error("Description is required");
    }
  };
  const { submitForm, isSubmitting, error } = useForm({
    formSubmit: async (values) => {
      const status = await addIngredient(values.name, values.description);
      if (status != 201) throw new Error("Failed to add ingredient");
    },
    onSuccess: () => {
      navigate("/ingredients");
    },
    formValidation: validateForm,
  });

  const navigate = useNavigate();
  const handleFormSubmit = async (name: string, description: string) => {
    submitForm({ name, description });
  };

  return (
    <div>
      <h2>Add ingredient</h2>
      {error && <div className="alert alert-danger">{error.message}</div>}
      <IngredientCategoryForm
        onSubmit={handleFormSubmit}
        isButtonDisabled={isSubmitting}
      />
    </div>
  );
}

export default AddIngredient;
