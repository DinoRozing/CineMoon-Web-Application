import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import AddMovieForm from "../components/AddMovieForm";
import MovieService from "../services/MovieService";
import ActorService from "../services/ActorService";
import GenreService from "../services/GenreService";
import LanguageService from "../services/LanguageService";

const AddMovie = () => {
  const [actors, setActors] = useState([]);
  const [genres, setGenres] = useState([]);
  const [languages, setLanguages] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const actorsResponse = await ActorService.getAllActors();
        setActors(actorsResponse.data);

        const genresResponse = await GenreService.getAllGenres();
        setGenres(genresResponse.data);

        const languagesResponse = await LanguageService.getAllLanguages();
        setLanguages(languagesResponse.data);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  const handleAddMovie = async (movieData) => {
    movieData.createdByUserId = "8583110f-f633-45bb-8a3d-8647922b09ed";
    movieData.updatedByUserId = "8583110f-f633-45bb-8a3d-8647922b09ed";

    try {
      await MovieService.addMovie(movieData);
      alert("Movie added successfully");
      navigate("/admin");
    } catch (error) {
      console.error("Error adding movie:", error);
      alert("Failed to add movie: " + error.message);
    }
  };

  const handleAddNewActor = async (actorName) => {
    try {
      const actorData = {
        name: actorName,
        isActive: true,
        createdByUserId: "8583110f-f633-45bb-8a3d-8647922b09ed",
        updatedByUserId: "8583110f-f633-45bb-8a3d-8647922b09ed",
      };
      const response = await ActorService.addActor(actorData);
      const newActorId = response.data.id;

      const actorsResponse = await ActorService.getAllActors();
      setActors(actorsResponse.data);

      return newActorId;
    } catch (error) {
      console.error("Error adding actor:", error);
      alert("Failed to add actor: " + error.message);
      return null;
    }
  };

  return (
    <div>
      <AddMovieForm
        actors={actors}
        genres={genres}
        languages={languages}
        onAddMovie={handleAddMovie}
        onAddNewActor={handleAddNewActor}
      />
    </div>
  );
};

export default AddMovie;
