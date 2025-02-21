using Dsmviz.Interfaces.Util;
using Dsmviz.Interfaces.ViewModel.Editing.Element;
using Dsmviz.Interfaces.ViewModel.Lists.Action;
using Dsmviz.Interfaces.ViewModel.Main;
using Dsmviz.Interfaces.ViewModel.Settings;
using Dsmviz.Viewer.Configuration;
using Dsmviz.Viewer.View.Editing;
using Dsmviz.Viewer.View.Lists;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SettingsView = Dsmviz.Viewer.View.Settings.SettingsView;

namespace Dsmviz.Viewer.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private IMainViewModel? _mainViewModel;
        private ProgressWindow? _progressWindow;

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_mainViewModel is { IsModified: true })
            {
                e.Cancel = MessageBox.Show("Are you sure to exit?", "You have unsaved changes", MessageBoxButton.YesNo) != MessageBoxResult.Yes;
            }
        }

        public void OpenModel(string filename)
        {
            if (_mainViewModel != null)
            {
                _mainViewModel.OpenFileCommand.Execute(filename);
            }
        }

        private void MainWindow_OnLoaded(object? sender, RoutedEventArgs e)
        {
            _mainViewModel = SoftwareConfiguration.CreateMainViewModel();
            _mainViewModel.ProgressViewModel.StateChanged += OnProgressViewModelStateChanged;

            _mainViewModel.ElementEditStarted += OnElementEditStarted;

            _mainViewModel.ActionsVisible += OnActionsVisible;
            _mainViewModel.SettingsVisible += OnSettingsVisible;

            _mainViewModel.ScreenshotRequested += OnScreenshotRequested;
            DataContext = _mainViewModel;

            OpenModelFile();
        }

        private void OnSettingsVisible(object? sender, ISettingsViewModel viewModel)
        {
            SettingsView view = new SettingsView { DataContext = viewModel };
            view.ShowDialog();
        }

        private void OnActionsVisible(object? sender, IActionListViewModel viewModel)
        {
            ActionListView view = new ActionListView { DataContext = viewModel };
            view.Show();
        }

        private void OnElementEditStarted(object? sender, IElementEditViewModel viewModel)
        {
            ElementEditDialog view = new ElementEditDialog { DataContext = viewModel };
            view.ShowDialog();
        }

        private void OpenModelFile()
        {
            // TODO FIX
            //App app = System.Windows.Application.Current as App;
            //if ((app != null) && (app..CommandLineArguments.Length == 1))
            //{
            //    string filename = app.CommandLineArguments[0];
            //    if (filename.EndsWith(".dsm") || filename.EndsWith(".dsi") || filename.EndsWith(".dsr"))
            //    {
            //        _mainViewModel.OpenFileCommand.Execute(filename);
            //    }
            //}
        }

        private void OnProgressViewModelStateChanged(object? sender, ProgressState state)
        {
            if (state != ProgressState.Ready)
            {
                if (_progressWindow == null)
                {
                    _progressWindow = new ProgressWindow
                    {
                        DataContext = _mainViewModel?.ProgressViewModel,
                        Owner = this
                    };
                    _progressWindow.ShowDialog();
                }
            }
            else
            {
                if (_progressWindow != null)
                {
                    _progressWindow.Close();
                    _progressWindow = null;
                }
            }
        }

        private void OnScreenshotRequested(object? sender, EventArgs e)
        {
            if (_mainViewModel != null)
            {
                const int leftMargin = 5;
                const int topMargin = 70;
                const int bottomMargin = 2;
                int width = (int)(Matrix.UsedWidth * _mainViewModel.MatrixViewModel.ZoomLevel) + leftMargin;
                int height = (int)(Matrix.UsedHeight * _mainViewModel.MatrixViewModel.ZoomLevel) + topMargin;
                RenderTargetBitmap renderTargetBitmap =
                    new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(Matrix);
                Int32Rect rect = new Int32Rect(leftMargin, topMargin, width - leftMargin,
                    height - topMargin - bottomMargin);
                CroppedBitmap croppedBitmap = new CroppedBitmap(renderTargetBitmap, rect);
                Clipboard.SetImage(croppedBitmap);
            }
        }
    }
}
