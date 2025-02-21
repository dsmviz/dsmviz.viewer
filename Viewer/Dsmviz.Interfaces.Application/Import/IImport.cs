using Dsmviz.Interfaces.Util;

namespace Dsmviz.Interfaces.Application.Import
{
    public interface IImport
    {
        IEnumerable<IImportType> ImportTypes { get; }

        Task Import(IImportType importType, string inputFilename, bool clearModel, IFileProgress progress);
    }
}
