﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Dsmviz.Interfaces.ViewModel.Lists.Element
{
    public interface IElementListViewModel
    {
        event EventHandler<string>? TextReadyForClipboard;

        string Title { get; }
        string SubTitle { get; }
        ObservableCollection<IElementListItemViewModel> Elements { get; }
        ICommand CopyToClipboardCommand { get; }
        event PropertyChangedEventHandler? PropertyChanged;
    }
}
