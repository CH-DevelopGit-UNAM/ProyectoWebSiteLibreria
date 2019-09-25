using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Controller.Security
{
    public class EncriptData
    {
        public EncriptData() { }

        // Servicio Notificador: solamente realiza un SHA1 a la contraseña para revisar el usuario                 

        // Sitio Evaluador:  Solamente valida la cookie PHP y que el valor exista en BD en la clase base SecurePage, luego revisa el usuario obteniendo los datos como usuario y contraseña
        //                      No aplica encriptacion, ya que revisa estos 3 parametros (PHP cookie, BD cookie ID, BD usuario asociada a id cookie)
        //                      Pero manda a llamar al sitio Notificador pasandole un GUID y asociandoles los datos de usuario y contraseña de la BD

        // Servicio Pdf : la contraseña se usa tal cual, no se le aplica nada. Pero usa metodos de encriptacion AES para encriptar el PDF y usa MD5 para el HASH 

        // Sitio Notificador: La página Login revisa los datos del GUID y usuario/contraseña asociados en la BD. Usa la clase SecurePage para validar el usuario
        //                      Si el usuario es academico (tiene el rol "Capturar_Informe") 
        //                          > Se aplica SHA1 a la contraseña (no de la BD) vs campo "pass_sup" devuelto al usar el sp "spNOTI_Web_AutenticaUsuario" de la BD
        //                          > Si el SHA1 no coincide con el campo "pass_sup"  (sp "spNOTI_Web_AutenticaUsuario") entonces valida la contraseña vs "clave" tabla "academicos_siah" (existe en otra BD)
        //                              La clave es desencriptada con RijndaelManaged
        //                              La tabla pertenece a la BD de SIAH
        //                      Si no, y todo falla, valida entonces el SHA1 aplicado a la contraseña actual vs contraseña de la BD (no se desencripta nada, solo se aplica el SHA1)


        public static string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        #region Métodos_Usados_por_servicio_PDF
        public void GenerateMD5()
        {

        }

        public string GenerateMD5Hash(string text)
        {

            DataTable objDt = new DataTable();

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.UTF8.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public string GetMD5FromFile(string fullfuleName)
        {
            MD5 md5 = null;
            StringBuilder strBuilder = null;
            byte[] bytes = null;
            byte[] result = null;

            try
            {
                bytes = File.ReadAllBytes(fullfuleName);

                md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text
                md5.ComputeHash(bytes);

                //get hash result after compute it
                result = md5.Hash;

                strBuilder = new StringBuilder();

                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits
                    //for each byte
                    strBuilder.Append(result[i].ToString("x2"));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return strBuilder.ToString();
        }

        private string EncryptText(string inputValue, string inputsalt)
        {

            string salt = "CoordinacionHumanidades" + inputsalt;

            byte[] utfData = UTF8Encoding.UTF8.GetBytes(inputValue);

            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            string encryptedString = string.Empty;

            using (AesManaged aes = new AesManaged())
            {

                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(salt, saltBytes);

                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;

                aes.KeySize = aes.LegalKeySizes[0].MaxSize;

                aes.Key = rfc.GetBytes(aes.KeySize / 8);

                aes.IV = rfc.GetBytes(aes.BlockSize / 8);

                using (ICryptoTransform encryptTransform = aes.CreateEncryptor())
                {

                    using (MemoryStream encryptedStream = new MemoryStream())
                    {

                        using (CryptoStream encryptor =

                        new CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write))
                        {

                            encryptor.Write(utfData, 0, utfData.Length);

                            encryptor.Flush();

                            encryptor.Close();

                            byte[] encryptBytes = encryptedStream.ToArray();

                            encryptedString = Convert.ToBase64String(encryptBytes);

                        }

                    }

                }

            }

            return encryptedString;

        }

        private string DecryptText(string inputValue, string inputsalt)
        {

            string salt = "CoordinacionHumanidades" + inputsalt;

            byte[] encryptedBytes = Convert.FromBase64String(inputValue);

            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            string decryptedString = string.Empty;

            using (var aes = new AesManaged())
            {

                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(salt, saltBytes);

                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;

                aes.KeySize = aes.LegalKeySizes[0].MaxSize;

                aes.Key = rfc.GetBytes(aes.KeySize / 8);

                aes.IV = rfc.GetBytes(aes.BlockSize / 8);

                using (ICryptoTransform decryptTransform = aes.CreateDecryptor())
                {

                    using (MemoryStream decryptedStream = new MemoryStream())
                    {

                        CryptoStream decryptor = new CryptoStream(decryptedStream, decryptTransform, CryptoStreamMode.Write);

                        decryptor.Write(encryptedBytes, 0, encryptedBytes.Length);

                        decryptor.Flush();

                        decryptor.Close();

                        byte[] decryptBytes = decryptedStream.ToArray();

                        decryptedString = UTF8Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);

                    }

                }

            }

            return decryptedString;

        }
        #endregion


        #region metodos_usador_sitio_ESIAH

        private string ConvertirContrasenaESIAH(string password)
        {
            const string KEY_AES_256 = "8j!3T)(ZJ4t&qvxMQNL$Pq;![6?547W;";
            string psw_desencriptado = "";
            byte[] bytes_password_base64 = null;
            string str_password_decode = "";
            string str_iv2_decode = "";
            string str_input2 = "";
            byte[] bytes_iv_decode = null;
            byte[] byte_key_decode = null;
            
            //Obtenemos  la cadena en base64
            bytes_password_base64 = Convert.FromBase64String(password);

            //Hacemos la conversión de bytes a cadena
            str_password_decode = Encoding.Default.GetString(bytes_password_base64);

            //Obtenemos los primeros 32 caracteres de la cadena decodificada que es el IV
            str_iv2_decode = str_password_decode.Substring(0, 32);

            //Obtenemos los 32 caracteres siguientes de la cadena que es el password encriptado
            str_input2 = str_password_decode.Substring(32, 32);

            //Obtenemos los bytes del IV 
            bytes_iv_decode = Encoding.Default.GetBytes(str_iv2_decode);

            //Obtenemos los bytes del password
            byte_key_decode = Encoding.Default.GetBytes(KEY_AES_256);

            
            try
            {
                //Obtenemos la cadena desencriptada
                psw_desencriptado = this.AES256_decrypt(str_input2, byte_key_decode, bytes_iv_decode);
            }
            catch { }

            return psw_desencriptado;
        }


        /// <summary>
        /// Método que decifra un algoritmo AES de 256 bits, los primeros 32 caracteres de la cadena es IV, los siguientes 32 caracteres son la contraseña
        /// </summary>
        /// <param name="password"></param>
        /// <param name="AES_Key"></param>
        /// <param name="AES_IV"></param>
        /// <returns></returns>
        public String AES256_decrypt(string password, byte[] AES_Key, byte[] AES_IV)
        {
            // Create decryptor
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.Zeros;
            var decrypt = aes.CreateDecryptor(AES_Key, AES_IV);

            // Decrypt Input
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                // Convert from base64 string to byte array, write to memory stream and decrypt, then convert to byte array.
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.Default.GetBytes(password);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = ms.ToArray();
            }

            // Convert from byte array to UTF-8 string then return
            String Output = Encoding.Default.GetString(xBuff);
            return Output;
        }

        #endregion
    }
}
