using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Unam.Cohu.Libreria.WinForm.Model;
using WinFormScriptorum.ADO;

namespace Unam.Cohu.Libreria.WinForm
{
    public partial class FormBGRMD : Form
    {
        private string _CurrentPathFolder = String.Empty;
        private bool _CambioRuta = false;
        private string _CnnString = String.Empty;
        SqlConnectionStringBuilder _CnnBuilder = null;


        private List<Archivos> _Titulos = null;
        private TitulosLibreriaDb _LibreriaBD = null;

        public FormBGRMD()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            _CnnBuilder = new SqlConnectionStringBuilder(Configuracion.ObtenerCnnString("DbConnection"));
            _LibreriaBD = new TitulosLibreriaDb("DbConnection");

            this.TextBoxDataSource.Text = _CnnBuilder.DataSource;
            this.TextBoxBD.Text = _CnnBuilder.InitialCatalog;
            this.TextBoxUser.Text = _CnnBuilder.UserID;
            this.TextBoxPassword.Text = _CnnBuilder.Password;
            this._CnnString = _CnnBuilder.ConnectionString; ;
            this.TextBoxRutaPDF.Text = Properties.Settings.Default.url;

            DataGridConvertidos.AutoGenerateColumns = false;
            DataGridProcesados.AutoGenerateColumns = false;
            DataGridConvertidos.ReadOnly = true;

            EstatusApp("Seleccione una Carpeta", false);

            HabilitarEdicion(CheckEditarCnnStr.Checked);
        }

