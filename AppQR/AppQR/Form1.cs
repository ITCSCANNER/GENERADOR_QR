using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppQR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DateTime fechaAc = DateTime.Now;
            var fecha = fechaAc.ToString("yyyy-MM-dd|");
            var hora = fechaAc.ToString("HH:mm");
            var aula = "A1|";
            var FechaCompleta = aula+fecha+"|"+hora;

            
            /*var FechaCompletaCod = "";
            for (int i=0; i< FechaCompleta.Length; i++)
            {
                var letra = Encoding.ASCII.GetBytes(FechaCompleta)[i];
                letra += 6;
                FechaCompletaCod += Convert.ToChar(letra).ToString();
            }

            var FechaCompletaDec = "";
            for (int i = 0; i < FechaCompletaCod.Length; i++)
            {
                var letra = Encoding.ASCII.GetBytes(FechaCompletaCod)[i];
                letra -= 6;
                FechaCompletaDec += Convert.ToChar(letra).ToString();
            }
            MessageBox.Show(FechaCompletaCod+ "\n" + FechaCompletaDec, "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            */
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(FechaCompleta, out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black,Brushes.White);

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imagenTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imagenTemporal, new Size(new Point(200, 200)));
            panelResultado.BackgroundImage = imagen;

            btnGuardar.Enabled = true;
        }

        private void btnCodificado_Click(object sender, EventArgs e)
        {
            
            DateTime fechaAc = DateTime.Now;
            var fecha = fechaAc.ToString("yyyy - MM - dd");
            var hora = fechaAc.ToString("HH:mm");
            var aula = "A1 ";
            var FechaCompleta = aula + fecha + " " + hora;


            var FechaCompletaCod = "";
            for (int i = 0; i < FechaCompleta.Length; i++)
            {
                var letra = Encoding.ASCII.GetBytes(FechaCompleta)[i];
                letra += 6;
                FechaCompletaCod += Convert.ToChar(letra).ToString();
            }

            var FechaCompletaDec = "";
            for (int i = 0; i < FechaCompletaCod.Length; i++)
            {
                var letra = Encoding.ASCII.GetBytes(FechaCompletaCod)[i];
                letra -= 6;
                FechaCompletaDec += Convert.ToChar(letra).ToString();
            }
            //MessageBox.Show(FechaCompletaCod + "\n" + FechaCompletaDec, "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            


            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(FechaCompletaCod, out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imagenTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imagenTemporal, new Size(new Point(200, 200)));
            panelResultado.BackgroundImage = imagen;

            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Image imgFinal = (Image)panelResultado.BackgroundImage.Clone();

            SaveFileDialog cajaDeDialogoGuardar = new SaveFileDialog();
            cajaDeDialogoGuardar.AddExtension = true;
            cajaDeDialogoGuardar.Filter = "Image PNG (*.png) | *.png";
            cajaDeDialogoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(cajaDeDialogoGuardar.FileName))
            {
                imgFinal.Save(cajaDeDialogoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }
    }
}
