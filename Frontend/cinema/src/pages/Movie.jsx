import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import MovieService from "../services/MovieService";

const Movie = () => {
  const { movieId } = useParams();
  const navigate = useNavigate();

  const [movie, setMovie] = useState(null);

  useEffect(() => {
    const fetchMovie = async () => {
      try {
        const response = await MovieService.getMovieById(movieId);
        setMovie(response.data);
        console.log(movie);
      } catch (error) {
        console.error("Error fetching movie:", error);
      }
    };

    fetchMovie();
  }, [movieId]);

  if (!movie) {
    return <div>Loading...</div>;
  }

  return <div>{movie.title}</div>;
};

export default Movie;

// <div className="container mt-4">
//   <h1>{movie.title}</h1>
//   <div className="row">
//     <div className="col-md-8">
//       <div className="card">
//         <div className="card-body">
//           <h5 className="card-title">{movie.title}</h5>
//           <p className="card-text">{movie.description}</p>
//           <p className="card-text">
//             <strong>Genre:</strong> {movie.genre}
//           </p>
//           <p className="card-text">
//             <strong>Language:</strong> {movie.language}
//           </p>
//         </div>
//       </div>
//     </div>
//     <div className="col-md-4">
//       <img src={movie.coverUrl} className="img-fluid" alt={movie.title} />
//     </div>
//   </div>
//   <div className="mt-4">
//     <button className="btn btn-primary" onClick={() => navigate("/movies")}>
//       Back to Movies
//     </button>
//   </div>
// </div>
