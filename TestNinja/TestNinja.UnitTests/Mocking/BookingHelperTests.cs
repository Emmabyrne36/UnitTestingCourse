using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class BookingHelperTests
    {
        private Mock<IBookingRepository> _repo;
        private Booking _existingBooking;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IBookingRepository>();
            _existingBooking = new Booking()
            {
                Id = 2,
                ArrivalDate = ArriveOn(2018, 12, 15),
                DepartureDate = DepartOn(2018, 12, 20),
                Reference = "2"
            };

            _repo.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }

        #region MyTests
        [Test]
        public void OverlappingBookingExists_StatusIsCancelled_ReturnsEmptyString()
        {
            Booking booking = new Booking() { Status = "Cancelled" };
            var result = BookingHelper.OverlappingBookingsExist(booking, _repo.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingExists_OverlapExists_ReturnsOverlapBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            }, _repo.Object);

            Assert.That(result, Is.EqualTo("2"));
        }

        [Test]
        public void OverlappingBookingExists_NoOverlap_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(_existingBooking, _repo.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }
        #endregion

        #region TestsFromCourse
        [Test]
        public void OverlappingBookingsExist_StartsAndFinishesBeforeExistingBooking_ReturnsEmptyString()
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
            }, _repo.Object);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_StartsBeforeAndFinishesInMiddleOfExistingBooking_ReturnsExistingReference()
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),
            }, _repo.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_StartsBeforeAndFinishesAfterExistingBooking_ReturnsExistingReference()
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            }, _repo.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_StartsAndFinishesInMiddleOfExistingBooking_ReturnsExistingReference()
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
            }, _repo.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_StartsInMiddleAndFinishesAfterExistingBooking_ReturnsExistingReference()
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            }, _repo.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_StartsAndFinishesInMiddleOfExistingBooking_ReturnsEmptyString()
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, days: 2),
            }, _repo.Object);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingsOverlapButNewBookingCancelled_ReturnsEmptyString()
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Status = "Cancelled"
            }, _repo.Object);

            // Assert
            Assert.That(result, Is.Empty);
        }

        #endregion

        #region HelperMethods
        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        #endregion

    }
}
