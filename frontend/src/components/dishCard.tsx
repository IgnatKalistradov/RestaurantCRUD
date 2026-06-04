interface DishCardProps {
  id: number;
  name: string;
  description: string;
  price: number;
  stock: number;
  onDelete: () => void;
}

function DishCard(dish: DishCardProps) {
  return (
    <div className="card">
      <div className="card-body">
        <h5 className="card-title">{dish.name}</h5>
        <p className="card-text">{dish.description}</p>
        <p>Price: {dish.price}</p>
        <p>Stock: {dish.stock}</p>
        <a href="#" className="btn btn-primary">
          Add to cart
        </a>
        <a href={`/dish/edit/${dish.id}`} className="btn btn-outline-primary">
          Edit
        </a>
        <a href={`/details-dish/${dish.id}`} className="btn btn-outline-dark">
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
