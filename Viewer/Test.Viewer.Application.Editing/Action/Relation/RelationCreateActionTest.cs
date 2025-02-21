using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.Data.Model.Relations;
using Dsmviz.Viewer.Application.Editing.Action.Relation;
using Moq;


namespace Dsmviz.Test.Application.Editing.Action.Relation
{
    [TestClass]
    public class RelationCreateActionTest
    {
        private readonly Mock<IRelationModelEditing> _relationModelEditingMock = new();
        private readonly Mock<IRelation> _relationMock = new();
        private readonly Mock<IElement> _consumerMock = new();
        private readonly Mock<IElement> _providerMock = new();

        private const int RelationId = 1;
        private const int ConsumerId = 11;
        private const int ProviderId = 12;
        private const string Type = "newtype";
        private const int Weight = 456;

        [TestInitialize()]
        public void Setup()
        {
            _relationModelEditingMock.Reset();
            _relationMock.Reset();
            _consumerMock.Reset();
            _providerMock.Reset();

            _relationModelEditingMock.Setup(x => x.AddRelation(_consumerMock.Object, _providerMock.Object, Type, Weight, null)).Returns(_relationMock.Object);
            _relationMock.Setup(x => x.Id).Returns(RelationId);
            _relationMock.Setup(x => x.Consumer.Id).Returns(ConsumerId);
            _relationMock.Setup(x => x.Provider.Id).Returns(ProviderId);
            _consumerMock.Setup(x => x.Id).Returns(ConsumerId);
            _providerMock.Setup(x => x.Id).Returns(ProviderId);
        }

        [TestMethod]
        public void WhenDoActionThenRelationIsAddedToDataModel()
        {
            RelationCreateAction action = new RelationCreateAction(_relationModelEditingMock.Object, _consumerMock.Object, _providerMock.Object, Type, Weight);
            Assert.IsTrue(action.IsValid());

            IRelation? relation = action.Do() as IRelation;
            Assert.AreEqual(relation, _relationMock.Object);

            _relationModelEditingMock.Verify(x => x.AddRelation(_consumerMock.Object, _providerMock.Object, Type, Weight, null), Times.Once());
        }

        [TestMethod]
        public void WhenUndoActionThenRelationIsRemovedFromDataModel()
        {
            RelationCreateAction action = new RelationCreateAction(_relationModelEditingMock.Object, _consumerMock.Object, _providerMock.Object, Type, Weight);
            Assert.IsTrue(action.IsValid());

            IRelation? relation = action.Do() as IRelation;
            Assert.AreEqual(relation, _relationMock.Object);

            _relationModelEditingMock.Verify(x => x.AddRelation(_consumerMock.Object, _providerMock.Object, Type, Weight, null), Times.Once());

            action.Undo();

            _relationModelEditingMock.Verify(x => x.RemoveRelation(RelationId), Times.Once());
        }
    }
}
