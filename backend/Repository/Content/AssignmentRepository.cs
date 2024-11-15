using backend.Controllers;
using backend.Data;
using backend.Images;
using backend.Models.Domain.Content.Assignments;
using backend.Models.Domain.Content.Images;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.Images;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Content
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly IImageRepository imageRepository;
        private readonly ImageHandling imageHandling;

        public AssignmentRepository(CampusBridgeDbContext campusBridgeDbContext,
            IImageRepository imageRepository,
            ImageHandling imageHandling)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.imageRepository = imageRepository;
            this.imageHandling = imageHandling;
        }
        public async Task<Assignment> CreateAssignment(Assignment assignment,
            ImageUploadRequestDTO imageUploadRequestDTO)
        {
            var course = await campusBridgeDbContext.Course.FindAsync(assignment.CourseId);
            var teacher = await campusBridgeDbContext.Teachers
                .FirstOrDefaultAsync(x => x.TeacherId == assignment.TeacherId);
            if (course != null)
            {
                assignment.Course = course;
            }
            if(teacher != null)
            {
                assignment.Teacher = teacher;
            }
            var imagePath = await imageHandling.UploadImage(imageUploadRequestDTO);
            if (imagePath != null)
            {
                assignment.ImagePath = imagePath;
            }
            await campusBridgeDbContext.Assignments.AddAsync(assignment);
            await campusBridgeDbContext.SaveChangesAsync();
            return assignment;
            
        }
        public async Task<List<Assignment>> GetAssignment()
        {
            return await campusBridgeDbContext.Assignments
                .Include(x => x.Course).Include(t => t.Teacher).Include(s => s.Submissions)
                .ToListAsync();
        }

        public async Task<Assignment> GetAssignmentById(string AssignmentId)
        {
            var assignment =  await campusBridgeDbContext.Assignments
                .Include(x => x.Course).Include(t => t.Teacher).Include(s => s.Submissions)
                .FirstOrDefaultAsync(x => x.AssignmentId == AssignmentId);
            if(assignment == null) { return null; }
            return assignment;
        }

        public async Task<Assignment> UpdateAssignment(string AssignmentId,
            Assignment assignment,
            ImageUploadRequestDTO imageUploadRequestDTO)
        {
            var existingAssignment = await GetAssignmentById(AssignmentId);
            if (existingAssignment == null) { return null; }
            existingAssignment.Question = assignment.Question;
            existingAssignment.CourseId = assignment.CourseId;
            existingAssignment.TeacherId = assignment.TeacherId;
            existingAssignment.AssignedDate = assignment.AssignedDate;
            existingAssignment.SubmissionDate = assignment.SubmissionDate;

            var course = await campusBridgeDbContext.Course.FindAsync(assignment.CourseId);
            var teacher = await campusBridgeDbContext.Teachers
                .FirstOrDefaultAsync(x => x.TeacherId == assignment.TeacherId);
            if (course != null)
            {
                existingAssignment.Course = course;
            }
            if (teacher != null)
            {
                existingAssignment.Teacher = teacher;
            }
            var imagePath = await imageHandling.UploadImage(imageUploadRequestDTO);
            if (imagePath != null)
            {
                existingAssignment.ImagePath = imagePath;
            }
            await campusBridgeDbContext.SaveChangesAsync();
            return existingAssignment;

        }
        public async Task<Assignment> DeleteAssignment(string AssignmentId)
        {
            var existingAssignment = await GetAssignmentById(AssignmentId);
            if (existingAssignment == null) { return null; }
            campusBridgeDbContext.Assignments.Remove(existingAssignment);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingAssignment;
        }
    }
}
