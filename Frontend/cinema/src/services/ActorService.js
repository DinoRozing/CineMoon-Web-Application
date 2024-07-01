import http from "../http.common";

const getAllActors = () => {
  return http.get("/actor");
};

const addActor = (actor) => {
  return http.post("/actor", actor);
};

const deleteActor = (actorId) => {
  return http.delete(`/actor/${actorId}`); 
};

const ActorService = {
  getAllActors,
  addActor,
  deleteActor,
};

export default ActorService;
