using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Controller.Security
{
    //http://msdn.microsoft.com/es-es/library/bb972217.aspx
    //http://msdn.microsoft.com/es-es/library/bb972216.aspx
    internal sealed class Encriptado
    {
        protected string VectorInicialización_PBKDF1;
        protected string PwdKey_PBKDF1;
        protected string Salt_PBKDF1;

        protected string VectorInicialización_PBKDF2;
        protected string PwdKey_PBKDF2;
        protected string Salt_PBKDF2;


        private bool NuevaParametrizacion_PBKDF1 = false;
        private bool NuevaParametrizacion_PBKDF2 = false;

        [Obsolete("Este Metodo usa PBKDF1, el cual ha sido depreciado. Usar los metodos con el encriptado PBKDF2.")]
        private void AsignarParametros_PBKDF1()
        {
            if (NuevaParametrizacion_PBKDF1 == false)
            {
                VectorInicialización_PBKDF1 = "Vect@rInizi1izaCi@n";
                PwdKey_PBKDF1 = "brend@";
                Salt_PBKDF1 = "liliana";
            }
        }
        private void AsignarParametros_PBKDF2()
        {
            if (NuevaParametrizacion_PBKDF2 == false)
            {
                VectorInicialización_PBKDF2 = "Vect@rInizi1izaCi@n";
                PwdKey_PBKDF2 = "brend@";
                Salt_PBKDF2 = "lilianaMasgrande";
            }
        }

        public Encriptado()
        {

        }

        [Obsolete("Este Metodo usa PBKDF1, el cual ha sido depreciado. Usar los metodos con el encriptado PBKDF2.")]
        public void SetKeyIVSalt_PBKDF1(string IV, string pwdKey, string salt)
        {
            if (String.IsNullOrEmpty(IV) || String.IsNullOrEmpty(pwdKey) || String.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("Algunos de los valores pasados no pueden ser vacios.");
            }
            else
            {
                NuevaParametrizacion_PBKDF1 = true;
                this.VectorInicialización_PBKDF1 = IV;
                this.PwdKey_PBKDF1 = pwdKey;
                this.Salt_PBKDF1 = salt;
            }
        }

        public void SetKeyIVSalt_PBKDF2(string IV, string pwdKey, string salt)
        {
            if (String.IsNullOrEmpty(IV) || String.IsNullOrEmpty(pwdKey) || String.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("Algunos de los valores pasados no pueden ser vacios.");
            }
            else
            {
                NuevaParametrizacion_PBKDF2 = true;
                this.VectorInicialización_PBKDF1 = IV;
                this.PwdKey_PBKDF1 = pwdKey;
                this.Salt_PBKDF1 = salt;
            }
        }

        [Obsolete("Este Metodo usa PBKDF1, el cual ha sido depreciado. Usar los metodos con el encriptado PBKDF2.")]
        public string Encriptar_PBKDF1(string cadenaAEncriptar)
        {
            string cadenaCrifrada = String.Empty;
            try
            {
                AsignarParametros_PBKDF1();
                string password = PwdKey_PBKDF1;
                byte[] passwordByte = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] cadenaByteAEncriptar = System.Text.Encoding.Unicode.GetBytes(cadenaAEncriptar);
                byte[] salt = System.Text.Encoding.ASCII.GetBytes(Salt_PBKDF1);

                /*Encriptacion:
                 *  Asimétrica: Dos claves diferentes para encriptar datos, clave pública y clave privada, publica para encriptar, privada para desencriptar              
                 *  Simétrica : Una sola clave para encriptar y desencriptar los datos
                 *  Clave (key): A sequence of symbols that controls the operation of a cryptographic transformation (e.g. encipherment, decipherment).
                 */

                /* PasswordDerivedBytes: 
                //'Deriva' los bytes adecuados de una contraseña(key) por medio del algoritmo PBKDF1, para que se obtenga una clave adecuada como contraseña
                //, o sea, ejecuta el algoritmo PBKDF1 (Máximo 160 bits-> 20 bytes) sobre un arreglo de bytes que contienen la contraseña legible por el humano (brenda)
                // y genera nuevos bytes que serán usados como un password real para una clave de encriptación.
                // Persona-> password ->PasswordDeriveBytes-> clave(key) para encriptar 
                 */

                /*Metodo AES(Advanced Encryption Standard):Rijndael (cifrado por bloques, clave de 256 bits)
                        *  es un algoritmo seguro y eficiente. Sus creadores son 
                        * Joan Daemen y Vincent Rijmen (Bélgica). Ha 
                        * sido elegido como el nuevo Estándar Avanzado de Encriptación (AES) por el 
                        * Instituto Nacional de Estándares y Tecnología (NIST) de los EEUU.
                 * Metodo RSA: Algoritmo de cifrado asimétrico por bloques, clave pública y clave privada
                */

                /* PBKDF1, derivacion de claves.
                 * Operaciones a realizar, para derivar una clave. un atacante conoce de los algoritmos de encriptación y procesos de derivación de claves
                 * y podría realziar el mismo proceso para obtener la clave de encriptación, por eso se usa 2 metodos:
                 * 1. Realizar un salt. significa realizar una mezcla de bytes aleatorios al mensaje ('concatenar'), algunas contraseñas suelen ser cortas,
                 *                     o en este caso la contraseña. Solo se realiza una vez, al inicio.
                 *                     Aumenta el numero posible de claves resultantes para una contraseña, y evitar que un hacker pueda
                 *                     obtener la misma clave. Esto evita que un atatcante pueda tener un diccionario de datos sobre posibles
                 *                     datos precalculados.
                 * 2. Iteraciones
                 *      2.1 Realizar o calcular el algrotimo HASH a los bytes resultantes entre la contraseña y el salt.
                 *      2.2 Stretching (extender) lo que se introduce, o sea, conteo de iteraciones.
                 *                      Especifica que la funcion HASH debe ser repetida en un numero de veces (minimo 1000)
                 * 
                 */

                // SHA1 es de 160 bits.
                //Se recomienda un minimo de iteraciones 1000
                PasswordDeriveBytes derivarPassword = new PasswordDeriveBytes(passwordByte, salt, "SHA1", 200);
                //Se obtienen bytes pseudo-aleatorios de la clave cifrada. Cuando se llama a este metodo, la clave(key), ya esta crifrada
                //lo que se hace es obtener de nuevo  bytes aleatorios. Se especifica el tamaño de la clave en bytes, como es 256-> 256/8
                byte[] key = derivarPassword.GetBytes(256 / 8);


                /****** Manejar encriptacion de los datos por medio de rijndael ***********/
                RijndaelManaged rijndaelSimetrico = new RijndaelManaged();
                //Modo de cifrado de bloques para el algoritmo, default CipherMode.CBC. esto indica que 
                //  Se utliza una clave (key) y vector de initializacion para realizar una transformación criptográfica de datos            
                rijndaelSimetrico.Mode = CipherMode.CBC;

                //Se crea un transformador de encriptacion en Rijndael en base a:
                // 1. La clave obtenida del proceso anterior
                // 2. Se utiliza un vector de inicialización
                // 3. KeySize se calcula automáticamente en base al tamaño de bytes de la clave (key)
                byte[] IV = System.Text.Encoding.ASCII.GetBytes(VectorInicialización_PBKDF1);
                ICryptoTransform encriptador = rijndaelSimetrico.CreateEncryptor(key, IV);


                /*
                    Para poder encriptar una gran cantidad de datos y lograr una independencia en el algoritmo de encriptacion
                    se utiliza CryptoStream: 
                        Esto nos permite trabajar sobre una gran cantidad de datos almacenados en un stream
                        y además aplicarle a cada secuenca (buffer) del stream, la encriptación
                 */
                MemoryStream memoryStream = new MemoryStream();

                //Aquí en realidad , el que realiza todo el trabajo de encriptacion es 'encriptador', obtenido de 'rijndaelSimetrico'.
                //  Se define como Write, para el caso de encriptacion
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encriptador, CryptoStreamMode.Write);

                //Aquí se procede a encriptar los datos basados en un stream
                //Se escribe en el Stream 'memoryStream', los datos encriptados en base a 'encriptador'
                cryptoStream.Write(cadenaByteAEncriptar, 0, cadenaByteAEncriptar.Length);
                //Se finaliza la encriptacion y actualiza el stream memorystream subyacente, limpia el buffer.
                cryptoStream.FlushFinalBlock();
                memoryStream.Flush();
                //Antes de cerrar los streams, se asignan los datos cifrados 
                cadenaCrifrada = Convert.ToBase64String(memoryStream.ToArray());
                memoryStream.Close();
                //cryptoStream.Close();

                return cadenaCrifrada;
            }
            catch (Exception)
            {
                // ARROJAR LA MISMA EXCEPCION. SIRVE ADEMÁS PARA NO LIMPIAR EL STACKTRACE, ASÍ SE SABRÁ 
                // EL ERROR REAL
                throw;
            }
        }

        [Obsolete("Este Metodo usa PBKDF1, el cual ha sido depreciado. Usar los metodos con el encriptado PBKDF2.")]
        public string DesEncriptar_PBKDF1(string cadenaEncriptada)
        {
            string cadenaDesCrifrada = String.Empty;
            try
            {
                AsignarParametros_PBKDF1();
                byte[] datosCifrados = Convert.FromBase64String(cadenaEncriptada);
                string password = PwdKey_PBKDF1;
                byte[] passwordByte = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] salt = System.Text.Encoding.ASCII.GetBytes(Salt_PBKDF1);

                // SHA1 es de 160 bits.
                //Se recomienda un minimo de iteraciones 1000
                PasswordDeriveBytes derivarPassword = new PasswordDeriveBytes(passwordByte, salt, "SHA1", 200);
                //Se obtienen bytes pseudo-aleatorios de la clave cifrada. Cuando se llama a este metodo, la clave(key), ya esta crifrada
                //lo que se hace es obtener de nuevo  bytes aleatorios. Se especifica el tamaño de la clave en bytes, como es 256-> 256/8
                byte[] key = derivarPassword.GetBytes(256 / 8);


                /****** Manejar encriptacion de los datos por medio de rijndael ***********/
                RijndaelManaged rijndaelSimetrico = new RijndaelManaged();
                //Modo de cifrado de bloques para el algoritmo, default CipherMode.CBC. esto indica que 
                //  Se utliza una clave (key) y vector de initializacion para realizar una transformación criptográfica de datos            
                rijndaelSimetrico.Mode = CipherMode.CBC;

                //Se crea un transformador de descencriptacion en Rijndael en base a:
                // 1. La clave obtenida del proceso anterior
                // 2. Se utiliza un vector de inicialización
                // 3. KeySize se calcula automáticamente en base al tamaño de bytes de la clave (key)
                byte[] IV = System.Text.Encoding.ASCII.GetBytes(VectorInicialización_PBKDF1);
                ICryptoTransform desEncriptador = rijndaelSimetrico.CreateDecryptor(key, IV);


                //Se define el memorystram que almacenará los datos encriptados
                MemoryStream memoryStream = new MemoryStream(datosCifrados);

                //Aquí en realidad , el que realiza todo el trabajo de desEncriptacion es 'encriptador', obtenido de 'rijndaelSimetrico'.
                //  Se define como Read, para el caso de DesEncriptacion
                CryptoStream cryptoStream = new CryptoStream(memoryStream, desEncriptador, CryptoStreamMode.Read);

                //buffer para almacenar los datos a descifrar
                byte[] buffer = new byte[datosCifrados.Length];
                //Aquí se procede a descifrar los datos basados en un stream
                //Se lee del Stream 'memoryStream', los datos que son desencriptados en base a 'encriptador'
                int cantidadLeida = cryptoStream.Read(buffer, 0, buffer.Length);
                cryptoStream.Flush();
                memoryStream.Flush();
                //Antes de cerrar los streams, se asignan los datos descifrados 
                //string cadenaDesCrifrada = System.Text.Encoding.Unicode.GetString(buffer, 0, cantidadLeida + 4);
                cadenaDesCrifrada = System.Text.Encoding.Unicode.GetString(buffer, 0, cantidadLeida);

                memoryStream.Close();
                //cryptoStream.Close();

                return cadenaDesCrifrada;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Encriptar_PBKDF2(string cadenaAEncriptar)
        {
            string cadenaCrifrada = String.Empty;
            try
            {
                AsignarParametros_PBKDF2();
                string password = PwdKey_PBKDF2;
                byte[] passwordByte = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] cadenaByteAEncriptar = System.Text.Encoding.Unicode.GetBytes(cadenaAEncriptar);
                byte[] salt = System.Text.Encoding.ASCII.GetBytes(Salt_PBKDF2);

                //Usa el algoritmo HMACSHA1, Codigo de autenticacion de Mensaje bsado en Hash (MAC- HASH)
                //Minimo de iteraciones 1000, auqnue si no se pone es el default
                Rfc2898DeriveBytes derivarPassword = new Rfc2898DeriveBytes(passwordByte, salt, 500);
                byte[] key = derivarPassword.GetBytes(16);

                /****** Manejar encriptacion de los datos por medio de rijndael ***********/
                RijndaelManaged rijndaelSimetrico = new RijndaelManaged();
                //Modo de cifrado de bloques para el algoritmo, default CipherMode.CBC. esto indica que 
                //  Se utliza una clave (key) y vector de initializacion para realizar una transformación criptográfica de datos            
                rijndaelSimetrico.Mode = CipherMode.CBC;

                //Se crea un transformador de encriptacion en Rijndael en base a:
                // 1. La clave obtenida del proceso anterior
                // 2. Se utiliza un vector de inicialización
                // 3. KeySize se calcula automáticamente en base al tamaño de bytes de la clave (key)
                byte[] IV = System.Text.Encoding.ASCII.GetBytes(VectorInicialización_PBKDF2);
                ICryptoTransform encriptador = rijndaelSimetrico.CreateEncryptor(key, IV);


                /*
                    Para poder encriptar una gran cantidad de datos y lograr una independencia en el algoritmo de encriptacion
                    se utiliza CryptoStream: 
                        Esto nos permite trabajar sobre una gran cantidad de datos almacenados en un stream
                        y además aplicarle a cada secuenca (buffer) del stream, la encriptación
                 */
                MemoryStream memoryStream = new MemoryStream();

                //Aquí en realidad , el que realiza todo el trabajo de encriptacion es 'encriptador', obtenido de 'rijndaelSimetrico'.
                //  Se define como Write, para el caso de encriptacion
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encriptador, CryptoStreamMode.Write);

                //Aquí se procede a encriptar los datos basados en un stream
                //Se escribe en el Stream 'memoryStream', los datos encriptados en base a 'encriptador'
                cryptoStream.Write(cadenaByteAEncriptar, 0, cadenaByteAEncriptar.Length);
                //Se finaliza la encriptacion y actualiza el stream memorystream subyacente, limpia el buffer.
                cryptoStream.FlushFinalBlock();
                memoryStream.Flush();
                //Antes de cerrar los streams, se asignan los datos cifrados 
                cadenaCrifrada = Convert.ToBase64String(memoryStream.ToArray());
                memoryStream.Close();
                //cryptoStream.Close();

                return cadenaCrifrada;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DesEncriptar_PBKDF2(string cadenaEncriptada)
        {
            string cadenaDesCrifrada = String.Empty;
            try
            {
                AsignarParametros_PBKDF2();
                byte[] datosCifrados = Convert.FromBase64String(cadenaEncriptada);
                string password = PwdKey_PBKDF2;
                byte[] passwordByte = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] salt = System.Text.Encoding.ASCII.GetBytes(Salt_PBKDF2);

                //Minimo de iteraciones 1000, auqnue si no se pone es el default
                Rfc2898DeriveBytes derivarPassword = new Rfc2898DeriveBytes(passwordByte, salt, 500);
                byte[] key = derivarPassword.GetBytes(16);

                /****** Manejar encriptacion de los datos por medio de rijndael ***********/
                RijndaelManaged rijndaelSimetrico = new RijndaelManaged();
                //Modo de cifrado de bloques para el algoritmo, default CipherMode.CBC. esto indica que 
                //  Se utliza una clave (key) y vector de initializacion para realizar una transformación criptográfica de datos            
                rijndaelSimetrico.Mode = CipherMode.CBC;

                //Se crea un transformador de descencriptacion en Rijndael en base a:
                // 1. La clave obtenida del proceso anterior
                // 2. Se utiliza un vector de inicialización
                // 3. KeySize se calcula automáticamente en base al tamaño de bytes de la clave (key)

                byte[] IV = System.Text.Encoding.ASCII.GetBytes(VectorInicialización_PBKDF2);
                //byte[] IV = System.Text.Encoding.ASCII.GetBytes("NeXt@rInizi0izaCi@n");
                ICryptoTransform desEncriptador = rijndaelSimetrico.CreateDecryptor(key, IV);


                //Se define el memorystram que almacenará los datos encriptados
                MemoryStream memoryStream = new MemoryStream(datosCifrados);

                //Aquí en realidad , el que realiza todo el trabajo de desEncriptacion es 'encriptador', obtenido de 'rijndaelSimetrico'.
                //  Se define como Read, para el caso de DesEncriptacion
                CryptoStream cryptoStream = new CryptoStream(memoryStream, desEncriptador, CryptoStreamMode.Read);

                //buffer para almacenar los datos a descifrar
                byte[] buffer = new byte[datosCifrados.Length];
                //Aquí se procede a descifrar los datos basados en un stream
                //Se lee del Stream 'memoryStream', los datos que son desencriptados en base a 'encriptador'
                int cantidadLeida = cryptoStream.Read(buffer, 0, buffer.Length);
                cryptoStream.Flush();
                memoryStream.Flush();
                //Antes de cerrar los streams, se asignan los datos descifrados 
                //string cadenaDesCrifrada = System.Text.Encoding.Unicode.GetString(buffer, 0, cantidadLeida + 4);
                cadenaDesCrifrada = System.Text.Encoding.Unicode.GetString(buffer, 0, cantidadLeida);

                memoryStream.Close();
                //cryptoStream.Close();            
                return cadenaDesCrifrada;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
