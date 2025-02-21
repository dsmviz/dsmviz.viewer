using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.ViewModel.Lists.Element;
using Dsmviz.Interfaces.ViewModel.Lists.Relation;
using Dsmviz.Interfaces.ViewModel.Sidebar;
using Dsmviz.Viewer.ViewModel.Common;
using Dsmviz.Viewer.ViewModel.Lists.Element;

namespace Dsmviz.Viewer.ViewModel.SideBar
{
    public class MatrixColumnSideBarViewModel : ViewModelBase, IMatrixColumnSideBarViewModel
    {
        // Selection
        private IElement? _selectedElement;
        private IElement? _selectedConsumer;
        private IElement? _selectedProvider;
        private bool _selected;

        // Element list
        private ElementListViewModelType _viewModelType = ElementListViewModelType.ElementConsumers;

        // Element properties
        private string _elementName;
        private string _elementType;

        public event EventHandler<IElementListViewModel>? ElementsReportReady;
        public event EventHandler<IRelationListViewModel>? RelationsReportReady;

        public MatrixColumnSideBarViewModel()
        {
        }

        public void SelectRow(IElement selectedElement)
        {
            _selectedConsumer = null;
            _selectedProvider = selectedElement;

            Selected = true;
            SelectedElement = selectedElement;
        }

        public void SelectColumn(IElement selectedElement)
        {
            _selectedConsumer = selectedElement;
            _selectedProvider = null;

            Selected = true;
            SelectedElement = selectedElement;
        }

        public void Unselect()
        {
            Selected = false;
            SelectedElement = null;
        }

        public bool Selected
        {
            get => _selected;
            private set { _selected = value; OnPropertyChanged(); }
        }

        public IElement? SelectedElement
        {
            get => _selectedElement;
            set { _selectedElement = value; OnPropertyChanged(); }
        }

        public string ElementName
        {
            get => _elementName;
            set { _elementName = value; OnPropertyChanged(); }
        }

        public string ElementType
        {
            get => _elementType;
            set { _elementType = value; OnPropertyChanged(); }
        }
    }
}
