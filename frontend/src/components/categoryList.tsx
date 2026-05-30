interface Category {
  id: string;
  name: string;
}

interface CategoryListProps {
  categories: Category[];
}

function CategoryList({ categories }: CategoryListProps) {
  const handleRemoveCategory = (categoryId: string) => {
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
            <tr key={category.id}>
              <td key={category.id + category.name}>{category.name}</td>
              <td key={category.id + "DetailsCol"}>
                <a href="/" key={category.id + "DetailsLink"}>
                  Details
                </a>
              </td>
              <td key={category.id + "EditCol"}>
                <a href="/" key={category.id + "EditLink"}>
                  Edit
                </a>
              </td>
              <td key={category.id + "RemoveCol"}>
                <a
                  href=""
                  key={category.id + "RemoveLink"}
                  onClick={() => handleRemoveCategory(category.id)}
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
