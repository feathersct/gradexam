/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: DbGradExam.cs 
// Description: Functions as a stage for the database. Handles various calls for reading from or saving to a database 
//				for each of the various controller calls. Reading from a database also includes reading various associated
//				keys for each table in the database. Reads or saves to tables created by migrations.
// 
// Course: CSCI 3250-001 - Software Engineering I
// Created: December, October 05, 2018 
// Copyright: Scrum Bags, 2018
// 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradExam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GradExam.Services
{	
	/// <summary>
		/// Class that holds the controller methods that interact with the database
		/// </summary>
    public class DbGradExam : IGradeExamRepository
    {
        private ApplicationDbContext _db;
		/// <summary>
		/// Setting up a dummy value for our database
		/// </summary>
		/// <param name="db">database that was returned</param>
		/// <returns></returns>
        public DbGradExam(ApplicationDbContext db)
        {
            _db = db;
        }
		/// <summary>
		/// Adding a user to a database
		/// </summary>
		/// <param name="user">user to add</param>
		/// <returns></returns>
        public User CreateUser(User user)
        {
            _db.User.Add(user);
            _db.SaveChanges();
            return user;
        }
		/// <summary>
		/// Adding a learnOutcome to a database
		/// </summary>
		/// <param name="learnOutcome">learnOutcome to add</param>
		/// <returns></returns>
        public LearnOutcome CreateLearningOutcome(LearnOutcome learnOutcome)
        {
            _db.LearnOutcome.Add(learnOutcome);
            _db.SaveChanges();
            return learnOutcome;
        }
		/// <summary>
		/// Exception handling for id deletion
		/// </summary>
		/// <param name="id">Id for an object</param>
		/// <returns></returns>
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
		/// <summary>
		/// Deleting a course
		/// </summary>
		/// <param name="id">Id for a course</param>
		/// <returns></returns>
        public void DeleteCourse(int id)
        {
            Course course = _db.Courses.Find(id);
            _db.Courses.Remove(course);
            _db.SaveChanges();
        }
		/// <summary>
		/// Deleting an outcome
		/// </summary>
		/// <param name="id">Id for a outcome</param>
		/// <returns></returns>
        public void DeleteOutcome(int id)
        {
            LearnOutcome learnOutcome = _db.LearnOutcome.Find(id);
            _db.LearnOutcome.Remove(learnOutcome);
            _db.SaveChanges();
        }
		/// <summary>
		/// Deleting a user
		/// </summary>
		/// <param name="id">Id for a user</param>
		/// <returns></returns>
        public void DeleteUser(int id)
        {
            var user = _db.User.Find(id);
            _db.User.Remove(user);
            _db.SaveChanges();
        }
		/// <summary>
		/// Reading a user
		/// </summary>
		/// <param name="id">Id for a user</param>
		/// <returns>User</returns>
        public User Read(string id)
        {
            return _db.User.FirstOrDefault(p => p.Id == id);
        }
		/// <summary>
		/// Reading an outcome
		/// </summary>
		/// <param name="id">Id for an outcome</param>
		/// <returns>LearnOutcome</returns>
        public LearnOutcome ReadOutcome(int id)
        {
            return _db.LearnOutcome.Include(l => l.QuestionAnswer).FirstOrDefault(c => c.Id == id);
        }
		/// <summary>
		/// Reading a QuesAnswer
		/// </summary>
		/// <param name="id">Id for a QuesAnswer</param>
		/// <returns>QuesAnswer</returns>
        public QuesAnswer ReadQuesAnswer(int id)
        {
            return _db.QuesAnswer.Include(q => q.Course).ThenInclude(q=> q.StudentCourses).ThenInclude(q=> q.Student)
                .Include(q => q.LearnOutcome).FirstOrDefault(c => c.Id == id);
        }
		/// <summary>
		/// Read all users
		/// </summary>
		/// <returns>ICollection<User></returns>
        public ICollection<User> ReadAllUsers()
        {
            return _db.User.ToList();
        }
		/// <summary>
		/// Read all learnOutcomes
		/// </summary>
		/// 
		/// <returns>ICollection<LearnOutcome></returns>
        public ICollection<LearnOutcome> ReadAllOutcomes()
        {
            return _db.LearnOutcome.Include(l => l.QuestionAnswer).ToList();
        }
		/// <summary>
		/// Read all QuesAnswers
		/// </summary>
		/// 
		/// <returns><ICollection<QuesAnswer></returns>
        public ICollection<QuesAnswer> ReadAllQuesAnswers()
        {
            return _db.QuesAnswer.Include(q => q.Course).Include(q => q.LearnOutcome).ToList();
        }		
		/// <summary>
		/// Read all courses
		/// </summary>
		/// 
		/// <returns>ICollection<Course></returns>
        public ICollection<Course> ReadAllCourses()
        {
            return _db.Courses.Include(u => u.Instructor).Include(c => c.Concentration).Include(c => c.StudentCourses).ThenInclude(c => c.Student).ToList();
        }
		/// <summary>
		/// Read all exams
		/// </summary>
		/// 
		/// <returns>ICollection<Exam></returns>
        public ICollection<Exam> ReadAllExams()
        {
            return _db.Exams.Include(c => c.Student).ThenInclude(c => c.StudentCourses).ThenInclude(c=> c.Course).ToList();
        }
		/// <summary>
		/// Read a course
		/// </summary>
		/// 
		/// <returns></returns>
        public Course ReadCourse(int id)
        {
            return _db.Courses.Include(u => u.Instructor).Include(c => c.Concentration).Include(c => c.LearnOutcomes).Include(c => c.StudentCourses).ThenInclude(c=>c.Student).Include(c => c.QuesAnswers).FirstOrDefault(c => c.Id == id);
        }
		/// <summary>
		/// Update a user
		/// </summary>
		/// 
		/// <returns></returns>
        public void Update(int id, User user)
        {
            //var oldPerson = Read(id);
            //if (oldPerson != null)
            //{
            //    oldPerson.FirstName = user.FirstName;
                
            //    oldPerson.LastName = user.LastName;
            //    oldPerson.Email = user.Email;
            //    _db.SaveChanges();
            //}

        }
		/// <summary>
		/// Update a course
		/// </summary>
		/// 
		/// <returns></returns>
        public void UpdateCourse(int id, Course course)
        {
            var oldCourse = ReadCourse(id);
            if (oldCourse != null)
            {
                oldCourse.Name = course.Name;
                oldCourse.Section = course.Section;
                oldCourse.Semester = course.Semester;
                oldCourse.Instructor = course.Instructor;
                _db.SaveChanges();
            }

        }
		/// <summary>
		/// Update an outcome
		/// </summary>
		/// 
		/// <returns></returns>
        public void UpdateOutcome(int id, LearnOutcome learnOutcome)
        {
            var oldOutcome = ReadOutcome(id);
            if (oldOutcome != null)
            {
                oldOutcome.LearningOutcome = learnOutcome.LearningOutcome;
                oldOutcome.QuestionAnswer = learnOutcome.QuestionAnswer;
                _db.SaveChanges();
            }
        }
		/// <summary>
		/// Add a course
		/// </summary>
		/// 
		/// <returns></returns>
        public Course AddCourse(Course Course)
        {
            _db.Courses.Add(Course);
            _db.SaveChanges();
            return Course;
        }

		/// <summary>
		/// Add a QuesAnswer
		/// </summary>
		/// 
		/// <returns></returns>
        public QuesAnswer AddQA(QuesAnswer Qa)
        {
            throw new NotImplementedException();
        }
		/// <summary>
		/// Create a QuesAnswer
		/// </summary>
		/// 
		/// <returns></returns>
        public QuesAnswer createQuesetion(QuesAnswer Qa)
        {
            _db.QuesAnswer.Add(Qa);
            _db.SaveChanges();
            return Qa;
        }
		/// <summary>
		/// Delete a question
		/// </summary>
		/// 
		/// <returns></returns>
        public void deleteQuestion(int id)
        {
            var ques = _db.QuesAnswer.Find(id);
            _db.QuesAnswer.Remove(ques);
            _db.SaveChanges();
        }	
		/// <summary>
		/// Update a question
		/// </summary>
		/// 
		/// <returns></returns>
        public void UpdateQuestion(int id, QuesAnswer ques)
        {
            var oldQuestion = ReadQuesAnswer(id);
            if (oldQuestion != null)
            {
                oldQuestion.Question = ques.Question;
                oldQuestion.Answer = ques.Answer;
                oldQuestion.Course = ques.Course;
                _db.SaveChanges();
            }
        }
		/// <summary>
		/// Read all concentrations
		/// </summary>
		/// 
		/// <returns></returns>
        public ICollection<Concentration> ReadAllConcentrations()
        {
            return _db.Concentrations.ToList();
        }

        
		/// <summary>
		/// Read concentration
		/// </summary>
		/// 
		/// <returns></returns>
        public Concentration ReadConcentration(int id)
        {
            return _db.Concentrations.FirstOrDefault(c => c.Id == id);
        }	
		/// <summary>
		/// UpdateConcentration
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
        public Concentration UpdateConcentration(int id, Concentration c)
        {
            if(id == -1)
            {
                var conc = _db.Concentrations.Add(c);
                _db.SaveChanges();
                return c;
            }

            var concentration = _db.Concentrations.FirstOrDefault(con => con.Id == c.Id);
            concentration.Name = c.Name;

            _db.SaveChanges();

            return c;
        }
		/// <summary>
		/// Read all students
		/// </summary>
		/// 
		/// <returns></returns>
        public ICollection<Student> ReadAllStudents()
        {
            return _db.Student.Include(c => c.StudentCourses).ThenInclude(c => c.Course).ToList();
        }
		/// <summary>
		/// Read a student
		/// </summary>
		/// 
		/// <returns></returns>
        public Student ReadStudent(int id)
        {
            return _db.Student.Include(c => c.StudentCourses).ThenInclude(s => s.Course).ThenInclude(c => c.Instructor)
                              .Include(c => c.StudentCourses).ThenInclude(s => s.Course).ThenInclude(c => c.QuesAnswers).FirstOrDefault(c => c.Id == id);
        }
		/// <summary>
		/// Update a student
		/// </summary>
		/// 
		/// <returns></returns>
        public void UpdateStudent(int id, Student student)
        {
            var oldStudent = ReadStudent(id);
            if (oldStudent != null)
            {
                oldStudent.Name = student.Name;

                _db.SaveChanges();
            }

        }
		/// <summary>
		/// Add a student 
		/// </summary>
		/// 
		/// <returns></returns>
        public Student AddStudent(Student student)
        {
            _db.Student.Add(student);
            _db.SaveChanges();
            return student;
        }
		/// <summary>
		/// Add a student to a course
		/// </summary>
		/// 
		/// <returns></returns>
        public void AddStudentToACourse(Student student, Course course)
        {
            StudentCourse relation = new StudentCourse
            {
                StudentId = student.Id,
                Student = student,
                CourseId = course.Id,
                Course = course
            };

            Course newCourse = ReadCourse(course.Id);
            newCourse.StudentCourses.Add(relation);

            _db.Courses.Update(newCourse);
            _db.SaveChanges();
        }
		/// <summary>
		/// Remove a student from a course
		/// </summary>
		/// 
		/// <returns></returns>
        public void RemoveStudentFromACourse(Student student, Course course)
        {

            Course newCourse = ReadCourse(course.Id);
            var relation = newCourse.StudentCourses.Where(s => s.StudentId == student.Id).SingleOrDefault();
            newCourse.StudentCourses.Remove(relation);

            _db.Courses.Update(newCourse);
            _db.SaveChanges();
        }
		/// <summary>
		/// Delete student
		/// </summary>
		/// 
		/// <returns></returns>
        public void DeleteStudent(int id)
        {
            Student student = _db.Student.Find(id);
            _db.Student.Remove(student);
            _db.SaveChanges();
        }
        
		/// <summary>
		/// Delete concentration
		/// </summary>
		/// 
		/// <returns></returns>
        public void DeleteConcentration(int id)
        {
            var con = _db.Concentrations.Find(id);
            _db.Concentrations.Remove(con);
            _db.SaveChanges();
        }
		/// <summary>
		/// Read exam
		/// </summary>
		/// 
		/// <returns></returns>
        public Exam ReadExam(int id)
        {
            var exam = _db.Exams.Include(e => e.Student).ThenInclude(s=>s.StudentCourses).ThenInclude(sc => sc.Course).ThenInclude(c=>c.Instructor)
                                .Include(e => e.Student).ThenInclude(s=>s.StudentCourses).ThenInclude(sc => sc.Course).ThenInclude(c=>c.QuesAnswers).FirstOrDefault(e => e.Id == id);

            return exam;
        }
		/// <summary>
		/// Create exam
		/// </summary>
		/// 
		/// <returns></returns>
        public Exam CreateExam(int studentId)
        {
            var student = ReadStudent(studentId);
            var exam = new Exam
            {
                Student = student
            };

            // Add the part that if there is a student with an exam already going then it should just return that exam
            var testExam = _db.Exams.FirstOrDefault(e => e.Student.Id == studentId);

            if (testExam != null)
            {
                return testExam;
            }

            var ex = _db.Exams.Add(exam);
            _db.SaveChanges();

            foreach(var sc in student.StudentCourses)
            {
                foreach(var item in sc.Course.QuesAnswers)
                {
                    var qr = new QuestionResponse
                    {
                        Course = sc.Course,
                        Exam = ex.Entity,
                        Question = item,
                    };

                    _db.Responses.Add(qr);
                }
            }
            _db.SaveChanges();
           
            return ReadExam(ex.Entity.Id);
        }
		/// <summary>
		/// Add learnOutcome to a course
		/// </summary>
		/// 
		/// <returns></returns>
        public void AddLOToACourse(LearnOutcome lo, Course course)
        {
            Course newCourse = ReadCourse(course.Id);
            newCourse.LearnOutcomes.Add(lo);

            _db.Courses.Update(newCourse);
            _db.SaveChanges();
        }
		/// <summary>
		/// Add QuesAnswer to an outcome
		/// </summary>
		/// 
		/// <returns></returns>
		public void AddQAToACourse(QuesAnswer qa, Course course)
        {
            Course newCourse = ReadCourse(course.Id);
            newCourse.QuesAnswers.Add(qa);

            _db.Courses.Update(newCourse);
            _db.SaveChanges();
        }
		/// <summary>
		/// Remove a learnOutcome from a Course
		/// </summary>
		/// 
		/// <returns></returns>
        public void RemoveLOFromACourse(LearnOutcome lo, Course course)
        {
            Course newCourse = ReadCourse(course.Id);
            LearnOutcome courseLO = newCourse.LearnOutcomes.SingleOrDefault(m => m.Id == lo.Id);
            newCourse.LearnOutcomes.Remove(courseLO);

            _db.Courses.Update(newCourse);
            _db.SaveChanges();
        }
		/// <summary>
		/// Remove QA from a course
		/// </summary>
		/// 
		/// <returns></returns>
		public void RemoveQAFromACourse(QuesAnswer qa, Course course)
        {
            Course newCourse = ReadCourse(course.Id);
            QuesAnswer QA = newCourse.QuesAnswers.SingleOrDefault(m => m.Id == qa.Id);
            newCourse.QuesAnswers.Remove(QA);

            _db.Courses.Update(newCourse);
            _db.SaveChanges();
        }
		/// <summary>
		/// Save response from a question
		/// </summary>
		/// 
		/// <returns></returns>
        public void SaveResponse(QuestionResponse qr)
        {
            if (qr.Id == 0)
                _db.Responses.Add(qr);
            else
                _db.Responses.Update(qr);

            _db.SaveChanges();
        }
		/// <summary>
		/// Read all responses
		/// </summary>
		/// 
		/// <returns></returns>
        public List<QuestionResponse> ReadAllResponses()
        {
            var items = _db.Responses.Include(qr => qr.Question).Include(ex => ex.Exam).Include(q => q.Question).ToList();
            return items;
        }
        /// <summary>
		/// Read Responses by Course
		/// </summary>
		/// 
		/// <returns></returns>
        public List<QuestionResponse> ReadResponsesByCourse(int courseId, int examId)
        {
            var response = _db.Responses.Include(c => c.Course).Include(qr => qr.Question).Include(ex => ex.Exam).Where(m => m.Course.Id == courseId && m.Exam.Id == examId).ToList();
            return response;
        }
        /// <summary>
        /// Read a Response
        /// </summary>
        /// 
        /// <returns></returns>
        public QuestionResponse ReadResponse(int id)
        {
            return _db.Responses.SingleOrDefault(m => m.Id == id);
        }
    }
}           
