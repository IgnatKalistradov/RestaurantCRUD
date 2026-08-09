import type { DishDetails } from "../types/dish";
import "../../public/css/dish-description.css";

interface DishDescriptionProps {
  dish: DishDetails;
}

const IMAGE_BASE_URL = import.meta.env.VITE_IMAGE_URL;

function DishDescription({ dish }: DishDescriptionProps) {
  return (
    <div className="container text-center">
      <div className="dish-container">
        {dish.imageUrl && (
          <img
            src={`${IMAGE_BASE_URL}/${dish.imageUrl}`}
            className="dish-image"
          />
        )}
        <div className="dish-info">
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
        </div>
      </div>
    </div>
  );
}

export default DishDescription;
