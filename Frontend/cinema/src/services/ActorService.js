import http from "../http.common";

const getAllActors = () => {
  return http.get("/actor");
};

const addActor = (actor) => {
  return http.post("/actor", actor);
};

const ActorService = {
  getAllActors,
  addActor,
};

export default ActorService;
