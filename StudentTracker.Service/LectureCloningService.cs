using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentTracker.Core.Entities;
using StudentTracker.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Service
{
    public class LectureCloningService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<LectureCloningService> _logger;
        private readonly TimeSpan _timeInterval = TimeSpan.FromDays(7);  // Run weekly

        public LectureCloningService(IServiceScopeFactory scopeFactory, ILogger<LectureCloningService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Lecture Cloning Service running at: {time}", DateTimeOffset.Now);
                await CloneLectures();
                await Task.Delay(_timeInterval, stoppingToken);
            }
        }
        private async Task CloneLectures()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TrackerContext>();
                var lectures = dbContext.lectures.ToList(); 

                foreach (var lecture in lectures)
                {
                    var newLecture = new Lecture
                    {
                        Day = lecture.Day,
                        StartTime = lecture.StartTime,
                        EndTime = lecture.EndTime,
                        

                        ProfessorNationalId = lecture.ProfessorNationalId,
                        CourseId = lecture.CourseId,
                        HallId = lecture.Hall.Id,

                        Professor = lecture.Professor,
                        Course = lecture.Course,
                        Hall = lecture.Hall,
                       Students = new HashSet<Student>()
                };

                    dbContext.lectures.Add(newLecture);
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
