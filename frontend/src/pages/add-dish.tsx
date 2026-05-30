function AddDish() {
  return (
    <>
      <h2>Create dish</h2>
      <form>
        <div className="mb-3">
          <label className="form-label">Name</label>
          <input type="text" className="form-control" />
        </div>
        <div className="mb-3">
          <label className="form-label">Description</label>
          <input type="text" className="form-control" />
        </div>
        <div className="mb-3">
          <label className="form-label">Price</label>
          <input type="number" className="form-control" />
        </div>
        <div className="mb-3">
          <label className="form-label">Stock</label>
          <input type="number" className="form-control" />
        </div>
        <div className="mb-3">
          <label className="form-label">Category</label>
          <select className="form-select">
            <option selected></option>
            <option value={1}>М'ясо</option>
            <option value={2}>Овочі</option>
          </select>
        </div>
        <div className="mb-3">
          <label className="form-label">Ingredients</label>
          <div className="form-check">
            <input className="form-check-input" type="checkbox" />
            <label className="form-check-label">М'ясо</label>
          </div>
          <div className="form-check">
            <input className="form-check-input" type="checkbox" />
            <label className="form-check-label">Риба</label>
          </div>
          <div className="form-check">
            <input className="form-check-input" type="checkbox" />
            <label className="form-check-label">Овочі</label>
          </div>
        </div>
      </form>
    </>
  );
}

export default AddDish;
