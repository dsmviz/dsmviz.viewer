using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.ViewModel.Lists.Element;
using Dsmviz.Interfaces.ViewModel.Lists.Relation;
using System.ComponentModel;

namespace Dsmviz.Interfaces.ViewModel.Sidebar
{
    public interface IMatrixColumnSideBarViewModel
    {
        event EventHandler<IElementListViewModel>? ElementsReportReady;
        event EventHandler<IRelationListViewModel>? RelationsReportReady;
        bool Selected { get; }
        IElement SelectedElement { get; set; }
        string ElementName { get; set; }
        string ElementType { get; set; }

        void SelectRow(IElement selectedElement);
        void SelectColumn(IElement selectedElement);
        void Unselect();
        event PropertyChangedEventHandler? PropertyChanged;
    }
}
