using Dsmviz.Interfaces.ViewModel.Editing.Relation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Dsmviz.Interfaces.ViewModel.Lists.Relation
{
    public interface IRelationListViewModel
    {
        event EventHandler<IRelationEditViewModel>? RelationAddStarted;
        event EventHandler<IRelationEditViewModel>? RelationEditStarted;
        event EventHandler<string>? TextReadyForClipboard;

        string Title { get; }
        string SubTitle { get; }
        ObservableCollection<IRelationListItemViewModel> Relations { get; }
        IRelationListItemViewModel? SelectedRelation { get; set; }
        ICommand CopyToClipboardCommand { get; }
        ICommand DeleteRelationCommand { get; }
        ICommand EditRelationCommand { get; }
        ICommand AddRelationCommand { get; }
        event PropertyChangedEventHandler? PropertyChanged;
    }
}
