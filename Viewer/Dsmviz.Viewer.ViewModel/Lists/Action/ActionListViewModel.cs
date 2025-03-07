﻿using Dsmviz.Interfaces.Application.Editing;
using Dsmviz.Interfaces.ViewModel.Lists.Action;
using Dsmviz.Viewer.ViewModel.Common;
using System.Text;
using System.Windows.Input;

namespace Dsmviz.Viewer.ViewModel.Lists.Action
{
    public class ActionListViewModel : ViewModelBase, IActionListViewModel
    {
        private readonly IActionManagement _actionManagement;
        private IEnumerable<IActionListItemViewModel> _actions = [];

        public event EventHandler<string>? TextReadyForClipboard;
        public ActionListViewModel(IActionManagement actionManagement)
        {
            Title = "Edit history";

            _actionManagement = actionManagement;
            _actionManagement.ActionPerformed += OnActionPerformed;

            UpdateActionList();

            CopyToClipboardCommand = RegisterCommand(CopyToClipboardExecute);
            ClearCommand = RegisterCommand(ClearExecute);
        }

        private void OnActionPerformed(object? sender, EventArgs e)
        {
            UpdateActionList();
        }

        public string Title { get; }

        public IEnumerable<IActionListItemViewModel> Actions
        {
            get => _actions;
            set { _actions = value; OnPropertyChanged(); }
        }

        public ICommand CopyToClipboardCommand { get; }
        public ICommand ClearCommand { get; }

        private void CopyToClipboardExecute(object? parameter)
        {
            StringBuilder builder = new StringBuilder();
            foreach (IActionListItemViewModel viewModel in Actions)
            {
                builder.AppendLine($"{viewModel.Index,-5}, {viewModel.Action,-30}, {viewModel.Details}");
            }
            TextReadyForClipboard?.Invoke(this, builder.ToString());
        }

        private void ClearExecute(object? parameter)
        {
            _actionManagement.ClearActions();
            UpdateActionList();
        }

        private void UpdateActionList()
        {
            var actionViewModels = new List<ActionListItemViewModel>();
            int index = 1;
            foreach (IAction action in _actionManagement.GetActions())
            {
                actionViewModels.Add(new ActionListItemViewModel(index, action));
                index++;
            }

            Actions = actionViewModels;
        }
    }
}
