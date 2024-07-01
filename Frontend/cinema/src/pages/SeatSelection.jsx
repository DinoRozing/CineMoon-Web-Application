import React, { useState } from "react";

const Booking = () => {
  const [selectedSeat, setSelectedSeat] = useState(null);

  const handleSeatSelect = (seat) => {
    setSelectedSeat(seat);
  };

  const handleConfirm = () => {
    if (selectedSeat) {
      alert(`You have selected seat ${selectedSeat}.`);
    } else {
      alert("Please select a seat.");
    }
  };

  const rows = ["A", "B", "C", "D", "E", "F", "G"];
  const columns = 11;

  return (
    <div className="container mt-4">
      <div className="row">
        <div className="col-md-7">
          <h2>Select a Seat</h2>
          <div className="d-flex justify-content-center mb-3">
            <div
              className="screen"
              style={{
                width: "calc(40px * 11 + 10px * 10)",
                height: "20px",
                backgroundColor: "#ccc",
                textAlign: "center",
                lineHeight: "20px",
                margin: "0 auto", // Center horizontally
              }}
            >
              Screen
            </div>
          </div>
          <div
            className="seat-grid"
            style={{
              display: "grid",
              gridTemplateColumns: `repeat(${columns + 1}, 40px)`,
              gap: "10px",
            }}
          >
            <div></div>
            {[...Array(columns).keys()].map((column) => (
              <div
                key={column}
                style={{
                  width: "40px",
                  height: "40px",
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "center",
                }}
              >
                {column + 1}
              </div>
            ))}
            {rows.map((row) => (
              <React.Fragment key={row}>
                <div
                  style={{
                    width: "40px",
                    height: "40px",
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                  }}
                >
                  {row}
                </div>
                {[...Array(columns).keys()].map((column) => {
                  const seat = `${row}${column + 1}`;
                  return (
                    <button
                      key={seat}
                      className={`btn ${
                        selectedSeat === seat ? "btn-success" : "btn-secondary"
                      }`}
                      onClick={() => handleSeatSelect(seat)}
                      style={{
                        width: "40px",
                        height: "40px",
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "center",
                      }}
                    >
                      {column + 1}
                    </button>
                  );
                })}
              </React.Fragment>
            ))}
          </div>
        </div>
      </div>

      <div className="mt-4 text-center">
        <button className="btn btn-primary" onClick={handleConfirm}>
          Confirm Selection
        </button>
      </div>
    </div>
  );
};

export default Booking;
