﻿using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.Data.Model.Relations;
using Dsmviz.Viewer.Application.Editing.Action.Relation;
using Moq;


namespace Dsmviz.Test.Application.Editing.Action.Relation
{
    [TestClass]
    public class RelationDeleteActionTest
    {
        private readonly Mock<IRelationModelEditing> _relationModelEditingMock = new();
        private readonly Mock<IRelation> _relationMock = new();

        private const int RelationId = 1;
        private const int ConsumerId = 2;
        private const int ProviderId = 3;

        [TestInitialize()]
        public void Setup()
        {
            _relationModelEditingMock.Reset();
            _relationMock.Reset();

            _relationMock.Setup(x => x.Id).Returns(RelationId);
            _relationMock.Setup(x => x.Consumer.Id).Returns(ConsumerId);
            _relationMock.Setup(x => x.Provider.Id).Returns(ProviderId);
        }

        [TestMethod]
        public void WhenDoActionThenRelationIsRemovedFromDataModel()
        {
            RelationDeleteAction action = new RelationDeleteAction(_relationModelEditingMock.Object, _relationMock.Object);
            Assert.IsTrue(action.IsValid());

            Assert.IsNull(action.Do());

            _relationModelEditingMock.Verify(x => x.RemoveRelation(RelationId), Times.Once());
        }

        [TestMethod]
        public void WhenUndoActionThenRelationIsRestoredInDataModel()
        {
            RelationDeleteAction action = new RelationDeleteAction(_relationModelEditingMock.Object, _relationMock.Object);
            Assert.IsTrue(action.IsValid());

            action.Undo();

            _relationModelEditingMock.Verify(x => x.RestoreRelation(RelationId), Times.Once());
        }
    }
}
