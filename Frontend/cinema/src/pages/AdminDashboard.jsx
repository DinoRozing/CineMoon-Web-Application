import React from "react";
import { Link } from "react-router-dom";

const AdminDashboard = () => {
  return (
    <div className="container mt-4">
      <h1>Admin Dashboard</h1>
      <div className="row mt-4">
        <div className="col-md-6">
          <div className="card mb-3">
            <div className="card-body">
              <h5 className="card-title">Manage Movies</h5>
              <p className="card-text">Add, delete, and manage movies.</p>
              <Link to="/add-movie" className="btn btn-primary">
                Add Movie
              </Link>
              <Link to="/view-movies" className="btn btn-success ms-2">
                View Movies
              </Link>
            </div>
          </div>
        </div>
        <div className="col-md-6">
          <div className="card mb-3">
            <div className="card-body">
              <h5 className="card-title">Manage Projections</h5>
              <p className="card-text" >Add, delete and manage projections.</p>
              <Link to="/add-projection" className="btn btn-primary">
                Add Projection
              </Link>
              <Link to="/manage-projections" className="btn btn-danger ms-2">
                Manage Projections
              </Link>
            </div>
          </div>
        </div>
      </div>
      <div className="row mt-4">
        <div className="col-md-6">
          <div className="card mb-3">
            <div className="card-body">
              <h5 className="card-title">Manage Users</h5>
              <p className="card-text">View and manage registered users.</p>
              <Link to="/admin/view-users" className="btn btn-primary">
                View Users
              </Link>
            </div>
          </div>
        </div>
        <div className="col-md-6">
          <div className="card mb-3">
            <div className="card-body">
              <h5 className="card-title">Manage Actors</h5>
              <p className="card-text">Add and delete movie actors.</p>
              <Link to="/add-actor" className="btn btn-primary">
                Add Actor
              </Link>
              <Link to="/delete-actor" className="btn btn-danger ms-2">
                Delete Actor
              </Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;
