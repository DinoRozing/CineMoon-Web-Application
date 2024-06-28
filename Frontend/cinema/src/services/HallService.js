import http from '../http.common';

const getAvailableHalls = (date, time, movieId) => {
  return http.get(`/hall/halls?date=${date}&time=${time}&movieId=${movieId}`);
};

const HallService = {
  getAvailableHalls,
};

export default HallService;
