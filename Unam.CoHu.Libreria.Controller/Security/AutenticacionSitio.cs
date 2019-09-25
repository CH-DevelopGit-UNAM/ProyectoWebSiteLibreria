using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Unam.CoHu.Libreria.ADO.Security;
using Unam.CoHu.Libreria.Controller.Web;
using Unam.CoHu.Libreria.Model.Security;

namespace Unam.CoHu.Libreria.Controller.Security
{
    public sealed class AutenticacionSitio
    {
        private UsuarioContext usuarioBD = new UsuarioContext();
        private Encriptado enc = new Security.Encriptado();
        private Usuario _usuario;
        private string _usuarioName;
        private string _passwd;
        private bool _isAutenticado;
        private bool _isValidado;
        private string _cnnString = ConfigurationManager.ConnectionStrings["LDAPServices"].ConnectionString;
        private string _MensajeValidacion = string.Empty;

        public bool IsAutenticado
        {
            get { return _isAutenticado; }
        }

        public Usuario UsuarioSistema { get { return _usuario; } }
        public string MensajeValidacion { get { return _MensajeValidacion; } }


        public AutenticacionSitio()
        {
            this._isAutenticado = false;
            this._isValidado = false;
            this._usuario = null;
        }

        public AutenticacionSitio(string usuario, string password)
        {
            this._usuarioName = usuario;
            this._passwd = password;
            this._isAutenticado = false;
            this._isValidado = false;
            this._usuario = null;
        }

        public bool AutorizarAcceso()
        {
            try
            {
                if (ValidarAcceso(this._usuarioName, this._passwd))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidarAcceso(string usuario, string pass)
        {
            _isValidado = true;
            // TO-DO: Encriptacion / generacion de SHA
            List<Usuario> usuarios = usuarioBD.SelectByNombrePassword(usuario, pass);
            bool retorno = false;
            if (usuarios != null && usuarios.Count > 0)
            {
                Usuario user = (from q in usuarios where q.Activo == true select q).FirstOrDefault();
                if (user != null)
                {
                    _isAutenticado = true;
                    _usuario = new Usuario() { IdUsuario = user.IdUsuario, IdUsuarioJefe = user.IdUsuarioJefe, Nombre = user.Nombre, UsuarioNombre = user.UsuarioNombre, Activo = user.Activo };
                    retorno = true;
                    _MensajeValidacion = "Usuario autenticado";
                }
                else
                {
                    _isAutenticado = false;
                    _isValidado = true;
                    _usuario = new Usuario() { IdUsuario = 0, IdUsuarioJefe = 0, Nombre = String.Empty, UsuarioNombre = string.Empty };
                    _MensajeValidacion = "El usuario y/o contraseña son incorrectos";
                }
            }
            else
            {
                _isValidado = false;
                _isAutenticado = false;
                _MensajeValidacion = "El usuario y/o contraseña son incorrectos";
            }
            return retorno;
        }

        private bool ValidarContrasena(string password, string passBD)
        {
            Encriptado encriptado = new Encriptado();
            if (passBD.Equals(encriptado.Encriptar_PBKDF2(password)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool AutorizarAccesoLDAP()
        {
            try
            {
                if (ValidarAccesoLDAP(this._usuarioName, this._passwd))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidarAccesoLDAP(string usuario, string pass)
        {
            System.Security.Principal.WindowsIdentity current = System.Security.Principal.WindowsIdentity.GetCurrent();
            _isValidado = true;
            // System.Web.ApplicationServices          
            MembershipProvider memberShipProvider = Membership.Providers["ActiveDirectoryMembershipProvider"];
            bool retorno = false;
            if (memberShipProvider.ValidateUser(usuario, pass))
            {
                _isAutenticado = true;
                _isValidado = true;
                _usuario = new Usuario() { IdUsuario = 1000, IdUsuarioJefe = 1001, Nombre = "PEPITO", UsuarioNombre = "PEPITO 2" };
                retorno = true;
            }
            else
            {
                _isAutenticado = false;
                _usuario = new Usuario() { IdUsuario = 0, IdUsuarioJefe = 0, Nombre = String.Empty, UsuarioNombre = string.Empty };
                _isValidado = false;
            }

            return retorno;
        }


        public static void TerminarSesionPrincipal(bool noStoreCache)
        {
            HttpContext.Current.Session.Remove("Usuario");
            ConfiguracionSitio.UsuarioSistema = null;

            FormsAuthentication.SignOut();

            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(rFormsCookie);

            if (noStoreCache)
            {
                // Clear session cookie 
                HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
                rSessionCookie.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Current.Response.Cookies.Add(rSessionCookie);

                // Invalidate the Cache on the Client Side
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetNoStore();

            }
        }

        public static bool IsUsuarioAutenticado()
        {
            bool isAuthenticated = false;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[".wsl"];
            Usuario usuario = ConfiguracionSitio.UsuarioSistema;
            System.Security.Principal.IIdentity user = HttpContext.Current.User.Identity;
            if (cookie != null & usuario != null & user != null) {
                isAuthenticated = usuario.Nombre.Equals(user.Name, StringComparison.InvariantCulture);
            }

            return isAuthenticated;
        }
    }
}
