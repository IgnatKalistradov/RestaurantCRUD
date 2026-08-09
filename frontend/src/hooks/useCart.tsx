import { createContext, useContext, useEffect, useState } from "react";

interface CartContextType {
  cartItems: ShoppingCartItem[];
  addItem: (item: ShoppingCartItem) => void;
  deleteItem: (item: ShoppingCartItem) => void;
  updateAmount: (item: ShoppingCartItem) => void;
  getTotal: () => number;
  clear: () => void;
}

const CartContext = createContext<CartContextType | undefined>(undefined);

export interface ShoppingCartItem {
  id: number;
  name: string;
  amount: number;
  price: number;
}

export default function CartProvider({
  children,
}: {
  children: React.ReactNode;
}) {
  const [cartItems, setCartItems] = useState<ShoppingCartItem[]>(() => {
    const existingCart = localStorage.getItem("cart");

    return existingCart ? (JSON.parse(existingCart) as ShoppingCartItem[]) : [];
  });

  useEffect(() => {
    localStorage.setItem("cart", JSON.stringify(cartItems));
  }, [cartItems]);

  const handleAddItem = (item: ShoppingCartItem) => {
    const index = cartItems.findIndex((i) => i.id === item.id);
    if (index !== -1) {
      const newCart = cartItems.map((i) => {
        return i.id === item.id ? { ...i, amount: i.amount + item.amount } : i;
      });

      setCartItems(newCart);
    } else {
      setCartItems((prev) => {
        return [...prev, item];
      });
    }
  };

  const handleDeleteItem = (item: ShoppingCartItem) => {
    setCartItems((prev) => {
      return prev.filter((i) => i.id !== item.id);
    });
  };

  const handleUpdateAmount = (item: ShoppingCartItem) => {
    setCartItems((prev) => {
      return prev.map((i) => {
        return i.id === item.id ? item : i;
      });
    });
  };

  const getTotal = () => {
    return cartItems.length > 0
      ? cartItems
          .map((item) => item.price * item.amount)
          .reduce((accumulator, currectValue) => {
            return accumulator + currectValue;
          })
      : 0;
  };

  const clear = () => {
    setCartItems([]);
  };

  return (
    <CartContext.Provider
      value={{
        cartItems: cartItems,
        addItem: handleAddItem,
        deleteItem: handleDeleteItem,
        updateAmount: handleUpdateAmount,
        getTotal: getTotal,
        clear: clear,
      }}
    >
      {children}
    </CartContext.Provider>
  );
}

export const useCart = () => {
  const context = useContext(CartContext);
  if (!context) throw new Error("Unproper use of CartProvider");
  return context;
};
