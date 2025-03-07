﻿using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.Data.Model.Relations;
using Dsmviz.Viewer.Application.Editing.Action.Relation;
using Moq;

namespace Dsmviz.Test.Application.Editing.Action.Relation
{
    [TestClass]
    public class RelationChangeTypeActionTest
    {
        private readonly Mock<IRelationModelEditing> _relationModelEditingMock = new();
        private readonly Mock<IRelation> _relationMock = new();

        private const int RelationId = 1;
        private const int ConsumerId = 2;
        private const int ProviderId = 3;
        private const string OldType = "oldtype";
        private const string NewType = "newtype";

        [TestInitialize()]
        public void Setup()
        {
            _relationModelEditingMock.Reset();
            _relationMock.Reset();

            _relationMock.Setup(x => x.Id).Returns(RelationId);
            _relationMock.Setup(x => x.Consumer.Id).Returns(ConsumerId);
            _relationMock.Setup(x => x.Provider.Id).Returns(ProviderId);
            _relationMock.Setup(x => x.Type).Returns(OldType);
        }

        [TestMethod]
        public void WhenDoActionThenRelationTypeIsChangedDataModel()
        {
            RelationChangeTypeAction action = new RelationChangeTypeAction(_relationModelEditingMock.Object, _relationMock.Object, NewType);
            Assert.IsTrue(action.IsValid());

            Assert.IsNull(action.Do());

            _relationModelEditingMock.Verify(x => x.ChangeRelationType(_relationMock.Object, NewType), Times.Once());
        }

        [TestMethod]
        public void WhenUndoActionThenRelationTypeIsRevertedDataModel()
        {
            RelationChangeTypeAction action = new RelationChangeTypeAction(_relationModelEditingMock.Object, _relationMock.Object, NewType);
            Assert.IsTrue(action.IsValid());

            action.Undo();

            _relationModelEditingMock.Verify(x => x.ChangeRelationType(_relationMock.Object, OldType), Times.Once());
        }
    }
}
