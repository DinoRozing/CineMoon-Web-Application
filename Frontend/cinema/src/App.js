import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Navbar from "./components/Navbar";
import Movie from "./pages/Movie";
import Projection from "./pages/Projection";
import ProjectionSelection from "./pages/ProjectionSelection";
import SeatSelection from "./pages/SeatSelection";
import AdminDashboard from "./pages/AdminDashboard";

function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route exact path="/" element={<Home />} />
        <Route exact path="/movie/:id" element={<Movie />} />
        <Route exact path="/addProjection" element={<Projection />} />
        <Route
          path="/select-projection/:movieId"
          element={<ProjectionSelection />}
        />
        <Route path="/select-seat/:projectionId" element={<SeatSelection />} />
        <Route path="/admin" element={<AdminDashboard />} />
      </Routes>
    </Router>
  );
}

export default App;
