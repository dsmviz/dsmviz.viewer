using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.Data.Model.Elements;
using Dsmviz.Viewer.Application.Editing.Action.Element;
using Moq;

namespace Dsmviz.Test.Application.Editing.Action.Element
{
    [TestClass]
    public class ElementChangeNameActionTest
    {
        private readonly Mock<IElementModelEditing> _elementModelEditingMock = new();
        private readonly Mock<IElement> _elementMock = new();

        private const int ElementId = 1;
        private const string OldName = "oldname";
        private const string NewName = "newname";

        [TestInitialize()]
        public void Setup()
        {
            _elementModelEditingMock.Reset();
            _elementMock.Reset();

            _elementMock.Setup(x => x.Id).Returns(ElementId);
            _elementMock.Setup(x => x.Name).Returns(OldName);
        }

        [TestMethod]
        public void WhenDoActionThenElementNameIsChangedDataModel()
        {
            ElementChangeNameAction action = new ElementChangeNameAction(_elementModelEditingMock.Object, _elementMock.Object, NewName);
            Assert.IsTrue(action.IsValid());

            Assert.IsNull(action.Do());

            _elementModelEditingMock.Verify(x => x.ChangeElementName(_elementMock.Object, NewName), Times.Once());
        }

        [TestMethod]
        public void WhenUndoActionThenElementNameIsRevertedDataModel()
        {
            ElementChangeNameAction action = new ElementChangeNameAction(_elementModelEditingMock.Object, _elementMock.Object, NewName);
            Assert.IsTrue(action.IsValid());

            action.Undo();

            _elementModelEditingMock.Verify(x => x.ChangeElementName(_elementMock.Object, OldName), Times.Once());
        }
    }
}
