using Dsmviz.Interfaces.Application.Matrix;
using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.ViewModel.Common;
using Dsmviz.Interfaces.ViewModel.Matrix;
using Dsmviz.Viewer.ViewModel.Common;

namespace Dsmviz.Viewer.ViewModel.Matrix
{
    public class MatrixCellsViewModel(
        IMatrixViewModel viewModel,
        IDependencyWeightMatrix dependencyWeightMatrix,
        IDependencyCycleMatrix dependencyCycleMatrix)
        : ViewModelBase, IMatrixCellsViewModel
    {
        private List<List<int>> _cellDepths = [];
        private string? _toolTipText;
        private IReadOnlyList<IMatrixRowHeaderTreeItemViewModel> _elementViewModelLeafs = [];

        public event EventHandler? RedrawRequested;

        public void Reload(IReadOnlyList<IMatrixRowHeaderTreeItemViewModel> elementViewModelLeafs)
        {
            _elementViewModelLeafs = elementViewModelLeafs;
            DefineCellDepths();
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

        public int MatrixSize => _elementViewModelLeafs.Count;

        public void HoverCell(int? row, int? column)
        {
            viewModel.HoverCell(row, column);
            UpdateTooltip(row, column);
        }

        public int? HoveredRow => viewModel.HoveredRow;
        public int? HoveredColumn => viewModel.HoveredColumn;

        public void SelectCell(int? row, int? column)
        {
            viewModel.SelectCell(row, column);
        }

        public int? SelectedRow => viewModel.SelectedRow;
        public int? SelectedColumn => viewModel.SelectedColumn;

        public int GetCellDepth(int row, int column)
        {
            return _cellDepths[row][column];
        }

        public int GetCellWeight(int row, int column)
        {
            IElement consumer = _elementViewModelLeafs[column].Element;
            IElement provider = _elementViewModelLeafs[row].Element;
            return dependencyWeightMatrix.GetDerivedDependencyWeight(consumer, provider);
        }

        public Cycle GetCellCycle(int row, int column)
        {
            IElement consumer = _elementViewModelLeafs[column].Element;
            IElement provider = _elementViewModelLeafs[row].Element;
            return dependencyCycleMatrix.GetCycle(consumer, provider);
        }

        public ViewPerspective SelectedViewPerspective => viewModel.SelectedViewPerspective;

        public string? ToolTipText
        {
            get => _toolTipText;
            set { _toolTipText = value; OnPropertyChanged(); }
        }

        private void DefineCellDepths()
        {
            int matrixSize = _elementViewModelLeafs.Count;

            _cellDepths = [];

            // Create multi dimensional array
            for (int row = 0; row < matrixSize; row++)
            {
                _cellDepths.Add([]);
                for (int column = 0; column < matrixSize; column++)
                {
                    _cellDepths[row].Add(0);
                }
            }

            // Determine cell depth
            for (int row = 0; row < matrixSize; row++)
            {
                IMatrixRowHeaderTreeItemViewModel viewModelLeaf = _elementViewModelLeafs[row];

                Stack<MatrixRowHeaderTreeItemViewModel> viewModelHierarchy = new Stack<MatrixRowHeaderTreeItemViewModel>();
                MatrixRowHeaderTreeItemViewModel? child = viewModelLeaf as MatrixRowHeaderTreeItemViewModel;
                MatrixRowHeaderTreeItemViewModel? parent = viewModelLeaf.Parent as MatrixRowHeaderTreeItemViewModel;
                while (parent != null && parent.Children[0] == child)
                {
                    viewModelHierarchy.Push(parent);
                    child = parent;
                    parent = parent.Parent as MatrixRowHeaderTreeItemViewModel;
                }

                foreach (MatrixRowHeaderTreeItemViewModel currentViewModel in viewModelHierarchy)
                {
                    int leafElements = 0;
                    CountLeafElements(currentViewModel.Element, ref leafElements);

                    if (leafElements > 0 && currentViewModel.Depth > 0)
                    {
                        int begin = row;
                        int end = row + leafElements;

                        for (int rowDelta = begin; rowDelta < end; rowDelta++)
                        {
                            for (int columnDelta = begin; columnDelta < end; columnDelta++)
                            {
                                _cellDepths[rowDelta][columnDelta] = currentViewModel.Depth;
                            }
                        }
                    }
                }
            }

            // Define diagonal depth
            for (int row = 0; row < matrixSize; row++)
            {
                _cellDepths[row][row] = _elementViewModelLeafs[row].Depth;
            }
        }

        private void CountLeafElements(IElement element, ref int count)
        {
            if (!element.IsExpanded)
            {
                count++;
            }
            else
            {
                foreach (IElement child in element.Children)
                {
                    CountLeafElements(child, ref count);
                }
            }
        }

        private void UpdateTooltip(int? row, int? column)
        {
            if (row.HasValue && column.HasValue)
            {
                IElement consumer = _elementViewModelLeafs[column.Value].Element;
                IElement provider = _elementViewModelLeafs[row.Value].Element;
                ToolTipText = consumer.Name + '\u2192' + provider.Name;
            }
        }
    }
}
