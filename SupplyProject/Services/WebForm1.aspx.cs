using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace SupplyProject.Services
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            //URL do distancematrix - adicionando endereço de origem e destino
            string url = string.Format("https://maps.googleapis.com/maps/api/distancematrix/xml?origins={0}&destinations={1}&key=AIzaSyBgVHiTj7QJ3EFpvCDKudHV083MK3K8ED4",
                txtOrigem.Text, txtDestino.Text);

            //Carregar o XML via URL
            XElement xml = XElement.Load(url);

            //Verificar se o status é OK
            if (xml.Element("status").Value == "OK")
            {
                //Formatar a resposta
                litResultado.Text = string.Format("<strong>Origem</strong>: {0} <br /><strong>Destino:</strong> {1} <br /><strong>Distância</strong>: {2} <br /><strong>Duração</strong>: {3}",
                    //Pegar endereço de origem
                    xml.Element("origin_address").Value,
                    //Pegar endereço de destino
                    xml.Element("destination_address").Value,
                    //Pegar duração
                    xml.Element("row").Element("element").Element("duration").Element("text").Value,
                    //Pegar distância ente os dois pontos
                    xml.Element("row").Element("element").Element("distance").Element("text").Value
                    );
            }
            else
            {
                //Se ocorrer algum erro
                litResultado.Text = String.Concat("Ocorreu o seguinte erro: ", xml.Element("status").Value);
            }
        }
    }
}