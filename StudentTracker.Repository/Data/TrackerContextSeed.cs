using StudentTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentTracker.Repository.Data
{
    public static class TrackerContextSeed
    {
        public async static Task SeedAsync(TrackerContext _dbContext)
        {
            if (!_dbContext.Courses.Any())
            {
                var coursesData = File.ReadAllText("../StudentTracker.Repository/Data/DataSeeding/courses.json");
                var courses = JsonSerializer.Deserialize<List<Course>>(coursesData);

                if (courses?.Count() > 0)
                {
                    foreach (var course in courses)
                    {
                        _dbContext.Set<Course>().Add(course);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            if (!_dbContext.Students.Any(s => s.Courses.Any()))
            {
                var enrollmentData = File.ReadAllText("../StudentTracker.Repository/Data/DataSeeding/enrollments.json");
                var enrollments = JsonSerializer.Deserialize<List<EnrollmentData>>(enrollmentData);

                if (enrollments?.Count > 0)
                {
                    foreach (var enrollment in enrollments)
                    {
                        var student = _dbContext.Students.Find(enrollment.StudentsNationalId);
                        if (student != null)
                        {
                            foreach (var courseId in enrollment.CoursesId)
                            {
                                var course = _dbContext.Courses.Find(courseId);
                                if (course != null && !student.Courses.Any(c => c.Id == courseId))
                                {
                                    student.Courses.Add(course);
                                }
                            }
                        }
                    }

                    _dbContext.SaveChanges();
                }
            }
        
    
            if (!_dbContext.Halls.Any())
            {
                var hallsData = File.ReadAllText("../StudentTracker.Repository/Data/DataSeeding/halls.json");
                var halls = JsonSerializer.Deserialize<List<Hall>>(hallsData);

                if (halls?.Count() > 0)
                {
                    foreach (var hall in halls)
                    {
                        _dbContext.Set<Hall>().Add(hall);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            if (!_dbContext.Professors.Any())
            {
                var professorsData = File.ReadAllText("../StudentTracker.Repository/Data/DataSeeding/Professors.json");
                var professors = JsonSerializer.Deserialize<List<Professor>>(professorsData);

                if (professors?.Count() > 0)
                {
                    foreach (var professor in professors)
                    {
                        _dbContext.Set<Professor>().Add(professor);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            if (!_dbContext.Students.Any())
            {
                var studentsData = File.ReadAllText("../StudentTracker.Repository/Data/DataSeeding/students.json");
                var students = JsonSerializer.Deserialize<List<Student>>(studentsData);

                if (students?.Count() > 0)
                {
                    foreach (var student in students)
                    {
                        _dbContext.Set<Student>().Add(student);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            if (!_dbContext.lectures.Any())
            {
                var lecturesData = File.ReadAllText("../StudentTracker.Repository/Data/DataSeeding/lectures.json");
                var lectures = JsonSerializer.Deserialize<List<Lecture>>(lecturesData);

                if (lectures?.Count() > 0)
                {
                    foreach (var lecture in lectures)
                    {
                        _dbContext.Set<Lecture>().Add(lecture);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
        
        }
    }
}
public class EnrollmentData
{
    public List<int> CoursesId { get; set; }
    public string StudentsNationalId { get; set; }
}