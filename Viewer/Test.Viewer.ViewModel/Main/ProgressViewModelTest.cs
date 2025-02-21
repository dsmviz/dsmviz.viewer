using Dsmviz.Interfaces.Util;
using Dsmviz.Viewer.ViewModel.Main;
using Moq;

namespace Dsmviz.Test.ViewModel.Main
{
    [TestClass]
    public class ProgressViewModelTests
    {
        [TestMethod]
        public void Given_InitialState_When_GettingTitle_Then_TitleIsEmpty()
        {
            // Arrange  
            ProgressViewModel viewModel = new();

            // Act  
            var title = viewModel.Title;

            // Assert  
            Assert.AreEqual(string.Empty, title);
        }

        [TestMethod]
        public void Given_BusyProgressState_When_UpdateCalled_Then_TitleAndProgressTextUpdated()
        {
            // Arrange  
            ProgressViewModel viewModel = new();
            var progressInfoMock = new Mock<IFileProgressInfo>();
            progressInfoMock.Setup(p => p.State).Returns(ProgressState.Busy);
            progressInfoMock.Setup(p => p.ActionText).Returns("Processing");
            progressInfoMock.Setup(p => p.CurrentItemCount).Returns(1);
            progressInfoMock.Setup(p => p.TotalItemCount).Returns(5);
            progressInfoMock.Setup(p => p.ItemType).Returns("Files");
            progressInfoMock.Setup(p => p.Percentage).Returns(20);

            // Act  
            viewModel.Update(progressInfoMock.Object);

            // Assert  
            Assert.AreEqual("Processing", viewModel.Title);
            Assert.AreEqual("1/5 Files", viewModel.ProgressText);
            Assert.AreEqual(20, viewModel.ProgressValue);
            Assert.AreEqual(ProgressState.Busy, viewModel.State);
        }

        [TestMethod]
        public void Given_ErrorProgressState_When_UpdateCalled_Then_ErrorTextUpdated()
        {
            // Arrange  
            ProgressViewModel viewModel = new();
            var progressInfoMock = new Mock<IFileProgressInfo>();
            progressInfoMock.Setup(p => p.State).Returns(ProgressState.Error);
            progressInfoMock.Setup(p => p.ErrorText).Returns("An error occurred.");

            // Act  
            viewModel.Update(progressInfoMock.Object);

            // Assert  
            Assert.AreEqual("An error occurred.", viewModel.ErrorText);
            Assert.AreEqual(ProgressState.Error, viewModel.State);
        }

        [TestMethod]
        public void Given_BusyProgressState_When_StateChanged_Then_StateChangedEventIsTriggered()
        {
            // Arrange  
            ProgressViewModel viewModel = new();
            bool eventTriggered = false;
            viewModel.StateChanged += (_, _) => eventTriggered = true;

            var progressInfoMock = new Mock<IFileProgressInfo>();
            progressInfoMock.Setup(p => p.State).Returns(ProgressState.Busy);
            progressInfoMock.Setup(p => p.ActionText).Returns("Processing");

            // Act  
            viewModel.Update(progressInfoMock.Object);

            // Assert  
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(ProgressState.Busy, viewModel.State);
        }
    }
}