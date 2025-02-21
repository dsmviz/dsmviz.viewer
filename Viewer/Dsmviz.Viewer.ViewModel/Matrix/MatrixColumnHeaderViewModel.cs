using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.ViewModel.Matrix;
using Dsmviz.Viewer.ViewModel.Common;

namespace Dsmviz.Viewer.ViewModel.Matrix
{
    public class MatrixColumnHeaderViewModel(IMatrixViewModel viewModel) : ViewModelBase, IMatrixColumnHeaderViewModel
    {
        private IReadOnlyList<IMatrixRowHeaderTreeItemViewModel> _elementViewModelLeafs = [];
        private string? _toolTipText;

        public event EventHandler? RedrawRequested;

        public void Reload(IReadOnlyList<IMatrixRowHeaderTreeItemViewModel> elementViewModelLeafs)
        {
            _elementViewModelLeafs = elementViewModelLeafs;
            RedrawRequested?.Invoke(this, EventArgs.Empty);
        }

        public void Redraw()
        {
            RedrawRequested?.Invoke(this, EventArgs.Empty);
        }

        public void ContentChanged(ContentChangeType changeType)
        {
            viewModel.ContentChanged(changeType);
        }

        public int ColumnCount => _elementViewModelLeafs.Count;

        public void HoverColumn(int? column)
        {
            viewModel.HoverColumn(column);
            UpdateTooltip(column);
        }

        public int? HoveredColumn => viewModel.HoveredColumn;

        public void SelectColumn(int? column)
        {
            viewModel.SelectColumn(column);
        }

        public int? SelectedColumn => viewModel.SelectedColumn;

        public int GetColumnDepth(int column)
        {
            return _elementViewModelLeafs[column].Depth;
        }

        public string GetColumnContent(int column)
        {
            return _elementViewModelLeafs[column].Element.Order.ToString();
        }

        public string? ToolTipText
        {
            get => _toolTipText;
            set { _toolTipText = value; OnPropertyChanged(); }
        }

        private void UpdateTooltip(int? column)
        {
            if (column.HasValue)
            {
                IElement element = _elementViewModelLeafs[column.Value].Element;
                ToolTipText = element.Name;
            }
        }
    }
}
