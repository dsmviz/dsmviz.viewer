using Dsmviz.Interfaces.Data.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Dsmviz.Interfaces.ViewModel.Matrix
{
    public interface IMatrixRowHeaderViewModel : INotifyPropertyChanged
    {
        event EventHandler? ReloadRequested;
        event EventHandler? RedrawRequested;

        void ContentChanged(ContentChangeType changeType);

        void ChangeElementParent(IElement element, IElement newParent, int index);

        ObservableCollection<IMatrixRowHeaderTreeItemViewModel> ElementViewModelTree { get; }

        void HoverTreeItem(IMatrixRowHeaderTreeItemViewModel? hoveredTreeItem);
        IMatrixRowHeaderTreeItemViewModel? HoveredTreeItem { get; }

        void SelectTreeItem(IMatrixRowHeaderTreeItemViewModel? selectedTreeItem);
        IMatrixRowHeaderTreeItemViewModel? SelectedTreeItem { get; }
    }
}
