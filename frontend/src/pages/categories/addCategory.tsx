import { useState } from "react";
import IngredientCategoryForm from "../../components/ingredientCategoryForm";
import { useNavigate } from "react-router-dom";
import { addCategory } from "../../services/categoriesApi";

function AddCategory() {
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleFormSubmit = async (name: string, description: string) => {
    try {
      const status = await addCategory(name, description);

      if (status != 201) {
        throw new Error();
      } else {
        navigate("/categories");
      }
    } catch {
      setError("Failed to add category");
    }
  };

  return (
    <div>
      <h2>Add category</h2>
      {error != "" && <div className="alert alert-danger">{error}</div>}
      <IngredientCategoryForm onSubmit={handleFormSubmit} />
    </div>
  );
}

export default AddCategory;
