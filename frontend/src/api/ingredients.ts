const BASE_URL = 'http://localhost:5164';

function addIngredient(name: string, description: string)
{
    alert(`Add ingredient ${name}: ${description}`);
}

export async function getIngredients()
{
    const url = BASE_URL + '/api/ingredient'
    const options = {
        method: `GET`,
        headers: {
            accept: `application/json`
        }
    }
    try{
        const response = await fetch(url, options);
        if(!response.ok)
        {
            throw new Error(`Response status: ${response.status}`);
        }

        const json = await response.json()

        return json;
    }
    catch(e: any)
    {
        console.log(e.message);
    }

}

export default addIngredient;