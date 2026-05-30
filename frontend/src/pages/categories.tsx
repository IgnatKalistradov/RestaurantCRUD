import CategoryList from "../components/categoryList";

function Categories() {
  const categories = [
    {
      id: "1",
      name: "Vegetables",
    },
    {
      id: "2",
      name: "Fruits",
    },
    {
      id: "3",
      name: "Dairy",
    },
    {
      id: "4",
      name: "Meat",
    },
    {
      id: "5",
      name: "Spices",
    },
    {
      id: "6",
      name: "Beverages",
    },
  ];
  return (
    <>
      <h2>Categories page</h2>
      <a href="/add-category">Add category</a>
      <CategoryList categories={categories} />
    </>
  );
}

export default Categories;
