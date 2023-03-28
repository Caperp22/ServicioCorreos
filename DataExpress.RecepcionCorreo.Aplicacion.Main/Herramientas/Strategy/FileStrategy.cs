using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using Microsoft.Office.Interop.Outlook;
using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy
{
    public class FileStrategy : IFileStrategy
    {
        private List<DocumentosDto> _lstMail = new List<DocumentosDto>();
        private string _directorio;
        private string _directorioZip;
        private string _directorioErrores;
        //private readonly ConfiguracionDto _optionsConfig;
        private ConfiguracionDto _configuracionDto;
        public List<DocumentosDto> lstMail { get { return _lstMail; } set { _lstMail = value; } }

        public FileStrategy(ConfiguracionDto configuracionDto )
        {
            _configuracionDto = configuracionDto;
        }

        public void LeerArchivos()
        {
            try
            {
                Directorios();
                ExtraerZip();
                Console.WriteLine("FIN: Extraer Zip. " + DateTime.Now.TimeOfDay.ToString());
                ObtenerArchivos();
                Console.WriteLine("FIN: Extraer Procesar XML,PDF. " + DateTime.Now.TimeOfDay.ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ERROR " + ex.Message);
            }
        }
 
        private void ObtenerArchivos()
        {
            var files = Directory.GetFiles(_directorio);
            var contador = 0;
            var contador2 = 0;

            foreach (var file in files)
            {
                var infoFile = new FileInfo(file);

                try
                {
                    if (Regex.IsMatch(infoFile.Extension.ToLower(), "pdf|xml"))
                    {
                        var attach = File.ReadAllBytes(infoFile.FullName);
                        var name2 = infoFile.Name.Replace(infoFile.Extension, "");
                        name2 = name2.Substring(2);

                        var existe = _lstMail.Where(x => x.nombre2 == name2).ToList();

                        contador2 = existe.Count > 0 ? existe.Select(s => s.id).FirstOrDefault() : contador++;

                        var documento = new DocumentosDto
                        {
                            id = contador2,
                            documentos = attach,
                            nombreDocumento = infoFile.Name,
                            nombre2 = name2
                        };
                        _lstMail.Add(documento);

                        //                      File.Delete(file);
                        Console.WriteLine("Archivo Procesado " + infoFile.Name);
                        Util.Log("Archivo", $"Archivo Valido,,,,{infoFile.Name}");

                    }
                    else
                    {
                        Util.Log("Archivo", $"Archivo invalido,,,,{infoFile.Name}");
                    }
                }
                catch (System.Exception ex)
                {
                    Util.Log("Archivo", $"Error de Lectura,,,,{infoFile.Name}");
                    Console.WriteLine("Error de Lectura " + infoFile.Name);
                }
            }

        }

        private void Directorios()
        {
            var subcarpeta = DateTime.Now.ToString("yyyy/MM/").Replace(@"/", @"\");
            _directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recepcion", subcarpeta);
            //_directorioZip = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Recepcion\Zip", subcarpeta);
            _directorioZip = _configuracionDto.RutaZip + subcarpeta;
            _directorioErrores = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Error\Zip\");
            if (!Directory.Exists(_directorio))
                Directory.CreateDirectory(_directorio);

            if (!Directory.Exists(_directorioZip))
                Directory.CreateDirectory(_directorioZip);

            if (!Directory.Exists(_directorioErrores))
                Directory.CreateDirectory(_directorioErrores);

        }

        private void ExtraerZip()
        {
            try
            {
                //var files = Directory.GetFiles(_optionsConfig.RutaZip + @"Zip\");
                var files = Directory.GetFiles( _directorioZip);

                Util.Log("Zip", $"Asunto,Nombre,Email,Fecha Recibido,archivo");
                var lstTemp = _lstMail;
                _lstMail = new List<DocumentosDto>();
                foreach (var file in files)
                {
                    bool existe = false;
                    var infoFile = new FileInfo(file);
                    var mail = lstTemp.Find(p => p.nombreZip == infoFile.Name);
                    if (mail == null)
                        mail = new DocumentosDto();
                    ZipArchive zips = null;
                    try
                    {
                        zips = ZipFile.OpenRead(file);
                        var nombresArchivos = zips.Entries
                            .Where(p => p.FullName.ToLower().EndsWith(".xml")
                            || p.FullName.ToLower().EndsWith(".pdf"))
                            .Select(p => p.FullName).ToList();



                        var NombrePDF = "";
                        var NombreXML = "";
                        var NombrePDF2 = "";
                        var NombreXML2 = "";

                        if (nombresArchivos[0].Contains(".pdf"))
                        {
                            NombrePDF = nombresArchivos[0];
                            NombreXML = nombresArchivos[1];
                            NombrePDF2 = NombrePDF.Substring(2);
                            NombreXML2 = NombreXML.Substring(2);


                            string extension = Path.GetExtension(NombrePDF2);
                            NombrePDF2 = NombrePDF2.Substring(0, NombrePDF2.Length - extension.Length);

                            string extension2 = Path.GetExtension(NombreXML2);
                            NombreXML2 = NombreXML2.Substring(0, NombreXML2.Length - extension.Length);

                            if (NombrePDF2 != NombreXML2)
                            {
                                nombresArchivos[0] = "fv" + NombreXML2 + ".pdf";
                                foreach (var nombre in nombresArchivos)
                                    if (File.Exists(_directorio + @"\" + nombre))
                                        existe = true;
                                if (!existe && nombresArchivos.Count > 0)
                                    zips.ExtractToDirectory(_directorio, true);
                                File.Move(_directorio + "\\" + NombrePDF, _directorio + "\\" + nombresArchivos[0]);
                                zips.Dispose();
                            }
                            else
                            {
                                foreach (var nombre in nombresArchivos)
                                    if (File.Exists(_directorio + @"\" + nombre))
                                        existe = true;
                                if (!existe && nombresArchivos.Count > 0)
                                    zips.ExtractToDirectory(_directorio, true);
                                zips.Dispose();
                            }
                        }
                        else
                        {
                            NombrePDF = nombresArchivos[1];
                            NombreXML = nombresArchivos[0];
                            NombrePDF2 = NombrePDF.Substring(2);
                            NombreXML2 = NombreXML.Substring(2);

                            string extension = Path.GetExtension(NombrePDF2);
                            NombrePDF2 = NombrePDF2.Substring(0, NombrePDF2.Length - extension.Length);

                            string extension2 = Path.GetExtension(NombreXML2);
                            NombreXML2 = NombreXML2.Substring(0, NombreXML2.Length - extension.Length);

                            if (NombrePDF2 != NombreXML2)
                            {
                                nombresArchivos[1] = "fv" + NombreXML2 + ".pdf";

                                foreach (var nombre in nombresArchivos)
                                    if (File.Exists(_directorio + @"\" + nombre))
                                        existe = true;
                                if (!existe && nombresArchivos.Count > 0)
                                    zips.ExtractToDirectory(_directorio, true);
                                File.Move(_directorio + "\\" + NombrePDF, _directorio + "\\" + nombresArchivos[1]);
                                zips.Dispose();
                            }
                            else
                            {
                                foreach (var nombre in nombresArchivos)
                                    if (File.Exists(_directorio + @"\" + nombre))
                                        existe = true;
                                if (!existe && nombresArchivos.Count > 0)
                                    zips.ExtractToDirectory(_directorio, true);
                                zips.Dispose();
                            }
                        }


                        //ZipFile.ExtractToDirectory(file, _directorio);
                        foreach (var nombre in nombresArchivos.ToList())
                        {
                            mail = new DocumentosDto();
                            mail.nombreDocumento = nombre;
                            mail.estatus = "Descompresion";
                            // _lstMail.Add(mail);
                        }

                        Util.Log("Zip", $"'','','','',{infoFile.Name}");
                        Console.WriteLine("Zip Procesado " + infoFile.Name);
                    }
                    catch (System.Exception ex)
                    {
                        mail.estatus = "Error Descompresion";
                        // _lstMail.Add(mail);
                        Util.Log("Zip", $"Error Descompresion,,,,{infoFile.Name}");
                        Console.WriteLine("Error al procesar" + infoFile.Name);
                    }
                    finally
                    {
                        zips.Dispose();
                        // File.Delete(file);
                        //File.Move(file, _directorioErrores + infoFile.Name,true);
                    }
                }


            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
