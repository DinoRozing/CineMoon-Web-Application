import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import MovieService from "../services/MovieService";
import GenreService from "../services/GenreService";
import LanguageService from "../services/LanguageService";

function Home() {
  const [movies, setMovies] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [filters, setFilters] = useState({
    genreId: "",
    languageId: "",
    sortBy: "Duration",
    sortOrder: "DESC",
    pageNumber: 1,
    pageSize: 4,
  });
  const navigate = useNavigate();

  const [genres, setGenres] = useState([]);
  const [languages, setLanguages] = useState([]);

  useEffect(() => {
    const fetchGenresAndLanguages = async () => {
      try {
        const genreResponse = await GenreService.getAllGenres();
        setGenres(genreResponse.data);

        const languageResponse = await LanguageService.getAllLanguages();
        setLanguages(languageResponse.data);
      } catch (error) {
        console.error("Error fetching genres and languages:", error);
      }
    };

    fetchGenresAndLanguages();
  }, []);

  useEffect(() => {
    fetchMovies();
  }, [filters]);

  const fetchMovies = () => {
    MovieService.getFilteredMovies(filters)
      .then((response) => {
        setMovies(response.data);
      })
      .catch((error) => {
        console.error("Error fetching movies:", error);
      });
  };

  const handleSearchChange = (e) => {
    const value = e.target.value;
    setSearchTerm(value);
    setFilters({
      ...filters,
      searchTerm: value,
    });
  };

  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters({
      ...filters,
      [name]: value,
    });
  };

  const handleMovieClick = (movieId) => {
    navigate(`/movie/${movieId}`);
  };

  return (
    <div className="container mt-5">
      <h1>Movies</h1>
      <div className="row mb-4">
        <div className="col-md-6">
          <input
            type="text"
            className="form-control"
            placeholder="Search..."
            value={searchTerm}
            onChange={handleSearchChange}
          />
        </div>
        <div className="col-md-3">
          <select
            className="form-control"
            name="genreId"
            value={filters.genreId}
            onChange={handleFilterChange}
          >
            <option value="">Genre</option>
            {genres.map((genre) => (
              <option key={genre.id} value={genre.id}>
                {genre.name}
              </option>
            ))}
          </select>
        </div>
        <div className="col-md-3">
          <select
            className="form-control"
            name="languageId"
            value={filters.languageId}
            onChange={handleFilterChange}
          >
            <option value="">Language</option>
            {languages.map((language) => (
              <option key={language.id} value={language.id}>
                {language.name}
              </option>
            ))}
          </select>
        </div>
      </div>
      <div className="row">
        {movies.length === 0 ? (
          <div className="col-12">
            <p>No movies found...</p>
          </div>
        ) : (
          movies.map((movie) => (
            <div className="col-md-4 mb-4" key={movie.movieId}>
              <div className="card">
                <img
                  src={movie.coverUrl}
                  className="card-img-top"
                  alt={movie.title}
                />
                <div className="card-body">
                  <h5 className="card-title">{movie.title}</h5>
                  <p className="card-text">{movie.description}</p>
                  <button
                    onClick={() => handleMovieClick(movie.movieId)}
                    className="btn btn-primary"
                  >
                    More →
                  </button>
                </div>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  );
}

export default Home;
