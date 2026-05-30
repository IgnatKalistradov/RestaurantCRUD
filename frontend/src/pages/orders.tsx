function Orders() {
  return (
    <>
      <h2>Orders</h2>
      <div className="card">
        <div className="card-body">
          <h5 className="card-title">Замовлення</h5>
          <h6 className="card-subtitle mb-2 text-body-secondary">
            Створено: 12:38 29.05.2026
          </h6>
          <p className="card-text">
            Страви:
            <ul>
              <li>Карбонара</li>
              <li>Карбонара</li>
              <li>Карбонара</li>
            </ul>
          </p>
          <button className="btn btn-outline-danger">Delete</button>
        </div>
      </div>
    </>
  );
}

export default Orders;
