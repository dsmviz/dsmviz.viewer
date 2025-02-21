using Dsmviz.Interfaces.ViewModel.Lists.Action;
using System.Windows;

namespace Dsmviz.Viewer.View.Lists
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class ActionListView
    {
        private IActionListViewModel? _viewModel;

        public ActionListView()
        {
            InitializeComponent();
        }

        private void ActionListView_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as IActionListViewModel;
            if (_viewModel != null)
            {
                _viewModel.TextReadyForClipboard += OnTextReadyForClipboard;
            }
        }

        private void OnTextReadyForClipboard(object? sender, string e)
        {
            Clipboard.SetText(e);
        }
    }
}
