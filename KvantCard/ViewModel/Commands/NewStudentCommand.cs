using KvantCard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KvantCard.ViewModel.Commands
{
    public class NewStudentCommand : ICommand
    {
        public StudentsVM VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public NewStudentCommand(StudentsVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Student selectedStudent = parameter as Student;
            VM.CreateNewStudent();
            //TODO: Implement new student
        }
    }
}
