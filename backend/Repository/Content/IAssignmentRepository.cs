using backend.Models.Domain.Content.Assignments;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.File;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public interface IAssignmentRepository
    {
        Task<Assignment> CreateAssignment(Assignment assignment,
            FileUploadRequestDTO fileUploadRequestDTO);
        Task<List<Assignment>> GetAssignment();
        Task<Assignment> GetAssignmentById(string AssignmentId);
        Task<List<Assignment>> GetAssignmentByTeacherId(string TeacherId);
        Task<Assignment> UpdateAssignment(string AssignmentId,
            Assignment assignment,
            FileUploadRequestDTO fileUploadRequestDTO);
        Task<Assignment> DeleteAssignment(string AssignmentId, string TeacherId);
        Task<List<StudentSubmission>> GradeAssignment(string submissionId, string Score);
   
        Task<Submission> SubmitAssignment(Submission submission,
            FileUploadRequestDTO fileUploadRequestDTO);
        Task<List<Submission>> GetSubmission();
        Task<Submission> GetSubmissionById(string SubmissionId);
        Task<List<Submission>> GetSubmissionByAssignmentId(string AssignmentId);
        Task<Submission> GetSubmissionByStudentId(string AssignmentId, string StudentId);
        Task<List<StudentSubmission>> GetStudentSubmissions(string StudentId);
        Task<Submission> UpdateSubmission(string SubmissionId, 
            Submission submission,
            FileUploadRequestDTO fileUploadRequestDTO);
        Task<Submission> DeleteSubmission(string SubmissionId, string StudentId);


    }
}
