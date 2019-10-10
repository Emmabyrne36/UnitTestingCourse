// using Microsoft.VisualStudio.TestTools.UnitTesting; // this is for MS Test
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    #region NUnit
    [TestFixture]
    public class ReservationTests
    {
        
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancellingReservation_ReturnsTrue()
        {
            User user = new User();
            Reservation reservation = new Reservation { MadeBy = user };

            bool result = reservation.CanBeCancelledBy(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_UserIsNotAdmin_ReturnsFalse()
        {
            Reservation reservation = new Reservation();

            bool result = reservation.CanBeCancelledBy(new User { IsAdmin = false });

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanBeCancelledBy_DifferentUserCancellingReservation_ReturnsFalse()
        {
            Reservation reservation = new Reservation { MadeBy = new User() };

            bool result = reservation.CanBeCancelledBy(new User());

            Assert.That(result, Is.False);
        }
    }
    #endregion

    #region MSTest
    // Using MSTest
    //[TestClass]
    //public class ReservationTests
    //{
    //    [TestMethod]
    //    public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
    //    {
    //        // Arrange
    //        var reservation = new Reservation();

    //        // Act
    //        var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

    //        // Assert
    //        Assert.IsTrue(result);
    //    }

    //    [TestMethod]
    //    public void CanBeCancelledBy_SameUserCancellingReservation_ReturnsTrue()
    //    {
    //        User user = new User();
    //        Reservation reservation = new Reservation { MadeBy = user };

    //        bool result = reservation.CanBeCancelledBy(user);

    //        Assert.IsTrue(result);
    //    }

    //    [TestMethod]
    //    public void CanBeCancelledBy_UserIsNotAdmin_ReturnsFalse()
    //    {
    //        Reservation reservation = new Reservation();

    //        bool result = reservation.CanBeCancelledBy(new User { IsAdmin = false });

    //        Assert.IsFalse(result);
    //    }

    //    [TestMethod]
    //    public void CanBeCancelledBy_DifferentUserCancellingReservation_ReturnsFalse()
    //    {
    //        Reservation reservation = new Reservation { MadeBy = new User() };

    //        bool result = reservation.CanBeCancelledBy(new User());

    //        Assert.IsFalse(result);
    //    }
    // }
    #endregion

}
