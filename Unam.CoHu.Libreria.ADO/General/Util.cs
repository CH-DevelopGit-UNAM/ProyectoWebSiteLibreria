using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.ADO.General
{
    public sealed class UtilidadesADO
    {
        private UtilidadesADO()
        {

        }

        public static string LimpiarCadena(string cadena, bool sustituirPalabras = true, bool eliminarAcentosPalabra =false )
        {
            if ((cadena != null))
            {
                cadena = cadena.Replace("--", " ");
                cadena = cadena.Replace("%'", " ");
                cadena = cadena.Replace("'", "''");

                if (sustituirPalabras)
                {
                    string[] palabrasNoPermitidas = { "SELECT ", "UPDATE ", "DELETE ", " DROP ", "CREATE ", "VALUES ", "FROM ", "TABLE ", " OR ", " AND ",
            "WHERE ", "LIKE ", " ORDER ", "FETCH ", "INFORMATION_SCHEMA", "SCHEMA", "TABLE_NAME" };

                    foreach (string palabra in palabrasNoPermitidas)
                    {
                        cadena = cadena.ToUpper().Replace(palabra, "");
                    }
                }

                if (eliminarAcentosPalabra)
                {
                    cadena = EliminarAcentos(cadena);
                }

                return cadena.Trim();
            }
            else
            {
                return "";
            }
        }

        public static string EliminarAcentos(string cadena)
        {
            string cadenaNormalizada = cadena.Normalize(NormalizationForm.FormD);
            StringBuilder nuevaCadena = new StringBuilder();
            char caracter = '\0';
            int contaAux = 0;
            for (int i = 0; i <= cadenaNormalizada.Length - 1; i++)
            {
                if (contaAux < cadena.Length && (cadena[contaAux].Equals('Ñ') | cadena[contaAux].Equals('ñ')))
                {
                    nuevaCadena.Append(cadena[contaAux]);
                    contaAux += 1;
                    i += 1;
                }
                else
                {
                    caracter = cadenaNormalizada[i];
                    if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(caracter) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    {
                        nuevaCadena.Append(caracter);
                        contaAux += 1;
                    }
                }
            }
            return nuevaCadena.ToString();
        }

        /// <summary>
        /// Obtiene la parte entera de una clave usada como PK (-1 si no se pudo obtener)
        /// </summary>
        /// <param name="clave">Clave que asocia Prefijo + separador + consecutivo </param>
        /// <param name="separador">Separador de Prefijo + Consecutivo</param>
        /// <returns>Regresa la parte entera de Consecutivo o -1 si no pudo realizarlo</returns>
        public static int SepararClaveEntero(string clave, char separador)
        {
            int retorno = -1;
            if (string.IsNullOrEmpty(clave) == false)
            {
                string[] valores = clave.Split(new char[] { separador });
                if (valores.Length == 2)
                {
                    Int32.TryParse(valores[1], out retorno);
                }
            }
            return retorno;
        }

        public static bool GenerarClaveConsecutiva(string prefijo, string separador, int nuevoConsecutivo, int longitudCampo, ref string nuevaClaveGenerada)
        {
            string clave = string.Empty;
            bool retorno = false;
            int defaultValue = 0;
            int sumaPrefijo = 0;
            int longitudNumero = nuevoConsecutivo.ToString().Length;

            if (prefijo != null && separador != null)
            {
                sumaPrefijo = prefijo.Length + separador.Length;
                if ((sumaPrefijo + longitudNumero) == longitudCampo)
                {
                    nuevaClaveGenerada = prefijo + separador + nuevoConsecutivo.ToString();
                    retorno = true;
                }
                else if ((sumaPrefijo + longitudNumero) < longitudCampo)
                {
                    nuevaClaveGenerada = prefijo + separador + nuevoConsecutivo.ToString().PadLeft((longitudCampo - sumaPrefijo), '0');
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }
            }
            else
            {
                if (nuevoConsecutivo > 0)
                {
                    nuevaClaveGenerada = nuevoConsecutivo.ToString().PadLeft(longitudCampo, '0');
                    retorno = true;
                }
                else
                {
                    nuevaClaveGenerada = defaultValue.ToString().PadLeft(longitudCampo, '0');
                    retorno = false;
                }
            }

            return retorno;
        }

        public static List<T> LimpiarNulosLista<T>(List<T> entrada, bool nullIfNoElements)
        {
            List<T> listaRetorno = null;
            if (entrada != null)
            {
                listaRetorno = new List<T>();
                foreach (T item in entrada)
                {
                    if (item != null)
                    {
                        listaRetorno.Add(item);
                    }
                }
                if (nullIfNoElements)
                {
                    if (listaRetorno.Count <= 0)
                    {
                        listaRetorno = null;
                    }
                }
            }
            return listaRetorno;
        }
    }
}
