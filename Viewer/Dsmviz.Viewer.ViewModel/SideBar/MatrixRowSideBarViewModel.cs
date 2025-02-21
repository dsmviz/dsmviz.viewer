using Dsmviz.Interfaces.Application.Editing;
using Dsmviz.Interfaces.Application.Query;
using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.ViewModel.Lists.Element;
using Dsmviz.Interfaces.ViewModel.Lists.Relation;
using Dsmviz.Interfaces.ViewModel.Sidebar;
using Dsmviz.Viewer.ViewModel.Common;
using Dsmviz.Viewer.ViewModel.Lists.Element;
using Dsmviz.Viewer.ViewModel.Lists.Relation;
using System.Windows.Input;

namespace Dsmviz.Viewer.ViewModel.SideBar
{
    public class MatrixRowSideBarViewModel : ViewModelBase, IMatrixRowSideBarViewModel
    {
        // Interfaces
        private readonly IRelationEditing _relationEditing;
        private readonly IRelationQuery _relationQuery;

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

        private int _consumerCount;
        private int _providedInterfaceCount;
        private int _requiredInterfaceCount;
        private int _childrenCount;

        private int _ingoingRelationCount;
        private int _outgoingRelationCount;
        private int _internalRelationCount;


        private void NotifyRelationsReportReady(RelationListViewModelType viewModelType, IElement selectedProvider)
        {
            RelationListViewModel viewModel = new RelationListViewModel(viewModelType, _relationQuery, _relationEditing, null, selectedProvider);
            RelationsReportReady?.Invoke(this, viewModel);
        }

        private void NotifyElementsReportReady(ElementListViewModelType viewModelType, IElement selectedProvider)
        {
            ElementListViewModel viewModel = new ElementListViewModel(viewModelType, _relationQuery, null, selectedProvider);
            ElementsReportReady?.Invoke(this, viewModel);
        }
        public event EventHandler<IElementListViewModel>? ElementsReportReady;
        public event EventHandler<IRelationListViewModel>? RelationsReportReady;

        public MatrixRowSideBarViewModel(IRelationEditing relationEditing, IRelationQuery relationQuery)
        {
            _relationEditing = relationEditing;
            _relationQuery = relationQuery;

            ShowConsumerListCommand = RegisterCommand(ShowConsumerListExecute, ShowConsumerListCanExecute);
            ShowProvidedInterfaceListCommand = RegisterCommand(ShowProvidedInterfaceListExecute, ShowProvidedInterfaceListCanExecute);
            ShowRequiredInterfacesListCommand = RegisterCommand(ShowRequiredInterfacesListExecute, ShowRequiredInterfacesListCanExecute);
            ShowChildListCommand = RegisterCommand(ShowChildListExecute, ShowChildListCanExecute);
            ShowIngoingRelationsListCommand = RegisterCommand(ShowIngoingRelationsListExecute, ShowIngoingRelationsListCanExecute);
            ShowOutgoingRelationsListCommand = RegisterCommand(ShowOutgoingRelationsListExecute, ShowOutgoingRelationsListCanExecute);
            ShowInternalRelationsListCommand = RegisterCommand(ShowInternalRelationsListExecute, ShowInternalRelationsListCanExecute);
        }

