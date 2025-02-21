using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.ViewModel.Common;
using System.ComponentModel;

namespace Dsmviz.Interfaces.ViewModel.Matrix
{
    public interface IMatrixRowHeaderTreeItemViewModel
    {
        event EventHandler? RedrawRequested;

        void ContentChanged(ContentChangeType changeType);

        string? ToolTipText { get; }
        IElement Element { get; }
        int Depth { get; }
        int Id { get; }
        int Order { get; }
        bool IsConsumer { get; set; }
        bool IsProvider { get; set; }
        bool IsMatch { get; }
        bool IsBookmarked { get; }
        string Name { get; }
        bool IsSearchActive { get; }
        bool IsExpandable { get; }
        bool IsExpanded { get; set; }
        IReadOnlyList<IMatrixRowHeaderTreeItemViewModel> Children { get; }
        IMatrixRowHeaderTreeItemViewModel? Parent { get; }
        ViewPerspective SelectedViewPerspective { get; }
        int LeafElementCount { get; }
        bool ToggleElementExpanded();
        void AddChild(IMatrixRowHeaderTreeItemViewModel viewModel);
        void ClearChildren();
        event PropertyChangedEventHandler? PropertyChanged;
    }
}
