CREATE TABLE JobOpenings (
    JobOpeningId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for each job opening
    Title NVARCHAR(255) NOT NULL,               -- Job title
    Description NVARCHAR(MAX),                  -- Job description
    LocationId INT NOT NULL,                    -- Foreign key for location
    DepartmentId INT NOT NULL,                  -- Foreign key for department
    ClosingDate DATE NOT NULL,                  -- Application closing date
    CONSTRAINT FK_JobOpenings_Location FOREIGN KEY (LocationId) REFERENCES Location(Id),
    CONSTRAINT FK_JobOpenings_Department FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)
);

CREATE TABLE Department (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for each department
    Title NVARCHAR(255) NOT NULL                -- Department title
);

CREATE TABLE Location (
    Id INT PRIMARY KEY,                         -- Unique identifier for each location
    Title NVARCHAR(100) NOT NULL,               -- Location title
    City NVARCHAR(100) NOT NULL,                -- City of the location
    State NVARCHAR(50),                         -- State of the location
    Country NVARCHAR(100) NOT NULL,             -- Country of the location
    Zip INT                                     -- Zip code of the location
);
