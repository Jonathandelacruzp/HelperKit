using System;
using System.IO;
using System.Web;

namespace HelperKit.Web.Extensions
{
    public static partial class Extensions
    {
        #region HttpPostedFileBase

        /// <summary>
        /// Guarda la imagen en una ruta especifica del servidor
        /// </summary>
        /// <param name="file">Archivo a guardar</param>
        /// <param name="server"></param>
        /// <param name="folder">Ruta en el servidor</param>
        /// <returns>Ruta del de la imagen</returns>
        public static string SaveFileToServer(this HttpPostedFileBase file, HttpServerUtilityBase server, string folder = null)
        {
            var filename = string.Empty;
            try
            {
                if (file != null && file.ContentLength != 0)
                {
                    var guid = Guid.NewGuid().ToString().Substring(0, 6);
                    filename = guid + "_" + Path.GetFileName(file.FileName);
                    var path = server.MapPath(folder ?? "~/");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    file.SaveAs(Path.Combine(path, filename));
                }
                return filename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Guarda la imagen en una ruta especifica del servidor
        /// </summary>
        /// <param name="file">Archivo a guardar</param>
        /// <param name="server"></param>
        /// <param name="size">Tamaño de la imagen a guardar</param>
        /// <param name="folder">Ruta en el servidor</param>
        /// <returns>Ruta del de la imagen</returns>
        public static string SaveImageToServer(this HttpPostedFileBase file, HttpServerUtilityBase server, int size = 320, string folder = null)
        {
            var filename = string.Empty;
            try
            {
                if (file != null && file.ContentLength != 0)
                {
                    var guid = Guid.NewGuid().ToString().Substring(0, 6);
                    filename = guid + "_" + Path.GetFileName(file.FileName);
                    var path = server.MapPath(folder ?? "~/");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var image = System.Drawing.Image.FromStream(file.InputStream);
                    image = image.GetThumbnailImage(size, (size * (image.Height / image.Width).ToDecimal()).ToInteger(), null, IntPtr.Zero);
                    image.Save(Path.Combine(path, filename));
                }
                return filename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Guarda la imagen en una ruta especifica del servidor
        /// </summary>
        /// <param name="file">Archivo a guardar</param>
        /// <param name="server"></param>
        /// <param name="size">Tamaño de la imagen a guardar</param>
        /// <param name="folder">Ruta en el servidor</param>
        /// <returns>Ruta del de la imagen</returns>
        public static string SaveImageToServer(this HttpPostedFileBase file, HttpServerUtility server, int size = 320, string folder = null)
        {
            var filename = string.Empty;
            try
            {
                if (file != null && file.ContentLength != 0)
                {
                    var guid = Guid.NewGuid().ToString().Split('-')[0];
                    filename = guid + "_" + Path.GetFileName(file.FileName);
                    var path = server.MapPath(folder ?? "~/");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var image = System.Drawing.Image.FromStream(file.InputStream);
                    image = image.GetThumbnailImage(size, (size * (image.Height / image.Width).ToDecimal()).ToInteger(), null, IntPtr.Zero);
                    image.Save(Path.Combine(path, filename));
                }
                return filename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Guarda la imagen en una ruta "~/Files/images" del servidor
        /// </summary>
        /// <param name="file">Archivo a guardar</param>
        /// <param name="server"></param>
        /// <returns>Ruta del de la imagen</returns>
        public static string SaveImageToServer(this HttpPostedFileBase file, HttpServerUtility server) => SaveImageToServer(file, server);

        /// <summary>
        /// Guarda la imagen en una ruta "~/Files/images" del servidor
        /// </summary>
        /// <param name="file">Archivo a guardar</param>
        /// <param name="server"></param>
        /// <returns>Ruta del de la imagen</returns>
        public static string SaveImageToServer(this HttpPostedFileBase file, HttpServerUtilityBase server) => SaveImageToServer(file, server);

        #endregion
    }
}