        private void ButtonAbrir_Click(object sender, EventArgs e)
        {
            
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _CambioRuta = true;
                LimpiarControles();
                this.TextBoxPath.Text = this.folderBrowserDialog1.SelectedPath;
                if (!this._CurrentPathFolder.Equals(this.TextBoxPath.Text))
                {
                    this._CurrentPathFolder = this.TextBoxPath.Text;
                }
                System.Threading.Tasks.Task.Run(new Action(() => {
                    EstatusApp("Revisando carpeta", true);
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() => {
                            UpdateGridArchivosConvertidos(this.TextBoxPath.Text);
                            BloquearBotonGenerarLista(false);
                        }));
                    }
                    else
                    {
                        UpdateGridArchivosConvertidos(this.TextBoxPath.Text);
                        BloquearBotonGenerarLista(false);
                    }
                    EstatusApp("Archivos analizados", false);
                }));

            }
            else
            {
                LabelPath.Text = "Selecione una carpeta";
                EstatusApp("Seleccione una Carpeta", false);
                BloquearBotonGenerarLista(true);
            }
        }

        private void ButtonGenerarLista_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TextBoxRutaPDF.Text)){
                MostrarMensaje(this, "Escriba la URL para la ruta de los PDFs", "Url PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }            
            try
            {
                Uri c = new Uri(this.TextBoxRutaPDF.Text, UriKind.Absolute);
            }
            catch (Exception)
            {
                MostrarMensaje(this, "Escriba una URL válida para la ruta de los PDFs", "Url PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Properties.Settings.Default.ServerInstance = this.TextBoxDataSource.Text;
            Properties.Settings.Default.bd = this.TextBoxBD.Text;
            Properties.Settings.Default.user = this.TextBoxUser.Text;
            Properties.Settings.Default.pass = this.TextBoxPassword.Text;
            Properties.Settings.Default.url = this.TextBoxRutaPDF.Text;
            Properties.Settings.Default.Save();

            System.Threading.Tasks.Task.Run(new Action(() => {
                EstatusApp("Generando Lista...", true);
                BloquearBotonGenerarLista(true);
                BloquearBotonBd(true);
                BloquearBotonQuery(true);
                List<Archivos> archivos = DataGridConvertidos.DataSource as List<Archivos>;
                if (archivos != null)
                {
                    List<Archivos> archivosProcesar = archivos.Where(s => s.IsArchivoConvertido == true).Select(s => s).ToList();
                    string mensaje = string.Empty;
                    if (archivosProcesar.Count > 0)
                    {
                        if (_LibreriaBD.PingServer(ref mensaje))
                        {
                            _CambioRuta = false;
                            //List<TituloGuardado> listado = this.GenerarListaBd(archivosProcesar);
                            List<TituloPdf> listado = this.GenerarListaPdf(archivosProcesar);
                            
                            UpdateGridProcesadosInvoke(listado);
                            BloquearBotonGenerarLista(false);
                            BloquearBotonBd(false);
                            BloquearBotonQuery(false);
                            EstatusApp("Lista Generada", false);
                        }
                        else
                        {
                            string msg = string.Format("No existe conexión con el servidor: {0}", mensaje);
                            MostrarMensaje(this, msg, "Sin conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            EstatusApp("Sin conexión", false);
                            BloquearBotonGenerarLista(false);
                        }
                    }
                    else
                    {
                        MostrarMensaje(this, "Primero carge los archivos a procesar", "Sin archivos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        EstatusApp("Sin archivos", false);
                        BloquearBotonGenerarLista(false);
                    }                    
                }
                else
                {
                    MostrarMensaje(this, "No se pudo obtener la lista de archivos a procesar", "Sin archivos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EstatusApp("Sin archivos", false);
                    BloquearBotonGenerarLista(false);
                }


               

            }));
        }



        private void ButtonActualizar_Click(object sender, EventArgs e)
        {
            List<TituloPdf> data = DataGridProcesados.DataSource as List<TituloPdf>;            
            if (data == null)
            {
                EstatusApp("Primero carge los archivos, y luego genere la lista a procesar", false);
                BloquearBotonGenerarLista(false);
                BloquearBotonBd(false);
                BloquearBotonQuery(false);
                MostrarMensaje(this, "Primero carge los archivos, y luego genere la lista a procesar", "Sin titulos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            if (data.Where(s => s.IsSeleccionado).Count()<=0)
            {
                EstatusApp("No hay titulos seleccionados a procesar", false);
                BloquearBotonGenerarLista(false);
                BloquearBotonBd(false);
                BloquearBotonQuery(false);
                MostrarMensaje(this, "No hay titulos seleccionados a procesar", "Sin titulos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MostrarMensaje(this, "Con la información actual se copiaran los archivos y se actualizarán los datos en la BD. ¿Desea continuar?", "Actualizar", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
            {
                EstatusApp("Creado archivos y actualizando en BD...", false);
                BloquearBotonGenerarLista(false);
                BloquearBotonBd(false);
                BloquearBotonQuery(false);

                return;
            }

            System.Threading.Tasks.Task.Run(new Action(() => {
                EstatusApp("Actualizando en BD...", true);
                BloquearBotonGenerarLista(true);
                BloquearBotonBd(true);
                BloquearBotonQuery(true);

                List<TituloPdf> archivos = DataGridProcesados.DataSource as List<TituloPdf>;
                int totalLista = archivos.Count;                
                int totalArchivos = 0;
                int totalSql = 0;

                string mensaje = string.Empty;
                if (_LibreriaBD.PingServer(ref mensaje))
                {
                    // TO-DO:
                    string mensajeArchivos = string.Empty;
                    string subFolder = Configuracion.ObtenerRutaArchivos("PathPostedFiles");

                    List<TituloPdf> procesarArchivos = archivos.Where(s => s.IsSeleccionado & s.IsArchivoCopiado == false).ToList();
                    totalArchivos = procesarArchivos.Count();

                    bool isArchivosGenerados = false;

                    if (totalArchivos > 0)
                    {
                        isArchivosGenerados = GenerarArchivos(this.TextBoxPath.Text, subFolder.Substring(1, subFolder.Length - 1).Replace("/", @"\"), this._CambioRuta, ref procesarArchivos, ref mensajeArchivos);
                    }
                    else
                    {
                        isArchivosGenerados = true;
                    }

                    if (isArchivosGenerados)
                    {
                        
                        foreach (var item in archivos.Where(s => ( s.IsArchivoCopiado == true & s.IsEjecutadoSQL == false) & s.IsSeleccionado).Select(s => s))
                        {
                            int rowsAffected = _LibreriaBD.Update((Titulo)item, null);
                            if (rowsAffected > 0)
                            {
                                item.IsEjecutadoSQL = true;
                                item.MensajeSQL = "Titulo actualizado en la BD";
                            }
                            else
                            {
                                item.MensajeSQL = "No se actualizó el titulo en la BD";
                            }
                        }                                                

                        int procesados = archivos.Where(s => s.IsSeleccionado).Count();
                        EstatusApp(String.Format("Se procesaron ({0}) titulos de ({1}).", procesados, totalLista), false);
                        BloquearBotonGenerarLista(false);
                        BloquearBotonBd(false);
                        BloquearBotonQuery(false);
                        MostrarMensaje(this, String.Format("Se procesaron ({0}) titulos de ({1}).", procesados, totalLista), "Generacion Archivos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        EstatusApp(mensajeArchivos, false);
                        BloquearBotonGenerarLista(false);
                        BloquearBotonBd(false);
                        BloquearBotonQuery(false);
                        MostrarMensaje(this, mensajeArchivos, "Generacion Archivos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    UpdateGridProcesadosInvoke(archivos);                 
                }
                else
                {
                    EstatusApp("No existe conexión con el servidor", false);
                    BloquearBotonGenerarLista(false);
                    BloquearBotonBd(false);
                    BloquearBotonQuery(false);
                    MostrarMensaje(this, "No existe conexión con el servidor", "Sin conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }));
        }

        private void ButtonGenerarQuery_Click(object sender, EventArgs e)
        {
            List<TituloPdf> data = DataGridProcesados.DataSource as List<TituloPdf>;
            if (data == null)
            {
                EstatusApp("Primero carge los archivos, y luego genere la lista a procesar", false);
                BloquearBotonGenerarLista(false);
                BloquearBotonBd(false);
                BloquearBotonQuery(false);
                MostrarMensaje(this, "Primero carge los archivos, y luego genere la lista a procesar", "Sin titulos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (data.Where(s => s.IsSeleccionado).Count() <= 0)
            {
                EstatusApp("No hay titulos seleccionados a procesar", false);
                BloquearBotonGenerarLista(false);
                BloquearBotonBd(false);
                BloquearBotonQuery(false);
                MostrarMensaje(this, "No hay titulos seleccionados a procesar", "Sin titulos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (data.Where(s => s.IsArchivoCopiado == false).Count() > 0)
            {
                if (MostrarMensaje(this, "Con la información actual se crearan los archivos y un archivo *.txt. ¿Desea continuar?", "Generar Archivos", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                {
                    EstatusApp("Exportación cancelada", false);
                    BloquearBotonGenerarLista(false);
                    BloquearBotonBd(false);
                    BloquearBotonQuery(false);
                    return;
                }
            }
            else
            {
                if (MostrarMensaje(this, "Se generará un archivo *.txt con la información actual. ¿Desea continuar?", "Generar Archivos", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                {
                    EstatusApp("Exportación cancelada", false);
                    BloquearBotonGenerarLista(false);
                    BloquearBotonBd(false);
                    BloquearBotonQuery(false);
                    return;
                }
            }

            System.Threading.Tasks.Task.Run(new Action(() => {
                EstatusApp("Exportando datos", true);
                BloquearBotonGenerarLista(true);
                BloquearBotonBd(true);
                BloquearBotonQuery(true);

                List<TituloPdf> archivos = DataGridProcesados.DataSource as List<TituloPdf>;
                // TO-DO:

                string mensajeArchivos = string.Empty;
                string subFolder = Configuracion.ObtenerRutaArchivos("PathPostedFiles");
                List<TituloPdf> procesarArchivos = archivos.Where(s => s.IsArchivoCopiado == false & s.IsSeleccionado ).ToList();
                List<TituloPdf> noProcesarArchivos = archivos.Where(s => s.IsArchivoCopiado == true & s.IsSeleccionado).ToList();

                int totalTitulos = archivos.Count;
                string mensajeExportacion = string.Empty;
                List<string> queryresultados = new List<string>();

                if (procesarArchivos.Count() > 0)
                {
                    bool hasArchivosGenerados = GenerarArchivos(this.TextBoxPath.Text, subFolder.Substring(1, subFolder.Length - 1).Replace("/", @"\"), this._CambioRuta, ref procesarArchivos, ref mensajeArchivos);
                    int totalProcesados = procesarArchivos.Where(s => s.IsArchivoCopiado).Count() + noProcesarArchivos.Count();
                    UpdateGridProcesadosInvoke(archivos);

                    if (hasArchivosGenerados)
                    {
                        MostrarMensaje(this, String.Format("Se procesaron ({0}) archivos de ({1}) titulos .", totalProcesados, totalTitulos), "Generacion Archivos", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (ExportarDatosCSV(archivos.Where(s => s.IsArchivoCopiado & s.IsSeleccionado).ToList(), this.TextBoxPath.Text, ref mensajeExportacion, ref queryresultados))
                        {
                            EstatusApp(String.Format("Se procesaron ({0}) archivos de ({1}) titulos .{2}", totalProcesados, totalTitulos, mensajeExportacion), false);
                            BloquearBotonGenerarLista(false);
                            BloquearBotonBd(false);
                            BloquearBotonQuery(false);

                            MostrarMensaje(this, mensajeExportacion, "Exportacion resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MostrarFormResultados(this, queryresultados);
                        }
                        else
                        {
                            EstatusApp(mensajeExportacion, false);
                            BloquearBotonGenerarLista(false);
                            BloquearBotonBd(false);
                            BloquearBotonQuery(false);

                            MostrarMensaje(this, mensajeExportacion, "Exportacion resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        EstatusApp(mensajeArchivos, false);
                        BloquearBotonGenerarLista(false);
                        BloquearBotonBd(false);
                        BloquearBotonQuery(false);
                        MostrarMensaje(this, mensajeArchivos, "Generacion Archivos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    if (ExportarDatosCSV(archivos.Where(s => s.IsArchivoCopiado & s.IsSeleccionado).ToList(), this.TextBoxPath.Text, ref mensajeExportacion, ref queryresultados))
                    {
                        EstatusApp(mensajeExportacion, false);
                        BloquearBotonGenerarLista(false);
                        BloquearBotonBd(false);
                        BloquearBotonQuery(false);
                        MostrarMensaje(this, mensajeExportacion, "Exportacion resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MostrarFormResultados(this, queryresultados);
                    }
                    else
                    {
                        EstatusApp(mensajeExportacion, false);
                        BloquearBotonGenerarLista(false);
                        BloquearBotonBd(false);
                        BloquearBotonQuery(false);
                        MostrarMensaje(this, mensajeExportacion, "Exportacion resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }));
        }


        private void UpdateGridArchivosConvertidos(string path)
        {
            DataGridConvertidos.Columns.Clear();
            DataGridConvertidos.Columns.Add(new DataGridViewCheckBoxColumn() { HeaderText = "", DataPropertyName = "IsArchivoConvertido", ReadOnly = true, Width = 50 });
            DataGridConvertidos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Archivo", DataPropertyName = "NombreArchivo", ReadOnly = true, Width = 250 });
            DataGridConvertidos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Extension", DataPropertyName = "ExtensionArchivo", ReadOnly = true });
            DataGridConvertidos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Mensaje", DataPropertyName = "MensajeArchivo", ReadOnly = true, Width = 200 });
            DataGridConvertidos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Id", DataPropertyName = "IdTitulo", ReadOnly = true });
            DataGridConvertidos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Colección", DataPropertyName = "Coleccion", ReadOnly = true });
            DataGridConvertidos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Título", DataPropertyName = "DescripcionTitulo", ReadOnly = true, Width = 250 });            
            DataGridConvertidos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Tipo", DataPropertyName = "TipoTitulo", ReadOnly = true });
            DataGridConvertidos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Edicion", DataPropertyName = "Edicion", ReadOnly = true });
            this.DataGridConvertidos.DataSource = GenerarListado(path);
        }

        private void UpdateGridProcesadosInvoke(List<TituloPdf> items)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => {
                    UpdateGridProcesados(items);
                }));
            }
            else
            {
                UpdateGridProcesados(items);
            }
        }

        private void UpdateGridProcesados(List<TituloPdf> items)
        {

            DataGridProcesados.Columns.Clear();
            // DataPropertyName = "IsProcesado",
            DataGridViewCheckBoxColumn chkbox = new DataGridViewCheckBoxColumn() { DataPropertyName = "IsProcesado", Width = 30, MinimumWidth = 30};            
            DatagridViewCheckBoxHeaderCell chkHeader = new DatagridViewCheckBoxHeaderCell();
            chkbox.HeaderCell = chkHeader;
            chkHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(chkHeader_OnCheckBoxClicked);
            DataGridProcesados.Columns.Add(chkbox);

            //DataGridProcesados.Columns.Add(new DataGridViewCheckBoxColumn() { DataPropertyName = "IsProcesado", Width = 30, MinimumWidth = 30 });                        
            DataGridProcesados.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Id", DataPropertyName = "IdTitulo", ReadOnly = true, Width = 45, MinimumWidth = 45 });
            DataGridProcesados.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Nombre ", DataPropertyName = "NombreArchivo", ReadOnly = true });
            DataGridProcesados.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Título", DataPropertyName = "DescripcionTitulo", ReadOnly = true });
            DataGridProcesados.Columns.Add(new DataGridViewCheckBoxColumn() { HeaderText = "File?", DataPropertyName = "IsArchivoCopiado", ReadOnly = true, Width = 45 });            
            DataGridProcesados.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Mensaje Archivo", DataPropertyName = "MensajeCopia", ReadOnly = true });
            DataGridProcesados.Columns.Add(new DataGridViewCheckBoxColumn() { HeaderText = "SQL?", DataPropertyName = "IsEjecutadoSQL", ReadOnly = true, Width = 45 });
            DataGridProcesados.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Mensaje SQL", DataPropertyName = "MensajeSQL", ReadOnly = true });
            DataGridProcesados.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "PDF anterior", DataPropertyName = "UrlPdfAnterior", ReadOnly = true });
            DataGridProcesados.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "PDF nuevo", DataPropertyName = "UrlPdf", ReadOnly = true });                                    
            DataGridProcesados.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Edicion", DataPropertyName = "Edicion", ReadOnly = true });
            DataGridProcesados.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Query", DataPropertyName = "Query", ReadOnly = true });
            DataGridProcesados.CellClick -= DataGridProcesados_CellClick;
            DataGridProcesados.CellClick += DataGridProcesados_CellClick;
            this.DataGridProcesados.DataSource = items;

            foreach (DataGridViewRow item in DataGridProcesados.Rows)
            {
                var row = item.DataBoundItem as TituloPdf;
                if (row.IsSeleccionado)
                {
                    item.Cells[0].Value = row.IsSeleccionado;
                }
            }
        }
        
        private void DataGridProcesados_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0 & e.RowIndex >= 0)
            {
                DataGridProcesados.BeginEdit(false);
                var row = DataGridProcesados.Rows[e.RowIndex];
                TituloPdf item = row.DataBoundItem as TituloPdf;
                item.IsSeleccionado = !item.IsSeleccionado;
                row.Cells[0].Value = item.IsSeleccionado;
                //var columna = DataGridProcesados.Columns[0];
                //foreach (DataGridViewRow row in DataGridProcesados.Rows)
                //{
                //    TituloPdf item = row.DataBoundItem as TituloPdf;
                //    item.IsProcesado = _Selected;
                //    row.Cells[0].Value = _Selected;
                //    row.Selected = false;
                //}
                DataGridProcesados.EndEdit();
                DataGridProcesados.Update();
            }
        }


        void chkHeader_OnCheckBoxClicked(bool state)
        {
            DataGridProcesados.BeginEdit(false);
            foreach (DataGridViewRow row in DataGridProcesados.Rows)
            {
                TituloPdf item = row.DataBoundItem as TituloPdf;
                item.IsSeleccionado = state;
                row.Cells[0].Value = state;
                row.Selected = false;
            }
            //DataGridProcesados.DataSource = titulos;
            DataGridProcesados.EndEdit();
        }

        private List<Archivos> GenerarListado(string path)
        {
            List<Archivos> lista = new List<Archivos>();
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo directorio = new DirectoryInfo(path);
                    FileInfo[] files = directorio.GetFiles();
                    if (files.Length > 0)
                    {
                        lista = new List<Archivos>();
                        int intDummy = 0;
                        foreach (FileInfo item in files)
                        {
                            string[] datos = item.Name.Split(new char[] { '$' });
                            Archivos file = new Archivos();
                            file.NombreArchivo = item.Name;
                            file.ExtensionArchivo = item.Extension;
                            file.RutaArchivo = item.FullName;

                            if (datos.Length == 5)
                            {
                                try
                                {
                                    file.Coleccion = datos[0];
                                    file.DescripcionTitulo = (string.IsNullOrEmpty(datos[1]) ? "" : datos[1]).Length > 80 ? datos[1].Substring(0,79) +"_" : datos[1];
                                    file.TipoTitulo = datos[2];
                                    intDummy = 0;
                                    Int32.TryParse(datos[3], out intDummy);
                                    file.Edicion = intDummy;
                                    intDummy = 0;
                                    Int32.TryParse(datos[4].Replace(file.ExtensionArchivo, "").Replace("TIT_",""), out intDummy);
                                    file.IdTitulo = intDummy;
                                    if (intDummy > 0)
                                    {
                                        if (lista.Where(s => s.IdTitulo == file.IdTitulo).Count() > 0)
                                        {
                                            file.IsArchivoConvertido = false;
                                            file.MensajeArchivo = "El id del titulo ya se encuentra en la lista";
                                        }
                                        else
                                        {
                                            file.IsArchivoConvertido = true;
                                            file.MensajeArchivo = "OK";
                                        }

                                    }
                                    else
                                    {
                                        file.IsArchivoConvertido = false;
                                        file.MensajeArchivo = "El Id del Titulo no es válido";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    file.IsArchivoConvertido = false;
                                    file.MensajeArchivo = ex.Message;
                                }
                            }
                            else
                            {
                                file.IsArchivoConvertido = false;
                                file.MensajeArchivo = "El nombre del archivo no es el correcto";
                            }
                            lista.Add(file);
                        }

                        LabelPath.Text = string.Format(" Se encontraron ({0}) archivos. Se seleccionaron ({1}) archivos ", lista.Count, lista.Where(s => s.IsArchivoConvertido == true).Count());
                    }
                    else
                    {
                        LabelPath.Text = "La carpeta no contiene archivos a analizar";
                    }
                }
                else
                {
                    LabelPath.Text = "La carpeta no existe o se ha eliminado o movido";
                }
            }
            catch (Exception)
            {
                LabelPath.Text = "No se generó la lista correctamente";
            }
            return lista;
        }

        private void HabilitarEdicion(bool isChecked)
        {
            this.TextBoxDataSource.Enabled = !isChecked;
            this.TextBoxBD.Enabled = !isChecked;
            this.TextBoxUser.Enabled = !isChecked;
            this.TextBoxPassword.Enabled = !isChecked;
            this.TextBoxRutaPDF.Enabled = !isChecked;
        }

        private void CheckEditarCnnStr_Click(object sender, EventArgs e)
        {
            HabilitarEdicion(CheckEditarCnnStr.Checked);
        }

        private DialogResult MostrarMensaje(object owner, string mensaje, string titulo, MessageBoxButtons buttons, MessageBoxIcon icono)
        {
            if (this.InvokeRequired)
            {
                return (DialogResult)this.Invoke(new Func<DialogResult>(() => {
                    return MessageBox.Show(this, mensaje, titulo, buttons, icono);
                }));
            }
            else
            {
                return MessageBox.Show(this, mensaje, titulo, buttons, icono);
            }
        }

        

        private List<TituloPdf> GenerarListaPdf(List<Archivos> listaProcesar)
        {
            List<TituloPdf> lista = new List<TituloPdf>();
            if (listaProcesar != null)
            {
                foreach (var item in listaProcesar)
                {
                    TituloPdf titulo = new TituloPdf();
                    titulo.IdTitulo = item.IdTitulo;
                    titulo.Coleccion = item.Coleccion;
                    titulo.ExtensionArchivo = item.ExtensionArchivo;
                    titulo.NombreArchivo = item.NombreArchivo;
                    titulo.RutaArchivo = item.RutaArchivo;
                    titulo.NombrePdf = item.DescripcionTitulo;
                    //titulo.UrlPdf = HttpUtility.UrlPathEncode(string.Format("{0}{1}{2}", this.TextBoxRutaPDF.Text, item.DescripcionTitulo, item.ExtensionArchivo));
                    titulo.UrlPdf = HttpUtility.UrlPathEncode(string.Format("{0}{1}", this.TextBoxRutaPDF.Text, item.NombreArchivo));

                    Titulo dato = _LibreriaBD.SelectBy(new int?(item.IdTitulo), null, null).FirstOrDefault();
                    if (dato != null)
                    {
                        titulo.DescripcionTitulo = dato.DescripcionTitulo;
                        titulo.UrlPdfAnterior = dato.UrlPdf;                        
                        titulo.Edicion = dato.Edicion;
                        titulo.IsInBD = true;
                    }
                    else {
                        titulo.MensajeSQL = "El titulo no existe en la BD";
                        titulo.IsInBD = false;
                    }
                    
                    titulo.Query = _LibreriaBD.GetQueryUpdate(titulo);
                    lista.Add(titulo);
                }
            }
            return lista;
        }

        private bool GenerarArchivos(string pathFolder, string pathSubFolder, bool hasNewPath, ref List<TituloPdf> lista, ref string mensaje)
        {
            bool retorno = false;
            string mensajeCrearcionFolder = String.Empty;
            if (Directory.Exists(pathFolder))
            {
                DirectoryInfo folder = new DirectoryInfo(pathFolder);
                DirectoryInfo subfolder = null;
                try
                {

                    if (Directory.Exists(string.Format(@"{0}\{1}", pathFolder, pathSubFolder)))
                    {
                        subfolder = new DirectoryInfo(string.Format(@"{0}\{1}", pathFolder, pathSubFolder)); //folder.CreateSubdirectory(pathSubFolder + DateTime.Now.ToString("-yyyymmdd-hhMMss"));
                        try
                        {
                            //FileInfo[] files = subfolder.GetFiles();
                            //for (int i = 0; i < files.Length; i++)
                            //{
                            //    // Borrar solamente aquellos que no esten en la lista con un guid ya generado
                            //    if (lista != null)
                            //    {
                            //        var conteo = lista.Where(s => files[i].Name.Contains(s.NombrePdf.ToString())).Count();
                            //        if (conteo <= 0)
                            //        {
                            //            files[i].Delete();
                            //        }
                            //    }
                            //}
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        subfolder = folder.CreateSubdirectory(pathSubFolder);
                    }
                }
                catch (Exception ex)
                {
                    mensajeCrearcionFolder = ex.Message;
                    subfolder = null;
                }



                if (subfolder != null)
                {
                    int errores = 0;
                    string rutaAnterior = string.Empty;
                    string rutaNueva = string.Empty;
                    foreach (var item in lista)
                    {
                        rutaAnterior = string.Format(@"{0}\{1}", folder.FullName, item.NombreArchivo);
                        //rutaNueva = string.Format(@"{0}\{1}{2}", subfolder.FullName, item.NombrePdf, item.ExtensionArchivo);
                        rutaNueva = string.Format(@"{0}\{1}", subfolder.FullName, item.NombreArchivo);
                        if (!File.Exists(rutaNueva))
                        {
                            try
                            {
                                FileInfo fileAnterior = new FileInfo(rutaAnterior);
                                fileAnterior.CopyTo(rutaNueva);
                                item.MensajeCopia = String.Format("Archivo copiado de ({0}) a ({1})", rutaAnterior, rutaNueva);
                                item.IsArchivoCopiado = true;
                            }
                            catch (Exception ex)
                            {
                                errores++;
                                item.MensajeCopia = String.Format("Error al copiar el archivo a ({0}). {1}", rutaNueva, ex.Message);
                                item.IsArchivoCopiado = false;
                            }
                        }
                        else
                        {
                            item.MensajeCopia = String.Format("El archivo '{0}' no fue copiado porque ya existía.", rutaNueva);
                            item.IsArchivoCopiado = true;
                        }
                    }
                    if (errores < 0)
                    {
                        retorno = true;
                        mensaje = "Archivos generados correctamente.";
                    }
                    else
                    {
                        retorno = true;
                        mensaje = "Archivos parcialmente generados. Revise el detalle de cada item";
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(mensajeCrearcionFolder))
                    {
                        mensaje = "No se puede crear el subdirectorio de imágenes en la carpeta seleccionada porque alguno de los archivos está siendo usado por otro proceso. ";
                    }
                    else
                    {
                        mensaje = mensajeCrearcionFolder;
                    }
                }


            }
            else
            {
                mensaje = "El directorio base no es válido. No se generaron los archivos ";
            }
            return retorno;

        }

        private string LimpiarNombreArchivo(string nombre)
        {
            string retorno = nombre;
            if (string.IsNullOrEmpty(nombre))
            {
                List<string> caracteresProhibidos = new List<string>() { "!", "\"", "#", "$", "%", "&", "/", "=", "?", "¡", "'", "¿", "!", "¨", "´", "*", "+", "~", "{", "}", "^", "`", ":", ";", "<", ">" };
                foreach (string item in caracteresProhibidos)
                {
                    retorno = retorno.Replace(item, "_");
                }

            }
            return retorno;
        }

        private void LimpiarControles()
        {
            this.DataGridConvertidos.Columns.Clear();
            this.DataGridConvertidos.DataSource = null;
            this.DataGridProcesados.Columns.Clear();
            this.DataGridProcesados.DataSource = null;
            this.BloquearBotonGenerarLista(true);
            this.BloquearBotonBd(true);
            this.BloquearBotonQuery(true);
            LabelPath.Text = string.Empty;
        }

        private bool ExportarDatosCSV(IEnumerable<TituloPdf> titulos, string basePath, ref string mensaje, ref List<string> querys)
        {
            bool exportacion = false;
            mensaje = string.Empty;
            if (titulos != null)
            {
                try
                {
                    // Query ; mensaje archivo; mensaje sql; IsProcesado
                    string lineaCSV = "\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"\n";
                    StringBuilder texto = new StringBuilder();
                    foreach (var item in titulos)
                    {
                        string linea = string.Format(lineaCSV, item.Query, item.NombreArchivo, item.IsEjecutadoSQL, item.MensajeSQL,item.IsArchivoCopiado, item.MensajeCopia);
                        querys.Add(string.Format("{0}{1}", item.Query, Environment.NewLine));
                        texto.Append(linea);
                    }
                    if (Directory.Exists(basePath))
                    {
                        FileInfo f = new FileInfo(string.Format("{0}/{1}-{2}.csv", basePath, "ExportQuerys", DateTime.Now.ToString("yyyyMMdd-HHmmss")));
                        using (FileStream fs = f.Create())
                        {
                            byte[] data = System.Text.Encoding.ASCII.GetBytes(texto.ToString());
                            fs.Write(data, 0, data.Length);
                            fs.Flush();
                            fs.Close();
                        }
                        mensaje = string.Format("Se generó el archivo '{0}' en el directorio seleccionado", f.Name);
                        exportacion = true;
                    }
                }
                catch (Exception ex)
                {
                    mensaje = string.Format("Error al generar el archivo:{0} {1}", Environment.NewLine, ex.Message);
                    exportacion = false;
                }

            }
            return exportacion;
        }

        private DialogResult MostrarFormResultados(object owner, List<string> resultados)
        {
            if (this.InvokeRequired)
            {
                return (DialogResult)this.Invoke(new Func<DialogResult>(() => {
                    FormResultadosQuery form = new FormResultadosQuery(resultados);
                    return form.ShowDialog(this);
                }));
            }
            else
            {
                FormResultadosQuery form = new FormResultadosQuery(resultados);
                return form.ShowDialog(this);
            }
        }

        private void EstatusApp(string mensajeLabelEstatus, bool showProgress)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => {
                    this.LabelEstatusStrip.Text = mensajeLabelEstatus;

                    if (showProgress)
                    {
                        this.pictureBox1.Show();
                        this.ButtonAbrir.Enabled = false;
                        this.CheckEditarCnnStr.Enabled = false;
                        this.TextBoxDataSource.Enabled = false;
                        this.TextBoxBD.Enabled = false;
                        this.TextBoxUser.Enabled = false;
                        this.TextBoxPassword.Enabled = false;
                        this.TextBoxRutaPDF.Enabled = false;                        
                    }
                    else
                    {
                        this.pictureBox1.Hide();
                        this.ButtonAbrir.Enabled = true;
                        this.CheckEditarCnnStr.Enabled = true;
                        if (!CheckEditarCnnStr.Checked)
                        {
                            this.TextBoxDataSource.Enabled = true;
                            this.TextBoxBD.Enabled = true;
                            this.TextBoxUser.Enabled = true;
                            this.TextBoxPassword.Enabled = true;
                            this.TextBoxRutaPDF.Enabled = true;
                        }
                    }

                }));
            }
            else
            {
                this.LabelEstatusStrip.Text = mensajeLabelEstatus;
                if (showProgress)
                {
                    this.pictureBox1.Show();
                    this.ButtonAbrir.Enabled = false;
                    this.CheckEditarCnnStr.Enabled = false;
                    this.TextBoxDataSource.Enabled = false;
                    this.TextBoxBD.Enabled = false;
                    this.TextBoxUser.Enabled = false;
                    this.TextBoxPassword.Enabled = false;
                    this.TextBoxRutaPDF.Enabled = false;
                }
                else
                {
                    this.pictureBox1.Hide();
                    this.ButtonAbrir.Enabled = true;
                    this.CheckEditarCnnStr.Enabled = true;
                    if (!CheckEditarCnnStr.Checked)
                    {
                        this.TextBoxDataSource.Enabled = true;
                        this.TextBoxBD.Enabled = true;
                        this.TextBoxUser.Enabled = true;
                        this.TextBoxPassword.Enabled = true;
                        this.TextBoxRutaPDF.Enabled = true;
                    }
                }
            }
        }

        private void BloquearBotonGenerarLista(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => {
                    this.ButtonGenerarLista.Enabled = !enabled;
                }));
            }
            else
            {
                this.ButtonGenerarLista.Enabled = !enabled;
            }

        }

        private void BloquearBotonBd(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => {
                    this.ButtonActualizar.Enabled = !enabled;
                }));
            }
            else
            {
                this.ButtonActualizar.Enabled = !enabled;
            }
        }

        private void BloquearBotonQuery(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => {
                    this.ButtonGenerarQuery.Enabled = !enabled;
                }));
            }
            else
            {
                this.ButtonGenerarQuery.Enabled = !enabled;
            }

        }

        private void TextBoxDataSource_TextChanged(object sender, EventArgs e)
        {
            _CnnBuilder.DataSource = this.TextBoxDataSource.Text;
            _LibreriaBD.UpdateCnnString(_CnnBuilder.ConnectionString);
        }

        private void TextBoxBD_TextChanged(object sender, EventArgs e)
        {
            _CnnBuilder.InitialCatalog = this.TextBoxBD.Text;
            _LibreriaBD.UpdateCnnString(_CnnBuilder.ConnectionString);
        }

        private void TextBoxPassword_TextChanged(object sender, EventArgs e)
        {
            _CnnBuilder.Password = this.TextBoxPassword.Text;
            _LibreriaBD.UpdateCnnString(_CnnBuilder.ConnectionString);
        }

        private void TextBoxUser_TextChanged(object sender, EventArgs e)
        {
            _CnnBuilder.UserID = this.TextBoxUser.Text;
            _LibreriaBD.UpdateCnnString(_CnnBuilder.ConnectionString);
        }

        /*
         private List<TituloGuardado> GenerarListaBd(List<Archivos> listaProcesar)
        {
            List<TituloGuardado> lista = new List<TituloGuardado>();
            if (listaProcesar != null)
            {
                Guid guidArchivo = Guid.Empty;
                bool existe = false;
                foreach (var item in listaProcesar)
                {
                    TituloGuardado titulo = new TituloGuardado();
                    do
                    {
                        guidArchivo = Guid.NewGuid();
                        List<Titulo> datos = _LibreriaBD.SelectBy(null, guidArchivo.ToString(), null);
                        existe = (datos != null ? datos.Count > 0 : false);
                        if (!existe)
                        {
                            existe = (lista.Where(s => s.GuidGenerado.ToString().Equals(guidArchivo.ToString())).Count() > 0);
                        }
                    } while (existe);
                    titulo.GuidGenerado = guidArchivo;
                    titulo.IdTitulo = item.IdTitulo;
                    titulo.Coleccion = item.Coleccion;
                    titulo.Extension = item.Extension;
                    titulo.ArchivoAnterior = item.NombreArchivo;
                    titulo.NombreArchivo = string.Format("{0}{1}", LimpiarNombreArchivo(item.DescripcionTitulo), item.Extension);
                    titulo.RutaArchivo = string.Format("{0}/{1}{2}", Configuracion.ObtenerRutaArchivos("PathPostedFiles"), guidArchivo, item.Extension);
                    titulo.TipoTitulo = item.TipoTitulo;
                    titulo.Edicion = item.Edicion;
                    titulo.DescripcionTitulo = item.DescripcionTitulo;
                    titulo.Query = _LibreriaBD.GetQueryUpdate(titulo);
                    lista.Add(titulo);
                }
            }
            return lista;
        }

         */
    }
}
