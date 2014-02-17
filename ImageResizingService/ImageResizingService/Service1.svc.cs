using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using ImageResizer;

namespace ImageResizingService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public void UploadImage(File file)
        {
            string fl = file.Files;
            var bytes = Convert.FromBase64String(fl);
            var streamByte = new MemoryStream(bytes);

            Dictionary<string, string> versions = new Dictionary<string, string>();
            //Define the versions to generate
            versions.Add("_thumb", "maxwidth=400&maxheight=400&format=jpg"); //Crop to square thumbnail
            versions.Add("_small", "maxwidth=600&maxheight=600&format=jpg"); //Fit inside 400x400 area, jpeg
            versions.Add("_medium", "maxwidth=900&maxheight=900&format=jpg"); 
            versions.Add("_large", "maxwidth=1200&maxheight=1200&format=jpg"); //Fit inside 1900x1200 area
            versions.Add("_small_lq", "maxwidth=600&maxheight=600&format=jpg&quality=50");
            versions.Add("_medium_lq", "maxwidth=900&maxheight=900&format=jpg&quality=50");
            versions.Add("_large_lq", "maxwidth=1200&maxheight=1200&format=jpg&quality=50");
            
            string basePath = ImageResizer.Util.PathUtils.RemoveExtension(file.FileName);

            //To store the list of generated paths
            List<string> generatedFiles = new List<string>();

            //Generate each version
            foreach (string suffix in versions.Keys)
            {
                streamByte.Seek(0, SeekOrigin.Begin);
                ////Let the image builder add the correct extension based on the output file type
                //generatedFiles.Add(ImageBuilder.Current.Build(streamByte, "c:\\uploads\\" + basePath + suffix,
                //    new ResizeSettings(versions[suffix]), false, true));

                generatedFiles.Add(ImageBuilder.Current.Build(new ImageJob(streamByte, "c:\\uploads\\" + basePath + suffix, new Instructions(versions[suffix]), false, true)).FinalPath);
            }
        }
    }
}
