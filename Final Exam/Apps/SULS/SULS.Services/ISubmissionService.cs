using SULS.Models;
using System.Collections.Generic;

namespace SULS.Services
{
    public interface ISubmissionService
    {
        Submission CreateSubmissionAndAddToCurrentProblem(string problemId, string userId, string code);

        ICollection<Submission> GetAllSubmissionByProblemID(string problemId);

        bool DeleteSubmissionById(string id);

    }
}
