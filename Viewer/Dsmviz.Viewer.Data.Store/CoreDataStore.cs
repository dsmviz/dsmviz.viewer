using Dsmviz.Interfaces.Data.Model.Model;
using Dsmviz.Interfaces.Data.Store;
using Dsmviz.Interfaces.Util;
using Dsmviz.Viewer.Utils;

namespace Dsmviz.Viewer.Data.Store
{
    public class CoreDataStore(IModelImport modelImport) : IDataStore
    {
        public int TotalElementCount => modelImport.ElementModelImport.GetPersistedElementCount();
        public int TotalRelationCount => modelImport.RelationModelImport.GetPersistedRelationCount();

        public bool LoadModel(string filename, IFileProgress progress)
        {
            Logger.LogDataModelMessage($"Load data model file={filename}");

            ModelFilename = filename;

            modelImport.Clear();
            CoreDsmFile dsmModelFile = new CoreDsmFile(filename, modelImport);
            bool result = dsmModelFile.Load(progress);
            IsCompressed = dsmModelFile.IsCompressedFile;
            ModelVersion = modelImport.ModelVersion;
            return result;
        }

        public bool SaveModel(string filename, bool compressFile, IFileProgress progress)
        {
            Logger.LogDataModelMessage($"Save data model file={filename} compress={compressFile}");

            ModelFilename = filename;

            CoreDsmFile dsmModelFile = new CoreDsmFile(filename, modelImport);
            bool result = dsmModelFile.Save(compressFile, progress);
            return result;
        }

        public string ModelFilename { get; private set; } = string.Empty;
        public int ModelVersion { get; private set; }

        public bool IsCompressed { get; private set; }

        public string FileExtension => ".dsm";
        public string FileDescription => "DSM model";
    }
}
