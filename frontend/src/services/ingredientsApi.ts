const BASE_URL = 'https://localhost:7174';

export async function addIngredient(name: string, description: string)
{
    const url = BASE_URL + '/api/Ingredient';
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
    const url = BASE_URL + '/api/Ingredient'
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
    const url = `${BASE_URL}/api/Ingredient/delete/${id}`;
    const options = {
        method: "POST",
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify(id)
    };

    const response = await fetch(url, options);

    return response.status;
}

export default addIngredient;