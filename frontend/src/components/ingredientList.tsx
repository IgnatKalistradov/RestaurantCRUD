import { useState } from "react";
import { Link } from "react-router-dom";

interface Ingredient {
  id: string;
  name: string;
}

interface IngredientListProps {
  ingredients: Ingredient[];
}

function IngredientList({ ingredients }: IngredientListProps) {
  const handleRemoveIngredient = (ingredientId: string) => {
    alert(`You are removing ingredient with id: ${ingredientId}`);
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
          {ingredients.map((ingredient) => (
            <tr key={ingredient.id}>
              <td key={ingredient.id + ingredient.name}>{ingredient.name}</td>
              <td key={ingredient.id + "DetailsCol"}>
                <a href="/" key={ingredient.id + "DetailsLink"}>
                  Details
                </a>
              </td>
              <td key={ingredient.id + "EditCol"}>
                <Link
                  to={"/edit-ingredient/" + ingredient.id}
                  key={ingredient.id + "EditLink"}
                >
                  Edit
                </Link>
              </td>
              <td key={ingredient.id + "RemoveCol"}>
                <a
                  href=""
                  key={ingredient.id + "RemoveLink"}
                  onClick={() => handleRemoveIngredient(ingredient.id)}
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

export default IngredientList;
