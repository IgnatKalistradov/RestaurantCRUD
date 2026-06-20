import { useState, type ChangeEvent } from "react";

interface AddPageProps {
  name?: string;
  description?: string;
  isButtonDisabled: boolean;
  onSubmit: (name: string, description: string) => void;
}

function IngredientCategoryForm(props: AddPageProps) {
  const [name, setName] = useState(props.name === undefined ? "" : props.name);
  const handleNameChange = (
    event: ChangeEvent<HTMLInputElement, HTMLInputElement>,
  ) => {
    setName(event.target.value);
  };

  const [description, setDescription] = useState(
    props.description === undefined ? "" : props.description,
  );
  const handleDescriptionChange = (
    event: ChangeEvent<HTMLInputElement, HTMLInputElement>,
  ) => {
    setDescription(event.target.value);
  };

  const handleFormSubmit = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
  ) => {
    event.preventDefault();
    props.onSubmit(name, description);
  };

  return (
    <form>
      <div className="mb-3">
        <label className="form-label">Name</label>
        <input
          value={name}
          type="text"
          className="form-control"
          onChange={handleNameChange}
        />
      </div>
      <div className="mb-3">
        <label className="form-label">Description</label>
        <input
          type="text"
          className="form-control"
          value={description}
          onChange={handleDescriptionChange}
        />
      </div>
      <button
        type="submit"
        className="btn btn-primary"
        disabled={props.isButtonDisabled}
        onClick={handleFormSubmit}
      >
        Submit
      </button>
    </form>
  );
}

export default IngredientCategoryForm;
