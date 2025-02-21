using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.ViewModel.Lists.Element;
using Dsmviz.Interfaces.ViewModel.Lists.Relation;
using System.ComponentModel;
using System.Windows.Input;

namespace Dsmviz.Interfaces.ViewModel.Sidebar
{
    public interface IMatrixRowSideBarViewModel
    {
        event EventHandler<IElementListViewModel>? ElementsReportReady;
        event EventHandler<IRelationListViewModel>? RelationsReportReady;
        bool Selected { get; }
        IElement SelectedElement { get; set; }
        string ElementName { get; set; }
        string ElementType { get; set; }
        int ConsumerCount { get; }
        ICommand ShowConsumerListCommand { get; }
        int ProvidedInterfaceCount { get; }
        ICommand ShowProvidedInterfaceListCommand { get; }
        int RequiredInterfaceCount { get; }
        ICommand ShowRequiredInterfacesListCommand { get; }
        int ChildrenCount { get; }
        ICommand ShowChildListCommand { get; }
        int IngoingRelationCount { get; }
        ICommand ShowIngoingRelationsListCommand { get; }
        int OutgoingRelationCount { get; }
        ICommand ShowOutgoingRelationsListCommand { get; }
        int InternalRelationCount { get; }
        ICommand ShowInternalRelationsListCommand { get; }
        void SelectRow(IElement selectedElement);
        void SelectColumn(IElement selectedElement);
        void Unselect();
        event PropertyChangedEventHandler? PropertyChanged;
    }
}
