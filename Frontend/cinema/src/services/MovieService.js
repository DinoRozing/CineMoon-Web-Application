import http from "../http.common";

const getFilteredMovies = (filters) => {
  const {
    genreId,
    languageId,
    searchTerm,
    sortBy,
    sortOrder,
    pageNumber,
    pageSize,
  } = filters;

  let url = `/movie?sortBy=${sortBy}&sortOrder=${sortOrder}&pageNumber=${pageNumber}&pageSize=${pageSize}`;

  if (genreId) {
    url += `&genreId=${genreId}`;
  }
  if (languageId) {
    url += `&languageId=${languageId}`;
  }
  if (searchTerm) {
    url += `&searchTerm=${searchTerm}`;
  }

  return http.get(url);
};

const getMovieById = (id) => {
  return http.get(`/movie?movieId=${id}`);
};

const addMovie = (movieData) => {
  return http.post(`/movie`, movieData);
};

const MovieService = {
  getFilteredMovies,
  getMovieById,
  addMovie,
};

export default MovieService;
