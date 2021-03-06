﻿using System.Collections.Generic;
using System.Web.Mvc;
using UniversityManagementApp.Manager;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Controllers
{
    public class TeacherController : Controller
    {
        TeacherManager aTeacherManager = new TeacherManager();
        CourseManager aCourseManager = new CourseManager();
        DepartmentManager aDepartmentManager = new DepartmentManager();
        public ActionResult Save()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Designations = aTeacherManager.GetAllDesignations();
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Teacher aTeacher)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.response = aTeacherManager.Save(aTeacher);
            ViewBag.Designations = aTeacherManager.GetAllDesignations();
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }

        public ActionResult AssignCourse()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public ActionResult AssignCourse(CourseAssign courseAssign)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.response = aTeacherManager.AssignCourse(courseAssign);
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public ActionResult DepartmentCourses(int DepartmentId)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<CourseSelectView> CourseSelectViews = aCourseManager.GetCoursesByDepartment(DepartmentId);
            return Json(CourseSelectViews, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DepartmentTeachers(int DepartmentId)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<Teacher> teachers = aTeacherManager.GetTeachersByDepartment(DepartmentId);
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public decimal CreditUsed(int TeacherId)
        {
            decimal creditUsed = aTeacherManager.GetCreditUsedByTeacherId(TeacherId);
            return creditUsed;
        }

        [HttpPost]
        public ActionResult AvailableCourses(int DepartmentId)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<CourseSelectView> CourseSelectViews = aCourseManager.GetAvailableCoursesForTeachers(DepartmentId);
            return Json(CourseSelectViews, JsonRequestBehavior.AllowGet);
        }
	}
}