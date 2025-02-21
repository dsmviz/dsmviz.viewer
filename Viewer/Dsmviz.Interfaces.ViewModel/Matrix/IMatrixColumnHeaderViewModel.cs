using System.ComponentModel;

namespace Dsmviz.Interfaces.ViewModel.Matrix
{
    public interface IMatrixColumnHeaderViewModel : INotifyPropertyChanged
    {
        event EventHandler? RedrawRequested;

        void ContentChanged(ContentChangeType changeType);

        void HoverColumn(int? column);
        int? HoveredColumn { get; }

        void SelectColumn(int? column);
        int? SelectedColumn { get; }

        int ColumnCount { get; }

        int GetColumnDepth(int column);
        string GetColumnContent(int column);

        string? ToolTipText { get; }
    }
}
