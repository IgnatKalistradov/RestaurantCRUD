import { useState, type ChangeEvent } from "react";
import { useParams } from "react-router-dom";

interface AddPageProps {
  type: "ingredient" | "category";
  id?: string;
  name?: string;
  description?: string;
  onSubmit: (name: string, description: string, id?: string) => void;
}

function AddEditPage(props: AddPageProps) {
  const { id } = useParams();

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
    alert(id);
  };

  return (
    <>
      <h2>Add {props.type}</h2>
      <form>
        <div className="mb-3">
          <label className="form-label">Name</label>
          <input
            placeholder={name}
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
            placeholder={description}
            onChange={handleDescriptionChange}
          />
        </div>
        <button
          type="submit"
          className="btn btn-primary"
          onClick={handleFormSubmit}
        >
          Create
        </button>
      </form>
    </>
  );
}

export default AddEditPage;
