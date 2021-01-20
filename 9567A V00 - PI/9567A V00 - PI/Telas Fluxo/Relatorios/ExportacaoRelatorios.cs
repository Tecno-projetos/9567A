using _9567A_V00___PI.DataBase;
using _9567A_V00___PI.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace _9567A_V00___PI.Telas_Fluxo.Relatorios
{
    class ExportacaoRelatorios
    {
        //#region Exportar PDF
        /// <summary>
        /// Produções existentes
        /// </summary>
        /// <param name="dataExportacaoInicial">Data Inical das Produções</param>
        /// <param name="dataExportacaoFinal">Data Final das produções</param>
        /// <returns>Retorna todas as tabelas</returns>
        private static DataTable returnProducao(DateTime dataExportacaoInicial, DateTime dataExportacaoFinal) 
        {
            DataTable Data = new DataTable();

            dynamic DTIn;
            dynamic DTOut;
            try
            {

                if (Utilidades.VariaveisGlobais.SQLCe_GS)
                {
                    DTIn = dataExportacaoInicial.ToString("yyyyMMdd") + " " + dataExportacaoInicial.Hour + ":" + dataExportacaoInicial.Minute;
                    DTOut = dataExportacaoFinal.ToString("yyyyMMdd") + " " + dataExportacaoFinal.Hour + ":" + dataExportacaoFinal.Minute;
                }
                else
                {
                    DTIn = dataExportacaoInicial;
                    DTOut = dataExportacaoFinal;

                    string CommandString = "SELECT * FROM Producao Where FinalizouProducao = 'True' AND DataFimProducao >= '" + DTIn + "' AND DataFimProducao <= '" + DTOut + "'";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                    Call.Open();

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);

                    Adapter.Fill(Data);

                    Call.Close();

                }
            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }
            return Data;
        }

        /// <summary>
        /// Produtos das Produções
        /// </summary>
        /// <param name="idProducao">Id da produção para a pesquisa de produtos</param>
        /// <returns></returns>
        private static DataTable returnProdutosProducao(Int32 idProducao) 
        {

            DataTable Data = new DataTable();

            try
            {
                string CommandString = "SELECT * FROM ProducaoProdutos Where IdProducaoReceita = '" + idProducao + "'";

                dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                Call.Open();

                dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);

                Adapter.Fill(Data);

                Call.Close();

            }
            catch (Exception ex)
            {

                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }

            return Data;
        }
        /// <summary>
        ///  Exporta tabela e adiociona uma linha no cabelaho de produção com datas de exportações
        /// </summary>
        /// <param name="strPdfPath">Caminho a ser salvo o PDF </param>
        /// <param name="strHeader">O que será escrito no cabeçalho da página</param>
        /// <param name="dataExportacaoInicial"> Envia uma data para ser salvo no PDF e para mostrar quando foi iniciado a exportação</param>
        /// <param name="dataExportacaoFinal"> Envia uma data para ser salvo no PDF e para mostrar quando foi finalizado a exportação</param>   
        public static bool exportProducao(String strPdfPath, string strHeader, DateTime dataExportacaoInicial, DateTime dataExportacaoFinal)
        {
            try
            {
                #region Váriveis
                float[] colsW = { 25, 25 };
                #endregion

                #region Cabeçalho

                System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
                Document document = new Document();
                document.SetPageSize(iTextSharp.text.PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                writer.PageEvent = new PDFFooter();

                document.Open();

                //Report Header
                BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntHead = new Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.BLACK);
                Paragraph prgHeading = new Paragraph();
                prgHeading.Alignment = Element.ALIGN_CENTER;
                prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
                document.Add(prgHeading);


                //Adiociona a imagem no projeto e no PDF
                #region Imagem Automasul
                //Busca a imagem
                string filename = "Logo_Automasul.png";
                //Salva a imagem no arquivo Bin
                Resources.Logo_Automasul.Save(Path.GetFullPath(filename));
                string path = Path.GetFullPath(filename);
                //Manda o caminho para o PDF
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
                image.SetAbsolutePosition(0, 20);

                image.ScaleAbsolute(150, 50);
                image.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                PdfContentByte cbhead = writer.DirectContent;
                PdfTemplate tp = cbhead.CreateTemplate(1000, 1000);
                tp.AddImage(image);

                cbhead.AddTemplate(tp, 0, 842 - 95);
                #endregion

                //Adiociona a imagem no projeto e no PDF
                #region Imagem Becker
                //Busca a imagem
                string filename1= "Logo_Automasul.png";
                //Salva a imagem no arquivo Bin
                Resources.Logo_Automasul.Save(Path.GetFullPath(filename1));
                string path1 = Path.GetFullPath(filename1);

                //Manda o caminho para o PDF
                iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(path1);
                image1.SetAbsolutePosition(460, 40);
                image1.ScaleAbsolute(100, 30);
                image1.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
                PdfContentByte cbhead1 = writer.DirectContent;
                PdfTemplate tp1 = cbhead1.CreateTemplate(1000, 1000);
                tp1.AddImage(image1);
                cbhead1.AddTemplate(tp1, 0, 842 - 95);
                #endregion


                //Author
                Paragraph prgAuthor = new Paragraph();
                BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntAuthor = new Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.GRAY);
                prgAuthor.Alignment = Element.ALIGN_RIGHT;
                prgAuthor.Add(new Chunk("Autor : " + Utilidades.VariaveisGlobais.UserLogged_GS, fntAuthor));
                prgAuthor.Add(new Chunk("\nExportado : " + DateTime.Now.ToShortDateString(), fntAuthor));
                document.Add(prgAuthor);


                //Add a line seperation
                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                document.Add(p);

                //Add line break
                //document.Add(new Chunk("\n", fntHead));

                #endregion

                DataTable DataProducao = new DataTable();
                DataProducao = returnProducao(dataExportacaoInicial, dataExportacaoFinal);

                foreach (DataRow row in DataProducao.Rows)
                {

                    document.Add(tableProducao("Produção N°: " + row[0].ToString() + ", Receita: " + row[8].ToString(), false, Element.ALIGN_CENTER));
                    document.Add(tableProducao(colsW, "Peso total produzido: " + Math.Round(Convert.ToSingle(row[2].ToString()), 2) + " kg", "Peso esperado producao: " + Math.Round(Convert.ToSingle(row[1].ToString()), 2) + " kg", false));
                    document.Add(tableProducao(colsW, "Data início produção: " + row[3].ToString(), "Data fim produção: " + row[4].ToString(), false));


                    //Add a line seperation
                    Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    document.Add(p1);

                    //Adiocona o detatalhamento da Produção
                    Paragraph Detalhamento = new Paragraph();
                    BaseFont btnDetalhamento = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    Font fntADetalhamento = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.BOLD, new BaseColor(0, 0, 0));
                    Detalhamento.Alignment = Element.ALIGN_CENTER;

                    Detalhamento.Add(new Chunk("Detalhamento Produção", fntADetalhamento));
                    document.Add(Detalhamento);

                    document.Add(new Chunk("\n", fntHead));





                    PdfPTable tablebatelada = new PdfPTable(4);
                    BaseColor preto = new BaseColor(0, 0, 0);
                    BaseColor fundo = new BaseColor(200, 200, 200);
                    BaseColor branco = new BaseColor(255, 255, 255);
                    Font font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);
                    Font titulo = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);

                    float[] colsWBatelada = { 20, 20, 20, 20 };
                    tablebatelada.HeaderRows = 0;
                    tablebatelada.WidthPercentage = 100f;
                    tablebatelada.DefaultCell.Border = PdfPCell.BOX;
                    tablebatelada.DefaultCell.BorderColor = new BaseColor(255, 255, 255);
                    tablebatelada.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
                    tablebatelada.DefaultCell.Padding = 5;




                    tablebatelada.AddCell(getNewCell("Desrição Produto", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco));
                    tablebatelada.AddCell(getNewCell("Peso Desejado", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco));
                    tablebatelada.AddCell(getNewCell("Peso Dosado", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco));
                    tablebatelada.AddCell(getNewCell("Percentual de Tolerância", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco));


                    DataTable DataProducaoProdutos = new DataTable();

                    DataProducaoProdutos = returnProdutosProducao(Convert.ToInt32(row[0].ToString()));


                    foreach (DataRow rowProdutos in DataProducaoProdutos.Rows) 
                    {
                        tablebatelada.AddCell(getNewCell(rowProdutos[2].ToString(), font, Element.ALIGN_LEFT, 5, PdfPCell.BOX));
                        tablebatelada.AddCell(getNewCell(rowProdutos[6].ToString() + " kg", font, Element.ALIGN_LEFT, 5, PdfPCell.BOX));
                        tablebatelada.AddCell(getNewCell(rowProdutos[7].ToString() + " kg", font, Element.ALIGN_LEFT, 5, PdfPCell.BOX));
                        tablebatelada.AddCell(getNewCell(rowProdutos[8].ToString() + " %", font, Element.ALIGN_LEFT, 5, PdfPCell.BOX));


                    }

                    tablebatelada.KeepTogether = true;
                    document.Add(tablebatelada);
                    document.Add(new Chunk("\n", fntHead));

                }

                document.Close();
                writer.Close();
                fs.Close();

                return true;

            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString() + " Erro exportar relatório Produção";


                return false;
            }

        }

        #region Complementos relatórios
        private static PdfPTable tableProducao(float[] colsW, string nomeColuna1, string nomeColuna2, bool comBorda)
        {

            PdfPTable tabela = new PdfPTable(2);
            tabela.KeepTogether = true;
            BaseColor preto = new BaseColor(0, 0, 0);
            BaseColor fundo = new BaseColor(200, 200, 200);
            BaseColor branco = new BaseColor(255, 255, 255);
            Font font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);
            Font titulo = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);

            tabela.SetWidths(colsW);
            tabela.HeaderRows = 0;
            tabela.WidthPercentage = 100f;

            tabela.DefaultCell.BorderColor = new BaseColor(255, 255, 255);
            tabela.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
            tabela.DefaultCell.Padding = 5;


            if (comBorda)
            {
                tabela.DefaultCell.Border = PdfPCell.BOX;

                tabela.AddCell(getNewCell(nomeColuna1, titulo, Element.ALIGN_LEFT, 5, preto, branco));
                tabela.AddCell(getNewCell(nomeColuna2, titulo, Element.ALIGN_LEFT, 5, preto, branco));

            }
            else
            {
                tabela.DefaultCell.Border = PdfPCell.NO_BORDER;

                tabela.AddCell(getNewCell(nomeColuna1, titulo, Element.ALIGN_LEFT, 5, PdfPCell.NO_BORDER, preto, branco));
                tabela.AddCell(getNewCell(nomeColuna2, titulo, Element.ALIGN_LEFT, 5, PdfPCell.NO_BORDER, preto, branco));
            }

            return tabela;

        }

        private static PdfPTable tableProducao(string nomeColuna1, bool comBorda)
        {

            PdfPTable tabela = new PdfPTable(1);

            tabela.KeepTogether = true;

            BaseColor preto = new BaseColor(0, 0, 0);
            BaseColor fundo = new BaseColor(200, 200, 200);
            BaseColor branco = new BaseColor(255, 255, 255);
            Font font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);
            Font titulo = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);

            tabela.HeaderRows = 0;
            tabela.WidthPercentage = 100f;

            tabela.DefaultCell.BorderColor = new BaseColor(255, 255, 255);
            tabela.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
            tabela.DefaultCell.Padding = 5;


            if (comBorda)
            {
                tabela.DefaultCell.Border = PdfPCell.BOX;

                tabela.AddCell(getNewCell(nomeColuna1, titulo, Element.ALIGN_CENTER, 5, preto, branco));

            }
            else
            {
                tabela.DefaultCell.Border = PdfPCell.NO_BORDER;

                tabela.AddCell(getNewCell(nomeColuna1, titulo, Element.ALIGN_CENTER, 5, PdfPCell.NO_BORDER, preto, branco));


            }

            return tabela;


        }

        private static PdfPTable tableProducao(string nomeColuna1, bool comBorda, int alinhamento)
        {

            PdfPTable tabela = new PdfPTable(1);
            tabela.KeepTogether = true;
            BaseColor preto = new BaseColor(0, 0, 0);
            BaseColor fundo = new BaseColor(200, 200, 200);
            BaseColor branco = new BaseColor(255, 255, 255);
            Font font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);
            Font titulo = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);

            tabela.HeaderRows = 0;
            tabela.WidthPercentage = 100f;

            tabela.DefaultCell.BorderColor = new BaseColor(255, 255, 255);
            tabela.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
            tabela.DefaultCell.Padding = 5;


            if (comBorda)
            {
                tabela.DefaultCell.Border = PdfPCell.BOX;

                tabela.AddCell(getNewCell(nomeColuna1, titulo, alinhamento, 5, preto, branco));

            }
            else
            {
                tabela.DefaultCell.Border = PdfPCell.NO_BORDER;

                tabela.AddCell(getNewCell(nomeColuna1, titulo, alinhamento, 5, PdfPCell.NO_BORDER, preto, branco));


            }

            return tabela;


        }

        private static PdfPCell getNewCell(string Texto, Font Fonte, int Alinhamento = 0, float Espacamento = 5, int Borda = 0)
        {
            return getNewCell(Texto, Fonte, Alinhamento, Espacamento, Borda, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255));
        }

        public class PDFFooter : PdfPageEventHelper
        {
            PdfTemplate template;
            PdfContentByte cb;

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
            }


            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {

                //Adiociona os direitos autorais na última página
                #region Footer
                base.OnEndPage(writer, document);
                PdfPTable footerTbl = new PdfPTable(1);
                footerTbl.WidthPercentage = 100f;
                footerTbl.TotalWidth = 1000f;
                footerTbl.HorizontalAlignment = 0;
                PdfPCell cell;
                footerTbl.DefaultCell.HorizontalAlignment = 0;
                footerTbl.WidthPercentage = 100;
                cell = new PdfPCell(new Phrase("Copyright © " + DateTime.Now.Year + " | Automasul ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell.Border = 0;
                cell.Colspan = 1;
                cell.PaddingLeft = 0;
                cell.HorizontalAlignment = 0;
                footerTbl.AddCell(cell);
                footerTbl.WriteSelectedRows(0, -30, 230, 30, writer.DirectContent);

                base.OnEndPage(writer, document);

                int pageN = writer.PageNumber;
                String text = pageN.ToString();
                iTextSharp.text.Rectangle pageSize = document.PageSize;

                cb.SetRGBColorFill(100, 100, 100);

                cb.BeginText();
                cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 11F);
                cb.SetTextMatrix(document.RightMargin, pageSize.GetBottom(document.BottomMargin));
                cb.ShowText(text);

                cb.EndText();

                cb.AddTemplate(template, document.RightMargin + 0, pageSize.GetBottom(document.BottomMargin));
                #endregion

            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                template.BeginText();
                template.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 11F);
                template.SetTextMatrix(0, 0);
                //template.ShowText("BOdsta" + (writer.PageNumber - 1));
                template.EndText();
            }

            //write on close of document

        }

        private static PdfPCell getNewCell(string Texto, Font Fonte, int Alinhamento, float Espacamento, int Borda, BaseColor CorBorda, BaseColor CorFundo)
        {
            var cell = new PdfPCell(new Phrase(Texto, Fonte));
            cell.HorizontalAlignment = Alinhamento;
            cell.Padding = Espacamento;
            cell.Border = Borda;
            cell.BorderColor = CorBorda;
            cell.BackgroundColor = CorFundo;

            return cell;
        }

        private static PdfPCell getNewCell(string Texto, Font Fonte, int Alinhamento, float Espacamento, BaseColor CorBorda, BaseColor CorFundo)
        {
            var cell = new PdfPCell(new Phrase(Texto, Fonte));
            cell.HorizontalAlignment = Alinhamento;
            cell.Padding = Espacamento;
            cell.Border = PdfPCell.BOX;
            cell.BorderColor = CorBorda;
            cell.BackgroundColor = CorFundo;

            return cell;
        }

        //Função de converter o Chart em imagem para adicionar em Gráficos, ou outros fins;
        #region Buscar Imagem Chart
        /// <summary>
        /// Converter o Gráfco em imagem
        /// </summary>
        /// <param name="visual"> ENviar apenas o Chart (Nome colocado em suas propriedades)</param>
        /// <param name="filename">Nome que deseja salvar essa imagem (Importante se utilzado para PDF pois o mesmo deve ter o mesmo nome.)</param>
        public static void Savetopng(FrameworkElement visual, string filename)
        {
            try
            {
                var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();

                EncodeVisual(visual, filename, encoder);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Função privada para converter a o Chart para imagem
        /// </summary>
        /// <param name="visual">Objeto que deseja salvar em tipo PNG</param>
        /// <param name="filename">Onde será salvo</param>
        /// <param name="encoder">Tipo de codificação da imagem</param>
        private static void EncodeVisual(FrameworkElement visual, string filename, System.Windows.Media.Imaging.BitmapEncoder encoder)
        {
            try
            {
                // Os itens 96 e 74 deve-se ser modificado conforme a necessidade da imagem.
                var bitmap = new System.Windows.Media.Imaging.RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 74, PixelFormats.Pbgra32);

                bitmap.Render(visual);

                var frame = System.Windows.Media.Imaging.BitmapFrame.Create(bitmap);
                encoder.Frames.Add(frame);

                using (var stream = File.Create(filename)) encoder.Save(stream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");

            }
        }
        #endregion
        #endregion
    }
}
