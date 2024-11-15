using backend.Models.Domain.Content.Assignments;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.Images;

namespace backend.Repository.Content
{
    public interface IAssignmentRepository
    {
        Task<Assignment> CreateAssignment(Assignment assignment,
            ImageUploadRequestDTO imageUploadRequestDTO);
        Task<List<Assignment>> GetAssignment();
        Task<Assignment> GetAssignmentById(string AssignmentId);
        Task<Assignment> UpdateAssignment(string AssignmentId,
            Assignment assignment,
            ImageUploadRequestDTO imageUploadRequestDTO);
        Task<Assignment> DeleteAssignment(string AssignmentId);
    }
}
