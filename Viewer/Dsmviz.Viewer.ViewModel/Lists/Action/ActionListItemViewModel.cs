﻿using Dsmviz.Interfaces.Application.Editing;
using Dsmviz.Interfaces.ViewModel.Lists.Action;
using Dsmviz.Viewer.ViewModel.Common;

namespace Dsmviz.Viewer.ViewModel.Lists.Action
{
    public class ActionListItemViewModel(int index, IAction action) : ViewModelBase, IActionListItemViewModel
    {
        public int Index { get; } = index;
        public string Action { get; } = action.Title;
        public string Details { get; } = action.Description;
    }
}
