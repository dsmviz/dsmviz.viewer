using Dsmviz.Interfaces.Data.Model.Elements;
using Dsmviz.Interfaces.Data.Model.MetaData;
using Dsmviz.Interfaces.Data.Model.Relations;

namespace Dsmviz.Interfaces.Data.Model.Model
{
    public interface IModelImport
    {
        void Clear();

        int ModelVersion { get; set; }

        IMetaDataModelImport MetaDataModelImport { get; }
        IElementModelImport ElementModelImport { get; }
        IRelationModelImport RelationModelImport { get; }
    }
}
