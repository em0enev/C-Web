using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SULS.Services
{
    public class ProblemService : IProblemService
    {
        private readonly SULSContext context;

        public ProblemService(SULSContext sulsContext)
        {
            this.context = sulsContext;
        }

        public bool AddSubmissionToProblem(string submissionId)
        {
            throw new NotImplementedException();
        }

        public Problem CreateProblem(string name, int points)
        {
            var problem = new Problem()
            {
                Name = name,
                Points = points
            };

            this.context.Problems.Add(problem);
            this.context.SaveChanges();

            return problem;
        }

        public List<Problem> GetAllProblems()
        {
            var a = GetProblemById("d2725520-1a7c-47c0-87c0-c2815c9a4297");
            var problems = this.context.Problems
                .Include(x => x.Submissions)
                .ToList();

            return problems;
        }

        public Problem GetProblemById(string problemId)
        {
            var problem = this.context.Problems
                .SingleOrDefault(p => p.Id == problemId);

            return problem;
        }
    }
}
