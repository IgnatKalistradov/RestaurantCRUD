import type { Item } from "./item"

export interface ProductBase
{
    id: number,
    name: string,
}

export interface ProductInfo
{
    id: number,
    name: string,
    description: string,
    price: number,
    stock: number
}

export interface ProductDetails
{
    id: number,
    name: string,
    description: string,
    price: number,
    stock: number,
    category: Item,
    ingredients: Item[]
}