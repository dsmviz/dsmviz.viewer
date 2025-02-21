using Dsmviz.Interfaces.ViewModel.Editing.Relation;
using Dsmviz.Interfaces.ViewModel.Lists.Relation;
using Dsmviz.Viewer.View.Editing;
using System.Windows;

namespace Dsmviz.Viewer.View.Lists
{
    /// <summary>
    /// Interaction logic for RelationListView.xaml
    /// </summary>
    public partial class RelationListView
    {
        private IRelationListViewModel? _viewModel;

        public RelationListView()
        {
            InitializeComponent();
        }

        private void RelationListView_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as IRelationListViewModel;
            if (_viewModel != null)
            {
                _viewModel.RelationAddStarted += OnRelationAddStarted;
                _viewModel.RelationEditStarted += OnRelationEditStarted;
                _viewModel.TextReadyForClipboard += OnTextReadyForClipboard;
            }
        }

        private void OnTextReadyForClipboard(object? sender, string e)
        {
            Clipboard.SetText(e);
        }

        private void OnRelationAddStarted(object? sender, IRelationEditViewModel viewModel)
        {
            RelationEditDialog view = new RelationEditDialog { DataContext = viewModel };
            view.ShowDialog();
        }

        private void OnRelationEditStarted(object? sender, IRelationEditViewModel viewModel)
        {
            RelationEditDialog view = new RelationEditDialog { DataContext = viewModel };
            view.ShowDialog();
        }
    }
}
