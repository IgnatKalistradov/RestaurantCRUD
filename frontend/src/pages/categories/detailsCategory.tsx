import { getCategory } from "../../services/categoriesApi";
import ItemDetails, {
  type ItemDetailsProps,
} from "../../components/itemDetails";
import { useParams } from "react-router-dom";
import useFetch from "../../hooks/useFetch";
import { useCallback } from "react";

function DetailsCategory() {
  const id = Number(useParams().id);

  const fetchCategory = useCallback(() => getCategory(id), [id]);

  const {
    data: details,
    isLoading,
    error,
  } = useFetch<ItemDetailsProps>({
    fetchFunction: fetchCategory,
  });

  return (
    <div>
      {isLoading ? (
        <p>Loading category...</p>
      ) : error ? (
        <div className="alert alert-danger">{error.message}</div>
      ) : (
        details && <ItemDetails item={details.item} dishes={details.dishes} />
      )}
    </div>
  );
}

export default DetailsCategory;
