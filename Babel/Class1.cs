using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Threading;

using Google.Cloud.Vision.V1;
using Google.Api.Gax.ResourceNames;
using Google.Cloud.Translate.V3;

using SImage = System.Drawing.Image;
using GImage = Google.Cloud.Vision.V1.Image;

using System.Windows.Forms;

namespace Babel
{
    public partial class frmBabel : Form
    {
        public void TestDoOCR(SImage image)
        {
            // Dump the provided image to a memory stream
            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Png);
            stream.Position = 0;

            // Load the stream as a gimage
            GImage gimage = GImage.FromStream(stream);

            // Make our connection client
            ImageAnnotatorClient client = new ImageAnnotatorClientBuilder
            {
                CredentialsPath = Properties.Settings.Default.apiKeyPath,
            }.Build();

            // Ask for OCR
            var response = client.DetectText(gimage);

            // If we didn't get anything back
            if (response.Count == 0)
            {
               
            }
            else
            {
               
            }
        }
    }
}
