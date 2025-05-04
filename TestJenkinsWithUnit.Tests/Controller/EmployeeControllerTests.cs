//using Xunit;
//using Moq;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using TestJenkinsWithUnit.Controllers;
//using TestJenkinsWithUnit.Logics.Interface;
//using TestJenkinsWithUnit.Models;

//namespace TestJenkinsWithUnit.Tests.Controllers
//{
//    public class EmployeeControllerTests
//    {
//        private readonly Mock<IEmployeeRepository> _mockRepo;
//        private readonly EmployeeController _controller;

//        public EmployeeControllerTests()
//        {
//            _mockRepo = new Mock<IEmployeeRepository>();
//            _controller = new EmployeeController(_mockRepo.Object);
//        }

//        [Fact]
//        public void Index_ReturnsViewResult_WithListOfEmployees()
//        {
//            // Arrange
//            var employees = new List<Employee> { new Employee { Id = 1, Name = "John" } };
//            _mockRepo.Setup(r => r.GetAll()).Returns(employees);

//            // Act
//            var result = _controller.Index();

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsAssignableFrom<IEnumerable<Employee>>(viewResult.Model);
//            Assert.Single(model);
//        }

//        [Fact]
//        public void Details_ValidId_ReturnsViewResult()
//        {
//            var employee = new Employee { Id = 1, Name = "Alice" };
//            _mockRepo.Setup(r => r.GetById(1)).Returns(employee);

//            var result = _controller.Details(1);

//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsType<Employee>(viewResult.Model);
//            Assert.Equal("Alice", model.Name);
//        }

//        [Fact]
//        public void Details_InvalidId_ReturnsNotFound()
//        {
//            _mockRepo.Setup(r => r.GetById(99)).Returns((Employee)null);

//            var result = _controller.Details(99);

//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public void Create_GET_ReturnsViewResult()
//        {
//            var result = _controller.Create();

//            Assert.IsType<ViewResult>(result);
//        }

//        [Fact]
//        public void Create_POST_ValidModel_RedirectsToIndex()
//        {
//            var employee = new Employee { Id = 1, Name = "Bob" };

//            var result = _controller.Create(employee);

//            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
//            Assert.Equal("Index", redirectResult.ActionName);
//            _mockRepo.Verify(r => r.Add(employee), Times.Once);
//        }

//        [Fact]
//        public void Create_POST_InvalidModel_ReturnsViewWithModel()
//        {
//            _controller.ModelState.AddModelError("Name", "Required");

//            var result = _controller.Create(new Employee());

//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.IsType<Employee>(viewResult.Model);
//        }

//        [Fact]
//        public void Edit_GET_ValidId_ReturnsViewResult()
//        {
//            var emp = new Employee { Id = 1, Name = "Rick" };
//            _mockRepo.Setup(r => r.GetById(1)).Returns(emp);

//            var result = _controller.Edit(1);

//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.IsType<Employee>(viewResult.Model);
//        }

//        [Fact]
//        public void Edit_GET_InvalidId_ReturnsNotFound()
//        {
//            _mockRepo.Setup(r => r.GetById(99)).Returns((Employee)null);

//            var result = _controller.Edit(99);

//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public void Edit_POST_ValidModel_RedirectsToIndex()
//        {
//            var emp = new Employee { Id = 1, Name = "Tom" };

//            var result = _controller.Edit(emp);

//            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
//            Assert.Equal("Index", redirectResult.ActionName);
//            _mockRepo.Verify(r => r.Update(emp), Times.Once);
//        }

//        [Fact]
//        public void Edit_POST_InvalidModel_ReturnsViewWithModel()
//        {
//            _controller.ModelState.AddModelError("Name", "Required");

//            var result = _controller.Edit(new Employee());

//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.IsType<Employee>(viewResult.Model);
//        }

//        [Fact]
//        public void Delete_GET_ValidId_ReturnsViewResult()
//        {
//            var emp = new Employee { Id = 1, Name = "Sandy" };
//            _mockRepo.Setup(r => r.GetById(1)).Returns(emp);

//            var result = _controller.Delete(1);

//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.IsType<Employee>(viewResult.Model);
//        }

