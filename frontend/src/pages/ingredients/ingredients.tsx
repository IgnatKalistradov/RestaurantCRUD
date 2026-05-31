import { useEffect, useState } from "react";
import ItemsList, { type ListItem } from "../../components/itemsList";
import { getIngredients } from "../../services/ingredientsApi";

function Ingredients() {
  const [ingredients, setIngredients] = useState([]);
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchIngredients = async () => {
      try {
        const response = await getIngredients();

        setIngredients(response);
      } catch {
        setError("Failed to load ingredients.");
      } finally {
        setLoading(false);
      }
    };

    fetchIngredients();
  }, []);

  const handleRemoveItem = (item: ListItem) => {
    console.log(`Delete ${item.id}?`);
  };

  return (
    <div>
      <h2>Ingredients page</h2>
      <a href="/add-ingredient">Add ingredient</a>
      {isLoading ? (
        <p>Loading ingredients...</p>
      ) : error === "" ? (
        <ItemsList
          items={ingredients}
          detailsBaseUrl="/edit-ingredient"
          editBaseUrl="/edit-ingredient"
          handleRemoveItem={handleRemoveItem}
        />
      ) : (
        <p>{error}</p>
      )}
    </div>
  );
}

export default Ingredients;
