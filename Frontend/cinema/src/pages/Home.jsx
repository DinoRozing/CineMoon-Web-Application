import React, { useState } from "react";

const Home = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const [genre, setGenre] = useState("");
  const [language, setLanguage] = useState("");

  const movies = [
    { title: "Movie 1", description: "Description 1", cover: "Cover Image" },
    { title: "Movie 2", description: "Description 2", cover: "Cover Image" },
    // Add more movie objects here
  ];

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleGenreChange = (e) => {
    setGenre(e.target.value);
  };

  const handleLanguageChange = (e) => {
    setLanguage(e.target.value);
  };

  const filteredMovies = movies.filter(
    (movie) =>
      movie.title.toLowerCase().includes(searchTerm.toLowerCase()) &&
      (genre ? movie.genre === genre : true) &&
      (language ? movie.language === language : true)
  );

  return (
    <div className="container mt-4">
      <div className="row mb-4">
        <div className="col-md-3">
          <select
            className="form-control"
            onChange={handleGenreChange}
            value={genre}
          >
            <option value="">Genre</option>
            <option value="Action">Action</option>
            <option value="Comedy">Comedy</option>
            <option value="Drama">Drama</option>
            {/* Add more genres here */}
          </select>
        </div>
        <div className="col-md-3">
          <select
            className="form-control"
            onChange={handleLanguageChange}
            value={language}
          >
            <option value="">Language</option>
            <option value="English">English</option>
            <option value="French">French</option>
            <option value="Spanish">Spanish</option>
            {/* Add more languages here */}
          </select>
        </div>
        <div className="col-md-6">
          <input
            type="text"
            className="form-control"
            placeholder="Search..."
            value={searchTerm}
            onChange={handleSearch}
          />
        </div>
      </div>
      <div className="row">
        {filteredMovies.map((movie, index) => (
          <div className="col-md-12 mb-4" key={index}>
            <div className="card">
              <div className="row no-gutters">
                <div className="col-md-8">
                  <div className="card-body">
                    <h5 className="card-title">{movie.title}</h5>
                    <p className="card-text">{movie.description}</p>
                    <a href="#" className="btn btn-primary">
                      More →
                    </a>
                  </div>
                </div>
                <div className="col-md-4">
                  <img
                    src={movie.cover}
                    className="card-img"
                    alt={movie.title}
                  />
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Home;
