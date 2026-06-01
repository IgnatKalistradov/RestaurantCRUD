import { useEffect, useState } from "react";
import ItemsList from "../../components/itemsList";
import {
  deleteIngredient,
  getIngredients,
} from "../../services/ingredientsApi";
import type { Item } from "../../types/item";
import DeleteForm from "../../components/deleteForm";

function Ingredients() {
  const [ingredients, setIngredients] = useState([]);
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [ingredientToDelete, setIngredientToDelete] = useState<Item | null>(
    null,
  );

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
  }, [isLoading]);

  const handleRemoveItemClick = (item: Item) => {
    setShowModal(true);
    setIngredientToDelete(item);
  };

  const handleRemoveItemConfirm = async () => {
    setShowModal(false);

    if (ingredientToDelete === null) return;

    try {
      const status = await deleteIngredient(ingredientToDelete.id);
      if (status != 204) {
        throw new Error();
      }
      setLoading(true);
    } catch {
      console.log("Failed to delete ingredient.");
    }
  };

  return (
    <div>
      {ingredientToDelete != null && (
        <DeleteForm
          isShown={showModal}
          onConfirm={handleRemoveItemConfirm}
          onClose={() => {
            setShowModal(false);
            setIngredientToDelete(null);
          }}
          itemName={ingredientToDelete.name}
        />
      )}

      <h2>Ingredients page</h2>
      <a href="/add-ingredient">Add ingredient</a>
      {isLoading ? (
        <p>Loading ingredients...</p>
      ) : error === "" ? (
        <ItemsList
          items={ingredients}
          detailsBaseUrl="/details-ingredient"
          editBaseUrl="/edit-ingredient"
          handleRemoveItem={handleRemoveItemClick}
        />
      ) : (
        <p>{error}</p>
      )}
    </div>
  );
}

export default Ingredients;
