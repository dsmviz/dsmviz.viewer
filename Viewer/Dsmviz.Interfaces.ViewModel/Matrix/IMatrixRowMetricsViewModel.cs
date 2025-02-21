using System.ComponentModel;

namespace Dsmviz.Interfaces.ViewModel.Matrix
{
    public interface IMatrixRowMetricsViewModel : INotifyPropertyChanged
    {
        event EventHandler? RedrawRequested;

        void ContentChanged(ContentChangeType changeType);

        void HoverRow(int? row);
        int? HoveredRow { get; }

        void SelectRow(int? row);
        int? SelectedRow { get; }

        int RowCount { get; }

        int GetRowDepth(int row);
        string GetRowMetric(int row);

        string? ToolTipText { get; }
    }
}
