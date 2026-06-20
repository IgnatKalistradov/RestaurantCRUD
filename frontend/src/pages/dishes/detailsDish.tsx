import { useCallback } from "react";
import { useParams } from "react-router-dom";
import { getDish } from "../../services/dishesApi";
import DishDescription from "../../components/dishDescription";
import type { DishDetails } from "../../types/dish";
import useFetch from "../../hooks/useFetch";

function DetailsDish() {
  const id = Number(useParams().id);
  const fetchDishes = useCallback(async () => {
    return await getDish(id);
  }, [id]);
  const {
    data: dish,
    isLoading,
    error,
  } = useFetch<DishDetails>({ fetchFunction: fetchDishes });

  return (
    <>
      {isLoading && <p>Loading dish...</p>}
      {error ? (
        <div className="alert alert-danger">Error loading dish.</div>
      ) : (
        dish && <DishDescription dish={dish} />
      )}
    </>
  );
}

export default DetailsDish;
