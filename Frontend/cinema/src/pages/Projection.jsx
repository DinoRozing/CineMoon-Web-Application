import React, { useState, useEffect } from 'react';
import ProjectionService from '../services/ProjectionService';
import MovieService from '../services/MovieService';
import HallService from '../services/HallService';
import "../App.css";

const AddProjection = () => {
  const [projection, setProjection] = useState({
    date: '',
    time: '', // Ensure time is stored in HH:mm:ss format
    movieId: '', 
    hall: ''
  });

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
    if (name === 'time') {
      const formattedTime = value + ':00';
      setProjection({ ...projection, [name]: formattedTime });
    } else {
      setProjection({ ...projection, [name]: value });
    }
  };

  console.log(projection.time)

  const handleSubmit = (e) => {
    e.preventDefault();
    ProjectionService.addProjection(projection)
      .then(response => {
        console.log("Projection added:", response.data);
      })
      .catch(e => {
        console.error(e);
      });
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
              value={projection.time.slice(0, 8)} 
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
                <option key={movie.id} value={movie.id}>
                  {movie.title}
                </option>
              ))}
            </select>
          </div>
          <div className="form-group">
            <label htmlFor="hall">Hall:</label>
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
                  {hall.name}
                </option>
              ))}
            </select>
          </div>
          
          <button type="submit" className="btn btn-primary">
            Add projection
          </button>
        </form>
      </div>
    </>
  );
};

export default AddProjection;
