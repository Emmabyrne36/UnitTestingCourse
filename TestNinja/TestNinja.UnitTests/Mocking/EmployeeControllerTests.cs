using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _storage;
        private EmployeeController _employeeController;

        [SetUp]
        public void SetUp()
        {
            _storage = new Mock<IEmployeeStorage>();
            _employeeController = new EmployeeController(_storage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_RedirectsToAction()
        {
            var result = _employeeController.DeleteEmployee(1);
            
            Assert.That(result, Is.InstanceOf<RedirectResult>());
            // Object of type redirect result
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeletesEmployeeFromDB()
        {
            _employeeController.DeleteEmployee(1);

            _storage.Verify(s => s.RemoveEmployee(1));

        }

    }
}
