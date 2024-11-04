using System.Security.Cryptography;

namespace cryptDefender
{
    [TestClass]
    public class AESTest1
    {
        private string testFilePath = @"c:\TestFiles\testfile.txt";
        private string encryptedFilePath = @"c:\Encrypt\testfile.enc";
        [TestMethod]
        public void EncryptWithAES()
        {
            var sut = new FileCryption();
            sut.selectedAlgorithm = "AES";
            sut._rsa = new RSACryptoServiceProvider();

            var expectedEncryptedFileName = @"c:\Encrypt\testfile.enc";
            var fileMock = new FileInfo(@"c:\TestFiles\testfile.txt");

            sut.EncryptFile(fileMock);

            Assert.IsTrue(File.Exists(expectedEncryptedFileName));
        }
        [TestMethod]
        public void DecryptWithAES()
        {
            Thread.Sleep(500);
            var sut = new FileCryption();
            sut.selectedAlgorithm = "AES";
            sut._rsa = new RSACryptoServiceProvider();

            var expectedDecryptedFileName = @"c:\Decrypt\testfile.txt";
            var encryptedfileMock = new FileInfo(@"c:\Encrypt\testfile.enc");

            sut.DecryptFile(encryptedfileMock);

            Assert.IsTrue(File.Exists(expectedDecryptedFileName));
        }

        [TestMethod]
        public void EncryptWithTripleDES()
        {
            var sut = new FileCryption();
            sut.selectedAlgorithm = "TripleDes";
            sut._rsa = new RSACryptoServiceProvider();

            var expectedEncryptedFileName = @"c:\Encrypt\testfile.enc";
            var fileMock = new FileInfo(@"c:\TestFiles\testfile.txt");

            sut.EncryptFile(fileMock);

            Assert.IsTrue(File.Exists(expectedEncryptedFileName));


        }

    }
}