using backend.Controllers;
using backend.Data;
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

        public AssignmentRepository(CampusBridgeDbContext campusBridgeDbContext,
            IImageRepository imageRepository)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.imageRepository = imageRepository;
        }
        public async Task<Assignment> CreateAssignment(Assignment assignment,
            AddAssignmentDTO addAssignmentDTO)
        {
            var course = await campusBridgeDbContext.Course.FindAsync(addAssignmentDTO.CourseId);
            var teacher = await campusBridgeDbContext.Teachers
                .FirstOrDefaultAsync(x => x.TeacherId == addAssignmentDTO.TeacherId);
            if (course != null)
            {
                assignment.Course = course;
            }
            if(teacher != null)
            {
                assignment.Teacher = teacher;
            }
            var images = new List<Image>();
            if(addAssignmentDTO.QuestionImage != null)
            {
                foreach(var image in addAssignmentDTO.QuestionImage)
                {
                    var img = new Image
                    {
                        ImageId = image.FileId,
                        File = image.File,
                        FileName = image.FileName,
                        FileDescription = image.FileDescription,
                        FileExtension = Path.GetExtension(image.File.FileName),
                        FileSizeInBytes = image.File.Length,
                    };
                    var uploadedImage = await imageRepository.UploadImage(img);
                    img.FilePath = uploadedImage.FilePath;
                    images.Add(img);
                }
            }
            assignment.QuestionImage = images;
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
            UpdateAssignmentDTO updateAssignmentDTO)
        {
            var existingAssignment = await GetAssignmentById(AssignmentId);
            if (existingAssignment == null) { return null; }
            existingAssignment.Question = assignment.Question;
            existingAssignment.CourseId = assignment.CourseId;
            existingAssignment.TeacherId = assignment.TeacherId;
            existingAssignment.AssignedDate = assignment.AssignedDate;
            existingAssignment.SubmissionDate = assignment.SubmissionDate;

            var course = await campusBridgeDbContext.Course.FindAsync(updateAssignmentDTO.CourseId);
            var teacher = await campusBridgeDbContext.Teachers
                .FirstOrDefaultAsync(x => x.TeacherId == updateAssignmentDTO.TeacherId);
            if (course != null)
            {
                assignment.Course = course;
            }
            if (teacher != null)
            {
                assignment.Teacher = teacher;
            }

            var images = new List<Image>();
            if (updateAssignmentDTO.QuestionImage != null)
            {
                foreach (var image in updateAssignmentDTO.QuestionImage)
                {
                    var img = new Image
                    {
                        ImageId = image.FileId,
                        File = image.File,
                        FileName = image.FileName,
                        FileDescription = image.FileDescription,
                        FileExtension = Path.GetExtension(image.File.FileName),
                        FileSizeInBytes = image.File.Length,
                    };
                    var uploadedImage = await imageRepository.UploadImage(img);
                    img.FilePath = uploadedImage.FilePath;
                    images.Add(img);
                }
            }
            existingAssignment.QuestionImage= images;
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
