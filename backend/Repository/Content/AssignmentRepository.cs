using backend.Data;
using backend.Files;
using backend.Models.Domain.Content.Assignments;
using backend.Models.Domain.Students;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.File;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly FileHandling fileHandling;

        public AssignmentRepository(CampusBridgeDbContext campusBridgeDbContext,
            FileHandling fileHandling)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.fileHandling = fileHandling;
        }
        public async Task<Assignment> CreateAssignment(Assignment assignment,
            FileUploadRequestDTO fileUploadRequestDTO)
        {
            var course = await campusBridgeDbContext.Course.FindAsync(assignment.CourseId);
            var teacher = await campusBridgeDbContext.Teachers
                .Include(x => x.Courses)
                .FirstOrDefaultAsync(x => x.Email == assignment.TeacherId);
            if (teacher.Courses.Contains(course))
            {
                if (course != null)
                {
                    assignment.Course = course;
                }
                if (teacher != null)
                {
                    assignment.Teacher = teacher;
                }
                var filePath = await fileHandling.UploadFile(fileUploadRequestDTO);
                if (filePath != null)
                {
                    assignment.FilePath = filePath;
                }
                await campusBridgeDbContext.Assignments.AddAsync(assignment);
                await campusBridgeDbContext.SaveChangesAsync();
                return assignment;
            }
            return null;
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
        public async Task<List<Assignment>> GetAssignmentByTeacherId(string TeacherId)
        {

            var teacher = await campusBridgeDbContext.Teachers
                .FirstOrDefaultAsync(x => (x.TeacherId == TeacherId) || (x.Email == TeacherId));


            var assignment = await campusBridgeDbContext.Assignments
                .Include(x => x.Course).Include(t => t.Teacher).Include(s => s.Submissions)
                .Where(x => x.TeacherId == teacher.TeacherId)
                .ToListAsync();
            if (assignment == null) { return null; }
            return assignment;
        }

        public async Task<Assignment> UpdateAssignment(string AssignmentId,
            Assignment assignment,
            FileUploadRequestDTO fileUploadRequestDTO)
        {
            var existingAssignment = await GetAssignmentById(AssignmentId);
            if (existingAssignment == null) { return null; }

            var course = await campusBridgeDbContext.Course.FindAsync(assignment.CourseId);
            var teacher = await campusBridgeDbContext.Teachers
                .FirstOrDefaultAsync(x => x.TeacherId == assignment.TeacherId);

            if (!teacher.Courses.Contains(course)) { return null; }
            if (existingAssignment.TeacherId != assignment.TeacherId) { return null; }


            if (course != null)
            {
                existingAssignment.Course = course;
            }
            if (teacher != null)
            {
                existingAssignment.Teacher = teacher;
            }

            existingAssignment.Question = assignment.Question;
            existingAssignment.CourseId = assignment.CourseId;
            existingAssignment.TeacherId = assignment.TeacherId;
            existingAssignment.AssignedDate = assignment.AssignedDate;
            existingAssignment.SubmissionDate = assignment.SubmissionDate;

            var filePath = await fileHandling.UploadFile(fileUploadRequestDTO);
            if (filePath != null)
            {
                existingAssignment.FilePath = filePath;
            }
            await campusBridgeDbContext.SaveChangesAsync();
            return existingAssignment;

        }
        public async Task<Assignment> DeleteAssignment(string AssignmentId, string TeacherId)
        {
            var existingAssignment = await GetAssignmentById(AssignmentId);
            if (existingAssignment == null) { return null; }

            var teacher = await campusBridgeDbContext.Teachers.Where(x => (x.TeacherId == TeacherId) || (x.Email == TeacherId)).FirstOrDefaultAsync();
            if (existingAssignment.TeacherId != teacher.TeacherId) { return null;}

            campusBridgeDbContext.Assignments.Remove(existingAssignment);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingAssignment;
        }

        public async Task<Submission> SubmitAssignment(Submission submission,
            FileUploadRequestDTO fileUploadRequestDTO)
        {
            var assignment = await campusBridgeDbContext.Assignments
                .FindAsync(submission.AssignmentId);
            var student = await campusBridgeDbContext.Students
                .FirstOrDefaultAsync(x=>x.Email==submission.StudentId);

            if (assignment != null) { submission.Assignment=assignment; }
            if (student != null) { submission.Student=student; }

            var filePath = await fileHandling.UploadFile(fileUploadRequestDTO);
            if (filePath != null)
            {
                submission.FilePath = filePath;
            }
            await campusBridgeDbContext.Submissions.AddAsync(submission);
            await campusBridgeDbContext.SaveChangesAsync();
            return submission;
        }
        public async Task<List<Submission>> GetSubmission()
        {
            var submissions = await campusBridgeDbContext.Submissions
                .Include(a=>a.Assignment).Include(s=>s.Student)
                .ToListAsync();
            if (submissions == null) { return null; }
            return submissions;
        }
        public async Task<Submission> GetSubmissionById(string SubmissionId)
        {
            var submission = await campusBridgeDbContext.Submissions
                .Include(a => a.Assignment).Include(s => s.Student)
                .FirstOrDefaultAsync(x => x.SubmissionId == SubmissionId);
            if (submission == null) { return null; }
            return submission;
        }
        public async Task<List<Submission>> GetSubmissionByAssignmentId(string AssignmentId)
        {
            var submission = await campusBridgeDbContext.Submissions
                .Include(a => a.Assignment).Include(s => s.Student)
                .Where(x => x.AssignmentId == AssignmentId)
                .ToListAsync() ;
            if (submission == null) { return null; }
            return submission;
        }
        public async Task<Submission> GetSubmissionByStudentId(string AssignmentId, string StudentId)
        {
            var submission = await campusBridgeDbContext.Submissions
                    .Include(a => a.Assignment)
                    .Include(s => s.Student)
                    .FirstOrDefaultAsync(x => x.AssignmentId == AssignmentId && x.StudentId == StudentId );
            if (submission == null) { return null; }
            return submission;
        }
        public async Task<List<StudentSubmission>> GetStudentSubmissions(string StudentId)
        {
            var student = await campusBridgeDbContext.Students.FirstOrDefaultAsync(x => (x.StudentId == StudentId) || (x.Email==StudentId));
            List<StudentSubmission> studentSubmissions = new List<StudentSubmission>();
            var submission = await campusBridgeDbContext.Submissions
                    .Include(a => a.Assignment)
                    .Include(s => s.Student)
                    .Where(x => x.StudentId == student.StudentId).ToListAsync();
            if (submission == null) { return null; }
            foreach(var subs in submission)
            {
                StudentSubmission studentSubmission = new StudentSubmission();
                studentSubmission.Question = subs.Assignment.Question;
                studentSubmission.Answer = subs.Answer;
                studentSubmission.Score = subs.Score;
                studentSubmission.SubmissionId = subs.SubmissionId;
                studentSubmission.CourseName = subs.Assignment.CourseId;
                studentSubmission.AssignmentFilePath = subs.Assignment.FilePath;
                studentSubmission.SubmissionFilePath = subs.FilePath;
                studentSubmissions.Add(studentSubmission);
            }
            return studentSubmissions;
        }
        public async Task<List<StudentSubmission>> GradeAssignment(string submissionId,string Score)
        {
            List<StudentSubmission> studentSubmissions = new List<StudentSubmission>();
            var submission = await campusBridgeDbContext.Submissions.FirstOrDefaultAsync(x=>x.SubmissionId== submissionId);
            submission.Score = Score;
            await campusBridgeDbContext.SaveChangesAsync();
            var submissions = await campusBridgeDbContext.Submissions
                    .Include(a => a.Assignment)
                    .Include(s => s.Student)
                    .ToListAsync();
            foreach (var subs in submissions)
            {
                StudentSubmission studentSubmission = new StudentSubmission();
                studentSubmission.Score = subs.Score;
                studentSubmission.Question = subs.Assignment.Question;
                studentSubmission.Answer = subs.Answer;
                studentSubmission.SubmissionId = subs.SubmissionId;
                studentSubmission.CourseName = subs.Assignment.CourseId;
                studentSubmission.AssignmentFilePath = subs.Assignment.FilePath;
                studentSubmission.SubmissionFilePath = subs.FilePath;
                studentSubmissions.Add(studentSubmission);
            }
            return studentSubmissions;
        }
        public async Task<Submission> UpdateSubmission(string SubmissionId,
            Submission submission,
            FileUploadRequestDTO fileUploadRequestDTO)
        {
            var existingSubmission = await GetSubmissionById(SubmissionId);
            if (existingSubmission == null) { return null; }

            if (existingSubmission.StudentId != submission.StudentId) { return null; }

            existingSubmission.Answer = submission.Answer;
            var filePath = await fileHandling.UploadFile(fileUploadRequestDTO);
            if (filePath != null)
            {
                existingSubmission.FilePath = filePath;
            }
            await campusBridgeDbContext.SaveChangesAsync();
            return existingSubmission;

        }

        public async Task<Submission> DeleteSubmission(string SubmissionId, string StudentId)
        {
            var student = await campusBridgeDbContext.Students.FirstOrDefaultAsync(x => (x.StudentId == StudentId) || (x.Email == StudentId));

            var existingSubmission = await GetSubmissionById(SubmissionId);
            if (existingSubmission == null) { return null; }

            if (existingSubmission.StudentId != student.StudentId) { return null; }

            campusBridgeDbContext.Submissions.Remove(existingSubmission);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingSubmission;
        }
    }
}
