import { type ShoppingCartItem } from "../hooks/useCart";

interface DishCardProps {
  id: number;
  name: string;
  description: string;
  price: number;
  stock: number;
  onDelete: () => void;
  onAddToCart: (item: ShoppingCartItem) => void;
}

function DishCard(dish: DishCardProps) {
  return (
    <div className="card">
      <div className="card-body">
        <h5 className="card-title">{dish.name}</h5>
        <p className="card-text">{dish.description}</p>
        <p>Price: {dish.price}</p>
        <p>Stock: {dish.stock}</p>
        <a
          className="btn btn-primary"
          onClick={() =>
            dish.onAddToCart({
              id: dish.id,
              name: dish.name,
              amount: 1,
              price: dish.price,
            })
          }
        >
          Add to cart
        </a>
        <a href={`/dish/edit/${dish.id}`} className="btn btn-outline-primary">
          Edit
        </a>
        <a href={`/dish/${dish.id}`} className="btn btn-outline-dark">
          Details
        </a>
        <a
          onClick={(e) => {
            e.preventDefault;
            dish.onDelete();
          }}
          href="#"
          className="btn btn-danger"
        >
          Remove
        </a>
      </div>
    </div>
  );
}

export default DishCard;
