import React, { useContext, useState } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
import { Context } from "../App";

const Login = ({ openRegisterModal, onSuccess }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [signedIn, setSignedIn] = useContext(Context);

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post("http://localhost:5058/user/login", {
        email,
        password,
      });
      alert("Login successful!");
      setSignedIn(true);
      localStorage.setItem("token", response.data);
      onSuccess(response.data);
    } catch (error) {
      setError("Invalid email or password. Please try again.");
    }
  };

  return (
    <div className="container mt-5">
      <h2>Login</h2>
      <form
        onSubmit={handleLogin}
        style={{ maxWidth: "400px", margin: "0 auto" }}
      >
        <div className="form-group">
          <label htmlFor="email">Email address</label>
          <input
            type="email"
            className="form-control"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="password">Password</label>
          <input
            type="password"
            className="form-control"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        {error && <p className="text-danger">{error}</p>}
        <button type="submit" className="btn btn-primary">
          Login
        </button>
        <p className="mt-3">
          Don't have an account?{" "}
          <Link to="#" onClick={openRegisterModal}>
            Register here
          </Link>
        </p>
      </form>
    </div>
  );
};

export default Login;
