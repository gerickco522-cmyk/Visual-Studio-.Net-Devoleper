CREATE TABLE Students (
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    BirthDate DATE NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Teachers (
    TeacherId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    Specialty NVARCHAR(200) NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Courses (
    CourseId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    TeacherId INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TeacherId) REFERENCES Teachers(TeacherId)
);

CREATE TABLE Enrollments (
    EnrollmentId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    EnrolledAt DATETIME DEFAULT GETDATE(),
    Grade DECIMAL(5,2) NULL,
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);


-- Teachers
INSERT INTO Teachers (FirstName, LastName, Email, Specialty) VALUES
('Laura', 'Ramírez', 'laura.ramirez@school.com', 'Backend .NET'),
('Carlos', 'Díaz', 'carlos.diaz@school.com', 'Frontend Angular');

-- Courses
INSERT INTO Courses (Title, Description, TeacherId) VALUES
('Introducción a .NET', 'Curso básico de C#, runtime y CLR', 1),
('API REST con ASP.NET Core', 'Construcción de APIs REST', 1),
('Frontend con Angular', 'Fundamentos de Angular', 2);

-- Students
INSERT INTO Students (FirstName, LastName, Email, BirthDate) VALUES
('Ana', 'Torres', 'ana.torres@email.com', '2001-05-12'),
('Luis', 'Martínez', 'luis.martinez@email.com', '2000-10-03'),
('Sofía', 'Paredes', 'sofia.paredes@email.com', '1999-07-21');

INSERT INTO Students VALUES
('Lucia', 'Suarez', 'Lucia.saurez@email.com', '2002-06-12','20251101');

-- Enrollments
INSERT INTO Enrollments (StudentId, CourseId, Grade) VALUES
(1, 1, 85.5),
(1, 2, NULL),
(2, 1, 90.0),
(3, 3, 88.3);

----------------SP

CREATE PROCEDURE sp_GetStudents
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        StudentId,
        FirstName,
        LastName,
        Email,
        BirthDate,
        CreatedAt
    FROM Students
    ORDER BY LastName, FirstName;
END;


CREATE  or alter PROCEDURE sp_GetStudentsFiltered
(
    @Name NVARCHAR(100) = NULL,
    @Email NVARCHAR(150) ,
    @FromDate DATE = NULL,
    @ToDate DATE = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        StudentId,
        FirstName,
        LastName,
        Email,
        BirthDate,
        CreatedAt
    FROM Students
    WHERE
        (@Name IS NULL OR (FirstName + ' ' + LastName) LIKE '%' + @Name + '%')
        AND (@Email is null or Email LIKE '%' + @Email + '%')
        AND (@FromDate IS NULL OR CreatedAt >= @FromDate)
        AND (@ToDate IS NULL OR CreatedAt <= @ToDate)
    ORDER BY CreatedAt DESC;
END;



sp_GetStudentsFilteredPage null,null,null,null,3,2

CREATE  or alter PROCEDURE sp_GetStudentsFilteredPage
(
	@StudentId int = null,
    @Name NVARCHAR(100) = NULL,
    @Email NVARCHAR(150) = null,
    @FromDate DATE = NULL,
    @ToDate DATE = NULL,
    @PageSize INT =10, 
    @PageNumber INT =1
)
AS
BEGIN
	
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize
			
	;WITH  PageListStudents as (
    SELECT 
        StudentId,
        FirstName,
        LastName,
        Email,
        BirthDate,
        CreatedAt
    FROM Students)
    
    
    SELECT 
        StudentId ,
        FirstName,
        LastName,
        Email,
        BirthDate,
        CreatedAt
    FROM PageListStudents
    WHERE
        (@Name IS NULL OR (FirstName + ' ' + LastName) LIKE '%' + @Name + '%')
        AND (@StudentId is null or StudentId =  @StudentId)
        AND (@Email is null or Email LIKE '%' + @Email + '%')
        AND (@FromDate IS NULL OR CreatedAt >= @FromDate)
        AND (@ToDate IS NULL OR CreatedAt <= @ToDate)
    ORDER BY StudentId  DESC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    
END;


CREATE OR ALTER PROCEDURE sp_InsertStudent
(
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Email NVARCHAR(150),
    @BirthDate DATE = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el email ya existe
    IF EXISTS (SELECT 1 FROM Students WHERE Email = @Email)
    BEGIN
        RAISERROR('El email ya está registrado en otro estudiante.', 16, 1);
        RETURN;
    END

    -- Insertar estudiante
    INSERT INTO Students (FirstName, LastName, Email, BirthDate)
    VALUES (@FirstName, @LastName, @Email, @BirthDate);

    -- Retornar el ID insertado
    Declare @StudentId int =  SCOPE_IDENTITY();
    Execute sp_GetStudentsFilteredPage @StudentId
    
END;
GO

CREATE OR ALTER PROCEDURE sp_UpdateStudent
(
    @StudentId INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100)=null,
    @Email NVARCHAR(150),
    @BirthDate DATE = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que el estudiante existe
    IF NOT EXISTS (SELECT 1 FROM Students WHERE StudentId = @StudentId)
    BEGIN
        RAISERROR('El estudiante no existe.', 16, 1);
        RETURN;
    END

    -- Validar que el email no esté repetido en otro estudiante
    IF EXISTS (
        SELECT 1 
        FROM Students 
        WHERE Email = @Email AND StudentId <> @StudentId
    )
    BEGIN
        RAISERROR('El email ya está registrado por otro estudiante.', 16, 1);
        RETURN;
    END

    -- Actualizar estudiante
    UPDATE Students
    SET 
        FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        BirthDate = @BirthDate
    WHERE StudentId = @StudentId;

    -- Retornar el registro actualizado
    Execute sp_GetStudentsFilteredPage @StudentId
END;
GO



CREATE OR ALTER PROCEDURE sp_DeleteStudent
(
    @StudentId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que el estudiante exista
    IF NOT EXISTS (SELECT 1 FROM Students WHERE StudentId = @StudentId)
    BEGIN
        RAISERROR('El estudiante no existe.', 16, 1);
        RETURN;
    END

    DELETE FROM Students
    WHERE StudentId = @StudentId;

    -- Retornar el registro eliminado
    Execute sp_GetStudentsFilteredPage 
END;
GO


CREATE TYPE StudentsInsertTableType AS TABLE
(
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(150),
    BirthDate DATE
);
GO


CREATE OR ALTER PROCEDURE sp_InsertStudentsBulk
(
    @Students StudentsInsertTableType READONLY
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Students (FirstName, LastName, Email, BirthDate)
    SELECT 
        FirstName,
        LastName,
        Email,
        BirthDate
    FROM @Students;

    Execute sp_GetStudentsFilteredPage
END;
GO

