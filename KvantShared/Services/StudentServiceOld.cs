using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KvantShared.Model;

namespace KvantShared.Services
{
    public class StudentServiceOld
    {
        public List<String> NavigationItems { get; set; }

        private String _selectedNavigationItem;

        public String SelectedNavigationItem
        {
            get { return _selectedNavigationItem; }
            set { _selectedNavigationItem = value; }
        }


        public ObservableCollection<Student> Students { get; set; }
        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set { _selectedStudent = value; }
        }

        //public NewStudentCommand newStudentCommand { get; set; }

        public StudentServiceOld()
        {
            //newStudentCommand = new NewStudentCommand(this);

            //TODO: Nav menu here ???
            NavigationItems = new List<String>
            {
                "Учащиеся",
                "Наставники",
                "Администрация"
            };

            //foreach (var item in DatabaseHelper.LoadDict("kvantum"))
            //    Kvantums.Add(item);

            //TODO: Read table
            Students = new ObservableCollection<Student>();
            //foreach (var item in DatabaseHelper.AllStudents())
            //{
            //    (item as Student).KvantumDict = Kvantums;
            //    students.Add(item);
            //}
        }


        public void CreateNewStudent()
        {
            Student newStudent = new Student();
            //DatabaseHelper.Insert(newStudent);
        }
    }
}
