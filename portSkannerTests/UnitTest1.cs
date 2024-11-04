using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Sockets;
using System.Reflection;

namespace portSkannerTests
{
    [TestClass]
    public class UnitTest01
    {
        private const string ValidIp = "127.0.0.1";
        private const string InvalidIp = "256.0.0.1";
        private const int OpenPort = 8080;
        private const int ClosedPort = 12345;

        [TestMethod]
        public void TestIsPortOpen_ValidPort_ShouldReturnTrue()
        {

            string target = ValidIp;
            int port = OpenPort;


            bool isOpen = IsPortOpen(target, port);


            Assert.IsTrue(isOpen, $"Port {port} should be open on {target}");
        }

        [TestMethod]
        public void TestIsPortOpen_InvalidIp_ShouldThrowException()
        {

            string target = InvalidIp;
            int port = OpenPort;

            Assert.ThrowsException<TargetInvocationException>(() => IsPortOpen(target, port), $"Invalid IP address: {target}");
        }

        [TestMethod]
        public void TestIsPortOpen_ClosedPort_ShouldReturnFalse()
        {

            string target = ValidIp;
            int port = ClosedPort;

            bool isOpen = IsPortOpen(target, port);


            Assert.IsFalse(isOpen, $"Port {port} should be closed on {target}");
        }

        private bool IsPortOpen(string target, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(target, port);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
