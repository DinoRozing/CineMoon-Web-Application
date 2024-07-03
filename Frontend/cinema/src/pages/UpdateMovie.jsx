import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import AddMovieForm from "../components/AddMovieForm";
import MovieService from "../services/MovieService";
import ActorService from "../services/ActorService";
import GenreService from "../services/GenreService";
import LanguageService from "../services/LanguageService";

const UpdateMovie = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [movieData, setMovieData] = useState(null);
  const [actors, setActors] = useState([]);
  const [genres, setGenres] = useState([]);
  const [languages, setLanguages] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const movieResponse = await MovieService.getMovieById(id);
        setMovieData(movieResponse.data[0]);

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
  }, [id]);

  const handleUpdateMovie = async (updatedMovieData) => {
    try {
      await MovieService.updateMovie(id, updatedMovieData);
      alert("Movie updated successfully");
      navigate("/view-movies");
    } catch (error) {
      console.error("Error updating movie:", error);
      alert("Failed to update movie: " + error.message);
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

  if (!movieData) return <div>Loading...</div>;

  return (
    <div>
      <AddMovieForm
        initialData={movieData}
        actors={actors}
        genres={genres}
        languages={languages}
        onAddMovie={handleUpdateMovie}
        onAddNewActor={handleAddNewActor}
        isUpdate={true}
      />
    </div>
  );
};

export default UpdateMovie;
