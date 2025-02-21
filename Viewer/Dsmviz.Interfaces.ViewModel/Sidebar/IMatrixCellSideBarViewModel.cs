using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.ViewModel.Lists.Relation;
using System.ComponentModel;
using System.Windows.Input;

namespace Dsmviz.Interfaces.ViewModel.Sidebar
{
    public interface IMatrixCellSideBarViewModel
    {
        event EventHandler<IRelationListViewModel>? RelationsReportReady;
        bool Selected { get; }
        IElement? SelectedConsumer { get; set; }
        IElement? SelectedProvider { get; set; }
        int CellDerivedWeight { get; }
        string CellCycle { get; }
        int RelationCount { get; }
        ICommand ShowRelationsListCommand { get; }
        void SelectCell(IElement selectedConsumer, IElement selectedProvider);
        void Unselect();
        event PropertyChangedEventHandler? PropertyChanged;
    }
}
