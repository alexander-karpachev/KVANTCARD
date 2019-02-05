using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Vms
{
    public class StudentVm : HumanVm
    {
        private List<ParentVm> _parents;
        public virtual List<ParentVm> Parents
        {
            get => _parents;
            set { SetProperty(ref _parents, value, () => Parents); }
        }

        private int _documentSetId;
        public int DocumentSetId
        {
            get => _documentSetId;
            set { SetProperty(ref _documentSetId, value, () => DocumentSetId); }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set { SetProperty(ref _dateTime, value, () => DateTime); }
        }

        private int _parent1Id;
        public int Parent1Id
        {
            get => _parent1Id;
            set { SetProperty(ref _parent1Id, value, () => Parent1Id); }
        }

        private int _mentorId;
        public int MentorId
        {
            get => _mentorId;
            set { SetProperty(ref _mentorId, value, () => MentorId); }
        }

        private int _levelId;
        public int LevelId
        {
            get => _levelId;
            set { SetProperty(ref _levelId, value, () => LevelId); }
        }

        private int _kvantumId;
        public int KvantumId
        {
            get => _kvantumId;
            set { SetProperty(ref _kvantumId, value, () => KvantumId); }
        }

        private int _groupId;
        public int GroupId
        {
            get => _groupId;
            set { SetProperty(ref _groupId, value, () => GroupId); }
        }

        //public List<DictionaryItem> KvantumDict { get; set; }
        //public string KvantumTitle
        //{
        //    get { return KvantumDict.Find(x => x.Id == KvantumId).Title; }
        //}

        // В текущей реализации это набор данных о полной истории посещения
        // кванториум, которая хранится в student_program_hist;
        private int _programId;
        public int ProgramId
        {
            get => _programId;
            set { SetProperty(ref _programId, value, () => ProgramId); }
        }

        private int _schoolId;
        public int SchoolId
        {
            get => _schoolId;
            set { SetProperty(ref _schoolId, value, () => SchoolId); }
        }
    }
}
