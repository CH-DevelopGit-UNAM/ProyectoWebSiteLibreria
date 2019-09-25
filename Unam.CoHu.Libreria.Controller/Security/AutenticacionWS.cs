using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Security;
using Unam.CoHu.Libreria.ADO.Security;
using Unam.CoHu.Libreria.Model.WebServices;

namespace Xamarin.Universal.WebService.Security
{
    public sealed class AutenticacionWS
    {        
        private string _usuarioName;
        private string _passwd;
        
        private string _cnnString = ConfigurationManager.ConnectionStrings["LDAPServices"].ConnectionString;
        private TokenUser _User;

        public TokenUser User {
            get { return _User;  }
        }

        public AutenticacionWS() {

        }
        public AutenticacionWS(string user, string password)
        {
            this._usuarioName = user;
            this._passwd = password;
        }

        public bool IsAutenticado
        {
            get {
                bool retorno = false;
                if (_User != null) {
                    retorno = _User.IsUserAuthenticated;
                }
                return retorno;
            }
        }
        
        public bool AutorizarAccesoLDAP()
        {
            try
            {
                //TokenUser usuarioToken = UsersLog.UsersOnline.Where(q => q.UserName.Equals(this._usuarioName, StringComparison.InvariantCultureIgnoreCase)).Select(q => q).FirstOrDefault();
                TokenUser usuarioToken = null;
                UsuarioContext bs = new UsuarioContext();
                if (usuarioToken == null)
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
                else {
                    this._User = usuarioToken;
                    return IsTokenValid(this._User.UserName, this._User.Token, DateTime.Now, ref this._User);
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
            // DLL: System.Web.ApplicationServices            
            MembershipProvider memberShipProvider = Membership.Providers["ActiveDirectoryMembershipProvider"];
            bool retorno = false;
            _User = new TokenUser();
            if (memberShipProvider.ValidateUser(usuario, pass))
            {
                _User.UserName = usuario;
                _User.Token = Guid.NewGuid().ToString();
                _User.IsTokenValid = true;
                _User.IsUserAuthenticated = true;
                _User.UntilDate = DateTime.Now.AddDays(2);
                _User.Message = "User Logged in sucessfully.";
                // TokenUser usuarioToken = (from q in UsersLog.UsersOnline where q.UserName.Equals(usuario, StringComparison.InvariantCultureIgnoreCase) select q).FirstOrDefault();

                TokenUser usuarioToken = null;

                /*
                if (usuarioToken == null) {
                    UsersLog.UsersOnline.Add(_User);
                }*/

                retorno = true;
            }
            else
            {
                _User.UserName = usuario;
                _User.Token = new Guid().ToString();
                _User.IsTokenValid = false;
                _User.IsUserAuthenticated = false;
            }
            return retorno;
        }

        public bool IsTokenValid(string userName, string token, DateTime timeRequest, ref TokenUser tokenUserResult) {
            bool retorno = false;
            //TokenUser usuarioToken = UsersLog.UsersOnline.Where(q => q.Token.Equals(token, StringComparison.InvariantCultureIgnoreCase)).Select(q => q).FirstOrDefault();
            TokenUser usuarioToken = null;
            if (usuarioToken != null)
            {               
                if (usuarioToken.IsUserAuthenticated)
                {
                    if (token.Equals(usuarioToken.Token, StringComparison.InvariantCultureIgnoreCase)==false)
                    {
                        usuarioToken.Message = $"El token solicitado no está registrado.";
                        usuarioToken.IsTokenValid = false;                        
                    }
                    else
                    {
                        if (timeRequest < usuarioToken.UntilDate)
                        {
                            usuarioToken.Message = "Token valido.";
                            usuarioToken.IsTokenValid = true;
                            retorno = true;
                        }
                        else
                        {
                            usuarioToken.Message = "El token ya expiró. Solicite uno nuevo";
                            usuarioToken.IsTokenValid = false;
                        }
                    }
                }
                else
                {
                    usuarioToken.Message = "El usuario no se ha autenticado.";                    
                }
            }
            tokenUserResult = usuarioToken;
            return retorno;
        }

        public TokenUser IsTokenUserValido(string userName, string token, DateTime timeRequest) {
            TokenUser tokenUserResult = null;
            IsTokenValid(userName, token, timeRequest, ref tokenUserResult);
            return tokenUserResult;
        }
    }
}