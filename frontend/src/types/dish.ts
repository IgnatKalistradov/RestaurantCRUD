import type { Item } from "./item";

export interface DishBase {
  id: number;
  name: string;
}

export interface DishInfo {
  id: number;
  name: string;
  description: string;
  price: number;
  stock: number;
}

export interface DishDetails {
  id: number;
  name: string;
  description: string;
  price: number;
  stock: number;
  category: Item;
  ingredients: Item[];
}
