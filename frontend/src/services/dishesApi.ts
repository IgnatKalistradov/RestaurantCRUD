import type { ProductInfo } from "../types/product";

const BASE_URL = "https://localhost:7174/api";

export async function getDishes() {
    const url = `${BASE_URL}/product`;
    const options = {
        method: "GET",
        headers: {
            'accept': 'application/json'
        }
    };

    const result = await fetch(url, options);

    const json = await result.json();

    return json as ProductInfo[];
}

export async function addDish(name: string, description: string, price: number, stock: number, categoryId: number, ingredientIds: number[])
{
    const url = `${BASE_URL}/product`;
    const jsonBody = {
        name: name,
        description: description,
        price: price,
        stock: stock,
        categoryId: categoryId,
        ingredientIds: ingredientIds
    };
    const options = {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(jsonBody)
    };

    const response = await fetch(url, options);

    return response.status;
}