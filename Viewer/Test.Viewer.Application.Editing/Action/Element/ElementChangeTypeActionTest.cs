using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.Data.Model.Elements;
using Dsmviz.Viewer.Application.Editing.Action.Element;
using Moq;

namespace Dsmviz.Test.Application.Editing.Action.Element
{
    [TestClass]
    public class ElementChangeTypeActionTest
    {
        private readonly Mock<IElementModelEditing> _elementModelEditingMock = new();
        private readonly Mock<IElement> _elementMock = new();

        private const int ElementId = 1;
        private const string OldType = "oldtype";
        private const string NewType = "newtype";

        [TestInitialize()]
        public void Setup()
        {
            _elementModelEditingMock.Reset();
            _elementMock.Reset();

            _elementMock.Setup(x => x.Id).Returns(ElementId);
            _elementMock.Setup(x => x.Type).Returns(OldType);
        }

        [TestMethod]
        public void WhenDoActionThenElementTypeIsChangedDataModel()
        {
            ElementChangeTypeAction action = new ElementChangeTypeAction(_elementModelEditingMock.Object, _elementMock.Object, NewType);
            Assert.IsTrue(action.IsValid());

            Assert.IsNull(action.Do());

            _elementModelEditingMock.Verify(x => x.ChangeElementType(_elementMock.Object, NewType), Times.Once());
        }

        [TestMethod]
        public void WhenUndoActionThenElementTypeIsRevertedDataModel()
        {
            ElementChangeTypeAction action = new ElementChangeTypeAction(_elementModelEditingMock.Object, _elementMock.Object, NewType);
            Assert.IsTrue(action.IsValid());

            action.Undo();

            _elementModelEditingMock.Verify(x => x.ChangeElementType(_elementMock.Object, OldType), Times.Once());
        }
    }
}
