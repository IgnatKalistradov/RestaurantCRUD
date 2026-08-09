import { useCallback } from "react";
import { getIngredient } from "../../services/ingredientsApi";
import ItemDetails, {
  type ItemDetailsProps,
} from "../../components/itemDetails";

import { useParams } from "react-router-dom";
import useFetch from "../../hooks/useFetch";

function DetailsIngredient() {
  const id = Number(useParams().id);

  const fetchData = useCallback(async () => {
    return await getIngredient(id);
  }, [id]);
  const { data, isLoading, error } = useFetch<ItemDetailsProps>({
    fetchFunction: fetchData,
  });

  return (
    <div>
      {isLoading ? (
        <p>Loading ingredient...</p>
      ) : error ? (
        <div className="alert alert-danger">{error.message}</div>
      ) : (
        data && <ItemDetails item={data.item} dishes={data.dishes} />
      )}
    </div>
  );
}

export default DetailsIngredient;
