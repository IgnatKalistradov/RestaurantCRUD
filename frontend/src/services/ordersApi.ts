import type { ShoppingCartItem } from "../hooks/useCart";

const BASE_URL = import.meta.env.VITE_API_URL;

export async function createOrder(cartItems: ShoppingCartItem[])
{
    const url = `${BASE_URL}/order`
    const options = {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(cartItems)
    }

    const result = await fetch(url, options);

    return result.status;
}

export async function getOrders()
{
    const url = `${BASE_URL}/order`;
    const options = {
        method: "GET",
        headers: {
            "accept": "application/json"
        }
    };

    const result = await fetch(url, options);

    return await result.json();
}

export async function deleteOrder(id: number) {
    const url = `${BASE_URL}/order/delete/${id}`;

    const options = {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        }
    };

    const result = await fetch(url, options);
    return result.status;
}