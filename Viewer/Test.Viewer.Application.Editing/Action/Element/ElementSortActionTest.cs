﻿using Dsmviz.Interfaces.Application.Algorithms;
using Dsmviz.Interfaces.Data.Entities;
using Dsmviz.Interfaces.Data.Model.Elements;
using Dsmviz.Viewer.Application.Editing.Action.Element;
using Moq;

namespace Dsmviz.Test.Application.Editing.Action.Element
{
    [TestClass]
    public class ElementSortActionTest
    {
        private readonly Mock<IElementModelEditing> _elementModelEditingMock = new();
        private readonly Mock<IElement> _elementMock = new();
        private readonly Mock<IElementWeightMatrix> _weightsMatrixMock = new();
        private readonly Mock<ISortAlgorithm> _sortAlgorithmMock = new();
        private readonly Mock<ISortResult> _sortResultMock = new();

        private const int ElementId = 1;

        [TestInitialize()]
        public void Setup()
        {
            _elementModelEditingMock.Reset();
            _elementMock.Reset();
            _weightsMatrixMock.Reset();
            _sortAlgorithmMock.Reset();
            _sortResultMock.Reset();

            _sortAlgorithmMock.Setup(x => x.Sort(_elementMock.Object, _weightsMatrixMock.Object)).Returns(_sortResultMock.Object);
            _elementMock.Setup(x => x.Id).Returns(ElementId);
        }

        [TestMethod]
        public void WhenDoActionThenElementsChildrenAreSorted()
        {
            ElementSortAction action = new ElementSortAction(_elementModelEditingMock.Object, _elementMock.Object, _weightsMatrixMock.Object, _sortAlgorithmMock.Object);
            Assert.IsTrue(action.IsValid());

            Assert.IsNull(action.Do());

            _elementModelEditingMock.Verify(x => x.ReorderChildren(_elementMock.Object, _sortResultMock.Object.SortedIndexValues), Times.Once());
        }

        [TestMethod]
        public void WhenUndoActionThenElementsChildrenAreSortIsReverted()
        {
            ElementSortAction action = new ElementSortAction(_elementModelEditingMock.Object, _elementMock.Object, _weightsMatrixMock.Object, _sortAlgorithmMock.Object);
            Assert.IsTrue(action.IsValid());

            action.Do();

            _elementModelEditingMock.Verify(x => x.ReorderChildren(_elementMock.Object, _sortResultMock.Object.SortedIndexValues), Times.Once());

            action.Undo();

            _sortResultMock.Verify(x => x.InvertOrder(), Times.Once());
            _elementModelEditingMock.Verify(x => x.ReorderChildren(_elementMock.Object, _sortResultMock.Object.SortedIndexValues), Times.Exactly(2));
        }
    }
}
