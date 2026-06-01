import type { Item } from "../types/item";
import type { ProductBase } from "../types/product";

export interface ItemDetailsProps {
  item: Item;
  products: ProductBase[];
}

function ItemDetails({ item, products }: ItemDetailsProps) {
  return (
    <div>
      <h2>{item.name}</h2>
      <h6>Description:</h6>
      <p>{item.description}</p>

      <h6>Dishes:</h6>
      <ul className="list-group">
        {products.map((product) => (
          <li className="list-group-item" key={product.id}>
            <a href={`/dish/${product.id}`}>{product.name}</a>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ItemDetails;
