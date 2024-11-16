using backend.Models.Domain.Content.Assignments;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.File;

namespace backend.Repository.Content
{
    public interface IAssignmentRepository
    {
        Task<Assignment> CreateAssignment(Assignment assignment,
            FileUploadRequestDTO fileUploadRequestDTO);
        Task<List<Assignment>> GetAssignment();
        Task<Assignment> GetAssignmentById(string AssignmentId);
        Task<Assignment> UpdateAssignment(string AssignmentId,
            Assignment assignment,
            FileUploadRequestDTO fileUploadRequestDTO);
        Task<Assignment> DeleteAssignment(string AssignmentId);
    }
}
