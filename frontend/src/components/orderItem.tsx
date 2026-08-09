interface OrderItemProps {
  id: number;
  name: string;
  price: number;
  amount: number;
  onDelete: () => void;
  onAmountChange: (amount: number) => void;
}

export default function OrderItem(props: OrderItemProps) {
  return (
    <tr>
      <td>{props.name}</td>
      <td>{props.price}</td>
      <td>
        <input
          type="number"
          className="input-group-number"
          onChange={(e) => props.onAmountChange(Number(e.target.value))}
          value={props.amount}
        />
      </td>
      <td>{props.amount * props.price}</td>
      <td>
        <button
          type="button"
          className="btn btn-outline-danger"
          onClick={() => props.onDelete()}
        >
          Delete
        </button>
      </td>
    </tr>
  );
}
