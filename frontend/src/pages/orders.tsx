import type { ShoppingCartItem } from "../hooks/useCart";
import { deleteOrder, getOrders } from "../services/ordersApi";
import useFetch from "../hooks/useFetch";
import useForm from "../hooks/useForm";

function Orders() {
  const {
    data: orders,
    isLoading,
    error: fetchError,
    refetch,
  } = useFetch<OrderInfo[]>({ fetchFunction: getOrders });

  const validateDelete = (values: number) => {
    if (values <= 0) {
      throw new Error("Invalid order id");
    }
  };

  const { submitForm } = useForm<number>({
    formSubmit: async (params) => {
      const status = await deleteOrder(params);
      if (status != 204) throw new Error("Failed to delete order");
    },
    onSuccess: refetch,
    formValidation: validateDelete,
  });

  const handleDeleteOrder = async (id: number) => {
    submitForm(id);
  };

  return (
    <>
      <h2>Orders</h2>
      {isLoading ? (
        <p>Loading orders...</p>
      ) : fetchError ? (
        <div className="alert alert-danger">{fetchError.message}</div>
      ) : orders && orders.length > 0 ? (
        orders.map((order) => (
          <OrderCard
            key={order.id}
            order={order}
            onDelete={() => {
              handleDeleteOrder(order.id);
            }}
          />
        ))
      ) : (
        <p>No orders found.</p>
      )}
    </>
  );
}

interface OrderInfo {
  id: number;
  createDate: string;
  orderItems: ShoppingCartItem[];
}

interface OrderCardProps {
  order: OrderInfo;
  onDelete: () => void;
}

function OrderCard({ order, onDelete }: OrderCardProps) {
  return (
    <div className="card">
      <div className="card-body">
        <h5 className="card-title">Замовлення №{order.id}</h5>
        <h6 className="card-subtitle mb-2 text-body-secondary">
          Створено: {order.createDate}
        </h6>
        <p className="card-text">Страви:</p>
        <ul>
          {order.orderItems.map((item) => (
            <li key={item.id}>
              <a key={`${item.id}/link`} href={`/dish/${item.id}`}>
                {item.name}
              </a>{" "}
              {item.amount}x
            </li>
          ))}
        </ul>
        <button
          type="button"
          className="btn btn-outline-danger"
          onClick={onDelete}
        >
          Delete
        </button>
      </div>
    </div>
  );
}

export default Orders;
