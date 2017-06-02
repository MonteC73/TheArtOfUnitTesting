﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogAn.Tests.Helpers
{
    public static class AssertException
    {
        public static void Throws<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof (TException),
                    "Expected exception of type " + typeof (TException) + " but type of " + ex.GetType() +
                    " was thrown instead.");
                return;
            }
            Assert.Fail("Expected exception of type " + typeof (TException) + " but no exception was thrown.");

        }

        public static void Throws<TException>(Action action, string expectedMessage) where TException : Exception
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(TException), 
                    "Expected exception of type " + typeof(TException) + " but type of " + ex.GetType() + " was thrown instead.");
                Assert.AreEqual(expectedMessage, ex.Message, 
                    "Expected exception with a message of '" + expectedMessage + "' but exception with message of '" + ex.Message + "' was thrown instead.");
                return;
            }
            Assert.Fail("Expected exception of type " + typeof(TException) + " but no exception was thrown.");
        }
    }
}
