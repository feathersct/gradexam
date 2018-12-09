/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: DbGradExam.cs 
// Description: Functions as a repository for all method signatures in DbGradExam relating to all of the controller calls
// 
// Course: CSCI 3250-001 - Software Engineering I
// Author: Levi Shelton, SHELTONL@etsu.edu 
// Created: December, October 05, 2018 
// Copyright: Scrum Bags, 2018
// 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using GradExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.Services
{
    public interface IGradeExamRepository
    {
        #region User
        User CreateUser(User user);
        User Read(string id);
        ICollection<User> ReadAllUsers();
        void Update(int id, User user);
        void Delete(int id);
        #endregion

        #region Course
        Course ReadCourse(int id);
        ICollection<Course> ReadAllCourses();
        void DeleteCourse(int id);
        void UpdateCourse(int id, Course course);
        Course AddCourse(Course Course);
        void AddLOToACourse(LearnOutcome lo, Course course);
        void RemoveLOFromACourse(LearnOutcome lo, Course course);
        #endregion

         #region Question and Answers
        QuesAnswer ReadQuesAnswer(int id);
        ICollection<QuesAnswer> ReadAllQuesAnswers();
        void UpdateQuestion(int id, QuesAnswer ques);
        void deleteQuestion(int id);
        QuesAnswer createQuesetion(QuesAnswer QA);
        void AddQAToACourse(QuesAnswer qa, Course course);
        void RemoveQAFromACourse(QuesAnswer qa, Course course);
        #endregion

        #region Concentration
        ICollection<Concentration> ReadAllConcentrations();
        Concentration ReadConcentration(int id);
        Concentration UpdateConcentration(int id, Concentration c);
        void DeleteConcentration(int id);
        #endregion

        #region LearningOutcome
        LearnOutcome CreateLearningOutcome(LearnOutcome learnOutcome);
        void UpdateOutcome(int id, LearnOutcome learnOutcome);
        ICollection<LearnOutcome> ReadAllOutcomes();
        LearnOutcome ReadOutcome(int id);
        void DeleteOutcome(int id);
        #endregion

        #region Student
        Student ReadStudent(int id);
        ICollection<Student> ReadAllStudents();
        void DeleteStudent(int id);
        void UpdateStudent(int id, Student student);
        Student AddStudent(Student Student);
        #endregion

        #region StudentCourses
        void AddStudentToACourse(Student student, Course course);
        void RemoveStudentFromACourse(Student student, Course course);
        #endregion

        #region Exam
        Exam ReadExam(int id);
        Exam CreateExam(int studentId);

        void SaveResponse(QuestionResponse qr);
        List<QuestionResponse> ReadAllResponses();

        List<QuestionResponse> ReadResponsesByCourse(int courseId, int examId);
        QuestionResponse ReadResponse(int id);
        ICollection<Exam> ReadAllExams();
        #endregion

    }
}
