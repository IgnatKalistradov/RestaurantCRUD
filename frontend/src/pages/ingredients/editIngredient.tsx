import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { getIngredient, updateIngredient } from "../../services/ingredientsApi";
import type { Item } from "../../types/item";
import IngredientCategoryForm from "../../components/ingredientCategoryForm";

function EditIngredient() {
  const id = Number(useParams().id);
  const navigate = useNavigate();
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [ingredient, setIngredient] = useState<Item | null>(null);

  useEffect(() => {
    const fetchIngredient = async () => {
      try {
        const result = await getIngredient(id);

        setIngredient(result.item);
      } catch {
        setError("Failed to load ingredient.");
      } finally {
        setLoading(false);
      }
    };
    fetchIngredient();
  }, []);

  const handleFormSubmit = async (name: string, description: string) => {
    if (name === "") {
      setError("Name can not be empty.");
      return;
    }

    try {
      const status = await updateIngredient(id, name, description);

      if (status != 204) throw new Error();

      navigate("/ingredients");
    } catch {
      setError("Failed to update ingredient");
    }
  };

  return (
    <>
      <h2>Edit ingredient</h2>
      {isLoading ? (
        <p>Loading ingredient...</p>
      ) : error != "" ? (
        <p>{error}</p>
      ) : (
        ingredient && (
          <IngredientCategoryForm
            name={ingredient.name}
            description={ingredient.description}
            onSubmit={handleFormSubmit}
          />
        )
      )}
    </>
  );
}

export default EditIngredient;
