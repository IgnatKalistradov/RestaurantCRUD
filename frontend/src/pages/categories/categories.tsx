import { useState } from "react";
import { deleteCategory, getCategories } from "../../services/categoriesApi";
import ItemsList from "../../components/itemsList";
import type { Item } from "../../types/item";
import DeleteForm from "../../components/deleteForm";
import useFetch from "../../hooks/useFetch";

function Categories() {
  const {
    data: categories,
    isLoading: isLoading,
    error,
    refetch: refetch,
  } = useFetch<Item[]>({
    fetchFunction: getCategories,
  });
  const [showModal, setShowModal] = useState(false);
  const [categoryToDelete, setCategoryToDelete] = useState<Item | null>(null);

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

      refetch();
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
      ) : error ? (
        <p>{error.message}</p>
      ) : categories?.length ? (
        <ItemsList
          items={categories}
          detailsBaseUrl="/details-category"
          editBaseUrl="/edit-category"
          handleRemoveItem={handleRemoveButtonClick}
        />
      ) : (
        <p>No categories found.</p>
      )}
    </>
  );
}

export default Categories;
