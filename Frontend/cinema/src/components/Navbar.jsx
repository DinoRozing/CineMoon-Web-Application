import React, { useState, useEffect, useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import Modal from "react-modal";
import { jwtDecode } from "jwt-decode";
import Login from "./Login";
import Register from "./Register";
import { Context } from "../App";

Modal.setAppElement("#root");

const Navbar = () => {
  const [signedIn, setSignedIn] = useContext(Context);
  const [loginModalIsOpen, setLoginModalIsOpen] = useState(false);
  const [registerModalIsOpen, setRegisterModalIsOpen] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [userName, setUserName] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      try {
        const decodedToken = jwtDecode(token);
        const name =
          decodedToken[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
          ];
        if (name) {
          setUserName(name);
          setIsLoggedIn(true);
        } else {
          setIsLoggedIn(false);
        }
      } catch (error) {
        console.error("Error decoding JWT:", error);
        setIsLoggedIn(false);
      }
    } else {
      setIsLoggedIn(false);
    }
  }, []);

  const customModalStyles = {
    content: {
      top: "50%",
      left: "50%",
      right: "auto",
      bottom: "auto",
      marginRight: "-50%",
      transform: "translate(-50%, -50%)",
      maxWidth: "320px",
      padding: "20px",
    },
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    setIsLoggedIn(false);
    setSignedIn(false);
    setUserName("");
    navigate("/");
  };

  const handleLoginSuccess = (token) => {
    localStorage.setItem("token", token);
    setLoginModalIsOpen(false);
    setRegisterModalIsOpen(false);
    try {
      const decodedToken = jwtDecode(token);
      const name = decodedToken.Name;
      if (name) {
        setUserName(name);
        setIsLoggedIn(true);
      } else {
        setIsLoggedIn(false);
      }
    } catch (error) {
      console.error("Error decoding JWT:", error);
      setIsLoggedIn(false);
    }
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <Link className="navbar-brand" to="/">
        CineMoon
      </Link>
      <button
        className="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span className="navbar-toggler-icon"></span>
      </button>
      <div className="collapse navbar-collapse" id="navbarSupportedContent">
        <ul className="navbar-nav ml-auto">
          <li className="nav-item dropdown">
            <a
              className="nav-link dropdown-toggle"
              href="#"
              id="navbarDropdown"
              role="button"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
            >
              Menu
            </a>
            <div
              className="dropdown-menu dropdown-menu-right"
              aria-labelledby="navbarDropdown"
            >
              <Link className="dropdown-item" to="/addProjection">
                Payment History
              </Link>
            </div>
          </li>
          {!isLoggedIn && (
            <>
              <li className="nav-item">
                <a
                  className="nav-link"
                  href="#"
                  onClick={() => setLoginModalIsOpen(true)}
                >
                  Login
                </a>
              </li>
              <li className="nav-item">
                <a
                  className="nav-link"
                  href="#"
                  onClick={() => setRegisterModalIsOpen(true)}
                >
                  Register
                </a>
              </li>
            </>
          )}
          {isLoggedIn && (
            <>
              <li className="nav-item">
                <span className="nav-link">Hello, {userName}</span>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#" onClick={handleLogout}>
                  Logout
                </a>
              </li>
            </>
          )}
        </ul>
      </div>

      <Modal
        isOpen={loginModalIsOpen}
        onRequestClose={() => setLoginModalIsOpen(false)}
        style={customModalStyles}
        contentLabel="Login Modal"
      >
        <button onClick={() => setLoginModalIsOpen(false)}>Close</button>
        <Login
          openRegisterModal={() => {
            setLoginModalIsOpen(false);
            setRegisterModalIsOpen(true);
          }}
          onSuccess={handleLoginSuccess}
        />
      </Modal>

      <Modal
        isOpen={registerModalIsOpen}
        onRequestClose={() => setRegisterModalIsOpen(false)}
        style={customModalStyles}
        contentLabel="Register Modal"
      >
        <button onClick={() => setRegisterModalIsOpen(false)}>Close</button>
        <Register
          openLoginModal={() => {
            setRegisterModalIsOpen(false);
            setLoginModalIsOpen(true);
          }}
          onSuccess={handleLoginSuccess} // Pass onSuccess prop to Register component
        />
      </Modal>
    </nav>
  );
};

export default Navbar;
