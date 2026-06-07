import { useEffect } from "react";
import OrderItem from "../components/orderItem";
import { useCart } from "../hooks/useCart";
import { createOrder } from "../services/ordersApi";
import { useNavigate } from "react-router-dom";

function NewOrder() {
  const cart = useCart();
  const navigate = useNavigate();

  useEffect(() => {
    if (cart.cartItems.length === 0) navigate("/");
  }, [cart.cartItems]);

  const handleCreateOrderClick = async () => {
    if (cart.cartItems.length < 1) return;
    try {
      const status = await createOrder(cart.cartItems);

      if (status != 204) throw new Error();

      navigate("/orders");
    } catch {
      console.log("Failed to create new order");
    } finally {
      cart.clear();
    }
  };

  return (
    <>
      <h2>New Order</h2>
      <table className="table table-striped">
        <thead>
          <tr>
            <th scope="col">Name</th>
            <th scope="col">Price</th>
            <th scope="col">Amount</th>
            <th scope="col">Total</th>
            <th scope="col">Action</th>
          </tr>
        </thead>
        <tbody>
          {cart.cartItems.map((item) => (
            <OrderItem
              key={item.id}
              id={item.id}
              name={item.name}
              price={item.price}
              amount={item.amount}
              onDelete={() => cart.deleteItem(item)}
              onAmountChange={(amount: number) =>
                cart.updateAmount({ ...item, amount: amount })
              }
            />
          ))}
        </tbody>
      </table>
      <h4 className="mb-3">Total: {cart.getTotal()}</h4>
      <button className="btn btn-primary" onClick={handleCreateOrderClick}>
        Create order
      </button>
    </>
  );
}

export default NewOrder;
