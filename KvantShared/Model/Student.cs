using System;
using System.Collections.Generic;

namespace KvantShared.Model
{
    public class Student : Human
    {
        public virtual List<Parent> Parents { get; set; }

        public int DocumentSetId { get; set; }

        public DateTime DateTime { get; set; }

        public int Parent1Id { get; set; }

        public int MentorId { get; set; }

        public int LevelId { get; set; }

        public int KvantumId { get; set; }

        public int GroupId { get; set; }

        //public List<DictionaryItem> KvantumDict { get; set; }
        //public string KvantumTitle
        //{
        //    get { return KvantumDict.Find(x => x.Id == KvantumId).Title; }
        //}

        // В текущей реализации это набор данных о полной истории посещения
        // кванториум, которая хранится в student_program_hist;
        public int ProgramId { get; set; }

        public int SchoolId { get; set; }
    }
}
