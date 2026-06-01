import { Link } from "react-router-dom";
import type { Item } from "../types/item";

interface ItemsListProps {
  items: Item[];
  detailsBaseUrl: string;
  editBaseUrl: string;
  handleRemoveItem: (item: Item) => void;
}

function ItemsList({
  items,
  detailsBaseUrl,
  editBaseUrl,
  handleRemoveItem,
}: ItemsListProps) {
  const handleRemoveLinkClick = (
    event: React.MouseEvent<HTMLAnchorElement, MouseEvent>,
    item: Item,
  ) => {
    event.preventDefault();
    handleRemoveItem(item);
  };
  return (
    <div>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Name</th>
            <th colSpan={3}>Options</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => (
            <tr key={item.id}>
              <td key={item.id + item.name}>{item.name}</td>
              <td key={item.id + "DetailsCol"}>
                <Link
                  to={`${detailsBaseUrl}/${item.id}`}
                  key={item.id + "DetailsLink"}
                >
                  Details
                </Link>
              </td>
              <td key={item.id + "EditCol"}>
                <Link
                  to={`${editBaseUrl}/${item.id}`}
                  key={item.id + "EditLink"}
                >
                  Edit
                </Link>
              </td>
              <td key={item.id + "RemoveCol"}>
                <a
                  href=""
                  key={item.id + "RemoveLink"}
                  onClick={(e) => handleRemoveLinkClick(e, item)}
                >
                  Remove
                </a>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ItemsList;
