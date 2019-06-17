using SULS.Models;
using System.Collections.Generic;

namespace SULS.Services
{
    public interface IProblemService
    {
        Problem CreateProblem(string name, int points);

        Problem GetProblemById(string problemId);

        List<Problem> GetAllProblems();

        bool AddSubmissionToProblem(string submissionId);
    }
}
