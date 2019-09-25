using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unam.CoHu.Libreria.ADO;
using Unam.CoHu.Libreria.ADO.Enumerations;
using Unam.CoHu.Libreria.Model;
using Unam.CoHu.Libreria.Model.Views;

namespace Unam.CoHu.Libreria.Controller
{
    public class ResponsableController
    {
        protected ResponsableDb responsableBd = null;        
        
        public ResponsableController() {
            responsableBd = new ResponsableDb();
        }

        public int ActualizarResponsable(Responsable param)
        {
            int rowsAffected = 0;
            try
            {
                if (param != null)
                {
                    rowsAffected = responsableBd.Update(param, null);
                    responsableBd.CloseConnection();
                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RegistrarResponsable(Responsable param )
        {
            int rowsAffected = 0;
            try
            {
                if (param != null)
                {
                    rowsAffected = responsableBd.Insert(param, null);
                    responsableBd.CloseConnection();
                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int BorrarResponsable(string idKey)
        {            
            try
            {
                int rowsAffected = responsableBd.Delete(idKey, null);
                responsableBd.CloseConnection();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public List<Responsable> CargarResponsables()
        {            
            try
            {
                List<Responsable> lista = responsableBd.SelectAll(null);
                responsableBd.CloseConnection();
                return ResultadosGroupBy(lista);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Responsable CargarResponsablePorId(string idResponsable )
        {
            Responsable responsable = null;
            try
            {
                List<Responsable> lista = responsableBd.SelectBy(idResponsable, null, null, null);
                responsableBd.CloseConnection();
                List<Responsable> listaAgrupada= ResultadosGroupBy(lista);
                if (listaAgrupada != null && listaAgrupada.Count > 0) {
                    responsable = listaAgrupada[0];
                }
                return responsable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Responsable> BusquedaResponsables(string busqueda, int? top)
        {
            List<Responsable> listRFC = null;
            List<Responsable> listNombres= null;
            List<Responsable> resultado = null;
            string rfc = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(busqueda))
                {
                    resultado = new List<Responsable>();
                    try
                    {
                        rfc = (busqueda.Length >= 11) ? busqueda.Substring(0, 10) : busqueda;
                        listRFC = this.CargarResponsablesPor(rfc, null, top);
                    }
                    catch (Exception)
                    {
                        listRFC = null;
                    }
                                        
                    listNombres = this.CargarResponsablesPor(null, busqueda, top);
                    if (listNombres != null)
                        resultado.AddRange(listNombres);
                    if (listRFC != null)
                        resultado.AddRange(listRFC);                    
                    return ResultadosGroupBy(resultado);
                }
                else {
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Responsable> CargarResponsablesPor(string rfc, string nombre, int? top)
        {
            try
            {
                List<Responsable> lista = responsableBd.SelectBy(null, rfc, nombre, top);
                responsableBd.CloseConnection();
                return ResultadosGroupBy(lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Responsable> ResultadosGroupBy(List<Responsable> lista)
        {
            List<Responsable> listaReturn = new List<Responsable>();
            if (lista != null)
            {
                if (lista != null)
                {
                    var query = from q in lista
                                group q by new
                                {
                                    q.IdResponsable,
                                    q.Rfc,
                                    q.Nombre,
                                    q.ApellidoPaterno,
                                    q.ApellidoMaterno,
                                    q.Descripcion
                                }
                               into g
                                select new Responsable()
                                {
                                    IdResponsable = g.Key.IdResponsable,
                                    Rfc = g.Key.Rfc,
                                    Nombre = g.Key.Nombre,
                                    ApellidoPaterno = g.Key.ApellidoPaterno,
                                    ApellidoMaterno = g.Key.ApellidoMaterno,
                                    Descripcion = g.Key.Descripcion                                    
                                };
                    listaReturn = query.ToList();
                }
            }

            return listaReturn;
        }


       
    }
}
