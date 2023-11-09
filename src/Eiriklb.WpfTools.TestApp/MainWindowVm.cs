using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Eiriklb.WpfTools.TestApp
{
    public class MainWindowVm : GlobalVm.ParentViewModel, INotifyPropertyChanged
    {
        private int _duration = 25000;

        public int Duration
        {
            get => _duration;
            set
            {
                if (value == _duration)
                    return;

                _duration = value;
                NotifyPropertyChanged();
            }
        }

        private bool _canUserCancel;

        public bool CanUserCancel
        {
            get => _canUserCancel;
            set
            {
                if (value == _canUserCancel)
                    return;

                _canUserCancel = value;
                NotifyPropertyChanged();
            }
        }


        private string _taskText = "A Random task";

        public string TaskText
        {
            get => _taskText;
            set
            {
                if (value == _taskText)
                    return;

                _taskText = value;
                NotifyPropertyChanged();
            }
        }


        private int _tasksStarted;

        public int TasksStarted
        {
            get => _tasksStarted;
            set
            {
                if (value == _tasksStarted)
                    return;

                _tasksStarted = value;
                NotifyPropertyChanged();
            }
        }
      



        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      
    }
}
