using Dsmviz.Interfaces.ViewModel.Lists.Element;
using System.Windows;

namespace Dsmviz.Viewer.View.Lists
{
    /// <summary>
    /// Interaction logic for ElementListView.xaml
    /// </summary>
    public partial class ElementListView
    {
        private IElementListViewModel? _viewModel;

        public ElementListView()
        {
            InitializeComponent();
        }

        private void ElementListView_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as IElementListViewModel;
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
