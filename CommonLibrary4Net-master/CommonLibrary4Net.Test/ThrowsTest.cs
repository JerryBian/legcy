using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonLibrary4Net.Test
{
    [TestClass]
    public class ThrowsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IfNull_Matched_ThrowException1()
        {
            const string obj = null;
            Throws.IfNull(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "error message")]
        public void IfNull_Matched_ThrowException2()
        {
            const string obj = null;
            Throws.IfNull(obj, "error message");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IfNullOrEmpty_Matched_ThrowException1()
        {
            const string obj = null;
            Throws.IfNullOrEmpty(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "error message")]
        public void IfNullOrEmpty_Matched_ThrowException2()
        {
            const string obj = null;
            Throws.IfNullOrEmpty(obj, "error message");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IfNullOrEmpty_Matched_ThrowException3()
        {
            const string obj = "";
            Throws.IfNullOrEmpty(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "error message")]
        public void IfNullOrEmpty_Matched_ThrowException4()
        {
            const string obj = "";
            Throws.IfNullOrEmpty(obj, "error message");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IfNullOrEmptyOrWhitespace_Matched_ThrowException1()
        {
            const string obj = null;
            Throws.IfNullOrEmptyOrWhitespace(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "error message")]
        public void IfNullOrEmptyOrWhitespace_Matched_ThrowException2()
        {
            const string obj = null;
            Throws.IfNullOrEmptyOrWhitespace(obj, "error message");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IfNullOrEmptyOrWhitespace_Matched_ThrowException3()
        {
            const string obj = "";
            Throws.IfNullOrEmptyOrWhitespace(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "error message")]
        public void IfNullOrEmptyOrWhitespace_Matched_ThrowException4()
        {
            const string obj = "";
            Throws.IfNullOrEmptyOrWhitespace(obj, "error message");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IfNullOrEmptyOrWhitespace_Matched_ThrowException5()
        {
            const string obj = "  \t  ";
            Throws.IfNullOrEmptyOrWhitespace(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "error message")]
        public void IfNullOrEmptyOrWhitespace_Matched_ThrowException6()
        {
            const string obj = "  \t  ";
            Throws.IfNullOrEmptyOrWhitespace(obj, "error message");
        }
    }
}