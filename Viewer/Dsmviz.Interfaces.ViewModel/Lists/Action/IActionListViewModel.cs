﻿using System.ComponentModel;
using System.Windows.Input;

namespace Dsmviz.Interfaces.ViewModel.Lists.Action
{
    public interface IActionListViewModel
    {
        event EventHandler<string>? TextReadyForClipboard;

        string Title { get; }
        IEnumerable<IActionListItemViewModel> Actions { get; set; }
        ICommand CopyToClipboardCommand { get; }
        ICommand ClearCommand { get; }
        event PropertyChangedEventHandler? PropertyChanged;
    }
}
