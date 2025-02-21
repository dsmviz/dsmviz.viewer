using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.Data.Model.Elements;
using Dsmviz.Viewer.Application.Editing.Action.Element;
using Moq;

namespace Dsmviz.Test.Application.Editing.Action.Element
{
    [TestClass]
    public class ElementCreateActionTest
    {
        private readonly Mock<IElementModelEditing> _elementModelEditingMock = new();
        private readonly Mock<IElement> _elementMock = new();
        private readonly Mock<IElement> _parentMock = new();

        private const int ElementId = 1;
        private const string Name = "name";
        private const string Type = "type";
        private const int ParentId = 456;
        private const int Index = 3;

        [TestInitialize()]
        public void Setup()
        {
            _elementModelEditingMock.Reset();
            _elementMock.Reset();
            _parentMock.Reset();

            _elementMock.Setup(x => x.Id).Returns(ElementId);
            _parentMock.Setup(x => x.Id).Returns(ParentId);

            _elementModelEditingMock.Setup(x => x.AddElement(Name, Type, ParentId, Index, null)).Returns(_elementMock.Object);
        }

        [TestMethod]
        public void WhenDoActionThenElementIsAddedToDataModel()
        {
            ElementCreateAction action = new ElementCreateAction(_elementModelEditingMock.Object, Name, Type, _parentMock.Object, Index);
            Assert.IsTrue(action.IsValid());

            IElement? element = action.Do() as IElement;
            Assert.AreEqual(element, _elementMock.Object);

            _elementModelEditingMock.Verify(x => x.AddElement(Name, Type, ParentId, Index, null), Times.Once());
        }

        [TestMethod]
        public void WhenUndoActionThenElementIsRemovedFromDataModel()
        {
            ElementCreateAction action = new ElementCreateAction(_elementModelEditingMock.Object, Name, Type, _parentMock.Object, Index);
            Assert.IsTrue(action.IsValid());

            IElement? element = action.Do() as IElement;
            Assert.AreEqual(element, _elementMock.Object);

            _elementModelEditingMock.Verify(x => x.AddElement(Name, Type, ParentId, Index, null), Times.Once());

            action.Undo();

            _elementModelEditingMock.Verify(x => x.RemoveElement(ElementId), Times.Once());
        }
    }
}
