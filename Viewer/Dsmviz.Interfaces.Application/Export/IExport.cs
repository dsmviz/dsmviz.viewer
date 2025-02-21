using Dsmviz.Interfaces.Util;

namespace Dsmviz.Interfaces.Application.Export
{
    public interface IExport
    {
        IEnumerable<IExportType> ExportTypes { get; }

        Task Export(IExportType exportTyper, string inputFilename, bool clearModel, IFileProgress progress);
    }
}