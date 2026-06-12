import { useState } from "react";
import type { Item } from "../types/item";
import type { DishInfo } from "../types/product";

interface DishFormProps {
  name?: string;
  description?: string;
  price?: number;
  stock?: number;
  dishCategoryId?: number;
  dishIngredientIds?: number[];
  categories: Item[];
  ingredients: Item[];
  onSubmit: (
    name: string,
    description: string,
    price: number,
    stock: number,
    categoryId: number,
    ingredientIds: number[],
  ) => void;
}

function DishForm(props: DishFormProps) {
  const [name, setName] = useState(props.name ? props.name : "");
  const [description, setDescription] = useState(
    props.description ? props.description : "",
  );
  const [price, setPrice] = useState(props.price ? props.price : 0);
  const [stock, setStock] = useState(props.stock ? props.stock : 0);
  const [choosedCategory, setChoosedCategory] = useState(
    props.dishCategoryId ?? 1,
  );
  const [choosedIngredients, setChoosedIngredients] = useState<number[]>(
    props.dishIngredientIds ? props.dishIngredientIds : [],
  );

  const handleCategoryChange = (
    event: React.ChangeEvent<HTMLSelectElement, HTMLSelectElement>,
  ) => {
    event.preventDefault();
    console.log(`Selected new category ${event.target.value}`);
    setChoosedCategory(Number(event.target.value));
  };

  const handleIngredientCheck = (
    event: React.ChangeEvent<HTMLInputElement, HTMLInputElement>,
    id: number,
  ) => {
    if (event.target.checked) {
      setChoosedIngredients((prev) => [...prev, id]);
    } else {
      setChoosedIngredients((prev) => prev.filter((prev) => prev !== id));
    }
    console.log(choosedIngredients);
  };

  const handleSubmitClick = (event: React.SubmitEvent<HTMLFormElement>) => {
    event.preventDefault();
    props.onSubmit(
      name,
      description,
      price,
      stock,
      choosedCategory,
      choosedIngredients,
    );
  };

  return (
    <form onSubmit={(e) => handleSubmitClick(e)}>
      <div className="mb-3">
        <label className="form-label">Name</label>
        <input
          type="text"
          className="form-control"
          value={name}
          onChange={(e) => {
            setName(e.target.value);
          }}
        />
      </div>
      <div className="mb-3">
        <label className="form-label">Description</label>
        <input
          type="text"
          className="form-control"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
      </div>
      <div className="mb-3">
        <label className="form-label">Price</label>
        <input
          type="number"
          className="form-control"
          value={price}
          onChange={(e) => setPrice(Number(e.target.value))}
        />
      </div>
      <div className="mb-3">
        <label className="form-label">Stock</label>
        <input
          type="number"
          className="form-control"
          value={stock}
          onChange={(e) => setStock(Number(e.target.value))}
        />
      </div>
      <div className="mb-3">
        <label className="form-label">Category</label>
        <select
          className="form-select"
          value={choosedCategory}
          onChange={(e) => handleCategoryChange(e)}
        >
          {props.categories.map((cat) => (
            <option key={cat.id} value={cat.id}>
              {cat.name}
            </option>
          ))}
        </select>
      </div>
      <div className="mb-3">
        <label className="form-label">Ingredients</label>
        {props.ingredients.map((ing) => (
          <IngredientOption
            key={ing.id}
            id={ing.id}
            name={ing.name}
            isChecked={choosedIngredients.some((id) => id === ing.id) ?? false}
            onCheck={handleIngredientCheck}
          />
        ))}
      </div>
      <button type="submit" className="btn btn-primary">
        Submit
      </button>
    </form>
  );
}

interface IngredientOptionProps {
  id: number;
  name: string;
  isChecked: boolean;
  onCheck: (
    event: React.ChangeEvent<HTMLInputElement, HTMLInputElement>,
    id: number,
  ) => void;
}

function IngredientOption({
  id,
  name,
  isChecked,
  onCheck,
}: IngredientOptionProps) {
  return (
    <div className="form-check">
      <input
        className="form-check-input"
        type="checkbox"
        checked={isChecked}
        onChange={(e) => onCheck(e, id)}
      />
      <label className="form-check-label">{name}</label>
    </div>
  );
}

export default DishForm;
