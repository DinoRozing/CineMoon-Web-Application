import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import MovieService from "../services/MovieService";
import ReviewService from "../services/ReviewService";
import "../App.css";
import StarDisplay from "../components/StarDisplay";

const Movie = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [movie, setMovie] = useState(null);
  const [rating, setRating] = useState(0);
  const [hover, setHover] = useState(0);
  const [reviews, setReviews] = useState([]);
  const [newReview, setNewReview] = useState("");

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

  useEffect(() => {
    const fetchReviews = async () => {
      try {
        const response = await ReviewService.getReviewById(id);
        setReviews(response.data);
      } catch (error) {
        console.error("Error fetching movie:", error);
      }
    };
    fetchReviews();
  });

  const handleReviewChange = (e) => {
    const { name, value } = e.target;
    setNewReview({
      ...newReview,
      [name]: value,
    });
  };

  const handleRatingChange = (newRating) => {
    setNewReview({
      ...newReview,
      rating: newRating,
    });
  };

  const handleReviewSubmit = () => {
    setReviews([...reviews, newReview]);
  };

  const handleBuyTicket = () => {
    alert("Ticket purchase functionality to be implemented.");
  };

  const calculateAverageRating = () => {
    if (reviews.length === 0) {
      return 0;
    }

    const totalRating = reviews.reduce((acc, curr) => acc + curr.rating, 0);
    return (totalRating / reviews.length).toFixed(1);
  };

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
                <p className="card-text">
                  <strong>Rating:</strong> {calculateAverageRating()}
                </p>
                <button
                  className="btn btn-success mt-3"
                  onClick={handleBuyTicket}
                >
                  Buy Ticket
                </button>
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
        <div className="mt-4">
          <h2>Add a Review</h2>
          <form className="form-group">
            <div className="form-group">
              <label htmlFor="comment">Comment:</label>
              <textarea
                className="form-control"
                id="comment"
                name="comment"
                value={newReview}
                onChange={(e) => {
                  setNewReview(e.target.value);
                }}
              ></textarea>
            </div>

            <label>Rating:</label>
            <div className="star-rating">
              {[...Array(5)].map((star, index) => {
                index += 1;
                return (
                  <button
                    type="button"
                    key={index}
                    className={
                      index <= (hover || newReview.rating) ? "on" : "off"
                    }
                    onClick={() => handleRatingChange(index)}
                    onMouseEnter={() => setHover(index)}
                    onMouseLeave={() => setHover(newReview.rating)}
                  >
                    <span className="star">&#9733;</span>
                  </button>
                );
              })}
            </div>
            <button className="btn btn-primary mt-2">Submit Review</button>
          </form>
        </div>
        <div className="mt-4">
          <h2>Reviews</h2>
          {reviews.map((review) => (
            <div className="card mb-2" key={review.id}>
              <div className="card-body">
                <h5 className="card-title">{review.firstName}</h5>
                <p className="card-text">{review.description}</p>
                <p className="card-text">
                  <strong>Rating:</strong>{" "}
                  <StarDisplay rating={review.rating} />
                </p>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default Movie;

{
  /* <div className="star-rating">
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
      </div> */
}
