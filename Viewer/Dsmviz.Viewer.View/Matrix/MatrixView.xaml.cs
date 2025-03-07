﻿using System.Windows.Controls;

namespace Dsmviz.Viewer.View.Matrix
{
    /// <summary>
    /// Interaction logic for MatrixView.xaml
    /// </summary>
    public partial class MatrixView
    {
        public MatrixView()
        {
            InitializeComponent();
        }

        public double UsedWidth => RowHeaderView.ActualWidth +
            Splitter1.ActualWidth +
            MatrixMetricsSelectorView.ActualWidth +
            Math.Min(CellsView.ActualWidth,
            ScrolledCellsView.ActualWidth) +
            Splitter1.ActualWidth +
            MatrixEditView.ActualWidth;

        public double UsedHeight => ColumnHeaderView.ActualHeight +
            Math.Min(CellsView.ActualHeight, ScrolledCellsView.ActualHeight);

        private void CellsViewOnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            Canvas.SetLeft(ColumnHeaderView, -e.HorizontalOffset);
            Canvas.SetTop(RowHeaderView, -e.VerticalOffset);
            //Canvas.SetTop(IndicatorView, -e.VerticalOffset);
            Canvas.SetTop(RowMetricsView, -e.VerticalOffset);
        }
    }
}
