using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class HouseKeeperServiceTests
    {
        private HouseKeeperService _service;
        private Housekeeper _houseKeeper;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private DateTime _statementDate = new DateTime(2019, 1, 1);
        private string _fileName;

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new Housekeeper
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _houseKeeper
            }.AsQueryable());

            _fileName = "filename";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(() => _fileName);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HouseKeeperService(
                unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GeneratesStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg =>
                    sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_WhenCalled_ShouldNotGenerateStatement(string email)
        {
            _houseKeeper.Email = email;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg =>
                    sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),
                    Times.Never); // this method is never called
        }


        [Test]
        public void SendStatementEmails_WhenCalled_EmailStatement()
        {
            _service.SendStatementEmails(_statementDate);
            VerifyEmailSent();
        }

        [Test]
        public void SendStatementEmails_FileNameIsNull_ShouldNotEmailStatement()
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(() => null);

            _service.SendStatementEmails(_statementDate);
            VerifyEmailNotSent();
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_WhenCalled_ShouldNotEmailStatement(string statement)
        {
            _fileName = statement;

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())
            ).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }





        #region HelperMethods

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>()), Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                            _houseKeeper.Email,
                            _houseKeeper.StatementEmailBody,
                            _fileName,
                            It.IsAny<string>()));
        }

        #endregion






        // These tests have been merged into the one test WhenCalled_ShouldNotGenerateStatement
        //    [Test]
        //    public void SendStatementEmails_EmailIsNull_ShouldNotGenerateStatement()
        //    {
        //        // Arrange
        //        _houseKeeper.Email = null;

        //        _service.SendStatementEmails(_statementDate);

        //        _statementGenerator.Verify(sg =>
        //                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),
        //                Times.Never); // this method is never called
        //    }


        //    [Test]
        //    public void SendStatementEmails_EmailIsWhiteSpace_ShouldNotGenerateStatement()
        //    {
        //        // Arrange
        //        _houseKeeper.Email = " ";

        //        _service.SendStatementEmails(_statementDate);

        //        _statementGenerator.Verify(sg =>
        //                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),
        //                Times.Never); // this method is never called
        //    }

        //    [Test]
        //    public void SendStatementEmails_EmailIsEmpty_ShouldNotGenerateStatement()
        //    {
        //        // Arrange
        //        _houseKeeper.Email = "";

        //        _service.SendStatementEmails(_statementDate);

        //        _statementGenerator.Verify(sg =>
        //                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),
        //                Times.Never); // this method is never called
        //    }
        //}
    }
}
