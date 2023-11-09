using System.Collections.ObjectModel;
using Eiriklb.WpfTools.Logviewer;

namespace Eiriklb.WpfTools.GlobalVm
{
    public class ParentViewModel
    {

        public InMemoryData InMemoryData => InMemoryData.Instance;


        public RunningTask AddTask(string status, bool canUserCancel)
        {
            var task = new RunningTask(status, canUserCancel, InMemoryData.Tasks); // Assume you have a suitable constructor
            InMemoryData.Tasks.Add(task);
            return task;
        }

   
    }
}
