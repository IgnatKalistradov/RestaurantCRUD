import { useEffect, useState } from "react";
import { deleteCategory, getCategories } from "../../services/categoriesApi";
import ItemsList from "../../components/itemsList";
import type { Item } from "../../types/item";
import DeleteForm from "../../components/deleteForm";

function Categories() {
  const [categories, setCategories] = useState([]);
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [categoryToDelete, setCategoryToDelete] = useState<Item | null>(null);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await getCategories();

        setCategories(response);
      } catch {
        setError("Failed to load categories.");
      } finally {
        setLoading(false);
      }
    };
    fetchCategories();
  }, [isLoading]);

  const handleRemoveButtonClick = (item: Item) => {
    setShowModal(true);
    setCategoryToDelete(item);
  };

  const handleRemoveItemConfirm = async () => {
    setShowModal(false);

    if (categoryToDelete === null) return;

    try {
      const status = await deleteCategory(categoryToDelete.id);

      if (status != 204) throw new Error();

      setLoading(true);
    } catch {}
  };

  return (
    <>
      {categoryToDelete != null && (
        <DeleteForm
          isShown={showModal}
          onConfirm={handleRemoveItemConfirm}
          onClose={() => {
            setShowModal(false);
            setCategoryToDelete(null);
          }}
          itemName={categoryToDelete.name}
        />
      )}

      <h2>Categories page</h2>
      <a href="/add-category">Add category</a>
      {isLoading ? (
        <p>Loading categories...</p>
      ) : error === "" ? (
        <ItemsList
          items={categories}
          detailsBaseUrl="/details-category"
          editBaseUrl="/edit-category"
          handleRemoveItem={handleRemoveButtonClick}
        />
      ) : (
        <p>{error}</p>
      )}
    </>
  );
}

export default Categories;
