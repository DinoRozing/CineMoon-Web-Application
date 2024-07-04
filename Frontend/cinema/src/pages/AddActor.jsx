import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import ActorService from "../services/ActorService";

const AddActor = () => {
  const [name, setName] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      const actorData = {
        name,
        isActive: true,
        createdByUserId: "8583110f-f633-45bb-8a3d-8647922b09ed",
        updatedByUserId: "8583110f-f633-45bb-8a3d-8647922b09ed",
      };
      await ActorService.addActor(actorData);
      alert("Actor added successfully");
      navigate("/view-actors");
    } catch (error) {
      console.error("Error adding actor:", error);
      alert("Failed to add actor: " + error.message);
    }
  };

  return (
    <div className="container mt-5">
      <h2>Add Actor</h2>
      <form onSubmit={handleSubmit}>
        <div className="input-group mb-3">
          <input
            type="text"
            className="form-control"
            placeholder="Actor Name"
            aria-label="Actor Name"
            aria-describedby="button-addon2"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
          <button
            className="btn btn-primary"
            type="submit"
            id="button-addon2"
          >
            Add Actor
          </button>
        </div>
      </form>
    </div>
  );
};

export default AddActor;
