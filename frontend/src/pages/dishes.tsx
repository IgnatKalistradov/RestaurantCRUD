import DishCard from "../components/dishCard";

function Dishes() {
  return (
    <>
      <h2>Menu page</h2>
      <a href="/add-dish">Add dish</a>
      <div className="container text-center">
        <div className="row">
          <div className="col">
            <DishCard
              name="Карбонара"
              description="Смачна традиційна карбонара"
              id=""
              price={123}
              stock={1}
            />
          </div>
          <div className="col">
            <DishCard
              name="Карбонара"
              description="Смачна традиційна карбонара"
              id=""
              price={123}
              stock={1}
            />
          </div>
          <div className="col">
            <DishCard
              name="Карбонара"
              description="Смачна традиційна карбонара"
              id=""
              price={123}
              stock={1}
            />
          </div>
        </div>
      </div>
    </>
  );
}

export default Dishes;
