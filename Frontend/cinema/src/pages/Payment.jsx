import React from "react";
import { useLocation } from "react-router-dom";

const Payment = () => {
  const location = useLocation();
  const { detailedSelectedSeats, projectionId } = location.state;

  return (
    <div className="container mt-4">
      <div className="row">
        <div className="col-md-8 offset-md-2">
          <div className="card">
            <div className="card-body">
              <h2 className="card-title">Payment Details</h2>

              <hr />
              <h5 className="card-title">Selected Seats:</h5>
              <ul className="list-group mb-3">
                {detailedSelectedSeats.map((seat) => (
                  <li
                    key={seat.id}
                    className="list-group-item d-flex justify-content-between align-items-center"
                  >
                    <div>
                      <strong>Row:</strong> {seat.rowLetter}
                      <br />
                      <strong>Seat Number:</strong> {seat.seatNumber}
                    </div>
                  </li>
                ))}
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Payment;