//        [Fact]
//        public void Delete_GET_InvalidId_ReturnsNotFound()
//        {
//            _mockRepo.Setup(r => r.GetById(99)).Returns((Employee)null);

//            var result = _controller.Delete(99);

//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public void DeleteConfirmed_DeletesAndRedirects()
//        {
//            var result = _controller.DeleteConfirmed(1);

//            var redirect = Assert.IsType<RedirectToActionResult>(result);
//            Assert.Equal("Index", redirect.ActionName);
//            _mockRepo.Verify(r => r.Delete(1), Times.Once);
//        }
//    }
//}
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestJenkinsWithUnit.Controllers;
using TestJenkinsWithUnit.Logics.Interface;
using TestJenkinsWithUnit.Models;

namespace TestJenkinsWithUnit.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _controller = new EmployeeController(_mockRepo.Object);
        }

        [Fact]
        public void Index_ReturnsView_WithEmployeeList()
        {
            var employees = new List<Employee> { new Employee { Id = 1, Name = "Test" } };
            _mockRepo.Setup(r => r.GetAll()).Returns(employees);

            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Employee>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Details_ValidId_ReturnsView()
        {
            var emp = new Employee { Id = 1, Name = "Test" };
            _mockRepo.Setup(r => r.GetById(1)).Returns(emp);

            var result = _controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Employee>(viewResult.Model);
            Assert.Equal("Test", model.Name);
        }

        [Fact]
        public void Details_InvalidId_ReturnsNotFound()
        {
            _mockRepo.Setup(r => r.GetById(999)).Returns((Employee)null);

            var result = _controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_GET_ReturnsEmptyView()
        {
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_POST_Valid_RedirectsToIndex()
        {
            var emp = new Employee { Id = 1, Name = "New" };

            var result = _controller.Create(emp);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            _mockRepo.Verify(r => r.Add(emp), Times.Once);
        }

        [Fact]
        public void Create_POST_Invalid_ReturnsView()
        {
            _controller.ModelState.AddModelError("Name", "Required");

            var result = _controller.Create(new Employee());

            var view = Assert.IsType<ViewResult>(result);
            Assert.IsType<Employee>(view.Model);
        }

        [Fact]
        public void Edit_GET_ValidId_ReturnsView()
        {
            var emp = new Employee { Id = 1, Name = "Edit" };
            _mockRepo.Setup(r => r.GetById(1)).Returns(emp);

            var result = _controller.Edit(1);

            var view = Assert.IsType<ViewResult>(result);
            Assert.IsType<Employee>(view.Model);
        }

        [Fact]
        public void Edit_GET_InvalidId_ReturnsNotFound()
        {
            _mockRepo.Setup(r => r.GetById(999)).Returns((Employee)null);

            var result = _controller.Edit(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_POST_Valid_RedirectsToIndex()
        {
            var emp = new Employee { Id = 1, Name = "Updated" };

            var result = _controller.Edit(emp);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            _mockRepo.Verify(r => r.Update(emp), Times.Once);
        }

        [Fact]
        public void Edit_POST_Invalid_ReturnsView()
        {
            _controller.ModelState.AddModelError("Name", "Required");

            var result = _controller.Edit(new Employee());

            var view = Assert.IsType<ViewResult>(result);
            Assert.IsType<Employee>(view.Model);
        }

        [Fact]
        public void Delete_GET_ValidId_ReturnsView()
        {
            var emp = new Employee { Id = 1, Name = "Del" };
            _mockRepo.Setup(r => r.GetById(1)).Returns(emp);

            var result = _controller.Delete(1);

            var view = Assert.IsType<ViewResult>(result);
            Assert.IsType<Employee>(view.Model);
        }

        [Fact]
        public void Delete_GET_InvalidId_ReturnsNotFound()
        {
            _mockRepo.Setup(r => r.GetById(999)).Returns((Employee)null);

            var result = _controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteConfirmed_CallsDeleteAndRedirects()
        {
            var result = _controller.DeleteConfirmed(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            _mockRepo.Verify(r => r.Delete(1), Times.Once);
        }
    }
}
