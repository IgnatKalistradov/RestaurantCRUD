import { Cart4 } from "react-bootstrap-icons";

function Navbar() {
  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary">
      <div className="container-fluid">
        <a className="navbar-brand" href="/">
          Restaurant
        </a>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <a className="nav-link" href="/">
                Menu
              </a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/ingredients">
                Ingredients
              </a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/categories">
                Categories
              </a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/orders">
                Orders
              </a>
            </li>
          </ul>
          <div className="d-flex">
            <a href="/new-order" className="link-dark">
              <Cart4 />
            </a>
          </div>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
