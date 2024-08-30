--Student Name: Huynh Tu Anh, Chau
--Student ID: 2231473
--Date 30th Nov, 2023

--Create the database
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'College1en')
BEGIN
    DROP DATABASE College1en;
END

CREATE DATABASE College1en;

-- Use the database
USE College1en;
GO

--'Programs' table
CREATE TABLE Programs (
    ProgId VARCHAR(5) NOT NULL,
    ProgName VARCHAR(50) NOT NULL,
    PRIMARY KEY (ProgId)
);
GO

--'Courses' table
CREATE TABLE Courses (
    CId VARCHAR(7) NOT NULL,
    CName VARCHAR(50) NOT NULL,
    ProgId VARCHAR(5) NOT NULL,
    PRIMARY KEY (CId),
    FOREIGN KEY (ProgId) REFERENCES Programs(ProgId) 
        ON DELETE CASCADE ON UPDATE CASCADE
);
GO

--'Students' table
CREATE TABLE Students (
    StId VARCHAR(10) NOT NULL,
    StName VARCHAR(50) NOT NULL,
    ProgId VARCHAR(5) NOT NULL,
    PRIMARY KEY (StId),
    FOREIGN KEY (ProgId) REFERENCES Programs(ProgId) 
        ON DELETE NO ACTION ON UPDATE CASCADE
);
GO

--'Enrollments' table
CREATE TABLE Enrollments (
    StId VARCHAR(10) NOT NULL,
    CId VARCHAR(7) NOT NULL,
    FinalGrade INT,
    PRIMARY KEY (StId, CId),
    FOREIGN KEY (StId) REFERENCES Students(StId) 
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (CId) REFERENCES Courses(CId) 
        ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

--'Programs' table with sample data
INSERT INTO Programs (ProgId, ProgName) VALUES
('P0001', 'Computer Science'),
('P0002', 'Business Administration'),
('P0003', 'Psychology'),
('P0004', 'Mechanical Engineering'),
('P0005', 'Environmental Science'),
('P0006', 'Graphic Design'),
('P0007', 'Culinary Arts');
GO

--'Courses' table with sample data
INSERT INTO Courses (CId, CName, ProgId) VALUES
('C000001', 'Introduction to Programming', 'P0001'),
('C000002', 'Introduction to Psychology', 'P0003'),
('C000003', 'Principles of Marketing', 'P0002'),
('C000004', 'Machine Design', 'P0004'),
('C000005', 'Ecology and Environmental Systems', 'P0005'),
('C000006', 'Environmental Policy and Management', 'P0005'),
('C000007', 'Fundamentals of Design', 'P0006'),
('C000008', 'Basic Culinary Skills', 'P0007'),
('C000009', 'Pastry and Baking', 'P0007');
GO

--'Students' table with sample data
INSERT INTO Students (StId, StName, ProgId) VALUES
('S000000001', 'John Doe', 'P0001'),
('S000000002', 'Jane Smith', 'P0002'),
('S000000003', 'William Johnson', 'P0003'),
('S000000004', 'Alice Moon', 'P0003'),
('S000000005', 'Adele Montoya', 'P0004'),
('S000000006', 'Raya Wade', 'P0005'),
('S000000007', 'Chana Serrano', 'P0005'),
('S000000008', 'Jayson Knapp', 'P0006'),
('S000000009', 'Linda Russo', 'P0007');
GO

--'Enrollments' table with sample data
INSERT INTO Enrollments (StId, CId, FinalGrade) VALUES
('S000000001', 'C000001', 90),
('S000000001', 'C000002', 85),
('S000000002', 'C000003', 88),
('S000000003', 'C000004', 77),
('S000000004', 'C000005', 66),
('S000000005', 'C000006', 99);
GO
