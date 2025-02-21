using Dsmviz.Interfaces.Application.Metrics;
using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.ViewModel.Matrix;
using Dsmviz.Viewer.ViewModel.Common;

namespace Dsmviz.Viewer.ViewModel.Matrix
{
    public class MatrixRowMetricsViewModel(IMatrixViewModel matrixViewModel, IMetrics applicationMetrics)
        : ViewModelBase, IMatrixRowMetricsViewModel
    {
        private IReadOnlyList<IMatrixRowHeaderTreeItemViewModel> _elementViewModelLeafs = [];
        private string? _toolTipText;
        private MetricType _selectedMetricType = MetricType.NumberOfElements;

        public event EventHandler? RedrawRequested;

        public void Reload(MetricType selectedMetricType, IReadOnlyList<IMatrixRowHeaderTreeItemViewModel> elementViewModelLeafs)
        {
            _elementViewModelLeafs = elementViewModelLeafs;
            _selectedMetricType = selectedMetricType;
            RedrawRequested?.Invoke(this, EventArgs.Empty);
        }

        public void Redraw()
        {
            RedrawRequested?.Invoke(this, EventArgs.Empty);
        }

        public void ContentChanged(ContentChangeType changeType)
        {
            matrixViewModel.ContentChanged(changeType);
        }

        public int RowCount => _elementViewModelLeafs.Count;

        public int GetRowDepth(int row)
        {
            return _elementViewModelLeafs[row].Depth;
        }

        public string GetRowMetric(int row)
        {
            IMetric? metric = applicationMetrics.GetMetric(_selectedMetricType, _elementViewModelLeafs[row].Element);
            return metric != null ? metric.FormattedValue : "";
        }

        public void HoverRow(int? row)
        {
            matrixViewModel.HoverRow(row);
            UpdateTooltip(row);
        }

        public int? HoveredRow => matrixViewModel.HoveredRow;

        public void SelectRow(int? row)
        {
            matrixViewModel.SelectRow(row);
        }

        public int? SelectedRow => matrixViewModel.SelectedRow;

        public string? ToolTipText
        {
            get => _toolTipText;
            set { _toolTipText = value; OnPropertyChanged(); }
        }

        private void UpdateTooltip(int? row)
        {
            if (row.HasValue)
            {
                IElement element = _elementViewModelLeafs[row.Value].Element;
                ToolTipText = element.Name;
            }
        }
    }
}
