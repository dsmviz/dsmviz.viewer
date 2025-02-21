namespace Dsmviz.Interfaces.Application.Export
{
    public interface IExportType
    {
        string FileExtension { get; }
        string FileDescription { get; }
    }
}