INSERT INTO "Role" ("Id", "RoleName") VALUES
(uuid_generate_v4(), 'Admin'),
(uuid_generate_v4(), 'User');

INSERT INTO "User" ("Id", "Email", "Password", "FirstName", "LastName", "RoleId", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), 'admin@example.com', 'password', 'Admin', 'User', (SELECT "Id" FROM "Role" WHERE "RoleName" = 'Admin'), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4()),
(uuid_generate_v4(), 'user@example.com', 'password', 'Regular', 'User', (SELECT "Id" FROM "Role" WHERE "RoleName" = 'User'), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4()),
(uuid_generate_v4(), 'user2@example.com', 'password', 'John', 'Doe', (SELECT "Id" FROM "Role" WHERE "RoleName" = 'User'), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Admin" ("Id", "Email", "Password", "FirstName", "LastName", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), 'admin1@example.com', 'password', 'Admin1', 'User', TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Movie" ("Id", "Title", "Genre", "Description", "Duration", "Language", "AdminId", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), 'Movie Title 1', 'Action', 'An action movie description', 120, 'English', (SELECT "Id" FROM "Admin" LIMIT 1), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4()),
(uuid_generate_v4(), 'Movie Title 2', 'Comedy', 'A comedy movie description', 90, 'English', (SELECT "Id" FROM "Admin" LIMIT 1), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Review" ("Id", "Description", "Rating", "UserId", "MovieId", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), 'Great movie!', 5, (SELECT "Id" FROM "User" WHERE "Email" = 'user@example.com'), (SELECT "Id" FROM "Movie" WHERE "Title" = 'Movie Title 1'), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4()),
(uuid_generate_v4(), 'Not bad', 3, (SELECT "Id" FROM "User" WHERE "Email" = 'user@example.com'), (SELECT "Id" FROM "Movie" WHERE "Title" = 'Movie Title 2'), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4()),
(uuid_generate_v4(), 'I enjoyed the movie!', 4, (SELECT "Id" FROM "User" WHERE "Email" = 'user2@example.com'), (SELECT "Id" FROM "Movie" WHERE "Title" = 'Movie Title 1'), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Actor" ("Id", "Name", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), 'Actor Name 1', TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4()),
(uuid_generate_v4(), 'Actor Name 2', TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "MovieActor" ("Id", "MovieId", "ActorId", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), (SELECT "Id" FROM "Movie" WHERE "Title" = 'Movie Title 1'), (SELECT "Id" FROM "Actor" WHERE "Name" = 'Actor Name 1'), CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4()),
(uuid_generate_v4(), (SELECT "Id" FROM "Movie" WHERE "Title" = 'Movie Title 2'), (SELECT "Id" FROM "Actor" WHERE "Name" = 'Actor Name 2'), CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Projection" ("Id", "Date", "Time", "MovieId", "UserId", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), '2024-06-15', '18:00:00', (SELECT "Id" FROM "Movie" WHERE "Title" = 'Movie Title 1'), (SELECT "Id" FROM "Admin" LIMIT 1), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4()),
(uuid_generate_v4(), '2024-06-16', '20:00:00', (SELECT "Id" FROM "Movie" WHERE "Title" = 'Movie Title 2'), (SELECT "Id" FROM "Admin" LIMIT 1), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Payment" ("Id", "TotalPrice", "PaymentDate", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), 25.00, '2024-06-15', TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4()),
(uuid_generate_v4(), 30.00, '2024-06-16', TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Ticket" ("Id", "Price", "PaymentId", "UserId", "ProjectionId", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), 25.00, (SELECT "Id" FROM "Payment" LIMIT 1), (SELECT "Id" FROM "User" WHERE "Email" = 'user@example.com'), (SELECT "Id" FROM "Projection" LIMIT 1), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "Hall" ("Id", "HallNumber") VALUES
(uuid_generate_v4(), 1),
(uuid_generate_v4(), 2);

INSERT INTO "Seat" ("Id", "SeatNumber", "RowNumber", "HallId", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), 1, 1, (SELECT "Id" FROM "Hall" WHERE "HallNumber" = 1), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4()),
(uuid_generate_v4(), 2, 1, (SELECT "Id" FROM "Hall" WHERE "HallNumber" = 1), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "ProjectionHall" ("Id", "ProjectionId", "HallId", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), (SELECT "Id" FROM "Projection" LIMIT 1), (SELECT "Id" FROM "Hall" WHERE "HallNumber" = 1), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());

INSERT INTO "SeatReserved" ("Id", "TicketId", "ProjectionId", "SeatId", "IsActive", "DateCreated", "DateUpdated", "CreatedByUserId", "UpdatedByUserId") VALUES
(uuid_generate_v4(), (SELECT "Id" FROM "Ticket" LIMIT 1), (SELECT "Id" FROM "Projection" LIMIT 1), (SELECT "Id" FROM "Seat" LIMIT 1), TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, uuid_generate_v4(), uuid_generate_v4());
