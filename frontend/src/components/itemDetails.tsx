import type { Item } from "../types/item";
import type { DishBase } from "../types/dish";

export interface ItemDetailsProps {
  item: Item;
  dishes: DishBase[];
}

function ItemDetails({ item, dishes }: ItemDetailsProps) {
  return (
    <div>
      <h2>{item.name}</h2>
      <h6>Description:</h6>
      <p>{item.description}</p>

      <h6>Dishes:</h6>
      <ul className="list-group">
        {dishes &&
          dishes.map((dish) => (
            <li className="list-group-item" key={dish.id}>
              <a href={`/dish/${dish.id}`}>{dish.name}</a>
            </li>
          ))}
      </ul>
    </div>
  );
}

export default ItemDetails;
