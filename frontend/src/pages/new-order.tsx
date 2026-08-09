import { useEffect } from "react";
import OrderItem from "../components/orderItem";
import { useCart, type ShoppingCartItem } from "../hooks/useCart";
import { createOrder } from "../services/ordersApi";
import { useNavigate } from "react-router-dom";
import useForm from "../hooks/useForm";

function NewOrder() {
  const cart = useCart();
  const navigate = useNavigate();

  useEffect(() => {
    if (cart.cartItems.length === 0) navigate("/");
  }, [cart.cartItems]);

  const validateCart = (values: ShoppingCartItem[]) => {
    if (values.length < 1) {
      throw new Error("Cart is empty");
    }
  };

  const { submitForm, isSubmitting, error } = useForm<ShoppingCartItem[]>({
    formSubmit: async (params) => {
      const status = await createOrder(params);
      if (status != 204) throw new Error("Failed to create order");
    },
    onSuccess: () => navigate("/orders"),
    formValidation: validateCart,
  });

  const handleCreateOrderClick = async () => {
    submitForm(cart.cartItems);
    cart.clear();
  };

  return (
    <>
      <h2>New Order</h2>
      {error && <div className="alert alert-danger">{error.message}</div>}
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
      <button
        className="btn btn-primary"
        onClick={handleCreateOrderClick}
        disabled={isSubmitting}
      >
        Create order
      </button>
    </>
  );
}

export default NewOrder;
