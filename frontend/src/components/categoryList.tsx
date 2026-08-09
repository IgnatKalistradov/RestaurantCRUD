interface Category {
  categoryId: number;
  name: string;
}

interface CategoryListProps {
  categories: Category[];
}

function CategoryList({ categories }: CategoryListProps) {
  const handleRemoveCategory = (categoryId: number) => {
    alert(`You are removing category with id: ${categoryId}`);
  };

  return (
    <div>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Name</th>
            <th colSpan={3}>Options</th>
          </tr>
        </thead>
        <tbody>
          {categories.map((category) => (
            <tr key={category.categoryId}>
              <td key={category.categoryId + category.name}>{category.name}</td>
              <td key={category.categoryId + "DetailsCol"}>
                <a href="/" key={category.categoryId + "DetailsLink"}>
                  Details
                </a>
              </td>
              <td key={category.categoryId + "EditCol"}>
                <a href="/" key={category.categoryId + "EditLink"}>
                  Edit
                </a>
              </td>
              <td key={category.categoryId + "RemoveCol"}>
                <a
                  href=""
                  key={category.categoryId + "RemoveLink"}
                  onClick={() => handleRemoveCategory(category.categoryId)}
                >
                  Remove
                </a>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default CategoryList;
