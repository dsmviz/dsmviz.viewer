using Dsmviz.Interfaces.ViewModel.Lists.Element;
using Dsmviz.Interfaces.ViewModel.Lists.Relation;
using Dsmviz.Interfaces.ViewModel.Sidebar;
using Dsmviz.Viewer.View.Lists;
using System.Windows;

namespace Dsmviz.Viewer.View.SideBar
{
    /// <summary>
    /// Interaction logic for MatrixElementInfoView.xaml
    /// </summary>
    public partial class SelectedMatrixColumnSideBarView
    {
        private IMatrixColumnSideBarViewModel? _viewModel;

        public SelectedMatrixColumnSideBarView()
        {
            InitializeComponent();
        }

        private void OnElementsReportReady(object? sender, IElementListViewModel e)
        {
            ElementListView view = new ElementListView
            {
                DataContext = e,
            };
            view.Show();
        }

        private void OnRelationsReportReady(object? sender, IRelationListViewModel e)
        {
            RelationListView view = new RelationListView
            {
                DataContext = e,
            };
            view.Show();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as IMatrixColumnSideBarViewModel;
            if (_viewModel != null)
            {
                _viewModel.ElementsReportReady += OnElementsReportReady;
                _viewModel.RelationsReportReady += OnRelationsReportReady;
            }
            InvalidateVisual();
        }
    }
}
