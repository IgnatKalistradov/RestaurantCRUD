import { useNavigate } from "react-router-dom";
import IngredientCategoryForm from "../../components/ingredientCategoryForm";
import { useState } from "react";
import { addIngredient } from "../../services/ingredientsApi";

function AddIngredient() {
  const [error, setError] = useState("");

  const navigate = useNavigate();
  const handleFormSubmit = async (name: string, description: string) => {
    if (name === "") {
      return;
    }

    try {
      const status = await addIngredient(name, description);
      if (status != 201) {
        throw new Error();
      } else {
        navigate("/ingredients");
      }
    } catch {
      setError("Failed to add ingredient!");
    }
  };

  return (
    <div>
      <h2>Add ingredient</h2>
      {error !== "" && <div className="alert alert-danger">{error}</div>}
      <IngredientCategoryForm onSubmit={handleFormSubmit} />
    </div>
  );
}

export default AddIngredient;
