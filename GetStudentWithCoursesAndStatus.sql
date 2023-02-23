USE [StudentDatabase]
GO

/****** Object:  StoredProcedure [dbo].[GetStudentWithCoursesAndStatus]    Script Date: 23/02/2023 04:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStudentWithCoursesAndStatus]
    @Id int
AS
BEGIN
    SELECT 
        s.Id, 
        s.Name, 
        s.Surname, 
        s.IndexNo, 
        s.Year, 
        s.StatusId, 
        ss.Status, 
        sc.CourseId, 
        c.Name AS CourseName
    FROM Students s
    INNER JOIN StudentStatuses ss ON s.StatusId = ss.Id
    INNER JOIN StudentCourses sc ON s.Id = sc.StudentId
    INNER JOIN Courses c ON sc.CourseId = c.Id
    WHERE s.Id = @Id;
END
GO


