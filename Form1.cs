
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AltoHttp;
using Clases.ApiRest;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Downloader
{
    public partial class Form1 : Form



    {

        DBApi dBApi = new DBApi();

        public Form1()
        {
            InitializeComponent();
        }

        HttpDownloader httpDownloader;
        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void HttpDownloader_DownloadCompleted(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {

                pictureBox1.Visible = false;
                textBox1.Text = "Esperando archivos";

            });


        }
        private void httpDownloader_ProgressChanged(object sender, AltoHttp.ProgressChangedEventArgs e)
        {

            progressBar1.Value = (int)e.Progress;


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            dynamic respuesta = dBApi.Get("https://portalhouses.com/administrador/apiPlantas/post.php?contratoinventario=1");
            if (respuesta[0].contrato.ToString() == "")
            {
                

            }
            else
            {
                pictureBox1.Visible = true;
                string ruta = @"\\servidor1\Fotos\FOTOS_FIRMA_DE_CONTRATOS\CTO_" + (respuesta[0].contrato.ToString()).Substring(19, 4) + @"\DOCUMENTOS\";
                texturl.Text = "https://portalhouses.com/administrador/apiPlantas/upload_inv/asesores_documentos/" +  respuesta[0].contrato.ToString() + ".pdf";
                textBox1.Text = respuesta[0].contrato.ToString();


                //inicio descarga

                httpDownloader = new HttpDownloader(texturl.Text, $"{ruta}\\{Path.GetFileName( texturl.Text)}");
                httpDownloader.DownloadCompleted += HttpDownloader_DownloadCompleted;
                httpDownloader.ProgressChanged += httpDownloader_ProgressChanged;
                httpDownloader.Start();


                //

                //enviamos validacion por correo

                dynamic respuestaCorreo = dBApi.Get("https://portalhouses.com/administrador/apiPlantas/post.php?contratoinventario=1");

                string URL = "https://portalhouses.com/administrador/apiPlantas/mail/index.php?contrato=" + (respuestaCorreo[0].contrato.ToString()).Substring(19, 4) + @"&tipo=Inventario_inicial_PDF" + @"&anotacion=" + (respuestaCorreo[0].anotacion.ToString()).Replace(" ", "%20") + @"&usuario=" + (respuestaCorreo[0].usuario.ToString()).Replace(" ", "%20");
                //MessageBox.Show(URL);
                
                var prs = new ProcessStartInfo(@"C:\Program Files\Google\Chrome\Application\chrome.exe");
                prs.Arguments = URL;
                Process.Start(prs);

                ///




                //vaciamos bd 
                dynamic respuesta2 = dBApi.Get("https://portalhouses.com/administrador/apiPlantas/post.php?contratoborrar=1");
                //

           






            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
          
           
        }
        public void correo()
        {
            
//            


        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            dynamic respuestaCorreo1 = dBApi.Get("https://portalhouses.com/administrador/apiPlantas/post.php?contratoinventario=1");

            string URL = "https://portalhouses.com/administrador/apiPlantas/mail/index.php?contrato=" + (respuestaCorreo1[0].contrato.ToString()).Substring(19, 4) + @"&tipo=Inventario_inicial_PDF" + @"&anotacion=" + (respuestaCorreo1[0].anotacion.ToString()).Replace(" ", "%20") + @"&usuario=" + respuestaCorreo1[0].usuario.ToString();
           MessageBox.Show(URL);
        

            // var prs = new ProcessStartInfo(@"C:\Program Files\Google\Chrome\Application\chrome.exe");
            // prs.Arguments = URL;
            // Process.Start(prs);
        }
    }
}