        public void SelectRow(IElement selectedElement)
        {
            _selectedConsumer = null;
            _selectedProvider = selectedElement;

            Selected = true;
            SelectedElement = selectedElement;

            UpdateAfterSelection();
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

        public int ConsumerCount
        {
            get => _consumerCount;
            private set { _consumerCount = value; OnPropertyChanged(); }
        }

        public ICommand ShowConsumerListCommand { get; }

        public int ProvidedInterfaceCount
        {
            get => _providedInterfaceCount;
            private set { _providedInterfaceCount = value; OnPropertyChanged(); }
        }

        public ICommand ShowProvidedInterfaceListCommand { get; }

        public int RequiredInterfaceCount
        {
            get => _requiredInterfaceCount;
            private set { _requiredInterfaceCount = value; OnPropertyChanged(); }
        }

        public ICommand ShowRequiredInterfacesListCommand { get; }

        public int ChildrenCount
        {
            get => _childrenCount;
            private set { _childrenCount = value; OnPropertyChanged(); }
        }

        public ICommand ShowChildListCommand { get; }

        public int IngoingRelationCount
        {
            get => _ingoingRelationCount;
            private set { _ingoingRelationCount = value; OnPropertyChanged(); }
        }

        public ICommand ShowIngoingRelationsListCommand { get; }

        public int OutgoingRelationCount
        {
            get => _outgoingRelationCount;
            private set { _outgoingRelationCount = value; OnPropertyChanged(); }
        }

        public ICommand ShowOutgoingRelationsListCommand { get; }

        public int InternalRelationCount
        {
            get => _internalRelationCount;
            private set { _internalRelationCount = value; OnPropertyChanged(); }
        }

        public ICommand ShowInternalRelationsListCommand { get; }

        private void ShowConsumerListExecute(object? parameter)
        {
            if (SelectedElement != null)
            {
                NotifyElementsReportReady(ElementListViewModelType.ElementConsumers, SelectedElement);
            }
        }

        private bool ShowConsumerListCanExecute(object? parameter)
        {
            return true;
        }

        private void ShowProvidedInterfaceListExecute(object? parameter)
        {
            if (SelectedElement != null)
            {
                NotifyElementsReportReady(ElementListViewModelType.ElementProvidedInterface, SelectedElement);
            }
        }

        private bool ShowProvidedInterfaceListCanExecute(object? parameter)
        {
            return true;
        }

        private void ShowRequiredInterfacesListExecute(object? parameter)
        {
            if (SelectedElement != null)
            {
                NotifyElementsReportReady(ElementListViewModelType.ElementRequiredInterface, SelectedElement);
            }
        }

        private bool ShowRequiredInterfacesListCanExecute(object? parameter)
        {
            return true;
        }

        private void ShowChildListExecute(object? parameter)
        {

        }

        private bool ShowChildListCanExecute(object? parameter)
        {
            return true;
        }

        private void ShowIngoingRelationsListExecute(object? parameter)
        {
            if (SelectedElement != null)
            {
                NotifyRelationsReportReady(RelationListViewModelType.ElementIngoingRelations, SelectedElement);
            }
        }

        private bool ShowIngoingRelationsListCanExecute(object? parameter)
        {
            return true;
        }

        private void ShowOutgoingRelationsListExecute(object? parameter)
        {
            if (SelectedElement != null)
            {
                NotifyRelationsReportReady(RelationListViewModelType.ElementOutgoingRelations, SelectedElement);
            }
        }

        private bool ShowOutgoingRelationsListCanExecute(object? parameter)
        {
            return true;
        }

        private void ShowInternalRelationsListExecute(object? parameter)
        {
            if (SelectedElement != null)
            {
                NotifyRelationsReportReady(RelationListViewModelType.ElementInternalRelations, SelectedElement);
            }
        }

        private bool ShowInternalRelationsListCanExecute(object? parameter)
        {
            return true;
        }

        private void UpdateAfterSelection()
        {
            if (_selectedProvider != null)
            {
                ConsumerCount = _relationQuery.GetElementConsumers(_selectedProvider).Count();

                ProvidedInterfaceCount = _relationQuery.GetElementInterface(_selectedProvider).Count();
                RequiredInterfaceCount = _relationQuery.GetElementProviders(_selectedProvider).Count();

                ChildrenCount = _selectedProvider.TotalElementCount - 1;

                IngoingRelationCount = _relationQuery.GetAllIngoingRelations(_selectedProvider).Count();
                OutgoingRelationCount = _relationQuery.GetAllOutgoingRelations(_selectedProvider).Count();
                InternalRelationCount = _relationQuery.GetAllInternalRelations(_selectedProvider).Count();
            }

            NotifyCommandsCanExecuteChanged();
        }
    }
}
