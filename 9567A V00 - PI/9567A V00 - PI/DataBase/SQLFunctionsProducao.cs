using _9567A_V00___PI.DataBase;
using _9567A_V00___PI.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9567A_V00___PI.DataBase
{
    class SQLFunctionsProducao
    {

        private static bool ExistTable(string nomeTabela)
        {

            DataTable Data_Produtos = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString_Produtos = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + nomeTabela + "';";

                    dynamic Adapter_Produtos = SqlGlobalFuctions.ReturnAdapter(CommandString_Produtos, Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    Adapter_Produtos.Fill(Data_Produtos);
                }
                catch (Exception ex)
                {

                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }

                if (!(Data_Produtos.Rows.Count > 0))
                {
                    return false;
                }
                else
                {
                    return true;
                }


            }

            return false;
        }

        public static void Create_Table_Producao()
        {
            if (!ExistTable("Producao"))
            {
                if (Utilidades.VariaveisGlobais.DB_Connected_GS)
                {
                    try
                    {
                        string CommandString = "CREATE TABLE Producao (" +
                            "Id int not null," +      //PK
                            "PesoTotalProducao real," +    
                            "PesoTotalProduzido real," +
                            "DataInicioProducao datetime," +
                            "DataFimProducao datetime," +
                            "IniciouProducao bit," +
                            "FinalizouProducao bit," +
                            "IdReceita int," +
                            "NomeReceita nvarchar(100)," +
                            "CodigoReceita bigint," +
                            "ObservacaoReceita nvarchar(300)," +
                            "TempoMisturaReceita bigint," +
                            "PRIMARY KEY (Id));";

                        dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                        Call.Open();

                        dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);
                        Command.ExecuteNonQuery();

                        Call.Close();
                    }
                    catch (Exception ex)
                    {
                        Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                    }
                }
            }

        }

        public static void Create_Table_ProducaoProdutos()
        {
            if (!ExistTable("ProducaoProdutos"))
            {
                if (Utilidades.VariaveisGlobais.DB_Connected_GS)
                {
                    try
                    {
                        string CommandString = "CREATE TABLE ProducaoProdutos (" +
                            "IdProducaoReceita int not null," +      //PK
                            "IdProduto int," +
                            "NomeProduto nvarchar(100)," +
                            "Codigo int," +
                            "Observacao nvarchar(200)," +
                            "PesoProdutoReceita real," +
                            "PesoProdutoDesejado real," +
                            "PesoProdutoDosado real," +
                            "Tolerancia real," +
                            "IniciouDosagem bit," +
                            "FinalizouDosagem bit," +
                            "CONSTRAINT FK_IdProducaoReceita FOREIGN KEY (IdProducaoReceita) REFERENCES Producao(Id));";

                        dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                        Call.Open();

                        dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);
                        Command.ExecuteNonQuery();

                        Call.Close();
                    }
                    catch (Exception ex)
                    {
                        Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                    }
                }
            }

        }

        public static int IntoDate_Table_Producao(ref Utilidades.Producao producao)
        {
            int ret = -1;

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    int idProd = -1;
                    string CommandString = "SELECT MAX(Id) AS maxid FROM Producao";
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                    DataTable Data = new DataTable();
                    Adapter.Fill(Data);

                    //last id is null?
                    if (DBNull.Value.Equals(Data.Rows[0][0]))
                        idProd = 1;
                    else
                        idProd = Convert.ToInt32(Data.Rows[0][0]) + 1;

                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);

                    Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);

                    string query = "INSERT into Producao (" +
                        "Id, " +
                        "PesoTotalProducao, " +
                        "PesoTotalProduzido, " +
                        "DataInicioProducao, " +
                        "DataFimProducao, " +
                        "IniciouProducao, " +
                        "FinalizouProducao, " +
                        "IdReceita, " +
                        "NomeReceita, " +
                        "CodigoReceita, " +
                        "ObservacaoReceita, " +
                        "TempoMisturaReceita) VALUES (" +
                        "@Id, " +
                        "@PesoTotalProducao, " +
                        "@PesoTotalProduzido, " +
                        "@DataInicioProducao, " +
                        "@DataFimProducao, " +
                        "@IniciouProducao, " +
                        "@FinalizouProducao, " +
                        "@IdReceita, " +
                        "@NomeReceita, " +
                        "@CodigoReceita, " +
                        "@ObservacaoReceita, " +
                        "@TempoMisturaReceita)";

                    //Atualiza o Id da produção 
                    producao.id = idProd;

                    Command = SqlGlobalFuctions.ReturnCommand(query, Call);
                    Command.Parameters.AddWithValue("@Id", producao.id);
                    Command.Parameters.AddWithValue("@PesoTotalProducao", producao.pesoTotalProducao);
                    Command.Parameters.AddWithValue("@PesoTotalProduzido", producao.pesoTotalProduzido);
                    Command.Parameters.AddWithValue("@DataInicioProducao", producao.dateTimeInicioProducao);
                    Command.Parameters.AddWithValue("@DataFimProducao", producao.dateTimeFimProducao);
                    Command.Parameters.AddWithValue("@IniciouProducao", producao.IniciouProducao);
                    Command.Parameters.AddWithValue("@FinalizouProducao", producao.FinalizouProducao);
                    Command.Parameters.AddWithValue("@IdReceita", producao.receita.id);
                    Command.Parameters.AddWithValue("@NomeReceita", producao.receita.nomeReceita);
                    Command.Parameters.AddWithValue("@CodigoReceita", producao.receita.Codigo);
                    Command.Parameters.AddWithValue("@ObservacaoReceita", producao.receita.observacao);
                    Command.Parameters.AddWithValue("@TempoMisturaReceita", producao.receita.tempoMistura);
                    Call.Open();
                    ret = Command.ExecuteNonQuery();
                    Call.Close();
                    ret = 0;
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                    ret = -1;
                }

                return ret;
            }
            else
            {
                return ret;
            }
        }

        public static int IntoDate_Table_ProducaoProdutos(Utilidades.Producao producao)
        {
            int ret = -1;

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                foreach (var item in producao.receita.listProdutos)
                {
                    try
                    {
                        int idProd = -1;
                        string CommandString = "SELECT MAX(IdProducaoReceita) AS maxid FROM ProducaoProdutos";
                        dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);

                        dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                        DataTable Data = new DataTable();
                        Adapter.Fill(Data);

                        //last id is null?
                        if (DBNull.Value.Equals(Data.Rows[0][0]))
                            idProd = 1;
                        else
                            idProd = Convert.ToInt32(Data.Rows[0][0]) + 1;

                        dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);

                        Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);

                        string query = "INSERT into ProducaoProdutos (" +
                            "IdProducaoReceita, " +
                            "IdProduto, " +
                            "NomeProduto, " +
                            "Codigo, " +
                            "Observacao," +
                            "PesoProdutoReceita, " +
                            "PesoProdutoDesejado, " +
                            "PesoProdutoDosado, " +
                            "Tolerancia, " +
                            "IniciouDosagem, " +
                            "FinalizouDosagem) VALUES (" +
                            "@IdProducaoReceita, " +
                            "@IdProduto, " +
                            "@NomeProduto, " +
                            "@Codigo, " +
                            "@Observacao, " +
                            "@PesoProdutoReceita, " +
                            "@PesoProdutoDesejado, " +
                            "@PesoProdutoDosado, " +
                            "@Tolerancia, " +
                            "@IniciouDosagem, " +
                            "@FinalizouDosagem)";

                        Command = SqlGlobalFuctions.ReturnCommand(query, Call);
                        Command.Parameters.AddWithValue("@IdProducaoReceita", producao.id);
                        Command.Parameters.AddWithValue("@IdProduto", item.produto.id);
                        Command.Parameters.AddWithValue("@NomeProduto", item.produto.descricao);
                        Command.Parameters.AddWithValue("@Codigo", item.produto.codigo);
                        Command.Parameters.AddWithValue("@Observacao", item.produto.observacao);
                        Command.Parameters.AddWithValue("@PesoProdutoReceita", item.pesoProdutoReceita);
                        Command.Parameters.AddWithValue("@PesoProdutoDesejado", item.pesoProdutoDesejado);
                        Command.Parameters.AddWithValue("@PesoProdutoDosado", item.pesoProdutoDosado);
                        Command.Parameters.AddWithValue("@Tolerancia", item.tolerancia);
                        Command.Parameters.AddWithValue("@IniciouDosagem", item.iniciouDosagem);
                        Command.Parameters.AddWithValue("@FinalizouDosagem", item.finalizouDosagem);

                        Call.Open();
                        ret = Command.ExecuteNonQuery();
                        Call.Close();
                        ret = 0;
                    }
                    catch (Exception ex)
                    {
                        Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                        ret = -1;
                        break;
                    }
                }


                return ret;
            }
            else
            {
                return ret;
            }
        }

        public static int AddProducaoBD(Utilidades.Producao producao)
        {
            Utilidades.messageBox inputDialog;
            int ret = -1;

            if (IntoDate_Table_Producao(ref producao) != -1)
            {
                if (IntoDate_Table_ProducaoProdutos(producao) != -1)
                {
                    ret = 0;
                }
                else
                {
                    inputDialog = new Utilidades.messageBox("Falha inserir DB", "Falha ao inserir dados na tabela de produtos!", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                    inputDialog.ShowDialog();
                }
            }
            else
            {
                inputDialog = new Utilidades.messageBox("Falha inserir DB", "Falha ao inserir dados na tabela de produção!", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();
            }

            return ret;

        }

        public static void AtualizaOrdemProducaoEmExecucao()
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM Producao WHERE IniciouProducao = 'True' AND FinalizouProducao = 'False'";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            //Passo 0:
            //Remove todas as ordens da lista
            VariaveisGlobais.OrdensProducao.Clear();

            //Passo 1:
            //Carrega todas as Ordens que iniciou a produção e não finalizou
            if (!DBNull.Value.Equals(Data))
            {
                if (Data.Rows.Count >= 1)
                {
                    foreach (DataRow row in Data.Rows)
                    {
                        VariaveisGlobais.OrdensProducao.Add(new Utilidades.Producao()); //Cria uma nova produção

                        functions.DataRow_To_Producao(row);
                    }            
                }
            }

            //Passo 2:
            //Carrega todas as Ordens em execução no PLC

            //Passo 3:
            //Confere se o que tem no BD e PLC estão iguais

            //Passo 4:
            //Verifica se esta ordenado as ordens no PLC e BD, se não estiver irá realizar a ordenção do SUP de acordo com o PLC

            //Passo 5:
            //Caso não tenha a Ordem no PLC e tenha no BD, irá encerrar a Ordem no SUP

            //Passo 6:
            //Caso não tenha a Orden no BD e tenha no PLC, irá somente avisar, e aguardar o PLC encerrar a ordem
        }

        public static DataTable getProducaoProdutosFromIdProducao(int ID_Producao)
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM ProducaoProdutos Where IdProducaoReceita = '" + ID_Producao + "'";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);

                    Adapter.Fill(Data);

                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return Data;
        }

        public static int Update_PesoDosado_Produto(int idProducao, int codigoProduto, float valorDosado)
        {
            int ret = -1;
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string valDosado = valorDosado.ToString(CultureInfo.GetCultureInfo("en-US"));

                    string CommandString = "UPDATE ProducaoProdutos SET PesoProdutoDosado = '" + valDosado + "' WHERE IdProducaoReceita = " + idProducao + " AND " +
                        "IdProduto = " + codigoProduto + ";";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);

                    Call.Open();
                    ret = Command.ExecuteNonQuery();
                    Call.Close();
                    return ret;
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                    ret = -1;
                    return ret;
                }
            }
            else
            {
                return ret;
            }

        }

        public static int Update_PesoDosadoTotal(int idProducao, float valorDosado)
        {
            int ret = -1;
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string valDosado = valorDosado.ToString(CultureInfo.GetCultureInfo("en-US"));

                    string CommandString = "UPDATE Producao SET PesoTotalProduzido = '" + valDosado + "' WHERE Id = " + idProducao + ";";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);

                    Call.Open();
                    ret = Command.ExecuteNonQuery();
                    Call.Close();
                    return ret;
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                    ret = -1;
                    return ret;
                }
            }
            else
            {
                return ret;
            }

        }

        public static int Update_FinalizaProducao(int idProducao)
        {
            int ret = -1;
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    dynamic DTnow = new DateTime();
                    DTnow = DateTime.Now;
                    DTnow = DTnow.ToString("yyyyMMdd") + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;

                    string CommandString = "UPDATE Producao SET FinalizouProducao = 'true', DataFimProducao = '" + DTnow + "'  WHERE Id = " + idProducao + ";";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);

                    Call.Open();
                    ret = Command.ExecuteNonQuery();
                    Call.Close();
                    return ret;
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                    ret = -1;
                    return ret;
                }
            }
            else
            {
                return ret;
            }

        }

        public static int Update_IniciouDosagemProduto(int idProducao, int idProduto)
        {
            int ret = -1;
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "UPDATE ProducaoProdutos SET IniciouDosagem = 'true' WHERE IdProducaoReceita = " + idProducao + " AND IdProduto = " + idProduto  + ";";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);

                    Call.Open();
                    ret = Command.ExecuteNonQuery();
                    Call.Close();
                    return ret;
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                    ret = -1;
                    return ret;
                }
            }
            else
            {
                return ret;
            }
        }

        public static int Update_FinalizouDosagemProduto(int idProducao, int idProduto)
        {
            int ret = -1;
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "UPDATE ProducaoProdutos SET FinalizouDosagem = 'true' WHERE IdProducaoReceita = " + idProducao + " AND IdProduto = " + idProduto + ";";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Producao_GS);
                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);

                    Call.Open();
                    ret = Command.ExecuteNonQuery();
                    Call.Close();
                    return ret;
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                    ret = -1;
                    return ret;
                }
            }
            else
            {
                return ret;
            }
        }
    }
}
