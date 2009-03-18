using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace TestUtilities.Tests
{
    [TestFixture]
    public class PropertyChangedTesterTest
    {
        private PropertyChangedTester<TypeToTest> _tester;
        private TypeToTest _test;


        [SetUp]
        public void SetUp()
        {
            _test = new TypeToTest();
            _tester = new PropertyChangedTester<TypeToTest>(_test);
        }

        [Test]
        public void CanCreate()
        {
            // happens in setup
        }

        [Test]
        public void VerifyPropertyChanged()
        {
            _test.SomeProperty = 42;

            Assert.IsTrue(_tester.PropertyChanged(o => o.SomeProperty));
        }

        [Test]
        public void DoesntWorkForNonPropertyExpressions()
        {
            try
            {
                _tester.PropertyChanged(o => o.SomeMethod());
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                
            }
            
        }

        [Test]
        public void VerifyAllPublicProperties()
        {
            List<string> list = new List<string>();
            _test.PropertyChanged += (sender, args) => list.Add(args.PropertyName);

            _tester.VerifyAllPublicProperties();

            Assert.That(list, Is.EquivalentTo(new[]{"SomeProperty", "SomeOtherProperty"}));

        }

        [Test]
        public void FailsIfDoesntRaise()
        {
            var obj = new TypeThatDoesntRaise();
            var tester = new PropertyChangedTester<TypeThatDoesntRaise>(obj);

            try
            {
                tester.VerifyAllPublicProperties();
                Assert.Fail();
            }
            catch (VerifyException)
            {
            }

        }

        [Test]
        public void ProtectedProperty()
        {
            var tester = new PropertyChangedTester<TypeWithProtectedProperty>(new TypeWithProtectedProperty());
            tester.VerifyAllPublicProperties();
        }


    }
}
