using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace GigHub.Tests.Extensions
{
    public static class MockDbSetExtensions
    {
        /*
         * I want to create an extension method for our DbSet that we can call in our test methods
         * 
         * we call this method on a DbSet and give it a source like a list
         * and that DbSet will be populated with that list
         * we want to make it generic so we can use it with gigs with anything
         * 
         * anytime we have a mock of DbSet of T we can call it and populate that mock DbSet
         * with a different list
         */

        public static void SetSource<T>(this Mock<DbSet<T>> mockSet, IList<T> source) where T : class
        {
            var data = source.AsQueryable();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }

    }
}
