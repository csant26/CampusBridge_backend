﻿using backend.Data;
using backend.Models.Domain.Content.Syllabi;
using backend.Models.DTO.Content.Syllabus;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public class SyllabusRepository : ISyllabusRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public SyllabusRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task<Course> CreateCourse(Course course)
        {
            foreach(var unit in course.Units)
            {
                unit.CourseId = course.CourseId;
                unit.Course = course;
            }
            await campusBridgeDbContext.Course.AddAsync(course);    
            await campusBridgeDbContext.SaveChangesAsync();
            return course;
        }

        public async Task<List<Course>> GetCourse()
        {
            var courses = await campusBridgeDbContext.Course
                .Include(u => u.Units)
                .Include(s => s.Syllabus)
                .ToListAsync();
            foreach(var course in courses)
            {
                var courseId = course.CourseId;
                var existingUnits = await campusBridgeDbContext.Unit
                    .AsNoTracking()
                    .Where(x => x.CourseId == courseId)
                    .ToListAsync();
                course.Units = existingUnits;
            }
            if (courses == null) { return null; }
            else { return courses; }
        }

        public async Task<Course> GetCourseById(string CourseId)
        {
            var course = await campusBridgeDbContext.Course
                .Include(u=>u.Units)
                .Include(s=>s.Syllabus)
                .FirstOrDefaultAsync(x=>x.CourseId==CourseId);
            var courseId = course.CourseId;
            var existingUnits = await campusBridgeDbContext.Unit
                .AsNoTracking()
                .Where(x => x.CourseId == courseId)
                .ToListAsync();
            course.Units = existingUnits;
            if (course == null) { return null; }
            else { return course; }
        }

        public async Task<Course> UpdateCourse(string CourseId, Course course)
        {
            var existingCourse = await GetCourseById(CourseId);
            if (existingCourse != null)
            {
                foreach (var unit in existingCourse.Units)
                {
                    unit.CourseId = course.CourseId;
                    unit.Course = course;
                }
                existingCourse.CourseTitle = course.CourseTitle;
                existingCourse.CourseDescription = course.CourseDescription;
                existingCourse.CourseObjective = course.CourseObjective;
                existingCourse.FullMarks = course.FullMarks;
                existingCourse.PassMarks = course.PassMarks;
                existingCourse.CreditHour = course.CreditHour;
                existingCourse.LabDescription = course.LabDescription;
                existingCourse.Books = course.Books;
                existingCourse.Units = course.Units;
                await campusBridgeDbContext.SaveChangesAsync();
                return existingCourse;
            }
            else
            {
                return null;
            }

        }
        public async Task<Course> DeleteCourse(string CourseId)
        {
            var exisitingCourse = await GetCourseById(CourseId);
            if (exisitingCourse != null)
            {
                campusBridgeDbContext.Course.Remove(exisitingCourse);
                await campusBridgeDbContext.SaveChangesAsync();
                return exisitingCourse;
            }
            else
            {
                return null;
            }
        }

        public async Task<Syllabus> CreateSyllabus(Syllabus syllabus, AddSyllabusDTO addSyllabusDTO)
        {
            var courses = await campusBridgeDbContext.Course
                .Where(x => addSyllabusDTO.CourseId.Contains(x.CourseId))
                .ToListAsync();
            if (courses != null)
            {
                syllabus.Courses = courses;
                foreach(var course in courses)
                {
                    course.SyllabusId = syllabus.SyllabusId;
                    course.Syllabus = syllabus;
                }
            }
            await campusBridgeDbContext.Syllabus.AddAsync(syllabus);
            await campusBridgeDbContext.SaveChangesAsync();
            return syllabus;
        }

        public async Task<List<Syllabus>> GetSyllabus()
        {
            var syllabus = await campusBridgeDbContext.Syllabus
                .Include(x => x.Courses)
                .ThenInclude(course => course.Units)
                .ToListAsync();
            foreach(var syllab in syllabus)
            {
                var courses = await campusBridgeDbContext.Course.Where(x => x.SyllabusId == syllab.SyllabusId).ToListAsync();
                foreach (var course in courses)
                {
                    var courseId = course.CourseId;
                    var existingUnits = await campusBridgeDbContext.Unit
                        .AsNoTracking()
                        .Where(x => x.CourseId == courseId)
                        .ToListAsync();
                    course.Units = existingUnits;
                }
            }
            if (syllabus == null) { return null; }
            else { return syllabus; }
        }

        public async Task<Syllabus> GetSyllabusById(string SyllabusId)
        {
            var syllabus = await campusBridgeDbContext.Syllabus
                .Include(x => x.Courses)
                    .ThenInclude(course => course.Units)
                .FirstOrDefaultAsync(s => s.SyllabusId == SyllabusId);
            if (syllabus == null) { return null; }
            var courses = await campusBridgeDbContext.Course.Where(x => x.SyllabusId == syllabus.SyllabusId).ToListAsync();
            foreach (var course in courses)
            {
                var courseId = course.CourseId;
                var existingUnits = await campusBridgeDbContext.Unit
                    .AsNoTracking()
                    .Where(x => x.CourseId == courseId)
                    .ToListAsync();
                course.Units = existingUnits;
            }
            if (syllabus == null) { return null; }
            else { return syllabus; }
        }

        public async Task<Syllabus> UpdateSyllabus(string SyllabusId, Syllabus syllabus,
            UpdateSyllabusDTO updateSyllabusDTO)
        {
            var existingSyllabus = await GetSyllabusById(SyllabusId);
            if(existingSyllabus== null) { return null; }
            existingSyllabus.Semester = syllabus.Semester;
            existingSyllabus.AllowedElectiveNo = syllabus.AllowedElectiveNo;
            var courses = await campusBridgeDbContext.Course
                .Where(x => updateSyllabusDTO.CourseId.Contains(x.CourseId))
                .ToListAsync();
            if (courses != null)
            {
                existingSyllabus.Courses = courses;
                foreach (var course in courses)
                {
                    course.SyllabusId = existingSyllabus.SyllabusId;
                    course.Syllabus = existingSyllabus;
                }
            }

            await campusBridgeDbContext.SaveChangesAsync();
            return existingSyllabus;    
        }

        public async Task<Syllabus> DeleteSyllabus(string SyllabusId)
        {
            var existingSyllabus = await GetSyllabusById(SyllabusId);
            if (existingSyllabus == null) { return null; }
            campusBridgeDbContext.Syllabus.Remove(existingSyllabus);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingSyllabus;
        }
    }
}
