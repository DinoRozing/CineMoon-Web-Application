import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Navbar from "./components/Navbar";
import Movie from "./pages/Movie";
import Projection from "./pages/Projection";
import ProjectionSelection from "./pages/ProjectionSelection";
import SeatSelection from "./pages/SeatSelection";
import AdminDashboard from "./pages/AdminDashboard";
import AddMovie from "./pages/AddMovie";
import AddActor from "./pages/AddActor";
import DeleteActor from "./pages/DeleteActor";
import Payment from "./pages/Payment";

function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route exact path="/" element={<Home />} />
        <Route exact path="/movie/:id" element={<Movie />} />
        <Route
          path="/select-projection/:movieId"
          element={<ProjectionSelection />}
        />
        <Route path="/select-seat/:projectionId" element={<SeatSelection />} />
        <Route path="/admin" element={<AdminDashboard />} />
        <Route exact path="/add-movie" element={<AddMovie />} />
        <Route exact path="/add-actor" element={<AddActor />} />
        <Route exact path="/delete-actor" element={<DeleteActor />} />
        <Route exact path="/add-projection" element={<Projection />} />
        <Route path="/payment/:projectionId" element={<Payment />} />
      </Routes>
    </Router>
  );
}

export default App;
