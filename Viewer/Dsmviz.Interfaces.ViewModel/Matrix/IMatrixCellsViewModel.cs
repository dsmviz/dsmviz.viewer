using Dsmviz.Interfaces.Application.Matrix;
using Dsmviz.Interfaces.ViewModel.Common;
using System.ComponentModel;

namespace Dsmviz.Interfaces.ViewModel.Matrix
{
    public interface IMatrixCellsViewModel : INotifyPropertyChanged
    {
        event EventHandler? RedrawRequested;

        void ContentChanged(ContentChangeType changeType);

        void HoverCell(int? row, int? column);
        int? HoveredRow { get; }
        int? HoveredColumn { get; }

        void SelectCell(int? row, int? column);
        int? SelectedRow { get; }
        int? SelectedColumn { get; }

        int MatrixSize { get; }

        string? ToolTipText { get; }

        int GetCellDepth(int row, int column);
        int GetCellWeight(int row, int column);
        Cycle GetCellCycle(int row, int column);

        ViewPerspective SelectedViewPerspective { get; }
    }
}
