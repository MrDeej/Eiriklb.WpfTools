using System.Collections.ObjectModel;
using System.ComponentModel;
using Eiriklb.WpfTools.GlobalVm;
using Eiriklb.WpfTools.Logviewer;

namespace Eiriklb.WpfTools
{
    public class InMemoryData
    {

        public static InMemoryData Instance { get; } = new InMemoryData();


        private InMemoryData()
        {


            if (!DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) 
                return;

        }


        public ObservableCollection<WpfLogItem> ObsCollLogg { get; } = new();
        public ObservableCollection<RunningTask> Tasks { get; } = new();

    }
}
