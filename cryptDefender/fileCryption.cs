using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace cryptDefender
{
    public partial class FileCryption : Form
    {

        private string _databaseName;
        private string connectionString;

        //private string fileEnd;
        public string selectedAlgorithm; // ennen testausta private
        // Declare CspParameters and RsaCryptoServiceProvider
        // objects with global scope of your Form class.
        readonly CspParameters _cspp = new CspParameters();
        public RSACryptoServiceProvider _rsa; // ennen testejä ei ollut public

        // Path variables for source, encryption, and
        // decryption folders. Must end with a backslash.
        const string EncrFolder = @"c:\Encrypt\";
        const string DecrFolder = @"c:\Decrypt\";
        const string SrcFolder = @"c:\docs\";

        // Public key file
        const string PubKeyFile = @"c:\encrypt\rsaPublicKey.txt";

        // Key container name for
        // private/public key value pair.
        const string KeyName = "Key01";


      





        public void EncryptFile(FileInfo file) // ennen testejä private
        {
            string fileName = file.Name; 

            



            if (selectedAlgorithm == "AES")
            {
                // Create instance of Aes for
                // symmetric encryption of the data.
                Aes aes = Aes.Create();
                ICryptoTransform transform = aes.CreateEncryptor();

                // Use RSACryptoServiceProvider to
                // encrypt the AES key.
                // rsa is previously instantiated:
                //    rsa = new RSACryptoServiceProvider(cspp);
                byte[] keyEncrypted = _rsa.Encrypt(aes.Key, false);

                // Create byte arrays to contain
                // the length values of the key and IV.
                int lKey = keyEncrypted.Length;
                byte[] LenK = BitConverter.GetBytes(lKey);
                int lIV = aes.IV.Length;
                byte[] LenIV = BitConverter.GetBytes(lIV);



                // Write the following to the FileStream
                // for the encrypted file (outFs):
                // - length of the key
                // - length of the IV
                // - encrypted key
                // - the IV
                // - the encrypted cipher content


                // Change the file's extension to ".enc"
                string outFile =
                    Path.Combine(EncrFolder, Path.ChangeExtension(file.Name, ".enc"));



                using (var outFs = new FileStream(outFile, FileMode.Create))
                {
                    outFs.Write(LenK, 0, 4);
                    outFs.Write(LenIV, 0, 4);
                    outFs.Write(keyEncrypted, 0, lKey);
                    outFs.Write(aes.IV, 0, lIV);

                    // Now write the cipher text using
                    // a CryptoStream for encrypting.
                    using (var outStreamEncrypted =
                        new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                    {
                        // By encrypting a chunk at
                        // a time, you can save memory
                        // and accommodate large files.
                        int count = 0;
                        int offset = 0;

                        // blockSizeBytes can be any arbitrary size.
                        int blockSizeBytes = aes.BlockSize / 8;
                        byte[] data = new byte[blockSizeBytes];
                        int bytesRead = 0;

                        using (var inFs = new FileStream(file.FullName, FileMode.Open))
                        {
                            do
                            {
                                count = inFs.Read(data, 0, blockSizeBytes);
                                offset += count;
                                outStreamEncrypted.Write(data, 0, count);
                                bytesRead += blockSizeBytes;
                            } while (count > 0);
                        }
                        outStreamEncrypted.FlushFinalBlock();
                    }
                }

                SaveAlgorithm(selectedAlgorithm, fileName);
            }
            else if (selectedAlgorithm == "TripleDES")
            {
                TripleDES tripleDES = TripleDES.Create();
                ICryptoTransform transform = tripleDES.CreateEncryptor();

                // Käytä RSACryptoServiceProvideria AES-avaimen salaamiseen
                // rsa on aiemmin alustettu:
                // rsa = new RSACryptoServiceProvider(cspp);
                byte[] keyEncrypted = _rsa.Encrypt(tripleDES.Key, false);

                // Luo byte-taulukot, jotka sisältävät
                // avaimen ja IV:n pituusarvot
                int lKey = keyEncrypted.Length;
                byte[] LenK = BitConverter.GetBytes(lKey);
                int lIV = tripleDES.IV.Length;
                byte[] LenIV = BitConverter.GetBytes(lIV);

                // Kirjoita seuraavat tiedostovirtaan
                // salatulle tiedostolle (outFs):
                // - avaimen pituus
                // - IV:n pituus
                // - salattu avain
                // - IV
                // - salattu tekstitieto

                // Vaihda tiedoston laajennus ".enc":ksi
                string outFile =
                    Path.Combine(EncrFolder, Path.ChangeExtension(file.Name, ".enc"));



                using (var outFs = new FileStream(outFile, FileMode.Create))
                {
                    outFs.Write(LenK, 0, 4);
                    outFs.Write(LenIV, 0, 4);
                    outFs.Write(keyEncrypted, 0, lKey);
                    outFs.Write(tripleDES.IV, 0, lIV);

                    // Kirjoita salausteksti käyttäen
                    // CryptoStreamia salaukseen.
                    using (var outStreamEncrypted =
                        new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                    {
                        // Salaamalla pala kerrallaan
                        // voit säästää muistia
                        // ja käsitellä suuria tiedostoja.
                        int count = 0;
                        int offset = 0;

                        // blockSizeBytes voi olla mikä tahansa mielivaltainen koko.
                        int blockSizeBytes = tripleDES.BlockSize / 8;
                        byte[] data = new byte[blockSizeBytes];
                        int bytesRead = 0;

                        using (var inFs = new FileStream(file.FullName, FileMode.Open))
                        {
                            do
                            {
                                count = inFs.Read(data, 0, blockSizeBytes);
                                offset += count;
                                outStreamEncrypted.Write(data, 0, count);
                                bytesRead += blockSizeBytes;
                            } while (count > 0);
                        }
                        outStreamEncrypted.FlushFinalBlock();
                    }
                }
                SaveAlgorithm(selectedAlgorithm, fileName);

            }


        }




        public void DecryptFile(FileInfo file) // ennen testejä private
        {
            string cryptedFileEnd = Path.GetExtension(file.Name);

            
            


            //string fileName = Path.GetFileName(file.FullName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);

            string fileInfo = GetAlgorithm(fileNameWithoutExtension);
            string[] parts = fileInfo.Split(',');

            string fileName = parts[0]; 
            string selectedAlgorithm = parts[1];

            string fileEnd = Path.GetExtension(fileName);

            Console.WriteLine("Tiedostonimi: " + fileName);
            Console.WriteLine("Tiedostopääte: " + fileEnd);
            Console.WriteLine("Algoritmi: " + selectedAlgorithm);

            if (cryptedFileEnd != ".enc")
            {
                MessageBox.Show("File is not encrypted!");
            }


            else if (selectedAlgorithm == "AES")
            //else if (cryptedFileEnd == ".a")

            {
                // Create instance of Aes for
                // symmetric decryption of the data.
                Aes aes = Aes.Create();

                // Create byte arrays to get the length of
                // the encrypted key and IV.
                // These values were stored as 4 bytes each
                // at the beginning of the encrypted package.
                byte[] LenK = new byte[4];
                byte[] LenIV = new byte[4];


                //fileEnd = ".jpg";
                // Testien takia fileEnd annetaan manuaalisesti !!!

                //string fileEnd = ".enc";

                // Construct the file name for the decrypted file.
                string outFile =
                    Path.ChangeExtension(file.FullName.Replace("Encrypt", "Decrypt"), fileEnd);




                // Use FileStream objects to read the encrypted
                // file (inFs) and save the decrypted file (outFs).

                using (var inFs = new FileStream(file.FullName, FileMode.Open))
                {
                    inFs.Seek(0, SeekOrigin.Begin);
                    inFs.Read(LenK, 0, 3);
                    inFs.Seek(4, SeekOrigin.Begin);
                    inFs.Read(LenIV, 0, 3);

                    // Convert the lengths to integer values.
                    int lenK = BitConverter.ToInt32(LenK, 0);
                    int lenIV = BitConverter.ToInt32(LenIV, 0);

                    // Determine the start position of
                    // the cipher text (startC)
                    // and its length(lenC).
                    int startC = lenK + lenIV + 8;
                    int lenC = (int)inFs.Length - startC;

                    // Create the byte arrays for
                    // the encrypted Aes key,
                    // the IV, and the cipher text.
                    byte[] KeyEncrypted = new byte[lenK];
                    byte[] IV = new byte[lenIV];

                    // Extract the key and IV
                    // starting from index 8
                    // after the length values.
                    inFs.Seek(8, SeekOrigin.Begin);
                    inFs.Read(KeyEncrypted, 0, lenK);
                    inFs.Seek(8 + lenK, SeekOrigin.Begin);
                    inFs.Read(IV, 0, lenIV);

                    Directory.CreateDirectory(DecrFolder);
                    // Use RSACryptoServiceProvider
                    // to decrypt the AES key.
                    byte[] KeyDecrypted = _rsa.Decrypt(KeyEncrypted, false);

                    // Decrypt the key.
                    ICryptoTransform transform = aes.CreateDecryptor(KeyDecrypted, IV);

                    // Decrypt the cipher text from
                    // from the FileSteam of the encrypted
                    // file (inFs) into the FileStream
                    // for the decrypted file (outFs).
                    using (var outFs = new FileStream(outFile, FileMode.Create))
                    {
                        int count = 0;
                        int offset = 0;

                        // blockSizeBytes can be any arbitrary size.
                        int blockSizeBytes = aes.BlockSize / 8;
                        byte[] data = new byte[blockSizeBytes];

                        // By decrypting a chunk a time,
                        // you can save memory and
                        // accommodate large files.

                        // Start at the beginning
                        // of the cipher text.
                        inFs.Seek(startC, SeekOrigin.Begin);
                        using (var outStreamDecrypted =
                            new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                        {
                            do
                            {
                                count = inFs.Read(data, 0, blockSizeBytes);
                                offset += count;
                                outStreamDecrypted.Write(data, 0, count);
                            } while (count > 0);

                            outStreamDecrypted.FlushFinalBlock();
                        }
                    }
                }
            }


            else if (selectedAlgorithm == "TripleDES")
            //else if (cryptedFileEnd == ".a")

            {
                    TripleDES tripleDES = TripleDES.Create();

                    
                    byte[] LenK = new byte[4];
                    byte[] LenIV = new byte[4];

                    
                    string outFile =
                        Path.ChangeExtension(file.FullName.Replace("Encrypt", "Decrypt"), fileEnd);

                   
                    using (var inFs = new FileStream(file.FullName, FileMode.Open))
                    {
                        inFs.Seek(0, SeekOrigin.Begin);
                        inFs.Read(LenK, 0, 3);
                        inFs.Seek(4, SeekOrigin.Begin);
                        inFs.Read(LenIV, 0, 3);

                        
                        int lenK = BitConverter.ToInt32(LenK, 0);
                        int lenIV = BitConverter.ToInt32(LenIV, 0);

                        
                        
                        int startC = lenK + lenIV + 8;
                        int lenC = (int)inFs.Length - startC;

                       
                        byte[] KeyEncrypted = new byte[lenK];
                        byte[] IV = new byte[lenIV];

                        
                        inFs.Seek(8, SeekOrigin.Begin);
                        inFs.Read(KeyEncrypted, 0, lenK);
                        inFs.Seek(8 + lenK, SeekOrigin.Begin);
                        inFs.Read(IV, 0, lenIV);

                        Directory.CreateDirectory(DecrFolder);
                        
                        byte[] KeyDecrypted = _rsa.Decrypt(KeyEncrypted, false);

                       
                        ICryptoTransform transform = tripleDES.CreateDecryptor(KeyDecrypted, IV);

                        
                        using (var outFs = new FileStream(outFile, FileMode.Create))
                        {
                            int count = 0;
                            int offset = 0;

                           
                            int blockSizeBytes = tripleDES.BlockSize / 8;
                            byte[] data = new byte[blockSizeBytes];

                            
                            inFs.Seek(startC, SeekOrigin.Begin);
                            using (var outStreamDecrypted =
                                new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                            {
                                do
                                {
                                    count = inFs.Read(data, 0, blockSizeBytes);
                                    offset += count;
                                    outStreamDecrypted.Write(data, 0, count);
                                } while (count > 0);

                                outStreamDecrypted.FlushFinalBlock();
                            }
                        }
                    }
                }

            
        }
        public FileCryption(string databaseName)
        {

            InitializeComponent();
            _databaseName = databaseName;
            connectionString = $"Data Source={_databaseName};Version=3;";
            radioButtonAES.Checked = true;




        }

        private void buttonCreateAsmKeys_Click(object sender, EventArgs e)
        {
            // Stores a key pair in the key container.
            _cspp.KeyContainerName = KeyName;
            _rsa = new RSACryptoServiceProvider(_cspp)
            {
                PersistKeyInCsp = true
            };

            label1.Text = _rsa.PublicOnly
                ? $"Key: {_cspp.KeyContainerName} - Public Only"
                : $"Key: {_cspp.KeyContainerName} - Full Key Pair";
        }

        private void buttonEncryptFile_Click(object sender, EventArgs e)
        {
            if (_rsa is null)
            {
                MessageBox.Show("Key not set.");
            }
            else
            {
                // Display a dialog box to select a file to encrypt.
                _encryptOpenFileDialog.InitialDirectory = SrcFolder;
                if (_encryptOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fName = _encryptOpenFileDialog.FileName;
                    EncrypttextBox.Text = fName;
                    if (fName != null)
                    {
                        // Pass the file name without the path.
                        EncryptFile(new FileInfo(fName));
                    }
                }
            }
        }

        private void buttonDecryptFile_Click(object sender, EventArgs e)
        {
            if (_rsa is null)
            {
                MessageBox.Show("Key not set.");
            }
            else
            {
                // Display a dialog box to select the encrypted file.
                _decryptOpenFileDialog.InitialDirectory = EncrFolder;
                if (_decryptOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fName = _decryptOpenFileDialog.FileName;
                    DecrypttextBox.Text = fName;
                    if (fName != null)
                    {
                        DecryptFile(new FileInfo(fName));
                    }
                }
            }
        }

        private void buttonExportPublicKey_Click(object sender, EventArgs e)
        {
            // Save the public key created by the RSA
            // to a file. Caution, persisting the
            // key to a file is a security risk.
            Directory.CreateDirectory(EncrFolder);
            using (var sw = new StreamWriter(PubKeyFile, false))
            {
                sw.Write(_rsa.ToXmlString(false));
            }
        }

        private void buttonImportPublicKey_Click(object sender, EventArgs e)
        {
            using (var sr = new StreamReader(PubKeyFile))
            {
                _cspp.KeyContainerName = KeyName;
                _rsa = new RSACryptoServiceProvider(_cspp);

                string keytxt = sr.ReadToEnd();
                _rsa.FromXmlString(keytxt);
                _rsa.PersistKeyInCsp = true;

                label1.Text = _rsa.PublicOnly
                    ? $"Key: {_cspp.KeyContainerName} - Public Only"
                    : $"Key: {_cspp.KeyContainerName} - Full Key Pair";
            }
        }

        private void buttonGetPrivateKey_Click(object sender, EventArgs e)
        {
            _cspp.KeyContainerName = KeyName;
            _rsa = new RSACryptoServiceProvider(_cspp)
            {
                PersistKeyInCsp = true
            };

            label1.Text = _rsa.PublicOnly
                ? $"Key: {_cspp.KeyContainerName} - Public Only"
                : $"Key: {_cspp.KeyContainerName} - Full Key Pair";
        }

        private void radioButtonAES_CheckedChanged(object sender, EventArgs e)
        {
            rbAlgorithm_CheckedChanged(sender, e);
        }

        private void radioButtonTriDES_CheckedChanged(object sender, EventArgs e)
        {
            rbAlgorithm_CheckedChanged(sender, e);
        }

        private void rbAlgorithm_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton == radioButtonAES)
            {
                selectedAlgorithm = "AES";
            }
            else if (radioButton == radioButtonTriDES)
            {
                selectedAlgorithm = "TripleDES";
            }
        }

        private bool SaveAlgorithm(string selectedAlgorithm, string filename)
        {


            try
            {
                if (IsFileExistsInDatabase(filename))
                {
                    MessageBox.Show("Choose another file or change current file name");
                    return false;
                }

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO FileEncryption (algorithm, encrypted_file_name) VALUES (@algorithm, @encrypted_file_name)";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@algorithm", selectedAlgorithm);
                        command.Parameters.AddWithValue("@encrypted_file_name", filename);
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Virhe tallennettaessa algoritmia ja tiedostonimeä: " + ex.Message);
                return false;
            }
        }


        private bool IsFileExistsInDatabase(string filename)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM FileEncryption WHERE encrypted_file_name = @filename";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@filename", filename);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }


            private string GetAlgorithm(string filename)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT encrypted_file_name, algorithm FROM FileEncryption WHERE encrypted_file_name LIKE @filename";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@filename", filename + "%");

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string encryptedFileName = reader.GetString(0);
                                string algorithm = reader.GetString(1);
                                return $"{encryptedFileName},{algorithm}";
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }





    }

}