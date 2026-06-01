import { useNavigate, useParams } from "react-router-dom";
import IngredientCategoryForm from "../../components/ingredientCategoryForm";
import { useEffect, useState } from "react";
import { getCategory, updateCategory } from "../../services/categoriesApi";
import type { Item } from "../../types/item";

function EditCategory() {
  const id = Number(useParams().id);
  const navigate = useNavigate();
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [category, setCategory] = useState<Item | null>(null);

  useEffect(() => {
    const fetchCategory = async () => {
      try {
        const result = await getCategory(id);

        setCategory(result.item);
      } catch {
        setError("Failed to load category.");
      } finally {
        setLoading(false);
      }
    };
    fetchCategory();
  }, []);

  const handleFormSubmit = async (name: string, description: string) => {
    if (name === "") {
      setError("Name can not be empty.");
      return;
    }

    try {
      const status = await updateCategory(id, name, description);

      if (status != 204) throw new Error();

      navigate("/categories");
    } catch {
      setError("Failed to update category");
    }
  };

  return (
    <>
      <h2>Edit category</h2>
      {isLoading ? (
        <p>Loading category...</p>
      ) : error != "" ? (
        <p>{error}</p>
      ) : (
        category && (
          <IngredientCategoryForm
            name={category.name}
            description={category.description}
            onSubmit={handleFormSubmit}
          />
        )
      )}
    </>
  );
}

export default EditCategory;
