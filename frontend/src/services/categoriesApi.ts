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

export default addCategory;