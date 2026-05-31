import { useEffect, useState } from "react";
import CategoryList from "../../components/categoryList";
import { getCategories } from "../../services/categoriesApi";
import ItemsList, { type ListItem } from "../../components/itemsList";

function Categories() {
  const [categories, setCategories] = useState([]);
  const [isLoading, setLoading] = useState(true);
  const [error, setError] = useState("");

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
  }, []);

  const handleRemoveItem = (item: ListItem) => {
    console.log(`Remove ${item.id}?`);
  };

  return (
    <>
      <h2>Categories page</h2>
      <a href="/add-category">Add category</a>
      {isLoading ? (
        <p>Loading categories...</p>
      ) : error === "" ? (
        <ItemsList
          items={categories}
          detailsBaseUrl="/details-category"
          editBaseUrl="/edit-category"
          handleRemoveItem={handleRemoveItem}
        />
      ) : (
        <p>{error}</p>
      )}
    </>
  );
}

export default Categories;
