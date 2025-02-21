using Dsmviz.Interfaces.Application.Metrics;
using System.ComponentModel;

namespace Dsmviz.Interfaces.ViewModel.Matrix
{
    public interface IMatrixMetricsSelectorViewModel : INotifyPropertyChanged
    {
        event EventHandler? RedrawRequested;

        void ContentChanged(ContentChangeType changeType);

        MetricType SelectedMetricType { get; }
    }
}
