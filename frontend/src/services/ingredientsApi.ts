import type { ItemDetailsProps } from "../components/itemDetails";

const BASE_URL = import.meta.env.VITE_API_URL;

export async function addIngredient(name: string, description: string)
{
    const url = BASE_URL + '/Ingredient';
    const payload = {
        name: name,
        description: description
    }
    const options = {
        method: `POST`,
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload)
    };

    const response = await fetch(url, options);

    return response.status;
}

export async function getIngredients()
{
    const url = BASE_URL + '/Ingredient'
    const options = {
        method: `GET`,
        headers: {
            accept: `application/json`
        }
    }
    const response = await fetch(url, options);
    if(!response.ok)
    {
        throw new Error(`Response status: ${response.status}`);
    }

    const json = await response.json()

    return json;

}

export async function deleteIngredient(id: number)
{
    const url = `${BASE_URL}/Ingredient/delete/${id}`;
    const options = {
        method: "POST",
        headers: {
            'Content-Type': "application/json"
        },
    };

    const response = await fetch(url, options);

    return response.status;
}

export async function getIngredient(id: number)
{
    const url = `${BASE_URL}/Ingredient/${id}`;
    const options = {
        method: "GET",
        headers: {
            'accept': "application/json"
        }
    }

    const response = await fetch(url, options);

    const json = await response.json();

    return {item: json.ingredient, dishes: json.dishes} as ItemDetailsProps
}

export async function updateIngredient(id: number, name: string, description: string)
{
    const requestBody = {
        id: id,
        name: name,
        description: description
    };

    const url = `${BASE_URL}/Ingredient/edit/${id}`;
    const options = {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
    };

    const result = await fetch(url, options);

    return result.status;
}