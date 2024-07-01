import React, { useState, useEffect } from 'react';
import ActorService from '../services/ActorService';

const DeleteActor = () => {
  const [actors, setActors] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const actorsResponse = await ActorService.getAllActors();
        setActors(actorsResponse.data);
      } catch (error) {
        console.error('Error fetching actors:', error);
      }
    };

    fetchData();
  }, []);

  const handleDeleteActor = async (actorId) => {
    try {
      await ActorService.deleteActor(actorId);
      const updatedActorsResponse = await ActorService.getAllActors();
      setActors(updatedActorsResponse.data);
      alert('Actor deleted successfully');
    } catch (error) {
      console.error('Error deleting actor:', error);
      alert('Failed to delete actor: ' + error.message);
    }
  };

  return (
    <div>
      <h2>Delete Actor</h2>
      <ul>
        {actors.map(actor => (
          <li key={actor.id}>
            {actor.name} 
            <button onClick={() => handleDeleteActor(actor.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default DeleteActor;
