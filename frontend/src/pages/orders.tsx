import { useEffect, useState } from "react";
import type { ShoppingCartItem } from "../hooks/useCart";
import { deleteOrder, getOrders } from "../services/ordersApi";

function Orders() {
  const [orders, setOrders] = useState<OrderInfo[] | null>(null);
  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const result = await getOrders();

        setOrders(result);
      } catch {
        console.log("Failed to load orders.");
      } finally {
        setLoading(false);
      }
    };

    fetchOrders();
  }, [isLoading]);

  const handleDeleteOrder = async (id: number) => {
    try {
      const status = await deleteOrder(id);

      if (status != 204) {
        throw new Error();
      }

      setLoading(true);
    } catch {
      console.log("Failed to remove order");
    }
  };

  return (
    <>
      <h2>Orders</h2>
      {orders &&
        orders.map((order) => (
          <OrderCard
            key={order.id}
            order={order}
            onDelete={() => {
              handleDeleteOrder(order.id);
            }}
          />
        ))}
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
