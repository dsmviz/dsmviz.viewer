﻿using Dsmviz.Interfaces.Data.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Dsmviz.Interfaces.ViewModel.Search
{
    public interface IElementSearchViewModel
    {
        event EventHandler? SearchUpdated;
        List<string> ElementTypes { get; }
        ICommand ClearSearchCommand { get; }
        string SearchPath { get; }
        IElement? SelectedElement { get; set; }
        string SearchText { get; set; }
        bool CaseSensitiveSearch { get; set; }
        string SelectedElementType { get; set; }
        ObservableCollection<string> SearchMatches { get; }
        SearchState SearchState { get; set; }
        string SearchResult { get; set; }
        void ClearSearchExecute(object? parameter);
        event PropertyChangedEventHandler? PropertyChanged;
    }
}
