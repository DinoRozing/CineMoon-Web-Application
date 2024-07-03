import React, { useState, useEffect } from 'react';
import ProjectionService from '../services/ProjectionService';
import MovieService from '../services/MovieService';
import HallService from '../services/HallService';
import "../App.css";
import { Link } from 'react-router-dom';

const AddProjection = () => {
  const initialProjectionState = {
    date: '',
    time: '',
    movieId: '',
    hall: '',
    userId: '8583110f-f633-45bb-8a3d-8647922b09ed'
  };

  const [projection, setProjection] = useState(initialProjectionState);
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [filters, setFilters] = useState({
    genreId: "",
    languageId: "",
    sortBy: "Duration",
    sortOrder: "DESC",
    pageNumber: 1,
    pageSize: 4,
  });

  const [availableHalls, setAvailableHalls] = useState([]);
  const [loadingHalls, setLoadingHalls] = useState(false);
  const [errorHalls, setErrorHalls] = useState(null);
  const [successMessage, setSuccessMessage] = useState('');

  useEffect(() => {
    const fetchFilteredMovies = async () => {
      try {
        const response = await MovieService.getFilteredMovies(filters);
        setMovies(response.data);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching filtered movies:", error);
        setError("Error fetching movies. Please try again later.");
        setLoading(false);
      }
    };

    fetchFilteredMovies();
  }, [filters]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setProjection((prev) => ({
      ...prev,
      [name]: name === 'time' ? value + ':00' : value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await ProjectionService.addProjection(projection);
      console.log("Projection added:", response.data);
      setSuccessMessage('Projection added successfully!');
      setProjection(initialProjectionState); 
      setTimeout(() => {
        setSuccessMessage('');
      }, 2500); 
    } catch (error) {
      console.error("Error adding projection:", error);
      if (error.response) {
        console.error("Response data:", error.response.data);
      }
      setError("Failed to add projection. Please check the data and try again.");
    }
  };
  
  useEffect(() => {
    const fetchAvailableHalls = async () => {
      if (projection.date && projection.time && projection.movieId) {
        setLoadingHalls(true);
        try {
          const response = await HallService.getAvailableHalls(projection.date, projection.time, projection.movieId);
          setAvailableHalls(response.data);
          setLoadingHalls(false);
        } catch (error) {
          console.error("Error fetching available halls:", error);
          setErrorHalls("Error fetching available halls. Please try again later.");
          setLoadingHalls(false);
        }
      }
    };
  
    fetchAvailableHalls();
  }, [projection.date, projection.time, projection.movieId]);
  
  return (
    <>
      <div className="container mt-4">
        <h2 className="text-center mb-4">Add new projection</h2>
        {successMessage && (
          <div className="alert alert-success">
            {successMessage}
          </div>
        )}
        <form id="projectionForm" onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="date">Date:</label>
            <input
              type="date"
              className="form-control"
              id="date"
              name="date"
              value={projection.date}
              onChange={handleChange}
              required
            />
          </div>
          <div className="form-group">
            <label htmlFor="time">Time:</label>
            <input
              type="time"
              className="form-control"
              id="time"
              name="time"
              value={projection.time.slice(0, 5)}
              onChange={handleChange}
              required
            />
          </div>
          <div className="form-group">
            <label htmlFor="movieId">Movie:</label>
            <select
              className="form-control"
              id="movieId"
              name="movieId"
              value={projection.movieId}
              onChange={handleChange}
              required
            >
              <option value="">Select a movie...</option>
              {movies.map((movie) => (
                <option key={movie.movieId} value={movie.movieId}>
                  {movie.title}
                </option>
              ))}
            </select>
          </div>
          <div className="form-group">
            <label htmlFor="hall">Hall:</label>
            {(projection.date && projection.time && projection.movieId) ? (
              loadingHalls ? (
                <p>Loading halls...</p>
              ) : availableHalls.length === 0 ? (
                <p>No available halls for selected date, time, and movie.</p>
              ) : (
                <select
                  className="form-control"
                  id="hall"
                  name="hall"
                  value={projection.hall}
                  onChange={handleChange}
                  required
                >
                  <option value="">Select a hall...</option>
                  {availableHalls.map((hall) => (
                    <option key={hall.id} value={hall.id}>
                      Hall {hall.hallNumber}
                    </option>
                  ))}
                </select>
              )
            ) : (
              <p>Please select date, time, and movie first.</p>
            )}
          </div>
          
          <button type="submit" className="btn btn-primary">
            Add projection
          </button>

          <Link to="/manage-projections" className="btn btn-secondary ml-2">
            Manage projections
          </Link>
        </form>
        {errorHalls && <div className="alert alert-danger mt-3">{errorHalls}</div>}
      </div>
    </>
  );
};

export default AddProjection;
