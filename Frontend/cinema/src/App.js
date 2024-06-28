import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Navbar from './components/Navbar';
import Movie from './pages/Movie';
import Projection from './pages/Projection'; 

function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route exact path="/" element={<Home />} />
        <Route exact path="/movie/:id" element={<Movie />} />
        <Route exact path="/addProjection" element={<Projection />} /> 
      </Routes>
    </Router>
  );
}

export default App;
