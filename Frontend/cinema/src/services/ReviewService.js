import http from "../http.common";

const getAllReviews = () => {
  return http.get("/review");
};

const getReviewById = (id) => {
  return http.get(`/review/movie/${id}`);
};

const ReviewService = {
  getAllReviews,
  getReviewById,
};

export default ReviewService;
