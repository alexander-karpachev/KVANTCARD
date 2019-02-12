using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KvantShared.Model;

namespace KvantShared.Services
{
    public class StudentServiceOld
    {
        public List<String> navigationItems { get; set; }

        private String selectedNavigationItem;

        public String SelectedNavigationItem
        {
            get { return selectedNavigationItem; }
            set { selectedNavigationItem = value; }
        }


        public ObservableCollection<Student> students { get; set; }
        private Student selectedStudent;
        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; }
        }

        public List<DictionaryItem> Kvantums { get; set; }

        //public NewStudentCommand newStudentCommand { get; set; }

        public StudentServiceOld()
        {
            //newStudentCommand = new NewStudentCommand(this);

            //TODO: Nav menu here ???
            navigationItems = new List<String>
            {
                "Учащиеся",
                "Наставники",
                "Администрация"
            };

            Kvantums = new List<DictionaryItem>();
            //foreach (var item in DatabaseHelper.LoadDict("kvantum"))
            //    Kvantums.Add(item);

            //TODO: Read table
            students = new ObservableCollection<Student>();
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
