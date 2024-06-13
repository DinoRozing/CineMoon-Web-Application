INSERT INTO "Role" ("Id", "RoleName") VALUES
  (uuid_generate_v4(), 'Admin'),
  (uuid_generate_v4(), 'User');
INSERT INTO "User" ("Id", "Email", "Password", "FirstName", "LastName", "RoleId", "IsActive") VALUES
  (uuid_generate_v4(), 'admin@example.com', 'password123', 'John', 'Doe', (SELECT "Id" FROM "Role" WHERE "RoleName" = 'Admin'), TRUE),
  (uuid_generate_v4(), 'user1@example.com', 'password123', 'Jane', 'Doe', (SELECT "Id" FROM "Role" WHERE "RoleName" = 'User'), TRUE);

INSERT INTO "Movie" ("Id", "Title", "Genre", "Description", "Duration", "Language", "CoverUrl", "TrailerUrl", "AdminId", "IsActive", "CreatedByUserId", "UpdatedByUserId") VALUES
  (uuid_generate_v4(), 'Inception', 'Sci-Fi', 'A mind-bending thriller', 148, 'English', 'http://example.com/inception.jpg', 'http://example.com/inception-trailer.mp4', (SELECT "Id" FROM "User" WHERE "Email" = 'admin@example.com'), TRUE, uuid_generate_v4(), uuid_generate_v4()),
  (uuid_generate_v4(), 'The Matrix', 'Action', 'A hacker discovers reality is a simulation', 136, 'English', 'http://example.com/matrix.jpg', 'http://example.com/matrix-trailer.mp4', (SELECT "Id" FROM "User" WHERE "Email" = 'admin@example.com'), TRUE, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Actor" ("Id", "Name", "IsActive", "CreatedByUserId", "UpdatedByUserId") VALUES
  (uuid_generate_v4(), 'Leonardo DiCaprio', TRUE, uuid_generate_v4(), uuid_generate_v4()),
  (uuid_generate_v4(), 'Keanu Reeves', TRUE, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "MovieActor" ("Id", "MovieId", "ActorId", "CreatedByUserId", "UpdatedByUserId") VALUES
  (uuid_generate_v4(), (SELECT "Id" FROM "Movie" WHERE "Title" = 'Inception'), (SELECT "Id" FROM "Actor" WHERE "Name" = 'Leonardo DiCaprio'), uuid_generate_v4(), uuid_generate_v4()),
  (uuid_generate_v4(), (SELECT "Id" FROM "Movie" WHERE "Title" = 'The Matrix'), (SELECT "Id" FROM "Actor" WHERE "Name" = 'Keanu Reeves'), uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Projection" ("Id", "Date", "Time", "MovieId", "UserId", "IsActive", "CreatedByUserId", "UpdatedByUserId") VALUES
  (uuid_generate_v4(), '2024-06-15', '19:00:00', (SELECT "Id" FROM "Movie" WHERE "Title" = 'Inception'), (SELECT "Id" FROM "User" WHERE "Email" = 'admin@example.com'), TRUE, uuid_generate_v4(), uuid_generate_v4()),
  (uuid_generate_v4(), '2024-06-16', '21:00:00', (SELECT "Id" FROM "Movie" WHERE "Title" = 'The Matrix'), (SELECT "Id" FROM "User" WHERE "Email" = 'admin@example.com'), TRUE, uuid_generate_v4(), uuid_generate_v4());


INSERT INTO "Review" ("Id", "Description", "Rating", "UserId", "MovieId", "IsActive", "CreatedByUserId", "UpdatedByUserId") VALUES
  (uuid_generate_v4(), 'Amazing movie!', 5, (SELECT "Id" FROM "User" WHERE "Email" = 'user1@example.com'), (SELECT "Id" FROM "Movie" WHERE "Title" = 'Inception'), TRUE, uuid_generate_v4(), uuid_generate_v4()),
  (uuid_generate_v4(), 'A mind-blowing experience', 4, (SELECT "Id" FROM "User" WHERE "Email" = 'user1@example.com'), (SELECT "Id" FROM "Movie" WHERE "Title" = 'The Matrix'), TRUE, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Hall" ("Id", "HallNumber") VALUES
  (uuid_generate_v4(), 1),
  (uuid_generate_v4(), 2);

INSERT INTO "Seat" ("Id", "SeatNumber", "RowNumber", "HallId", "IsActive", "CreatedByUserId", "UpdatedByUserId") VALUES
  (uuid_generate_v4(), 1, 1, (SELECT "Id" FROM "Hall" WHERE "HallNumber" = 1), TRUE, uuid_generate_v4(), uuid_generate_v4()),
  (uuid_generate_v4(), 2, 1, (SELECT "Id" FROM "Hall" WHERE "HallNumber" = 1), TRUE, uuid_generate_v4(), uuid_generate_v4()),
  (uuid_generate_v4(), 3, 1, (SELECT "Id" FROM "Hall" WHERE "HallNumber" = 2), TRUE, uuid_generate_v4(), uuid_generate_v4()),
  (uuid_generate_v4(), 4, 1, (SELECT "Id" FROM "Hall" WHERE "HallNumber" = 2), TRUE, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Payment" ("Id", "TotalPrice", "PaymentDate", "IsActive", "CreatedByUserId", "UpdatedByUserId") VALUES
  (uuid_generate_v4(), 15.00, '2024-06-10', TRUE, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Ticket" ("Id", "Price", "PaymentId", "UserId", "ProjectionId", "IsActive", "CreatedByUserId", "UpdatedByUserId") VALUES
  (uuid_generate_v4(), 15.00, (SELECT "Id" FROM "Payment" LIMIT 1), (SELECT "Id" FROM "User" WHERE "Email" = 'user1@example.com'), (SELECT "Id" FROM "Projection" LIMIT 1), TRUE, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "ProjectionHall" ("Id", "ProjectionId", "HallId", "IsActive", "CreatedByUserId", "UpdatedByUserId") VALUES
  (uuid_generate_v4(), (SELECT "Id" FROM "Projection" LIMIT 1), (SELECT "Id" FROM "Hall" WHERE "HallNumber" = 1), TRUE, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "SeatReserved" ("Id", "TicketId", "ProjectionId", "SeatId", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
  (uuid_generate_v4(), (SELECT "Id" FROM "Ticket" LIMIT 1), (SELECT "Id" FROM "Projection" LIMIT 1), (SELECT "Id" FROM "Seat" LIMIT 1), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

