using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Eiriklb.WpfTools.GenericDatagrid
{
    public class AllGenericDatagridVm : INotifyPropertyChanged
    {
        public DataGridSelectionUnit[] AllPossibleSelectionUnits { get; } = (DataGridSelectionUnit[])Enum.GetValues(typeof(DataGridSelectionUnit));

        private bool _rowsSelected;

        public bool RowsSelected
        {
            get => _rowsSelected;
            set
            {
                if (value == _rowsSelected)
                    return;

                _rowsSelected = value;
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
