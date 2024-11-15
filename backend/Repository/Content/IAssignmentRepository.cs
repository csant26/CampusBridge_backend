using backend.Models.Domain.Content.Assignments;
using backend.Models.DTO.Content.Assignment;

namespace backend.Repository.Content
{
    public interface IAssignmentRepository
    {
        Task<Assignment> CreateAssignment(Assignment assignment,
            AddAssignmentDTO addAssignmentDTO);
        Task<List<Assignment>> GetAssignment();
        Task<Assignment> GetAssignmentById(string AssignmentId);
        Task<Assignment> UpdateAssignment(string AssignmentId,
            Assignment assignment,
            UpdateAssignmentDTO updateAssignmentDTO);
        Task<Assignment> DeleteAssignment(string AssignmentId);
    }
}
