using Dsmviz.Interfaces.Util;
using System.ComponentModel;
using System.Windows.Input;

namespace Dsmviz.Interfaces.ViewModel.Settings
{
    public interface ISettingsViewModel
    {
        ICommand AcceptChangeCommand { get; }
        LogLevel LogLevel { get; set; }
        List<string> SupportedThemeNames { get; }
        string SelectedThemeName { get; set; }
        event PropertyChangedEventHandler? PropertyChanged;
    }
}
