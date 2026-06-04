import type { ProductDetails, ProductInfo } from "../types/product";

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

export async function deleteDish(id: number)
{
    const url = `${BASE_URL}/product/delete/${id}`;
    const options = {
        method: "POST"
    }

    const result = await fetch(url, options);

    return result.status;
}

export async function getDish(id: number)
{
    const url = `${BASE_URL}/product/${id}`;

    const options = {
        method: "GET",
        headers: {
            'accept': 'application/json'
        }
    }

    const response = await fetch(url, options);

    const json = await response.json();

    return json as ProductDetails;
}

export async function updateDish(id: number, name: string, description: string, price: number, stock: number, categoryId: number, ingredientIds: number[]) {
    const url = `${BASE_URL}/product/edit/${id}`;

    const body = {
        id: id,
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
        body: JSON.stringify(body)
    };

    const result = await fetch(url, options);

    return result.status;
}