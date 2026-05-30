interface DishCardProps {
  id: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  imagePath?: string;
}

function DishCard(dish: DishCardProps) {
  return (
    <div className="card">
      <img src={dish.imagePath} className="card-img-top" alt={dish.name} />
      <div className="card-body">
        <h5 className="card-title">{dish.name}</h5>
        <p className="card-text">{dish.description}</p>
        <a href="#" className="btn btn-primary">
          Add to cart
        </a>
        <a href="#" className="btn btn-outline-dark">
          Details
        </a>
        <a href="#" className="btn btn-danger">
          Remove
        </a>
      </div>
    </div>
  );
}

export default DishCard;
