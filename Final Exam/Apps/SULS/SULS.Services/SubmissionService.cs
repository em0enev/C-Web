using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SULS.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly SULSContext context;
        private readonly IProblemService problemService;

        public SubmissionService(SULSContext sulsContext, IProblemService problemService)
        {
            this.context = sulsContext;
            this.problemService = problemService;
        }

        public Submission CreateSubmissionAndAddToCurrentProblem(string problemId, string userId, string code)
        {
            var random = new Random();
            var problem = this.problemService.GetProblemById(problemId);
            int problemTotalPoints = problem.Points;
            int minPoints = 0;

            var submission = new Submission()
            {
                Code = code,
                UserId = userId,
                AchievedResult = random.Next(minPoints, problemTotalPoints),
                CreatedOn = DateTime.UtcNow,
                ProblemId = problemId
            };

            problem.Submissions.Add(submission);

            this.context.Update(problem);
            this.context.SaveChanges();

            return submission;
        }

        public ICollection<Submission> GetAllSubmissionByProblemID(string problemId)
        {
            var submission = this.context.Submissions
                .Include(x => x.Problem)
                .Include(x => x.User)
                .Where(x => x.ProblemId == problemId)
                .ToList();

            return submission;
        }

        public bool DeleteSubmissionById(string id)
        {
            var submission = this.context.Submissions.SingleOrDefault(x => x.Id == id);


            this.context.Remove(submission);
            this.context.SaveChanges();

            return true;
        }
    }
}
