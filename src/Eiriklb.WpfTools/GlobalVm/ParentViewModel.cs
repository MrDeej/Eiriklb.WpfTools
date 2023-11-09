using System.Collections.ObjectModel;
using Eiriklb.WpfTools.Logviewer;

namespace Eiriklb.WpfTools.GlobalVm
{
    public class ParentViewModel
    {

        public InMemoryData InMemoryData => InMemoryData.Instance;

        public ObservableCollection<RunningTask> Tasks { get; } = new();

        public RunningTask AddTask(string status, bool canUserCancel)
        {
            var task = new RunningTask(status, canUserCancel, Tasks); // Assume you have a suitable constructor
            Tasks.Add(task);
            return task;
        }

   
    }
}
