import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import ProjectionSelector from "../components/ProjectionSelector";

const ProjectionSelection = () => {
  const { movieId } = useParams();
  const navigate = useNavigate();
  const [projections, setProjections] = useState([]);
  const [selectedProjection, setSelectedProjection] = useState(null);

  useEffect(() => {
    const fetchProjections = async () => {
      try {
        const response = await axios.get(
          `http://localhost:5058/projection/movie/${movieId}`
        );
        setProjections(response.data);
      } catch (error) {
        console.error("Error fetching projections:", error);
      }
    };

    fetchProjections();
  }, [movieId]);

  console.log(projections);

  const handleProjectionSelect = (projection) => {
    setSelectedProjection(projection);
  };

  const handleSeatSelection = () => {
    if (selectedProjection) {
      navigate(`/select-seat/${selectedProjection.id}`);
    }
  };

  return (
    <div className="container mt-4">
      <div className="row">
        <div className="col-md-6">
          <h2>Select a Showtime</h2>
          <div className="list-group">
            {projections.map((projection) => (
              <ProjectionSelector
                key={projection.id}
                projection={projection}
                onSelect={() => handleProjectionSelect(projection)}
              />
            ))}
          </div>
          {selectedProjection && (
            <div className="mt-4 text-center">
              <button className="btn btn-primary" onClick={handleSeatSelection}>
                Select Seats
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default ProjectionSelection;
