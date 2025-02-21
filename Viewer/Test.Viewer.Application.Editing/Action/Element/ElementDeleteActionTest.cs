using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.Data.Model.Elements;
using Dsmviz.Viewer.Application.Editing.Action.Element;
using Moq;

namespace Dsmviz.Test.Application.Editing.Action.Element
{
    [TestClass]
    public class ElementDeleteActionTest
    {
        private readonly Mock<IElementModelEditing> _elementModelEditingMock = new();
        private readonly Mock<IElement> _elementMock = new();

        private const int ElementId = 1;

        [TestInitialize()]
        public void Setup()
        {
            _elementModelEditingMock.Reset();
            _elementMock.Reset();
            _elementMock.Setup(x => x.Id).Returns(ElementId);
        }

        [TestMethod]
        public void WhenDoActionThenElementIsRemovedFromDataModel()
        {
            ElementDeleteAction action = new ElementDeleteAction(_elementModelEditingMock.Object, _elementMock.Object);
            Assert.IsTrue(action.IsValid());

            Assert.IsNull(action.Do());

            _elementModelEditingMock.Verify(x => x.RemoveElement(ElementId), Times.Once());
        }

        [TestMethod]
        public void WhenUndoActionThenElementIsRestoredInDataModel()
        {
            ElementDeleteAction action = new ElementDeleteAction(_elementModelEditingMock.Object, _elementMock.Object);
            Assert.IsTrue(action.IsValid());

            action.Undo();

            _elementModelEditingMock.Verify(x => x.RestoreElement(ElementId), Times.Once());
        }
    }
}
