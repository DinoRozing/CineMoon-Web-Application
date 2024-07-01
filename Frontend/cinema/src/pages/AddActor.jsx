import React, { useState, useEffect } from 'react';
import AddActorForm from '../components/AddActorForm'; 
import ActorService from '../services/ActorService';

const AddActor = () => {
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

  const handleAddActor = async (actorData) => {
    try {
      const response = await ActorService.addActor(actorData);
      const newActorId = response.data.id;

      const updatedActorsResponse = await ActorService.getAllActors();
      setActors(updatedActorsResponse.data);

      return newActorId;
    } catch (error) {
      console.error('Error adding actor:', error);
      alert('Failed to add actor: ' + error.message);
      return null;
    }
  };

  return (
    <div>
      <AddActorForm actors={actors} onAddActor={handleAddActor} />
    </div>
  );
};

export default AddActor;
