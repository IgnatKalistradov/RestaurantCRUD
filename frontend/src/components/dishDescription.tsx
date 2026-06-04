import type { ProductDetails } from "../types/product";

interface DishDescriptionProps {
  dish: ProductDetails;
}

function DishDescription({ dish }: DishDescriptionProps) {
  return (
    <>
      <h2>{dish.name}</h2>
      <h5>{dish.description}</h5>
      <p>Price: {dish.price}</p>
      <p>Stock: {dish.stock}</p>
      <p>
        Category:{" "}
        <a href={`/details-category/${dish.category.id}`}>
          {dish.category.name}
        </a>
      </p>
      <p>Ingredients:</p>
      <ul>
        {dish.ingredients.map((ing) => (
          <li key={`${ing.id}`}>
            <a key={`${ing.id}Link`} href={`/details-ingredient/${ing.id}`}>
              {ing.name}
            </a>
          </li>
        ))}
      </ul>
    </>
  );
}

export default DishDescription;
