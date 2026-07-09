import type { DishDetails, DishInfo } from "../types/dish";

const BASE_URL = import.meta.env.VITE_API_URL;

export async function getDishes() {
  const url = `${BASE_URL}/dish`;
  const options = {
    method: "GET",
    headers: {
      accept: "application/json",
    },
  };

  const result = await fetch(url, options);

  const json = await result.json();

  return json as DishInfo[];
}

export async function addDish(
  name: string,
  description: string,
  price: number,
  stock: number,
  categoryId: number,
  ingredientIds: number[],
  image: File | null,
) {
  const url = `${BASE_URL}/dish`;
  const formData = new FormData();

  formData.append("name", name);
  formData.append("description", description);
  formData.append("price", price.toString());
  formData.append("stock", stock.toString());
  formData.append("categoryId", categoryId.toString());
  ingredientIds.forEach((id) =>
    formData.append("ingredientIds", id.toString()),
  );
  if (image) {
    formData.append("image", image);
  }

  const options = {
    method: "POST",
    body: formData,
  };

  const response = await fetch(url, options);

  return response.status;
}

export async function deleteDish(id: number) {
  const url = `${BASE_URL}/dish/delete/${id}`;
  const options = {
    method: "POST",
  };

  const result = await fetch(url, options);

  return result.status;
}

export async function getDish(id: number) {
  const url = `${BASE_URL}/dish/${id}`;

  const options = {
    method: "GET",
    headers: {
      accept: "application/json",
    },
  };

  const response = await fetch(url, options);

  const json = await response.json();

  return json as DishDetails;
}

export async function updateDish(
  id: number,
  name: string,
  description: string,
  price: number,
  stock: number,
  categoryId: number,
  ingredientIds: number[],
  image: File | null,
) {
  const url = `${BASE_URL}/dish/edit/${id}`;

  const formData = new FormData();
  formData.append("id", id.toString());
  formData.append("name", name);
  formData.append("description", description);
  formData.append("price", price.toString());
  formData.append("stock", stock.toString());
  formData.append("categoryId", categoryId.toString());
  ingredientIds.forEach((id) =>
    formData.append("ingredientIds", id.toString()),
  );
  if (image) {
    formData.append("image", image);
  }

  const options = {
    method: "POST",
    body: formData,
  };

  const result = await fetch(url, options);

  return result.status;
}
