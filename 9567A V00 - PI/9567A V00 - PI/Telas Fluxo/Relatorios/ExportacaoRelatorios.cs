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

        ///// <summary>
        /////  Exporta tabela e adiociona uma linha no cabelaho de produção com datas de exportações
        ///// </summary>
        ///// <param name="strPdfPath">Caminho a ser salvo o PDF </param>
        ///// <param name="PesquisaProducao">Lista de produções para ferar o relátorio</param>
        ///// <param name="strHeader">O que será escrito no cabeçalho da página</param>
        ///// <param name="dataExportacaoInicial"> Envia uma data para ser salvo no PDF e para mostrar quando foi iniciado a exportação</param>
        ///// <param name="dataExportacaoFinal"> Envia uma data para ser salvo no PDF e para mostrar quando foi finalizado a exportação</param>   
        //public static bool exportProducao(String strPdfPath, List<Utilidades.Producao> PesquisaProducao, string strHeader, DateTime dataExportacaoInicial, DateTime dataExportacaoFinal)
        //{
        //    try
        //    {
        //        #region Váriveis
        //        float[] colsW = { 25, 25 };

        //        double pesoTotalproduzido = 0;
        //        double volumeTotalProduzido = 0;
        //        Int64 quantidadeProducoes = 0;
        //        Int64 quantidadeBateladas = 0;

        //        #endregion

        //        #region Cabeçalho

        //        System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
        //        Document document = new Document();
        //        document.SetPageSize(iTextSharp.text.PageSize.A4);
        //        PdfWriter writer = PdfWriter.GetInstance(document, fs);
        //        writer.PageEvent = new PDFFooter();

        //        document.Open();

        //        //Report Header
        //        BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //        Font fntHead = new Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.BLACK);
        //        Paragraph prgHeading = new Paragraph();
        //        prgHeading.Alignment = Element.ALIGN_CENTER;
        //        prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
        //        document.Add(prgHeading);


        //        //Adiociona a imagem no projeto e no PDF
        //        #region Imagem Automasul
        //        //Busca a imagem
        //        string filename = "Logo_Automasul.png";
        //        //Salva a imagem no arquivo Bin
        //        Resources.Logo_Automasul.Save(Path.GetFullPath(filename));
        //        string path = Path.GetFullPath(filename);
        //        //Manda o caminho para o PDF
        //        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
        //        image.SetAbsolutePosition(0, 20);

        //        image.ScaleAbsolute(150, 50);
        //        image.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
        //        PdfContentByte cbhead = writer.DirectContent;
        //        PdfTemplate tp = cbhead.CreateTemplate(1000, 1000);
        //        tp.AddImage(image);

        //        cbhead.AddTemplate(tp, 0, 842 - 95);
        //        #endregion

        //        //Adiociona a imagem no projeto e no PDF
        //        #region Imagem Becker
        //        //Busca a imagem
        //        string filename1 = "Logo_Becker.png";
        //        //Salva a imagem no arquivo Bin
        //        Resources.Logo_Becker.Save(Path.GetFullPath(filename1));
        //        string path1 = Path.GetFullPath(filename1);

        //        //Manda o caminho para o PDF
        //        iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(path1);
        //        image1.SetAbsolutePosition(460, 40);
        //        image1.ScaleAbsolute(100, 30);
        //        image1.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
        //        PdfContentByte cbhead1 = writer.DirectContent;
        //        PdfTemplate tp1 = cbhead1.CreateTemplate(1000, 1000);
        //        tp1.AddImage(image1);
        //        cbhead1.AddTemplate(tp1, 0, 842 - 95);
        //        #endregion


        //        //Author
        //        Paragraph prgAuthor = new Paragraph();
        //        BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //        Font fntAuthor = new Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.GRAY);
        //        prgAuthor.Alignment = Element.ALIGN_RIGHT;
        //        prgAuthor.Add(new Chunk("Autor : " + Utilidades.VariaveisGlobais.UserLogged_GS, fntAuthor));
        //        prgAuthor.Add(new Chunk("\nExportado : " + DateTime.Now.ToShortDateString(), fntAuthor));
        //        document.Add(prgAuthor);


        //        //Add a line seperation
        //        Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        //        document.Add(p);

        //        //Add line break
        //        //document.Add(new Chunk("\n", fntHead));

        //        #endregion

        //        //Quantidade de produçoes
        //        quantidadeProducoes = PesquisaProducao.Count;

        //        foreach (var item in PesquisaProducao)
        //        {
        //            //Quantidade de bateladas em produçao pesquisada.
        //            quantidadeBateladas = quantidadeBateladas + item.quantidadeBateladas;
        //            pesoTotalproduzido = pesoTotalproduzido + item.pesoTotalProduzido;
        //            volumeTotalProduzido = volumeTotalProduzido + item.volumeTotalProduzido;

        //        }

        //        document.Add(tableProducao(colsW, "Peso total produzido: " + Math.Round(pesoTotalproduzido, 2) + " kg", "Volume total produzido: " + Math.Round(pesoTotalproduzido, 2) + " m³", false));
        //        document.Add(tableProducao(colsW, "Quantidade total produzida: " + quantidadeProducoes + " produções", "Total de bateladas produzidas: " + quantidadeBateladas + " und.", false));
        //        document.Add(tableProducao(colsW, "Data início produção: " + dataExportacaoInicial.ToShortDateString() + "\n" + dataExportacaoInicial.ToLongTimeString(), "Data fim produção: " + dataExportacaoFinal.ToShortDateString() + "\n" + dataExportacaoFinal.ToLongTimeString(), false)); ;


        //        //Add a line seperation
        //        Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        //        document.Add(p1);

        //        //Adiocona o detatalhamento da Produção
        //        Paragraph Detalhamento = new Paragraph();
        //        BaseFont btnDetalhamento = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //        Font fntADetalhamento = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.BOLD, new BaseColor(0, 0, 0));
        //        Detalhamento.Alignment = Element.ALIGN_CENTER;

        //        Detalhamento.Add(new Chunk("Detalhamento Produção", fntADetalhamento));
        //        document.Add(Detalhamento);

        //        document.Add(new Chunk("\n", fntHead));

        //        int cont = 0;
        //        int mudanca = 2;

        //        foreach (var item in PesquisaProducao)
        //        {
        //            if (!(cont < mudanca))
        //            {
        //                document.NewPage();
        //                cont = 0;
        //                mudanca = 3;
        //            }

        //            document.Add(tableProducao("Relatório produção N°: " + Convert.ToString(item.id), true));
        //            document.Add(tableProducao(item.receita.nomeReceita, true));
        //            document.Add(tableProducao(colsW, "Peso total desejado: " + item.pesoTotalProducao + " kg", "Volume total desejado: " + item.volumeTotalProducao + " m³", true));
        //            document.Add(tableProducao(colsW, "Peso total produzido: " + Math.Round(item.pesoTotalProduzido, 2) + " kg", "Volume total produzido: " + Math.Round(item.volumeTotalProduzido, 2) + " m³", true));
        //            document.Add(tableProducao("Quantidade de bateldas: " + item.quantidadeBateladas + " und.", true, Element.ALIGN_LEFT));
        //            document.Add(tableProducao(colsW, "Tempo pré mistura: " + item.tempoPreMistura + " segundos", "Tempo pós mistura: " + item.tempoPosMistura + " segundos", true));
        //            document.Add(tableProducao(colsW, "Data início produção: " + item.dateTimeInicioProducao, "Data envio silo expedição: " + item.dateTimeFimProducao, true));

        //            //Controle Produção Ensaque
        //            DataTable dt = new DataTable();
        //            dt = DataBase.SqlFunctionsEnsaques.getProducaoEnsaqueFromDateTime(dataExportacaoInicial, dataExportacaoFinal, item.id);

        //            int quantidade = 0;
        //            //Verifica 
        //            if (dt.Rows != null)
        //            {

        //                if (dt.Rows.Count > 0)
        //                {

        //                    int a = DataBase.SqlFunctionsEnsaques.getIdProducaoEnsaque(item.id);

        //                    quantidade = dt.Rows.Count;

        //                    document.Add(tableProducao("Quantidade de ensaques iniciados: " + quantidade, true));
        //                    document.Add(tableProducao(colsW, "Peso ensacado: " + Math.Round(DataBase.SqlFunctionsEnsaques.getPesoTotalEnsaque(a), 2) + " kg", "Peso médio ensaque: " + Math.Round(DataBase.SqlFunctionsEnsaques.getPesoMedioEnsaque(a), 2) + " kg", true));
        //                    document.Add(tableProducao(colsW, "Peso mínimo saco: " + (float)Math.Round(DataBase.SqlFunctionsEnsaques.getPesoMinEnsaque(a), 2) + " kg", "Peso máximo saco: " + (float)Math.Round(DataBase.SqlFunctionsEnsaques.getPesoMaxEnsaque(a), 2) + " kg", true));
        //                    document.Add(tableProducao("Quantidade de sacos: " + DataBase.SqlFunctionsEnsaques.getCoutEnsaque(a) + " und.", true, Element.ALIGN_LEFT));

        //                }
        //                else
        //                {
        //                    document.Add(tableProducao("Produção não possui ensaque!", true));
        //                }

        //            }
        //            else
        //            {
        //                document.Add(tableProducao("Produção não possui ensaque!", true));
        //            }

        //            document.Add(new Chunk("\n", fntHead));
        //            cont++;
        //        }

        //        document.Close();
        //        writer.Close();
        //        fs.Close();


        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString() + " Erro exportar relatório Produção";


        //        return false;
        //    }

        //}

        ///// <summary>
        ///// Funçao para exportar as bateladas de uma produção.
        ///// </summary>
        ///// <param name="strPdfPath">Caminho a ser salvo o PDF </param>
        ///// <param name="producao">Classe da produção para gerar o relatório</param>
        ///// <param name="strHeader">O que será escrito no cabeçalho da página</param>
        ///// <returns></returns>
        //public static bool exportarBatelada(String strPdfPath, Utilidades.Producao producao, string strHeader)
        //{
        //    try
        //    {
        //        #region Cabeçalho

        //        System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
        //        Document document = new Document();
        //        document.SetPageSize(iTextSharp.text.PageSize.A4);
        //        PdfWriter writer = PdfWriter.GetInstance(document, fs);
        //        writer.PageEvent = new PDFFooter();

        //        document.Open();

        //        //Report Header
        //        BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //        Font fntHead = new Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.BLACK);
        //        Paragraph prgHeading = new Paragraph();
        //        prgHeading.Alignment = Element.ALIGN_CENTER;
        //        prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
        //        document.Add(prgHeading);


        //        //Adiociona a imagem no projeto e no PDF
        //        #region Imagem Automasul
        //        //Busca a imagem
        //        string filename = "Logo_Automasul.png";
        //        //Salva a imagem no arquivo Bin
        //        Resources.Logo_Automasul.Save(Path.GetFullPath(filename));
        //        string path = Path.GetFullPath(filename);
        //        //Manda o caminho para o PDF
        //        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
        //        image.SetAbsolutePosition(0, 20);

        //        image.ScaleAbsolute(150, 50);
        //        image.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
        //        PdfContentByte cbhead = writer.DirectContent;
        //        PdfTemplate tp = cbhead.CreateTemplate(1000, 1000);
        //        tp.AddImage(image);

        //        cbhead.AddTemplate(tp, 0, 842 - 95);
        //        #endregion

        //        //Adiociona a imagem no projeto e no PDF
        //        #region Imagem Becker
        //        //Busca a imagem
        //        string filename1 = "Logo_Becker.png";
        //        //Salva a imagem no arquivo Bin
        //        Resources.Logo_Becker.Save(Path.GetFullPath(filename1));
        //        string path1 = Path.GetFullPath(filename1);

        //        //Manda o caminho para o PDF
        //        iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(path1);
        //        image1.SetAbsolutePosition(460, 40);
        //        image1.ScaleAbsolute(100, 30);
        //        image1.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
        //        PdfContentByte cbhead1 = writer.DirectContent;
        //        PdfTemplate tp1 = cbhead1.CreateTemplate(1000, 1000);
        //        tp1.AddImage(image1);
        //        cbhead1.AddTemplate(tp1, 0, 842 - 95);
        //        #endregion


        //        //Author
        //        Paragraph prgAuthor = new Paragraph();
        //        BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //        Font fntAuthor = new Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.GRAY);
        //        prgAuthor.Alignment = Element.ALIGN_RIGHT;
        //        prgAuthor.Add(new Chunk("Autor : " + Utilidades.VariaveisGlobais.UserLogged_GS, fntAuthor));
        //        prgAuthor.Add(new Chunk("\nExportado : " + DateTime.Now.ToShortDateString(), fntAuthor));
        //        document.Add(prgAuthor);


        //        //Add a line seperation
        //        Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        //        document.Add(p);

        //        //Add line break
        //        //document.Add(new Chunk("\n", fntHead));

        //        #endregion

        //        float[] colsW = { 25, 25 };

        //        //Quantidade de produçoes
        //        document.Add(tableProducao("Produção N°: " + producao.id + ", Receita: " + producao.receita.nomeReceita, false, Element.ALIGN_CENTER));
        //        document.Add(tableProducao(colsW, "Peso total produzido: " + Math.Round(producao.pesoTotalProduzido, 2) + " kg", "Volume total produzido: " + Math.Round(producao.volumeTotalProduzido, 2) + " m³", false));
        //        document.Add(tableProducao("Quantidade de bateladas: " + producao.quantidadeBateladas + " und.", false, Element.ALIGN_LEFT));
        //        document.Add(tableProducao(colsW, "Data início produção: " + producao.dateTimeInicioProducao.ToString(), "Data fim produção: " + producao.dateTimeFimProducao.ToString(), false)); ;


        //        //Add a line seperation
        //        Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        //        document.Add(p1);

        //        //Adiocona o detatalhamento da Produção
        //        Paragraph Detalhamento = new Paragraph();
        //        BaseFont btnDetalhamento = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //        Font fntADetalhamento = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.BOLD, new BaseColor(0, 0, 0));
        //        Detalhamento.Alignment = Element.ALIGN_CENTER;

        //        Detalhamento.Add(new Chunk("Detalhamento Bateladas", fntADetalhamento));
        //        document.Add(Detalhamento);

        //        document.Add(new Chunk("\n", fntHead));

        //        int OldBatelada = -1;
        //        foreach (var batelada in producao.batelada)
        //        {
        //            PdfPTable tablebatelada = new PdfPTable(4);
        //            BaseColor preto = new BaseColor(0, 0, 0);
        //            BaseColor fundo = new BaseColor(200, 200, 200);
        //            BaseColor branco = new BaseColor(255, 255, 255);
        //            Font font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);
        //            Font titulo = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);

        //            float[] colsWBatelada = { 20, 20, 20, 20 };
        //            tablebatelada.HeaderRows = 0;
        //            tablebatelada.WidthPercentage = 100f;
        //            tablebatelada.DefaultCell.Border = PdfPCell.BOX;
        //            tablebatelada.DefaultCell.BorderColor = new BaseColor(255, 255, 255);
        //            tablebatelada.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
        //            tablebatelada.DefaultCell.Padding = 5;

        //            if (OldBatelada == -1)
        //            {
        //                tablebatelada.KeepTogether = false;
        //                OldBatelada = 1;
        //            }
        //            else
        //            {
        //                tablebatelada.KeepTogether = true;
        //            }



        //            var cell = getNewCell("Batelada N°: " + batelada.numeroBatelada, titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco);
        //            cell.Colspan = 5;
        //            tablebatelada.AddCell(cell);

        //            var cell1 = getNewCell("Peso desejado: " + batelada.pesoDesejado + " kg" + " | Peso dosado:" + Math.Round(batelada.pesoDosado, 2) + " kg", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco);
        //            cell1.Colspan = 5;
        //            tablebatelada.AddCell(cell1);

        //            tablebatelada.AddCell(getNewCell("Desrição Produto", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco));
        //            tablebatelada.AddCell(getNewCell("Peso Desejado", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco));
        //            tablebatelada.AddCell(getNewCell("Peso Dosado", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco));
        //            tablebatelada.AddCell(getNewCell("Percentual de Peso Dosado", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco));

        //            foreach (var produtos in batelada.produtos)
        //            {

        //                tablebatelada.AddCell(getNewCell(produtos.descricao, font, Element.ALIGN_LEFT, 5, PdfPCell.BOX));
        //                tablebatelada.AddCell(getNewCell(Convert.ToString(produtos.pesoDesejado) + " kg", font, Element.ALIGN_LEFT, 5, PdfPCell.BOX));
        //                tablebatelada.AddCell(getNewCell(Convert.ToString(Math.Round(produtos.pesoDosado, 2)) + " kg", font, Element.ALIGN_LEFT, 5, PdfPCell.BOX));
        //                tablebatelada.AddCell(getNewCell(Convert.ToString(Math.Round(Utilidades.functions.percentualProduto(produtos.pesoDosado, batelada.pesoDesejado), 2)) + " %", font, Element.ALIGN_LEFT, 5, PdfPCell.BOX));

        //            }

        //            document.Add(tablebatelada);
        //            document.Add(new Chunk("\n", fntHead));


        //        }

        //        document.Close();
        //        writer.Close();
        //        fs.Close();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString() + " Erro exportar relatório Bateladas";


        //        return false;
        //    }


        //}

        ///// <summary>
        ///// Funçao para exportar as bateladas de uma produção.
        ///// </summary>
        ///// <param name="strPdfPath">Caminho a ser salvo o PDF </param>
        ///// <param name="producao">Classe da produção para gerar o relatório</param>
        ///// <param name="strHeader">O que será escrito no cabeçalho da página</param>
        ///// <returns> 
        ///// -1 = Erro
        /////  0 = OK
        /////  1 = Produção sem ensaque
        ///// 
        ///// 
        ///// </returns>
        //public static int exportarEnsaque(String strPdfPath, Utilidades.Producao producao, string strHeader)
        //{
        //    try
        //    {

        //        DataTable dt = new DataTable();

        //        int a = DataBase.SqlFunctionsEnsaques.getIdProducaoEnsaque(producao.id);
        //        dt = DataBase.SqlFunctionsEnsaques.getEnsaqueFromIdProducaoEnsaque(a);

        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                #region Cabeçalho

        //                System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
        //                Document document = new Document();
        //                document.SetPageSize(iTextSharp.text.PageSize.A4);
        //                PdfWriter writer = PdfWriter.GetInstance(document, fs);
        //                writer.PageEvent = new PDFFooter();

        //                document.Open();

        //                //Report Header
        //                BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //                Font fntHead = new Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.BLACK);
        //                Paragraph prgHeading = new Paragraph();
        //                prgHeading.Alignment = Element.ALIGN_CENTER;
        //                prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
        //                document.Add(prgHeading);


        //                //Adiociona a imagem no projeto e no PDF
        //                #region Imagem Automasul
        //                //Busca a imagem
        //                string filename = "Logo_Automasul.png";
        //                //Salva a imagem no arquivo Bin
        //                Resources.Logo_Automasul.Save(Path.GetFullPath(filename));
        //                string path = Path.GetFullPath(filename);
        //                //Manda o caminho para o PDF
        //                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
        //                image.SetAbsolutePosition(0, 20);

        //                image.ScaleAbsolute(150, 50);
        //                image.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
        //                PdfContentByte cbhead = writer.DirectContent;
        //                PdfTemplate tp = cbhead.CreateTemplate(1000, 1000);
        //                tp.AddImage(image);

        //                cbhead.AddTemplate(tp, 0, 842 - 95);
        //                #endregion

        //                //Adiociona a imagem no projeto e no PDF
        //                #region Imagem Becker
        //                //Busca a imagem
        //                string filename1 = "Logo_Becker.png";
        //                //Salva a imagem no arquivo Bin
        //                Resources.Logo_Becker.Save(Path.GetFullPath(filename1));
        //                string path1 = Path.GetFullPath(filename1);

        //                //Manda o caminho para o PDF
        //                iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(path1);
        //                image1.SetAbsolutePosition(460, 40);
        //                image1.ScaleAbsolute(100, 30);
        //                image1.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
        //                PdfContentByte cbhead1 = writer.DirectContent;
        //                PdfTemplate tp1 = cbhead1.CreateTemplate(1000, 1000);
        //                tp1.AddImage(image1);
        //                cbhead1.AddTemplate(tp1, 0, 842 - 95);
        //                #endregion


        //                //Author
        //                Paragraph prgAuthor = new Paragraph();
        //                BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //                Font fntAuthor = new Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.GRAY);
        //                prgAuthor.Alignment = Element.ALIGN_RIGHT;
        //                prgAuthor.Add(new Chunk("Autor : " + Utilidades.VariaveisGlobais.UserLogged_GS, fntAuthor));
        //                prgAuthor.Add(new Chunk("\nExportado : " + DateTime.Now.ToShortDateString(), fntAuthor));
        //                document.Add(prgAuthor);


        //                //Add a line seperation
        //                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        //                document.Add(p);

        //                //Add line break
        //                //document.Add(new Chunk("\n", fntHead));

        //                #endregion

        //                float[] colsW = { 25, 25 };

        //                //Quantidade de produçoes
        //                document.Add(tableProducao("Produção N°: " + a + ", Receita: " + producao.receita.nomeReceita, false, Element.ALIGN_CENTER));
        //                document.Add(tableProducao(colsW, "Peso ensacado: " + Math.Round(DataBase.SqlFunctionsEnsaques.getPesoTotalEnsaque(a), 2) + " kg", "Peso médio ensaque: " + Math.Round(DataBase.SqlFunctionsEnsaques.getPesoMedioEnsaque(a), 2) + " kg", false));
        //                document.Add(tableProducao(colsW, "Peso mínimo saco: " + Math.Round(DataBase.SqlFunctionsEnsaques.getPesoMinEnsaque(a), 2) + " kg", "Peso máximo saco: " + Math.Round(DataBase.SqlFunctionsEnsaques.getPesoMaxEnsaque(a), 2) + " kg", false));
        //                document.Add(tableProducao("Quantidade de sacos: " + DataBase.SqlFunctionsEnsaques.getCoutEnsaque(a) + " und.", false, Element.ALIGN_LEFT));



        //                //Add a line seperation
        //                Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        //                document.Add(p1);

        //                //Adiocona o detatalhamento da Produção
        //                Paragraph Detalhamento = new Paragraph();
        //                BaseFont btnDetalhamento = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //                Font fntADetalhamento = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.BOLD, new BaseColor(0, 0, 0));
        //                Detalhamento.Alignment = Element.ALIGN_CENTER;

        //                Detalhamento.Add(new Chunk("Detalhamento Ensaque", fntADetalhamento));
        //                document.Add(Detalhamento);

        //                document.Add(new Chunk("\n", fntHead));

        //                int OldEnsaque = -1;

        //                PdfPTable tablebatelada = new PdfPTable(2);
        //                BaseColor preto = new BaseColor(0, 0, 0);
        //                BaseColor fundo = new BaseColor(200, 200, 200);
        //                BaseColor branco = new BaseColor(255, 255, 255);
        //                Font font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);
        //                Font titulo = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, preto);

        //                float[] colsWBatelada = { 20, 20 };
        //                tablebatelada.HeaderRows = 0;
        //                tablebatelada.WidthPercentage = 100f;
        //                tablebatelada.DefaultCell.Border = PdfPCell.BOX;
        //                tablebatelada.DefaultCell.BorderColor = new BaseColor(255, 255, 255);
        //                tablebatelada.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
        //                tablebatelada.DefaultCell.Padding = 5;



        //                tablebatelada.AddCell(getNewCell("Saco", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco));
        //                tablebatelada.AddCell(getNewCell("Peso Dosado", titulo, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, branco));

        //                int contadorSaco = 1;

        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    if (OldEnsaque == -1 || OldEnsaque < 35)
        //                    {
        //                        tablebatelada.KeepTogether = false;
        //                        OldEnsaque++;
        //                    }
        //                    else
        //                    {
        //                        tablebatelada.KeepTogether = true;
        //                    }


        //                    tablebatelada.AddCell(getNewCell("Saco " + contadorSaco, font, Element.ALIGN_LEFT, 5, PdfPCell.BOX));
        //                    tablebatelada.AddCell(getNewCell((float)Math.Round(Convert.ToSingle(row["PesoDosado"]), 2) + " kg", font, Element.ALIGN_LEFT, 5, PdfPCell.BOX));

        //                    contadorSaco++;

        //                }

        //                document.Add(tablebatelada);
        //                document.Add(new Chunk("\n", fntHead));

        //                document.Close();
        //                writer.Close();
        //                fs.Close();

        //                return 0;
        //            }
        //            else
        //            {
        //                return 1;
        //            }
        //        }
        //        else
        //        {
        //            return 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString() + " Erro exportar relatório Bateladas";


        //        return -1;
        //    }
        //}

        ///// <summary>
        ///// Funçao para exportar produção a produção.
        ///// </summary>
        ///// <param name="strPdfPath">Caminho a ser salvo o PDF </param>
        ///// <param name="producao">Classe da produção para gerar o relatório</param>
        ///// <param name="strHeader">O que será escrito no cabeçalho da página</param>
        ///// <returns></returns>
        //public static bool producaoInicial(String strPdfPath, Utilidades.Producao producao, string strHeader)
        //{
        //    try
        //    {
        //        #region Cabeçalho

        //        System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
        //        Document document = new Document();
        //        document.SetPageSize(iTextSharp.text.PageSize.A4);
        //        PdfWriter writer = PdfWriter.GetInstance(document, fs);
        //        writer.PageEvent = new PDFFooter();

        //        document.Open();

        //        //Report Header
        //        BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //        Font fntHead = new Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.BLACK);
        //        Paragraph prgHeading = new Paragraph();
        //        prgHeading.Alignment = Element.ALIGN_CENTER;
        //        prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
        //        document.Add(prgHeading);


        //        //Adiociona a imagem no projeto e no PDF
        //        #region Imagem Automasul
        //        //Busca a imagem
        //        string filename = "Logo_Automasul.png";
        //        //Salva a imagem no arquivo Bin
        //        Resources.Logo_Automasul.Save(Path.GetFullPath(filename));
        //        string path = Path.GetFullPath(filename);
        //        //Manda o caminho para o PDF
        //        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
        //        image.SetAbsolutePosition(0, 20);

        //        image.ScaleAbsolute(150, 50);
        //        image.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
        //        PdfContentByte cbhead = writer.DirectContent;
        //        PdfTemplate tp = cbhead.CreateTemplate(1000, 1000);
        //        tp.AddImage(image);

        //        cbhead.AddTemplate(tp, 0, 842 - 95);
        //        #endregion

        //        //Adiociona a imagem no projeto e no PDF
        //        #region Imagem Becker
        //        //Busca a imagem
        //        string filename1 = "Logo_Becker.png";
        //        //Salva a imagem no arquivo Bin
        //        Resources.Logo_Becker.Save(Path.GetFullPath(filename1));
        //        string path1 = Path.GetFullPath(filename1);

        //        //Manda o caminho para o PDF
        //        iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(path1);
        //        image1.SetAbsolutePosition(460, 40);
        //        image1.ScaleAbsolute(100, 30);
        //        image1.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
        //        PdfContentByte cbhead1 = writer.DirectContent;
        //        PdfTemplate tp1 = cbhead1.CreateTemplate(1000, 1000);
        //        tp1.AddImage(image1);
        //        cbhead1.AddTemplate(tp1, 0, 842 - 95);
        //        #endregion


        //        //Author
        //        Paragraph prgAuthor = new Paragraph();
        //        BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //        Font fntAuthor = new Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.GRAY);
        //        prgAuthor.Alignment = Element.ALIGN_RIGHT;
        //        prgAuthor.Add(new Chunk("Autor : " + Utilidades.VariaveisGlobais.UserLogged_GS, fntAuthor));
        //        prgAuthor.Add(new Chunk("\nExportado : " + DateTime.Now.ToShortDateString(), fntAuthor));
        //        document.Add(prgAuthor);


        //        //Add a line seperation
        //        Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        //        document.Add(p);

        //        //Add line break
        //        //document.Add(new Chunk("\n", fntHead));

        //        #endregion


        //        //Quantidade de produçoes
        //        document.Add(tableProducao("Produção N°: " + producao.id + ", Receita: " + producao.receita.nomeReceita, false, Element.ALIGN_CENTER));
        //        document.Add(tableProducao("Quantidade de bateladas: " + producao.quantidadeBateladas + " und.", false, Element.ALIGN_LEFT));


        //        //Add a line seperation
        //        Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        //        document.Add(p1);

        //        //Adiocona o detatalhamento da Produção
        //        Paragraph Detalhamento = new Paragraph();
        //        BaseFont btnDetalhamento = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //        Font fntADetalhamento = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.BOLD, new BaseColor(0, 0, 0));
        //        Detalhamento.Alignment = Element.ALIGN_CENTER;

        //        Detalhamento.Add(new Chunk("Detalhamento Bateladas", fntADetalhamento));
        //        document.Add(Detalhamento);

        //        document.Add(new Chunk("\n", fntHead));

        //        int OldBatelada = -1;

        //        float bateladaPeso = -1;

        //        int contbateladas = 0;
        //        float bateladaPesquisa1 = -1;
        //        int contbateladas1 = 0;
        //        int contauxBatelada = 0;
        //        int umavez = -1;
        //        int segundavez = -1;

        //        bool controle = false;
        //        foreach (var bateladaPesquisa in producao.batelada)
        //        {
        //            //Quantidade de bateladas
        //            contbateladas1++;

        //            //Pega as bateladas com peso diferente
        //            if (bateladaPesquisa1 != bateladaPesquisa.pesoDesejado)
        //            {
        //                //Atualiza variavel quando atualizada
        //                bateladaPesquisa1 = bateladaPesquisa.pesoDesejado;

        //                //Conta quantidade de batelada
        //                contauxBatelada++;

        //                if (umavez != -1)
        //                {
        //                    segundavez = bateladaPesquisa.numeroBatelada;
        //                }

        //                umavez = bateladaPesquisa.numeroBatelada;



        //            }

        //        }
        //        foreach (var batelada in producao.batelada)
        //        {
        //            PdfPTable tablebatelada = new PdfPTable(5);
        //            BaseColor preto = new BaseColor(0, 0, 0);
        //            BaseColor fundo = new BaseColor(200, 200, 200);
        //            BaseColor branco = new BaseColor(255, 255, 255);
        //            Font font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 16, Font.BOLD, preto);
        //            Font titulo = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 16, Font.BOLD, preto);

        //            float[] colsWBatelada = { 20, 20, 20, 20, 20 };
        //            tablebatelada.HeaderRows = 0;
        //            tablebatelada.WidthPercentage = 100f;
        //            tablebatelada.DefaultCell.Border = PdfPCell.BOX;
        //            tablebatelada.DefaultCell.BorderColor = new BaseColor(255, 255, 255);
        //            tablebatelada.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
        //            tablebatelada.DefaultCell.Padding = 5;

        //            if (OldBatelada == -1)
        //            {
        //                tablebatelada.KeepTogether = false;
        //                OldBatelada = 1;
        //            }
        //            else
        //            {
        //                tablebatelada.KeepTogether = true;
        //            }



        //            if (bateladaPeso != batelada.pesoDesejado && contbateladas <= producao.quantidadeBateladas)
        //            {

        //                string descrição = "";

        //                if (!controle)
        //                {
        //                    descrição = "Batelada N°: " + batelada.numeroBatelada + " há Batelada N°: " + (contbateladas1 - contauxBatelada + 1);
        //                    controle = true;

        //                }
        //                else
        //                {
        //                    descrição = "Batelada N°: " + batelada.numeroBatelada;

        //                }

        //                var cell = getNewCell(descrição, titulo, Element.ALIGN_CENTER, 7, PdfPCell.BOX, preto, branco);

        //                cell.Colspan = 5;
        //                tablebatelada.AddCell(cell);

        //                var cell1 = getNewCell("Peso desejado: " + batelada.pesoDesejado + " kg", titulo, Element.ALIGN_CENTER, 7, PdfPCell.BOX, preto, branco);
        //                cell1.Colspan = 5;
        //                tablebatelada.AddCell(cell1);

        //                tablebatelada.AddCell(getNewCell("Desrição Produto", titulo, Element.ALIGN_CENTER, 7, PdfPCell.BOX, preto, branco));
        //                tablebatelada.AddCell(getNewCell("Peso Desejado", titulo, Element.ALIGN_CENTER, 7, PdfPCell.BOX, preto, branco));
        //                tablebatelada.AddCell(getNewCell("Peso Percentual", titulo, Element.ALIGN_CENTER, 7, PdfPCell.BOX, preto, branco));
        //                tablebatelada.AddCell(getNewCell("TP. Produto", titulo, Element.ALIGN_CENTER, 7, PdfPCell.BOX, preto, branco));
        //                tablebatelada.AddCell(getNewCell("Dosagem", titulo, Element.ALIGN_CENTER, 7, PdfPCell.BOX, preto, branco));

        //                foreach (var produtos in batelada.produtos)
        //                {

        //                    tablebatelada.AddCell(getNewCell(produtos.descricao, font, Element.ALIGN_LEFT, 7, PdfPCell.BOX));
        //                    tablebatelada.AddCell(getNewCell(Convert.ToString(produtos.pesoDesejado) + " kg", font, Element.ALIGN_LEFT, 7, PdfPCell.BOX));
        //                    tablebatelada.AddCell(getNewCell(Convert.ToString(Utilidades.functions.percentualProduto(produtos.pesoDesejado, batelada.pesoDesejado)) + " %", font, Element.ALIGN_LEFT, 7, PdfPCell.BOX));

        //                    string produto = "";

        //                    if (produtos.tipoProduto.Contains("Complemento"))
        //                    {
        //                        produto = "Comple.";
        //                    }
        //                    else
        //                    {
        //                        produto = produtos.tipoProduto;
        //                    }


        //                    tablebatelada.AddCell(getNewCell(produto, font, Element.ALIGN_LEFT, 7, PdfPCell.BOX));

        //                    foreach (var item in producao.receita.listProdutos)
        //                    {
        //                        if (item.produto.codigo == produtos.codigo)
        //                        {
        //                            produto = "";
        //                            if (item.tipoDosagemMateriaPrima.Length < 3)
        //                            {
        //                                produto = "N/A";
        //                            }
        //                            else
        //                            {

        //                                produto = item.tipoDosagemMateriaPrima;


        //                            }
        //                            tablebatelada.AddCell(getNewCell(produto, font, Element.ALIGN_LEFT, 8, PdfPCell.BOX));


        //                        }

        //                    }
        //                }

        //                document.Add(tablebatelada);
        //                document.Add(new Chunk("\n", fntHead));

        //                bateladaPeso = batelada.pesoDesejado;
        //                contbateladas++;
        //            }

        //        }

        //        document.Close();
        //        writer.Close();
        //        fs.Close();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString() + " Erro exportar relatório Bateladas";


        //        return false;
        //    }


        //}


        //#endregion

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
