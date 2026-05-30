import { useEffect, useState } from "react";
import IngredientList from "../components/ingredientList";
import { getIngredients } from "../api/ingredients";

function Ingredients() {
  const [ingredients, setIngredients] = useState([]);

  useEffect(() => {
    const fetchIngredients = async () => {
      const response = await getIngredients();

      console.log(response);
    };

    fetchIngredients();
  });

  return (
    <div>
      <h2>Ingredients page</h2>
      <a href="/add-ingredient">Add ingredient</a>
      <IngredientList ingredients={ingredients} />
    </div>
  );
}

export default Ingredients;
