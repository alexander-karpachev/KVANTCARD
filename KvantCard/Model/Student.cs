using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model 
{
    public class Student : _Human, INotifyPropertyChanged
    {

        private List<Parent> parents;
        public virtual List<Parent> Parents
        {
            get { return parents; }
            set { parents = value; OnPropertyChanged("Parents"); }
        }

        private int documentSetID;
        public int DocumentSetID
        {
            get { return documentSetID; }
            set { documentSetID = value; OnPropertyChanged("DocumentSetID"); }
        }

        private DateTime dateTime;
        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; OnPropertyChanged("DateTime"); }
        }

        private int age;
        public int Age
        {
            get { return (int)((DateTime.Now - BirthDate).Days / 365.25); }
        }

        private int parent1ID;
        public int Parent1ID
        {
            get { return parent1ID; }
            set { parent1ID = value; }
        }

        private int mentorID;
        public int MentorID
        {
            get { return mentorID; }
            set { mentorID = value; }
        }

        private int levelID;
        public int LevelID
        {
            get { return levelID; }
            set { levelID = value; }
        }

        private int kvantumID;
        public int KvantumID
        {
            get { return kvantumID; }
            set { kvantumID = value; }
        }

        private int groupID;
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public List<DictionaryItem> KvantumDict { get; set; }
        public string KvantumTitle
        {
            get { return KvantumDict.Find(x => x.ID == KvantumID).Title; } 
        }

        // В текущей реализации это набор данных о полной истории посещения
        // кванториум, которая хранится в student_program_hist;
        private int programID;

        public int ProgramID
        {
            get { return programID; }
            set { programID = value; OnPropertyChanged("ProgramID"); }
        }

        private int schoolID;

        public int SchoolID
        {
            get { return schoolID; }
            set { schoolID = value; OnPropertyChanged("SchoolID"); }
        }
    }
}
