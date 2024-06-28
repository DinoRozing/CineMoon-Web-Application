import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import MovieService from "../services/MovieService";
import "../App.css";

const Movie = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [movie, setMovie] = useState(null);
  const [rating, setRating] = useState(0);
  const [hover, setHover] = useState(0);

  useEffect(() => {
    const fetchMovie = async () => {
      try {
        const response = await MovieService.getMovieById(id);
        setMovie(response.data);
      } catch (error) {
        console.error("Error fetching movie:", error);
      }
    };

    fetchMovie();
  }, [id]);

  if (!movie) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <div className="container mt-4">
        <h1>{movie[0].title}</h1>
        <div className="row">
          <div className="col-md-8">
            <div className="card">
              <div className="card-body">
                <h5 className="card-title">{movie[0].title}</h5>
                <p className="card-text">{movie[0].description}</p>
                <p className="card-text">
                  <strong>Genre:</strong> {movie[0].genre}
                </p>
                <p className="card-text">
                  <strong>Language:</strong> {movie[0].language}
                </p>
                <p className="card-text">
                  <strong>Duration:</strong> {movie[0].duration} min
                </p>
                <p className="card-text">
                  <strong>Actors:</strong> {movie[0].actorNames.join(", ")}
                </p>
              </div>
            </div>
          </div>
          <div className="col-md-4">
            <img
              src={movie[0].coverUrl}
              className="img-fluid"
              alt={movie[0].title}
            />
          </div>
        </div>
      </div>
      <div className="star-rating">
        {[...Array(5)].map((star, index) => {
          index += 1;
          return (
            <button
              type="button"
              key={index}
              className={index <= (hover || rating) ? "on" : "off"}
              onClick={() => setRating(index)}
              onMouseEnter={() => setHover(index)}
              onMouseLeave={() => setHover(rating)}
            >
              <span className="star">&#9733;</span>
            </button>
          );
        })}
      </div>
    </div>
  );
};

export default Movie;
