function NewOrder() {
  return (
    <>
      <h2>New Order</h2>
      <table className="table table-striped">
        <thead>
          <tr>
            <th scope="col">Name</th>
            <th scope="col">Price</th>
            <th scope="col">Amount</th>
            <th scope="col">Total</th>
            <th scope="col">Action</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Карбонара</td>
            <td>100</td>
            <td>2</td>
            <td>200</td>
            <td>
              <button type="button" className="btn btn-outline-danger">
                Delete
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </>
  );
}

export default NewOrder;
