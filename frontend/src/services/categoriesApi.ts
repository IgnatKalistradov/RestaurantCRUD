import type { ItemDetailsProps } from "../components/itemDetails";

const BASE_URL = `https://localhost:7174/api`;

export async function addCategory(name: string, description: string)
{
    const body = {
        name: name,
        description: description
    };
    const url = `${BASE_URL}/Category`;
    const options = {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(body)
    }

    const response = await fetch(url, options);

    return response.status;
}

export async function getCategories()
{
    const url = BASE_URL + "/category"
    const options = {
        method: `GET`,
        Headers: {
            'accept': 'application/json' 
        }
    };

    const response = await fetch(url, options);

    if(!response.ok)
    {
        throw new Error("Failed to fetch");
    }

    return response.json();
}

export async function getCategory(id: number)
{
    const url = `${BASE_URL}/Category/${id}`;
    const options = {
        method: "GET",
        headers: {
            "accept": "application/json"
        }
    }

    const result = await fetch(url, options);

    const json = await result.json();

    return {item: json.category, products: json.products} as ItemDetailsProps;
}

export async function deleteCategory(id: number)
{
    const url = `${BASE_URL}/Category/delete/${id}`;
    const options = {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
    };

    const result = await fetch(url, options);

    return result.status;
}
