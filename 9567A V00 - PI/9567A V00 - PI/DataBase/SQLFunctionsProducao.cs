using _9567A_V00___PI.DataBase;
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
                            "PesoTotalProducao real," +    //FK do Id da receita     
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
                            "IdReceita int," +
                            "CONSTRAINT FK_IdReceitaBase FOREIGN KEY (IdReceitaBase) REFERENCES Receitas(Id), " +
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

        public static void Create_Table_Bateladas()
        {
            if (!ExistTable("Bateladas"))
            {
                if (Utilidades.VariaveisGlobais.DB_Connected_GS)
                {
                    try
                    {
                        string CommandString = "CREATE TABLE ProducaoProdutos (" +
                            "IdProducaoReceita int not null," +
                            "IdProduto nvarchar int," +
                            "NomeProduto nvarchar(100)," +
                            "CodigoProduto bigint," +
                            "ObservaocaoProduto nvarchar(300)," +
                            "CONSTRAINT FK_IdProducaoReceita FOREIGN KEY (IdProducaoReceita) REFERENCES PremixProdutos(Codigo));";

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
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);
                    DataTable Data = new DataTable();
                    Adapter.Fill(Data);

                    //last id is null?
                    if (DBNull.Value.Equals(Data.Rows[0][0]))
                        idProd = 1;
                    else
                        idProd = Convert.ToInt32(Data.Rows[0][0]) + 1;

                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);

                    Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    string query = "INSERT into Producao (" +
                        "Id, " +
                        "IdReceitaBase, " +
                        "QtdBateladas, " +
                        "TempoPreMistura, " +
                        "TempoPosMistura, " +
                        "PesoTotalProducao, " +
                        "PesoTotalProduzido, " +
                        "VolumeTotalProducao, " +
                        "VolumeTotalProduzido, " +
                        "CodigoProdutoDosagemAutomaticaSilo1, " +
                        "CodigoProdutoDosagemAutomaticaSilo2, " +
                        "DataInicioProducao, " +
                        "DataFimProducao, " +
                        "IniciouProducao," +
                        "FinalizouProducao) VALUES (" +
                        "@Id, " +
                        "@IdReceitaBase, " +
                        "@QtdBateladas, " +
                        "@TempoPreMistura, " +
                        "@TempoPosMistura, " +
                        "@PesoTotalProducao, " +
                        "@PesoTotalProduzido, " +
                        "@VolumeTotalProducao, " +
                        "@VolumeTotalProduzido, " +
                        "@CodigoProdutoDosagemAutomaticaSilo1, " +
                        "@CodigoProdutoDosagemAutomaticaSilo2, " +
                        "@DataInicioProducao, " +
                        "@DataFimProducao," +
                        "@IniciouProducao," +
                        "@FinalizouProducao)";

                    //Atualiza o Id da produção 
                    producao.id = idProd;

                    Command = SqlGlobalFuctions.ReturnCommand(query, Call);
                    Command.Parameters.AddWithValue("@Id", producao.id);
                    Command.Parameters.AddWithValue("@IdReceitaBase", producao.IdReceitaBase);
                    Command.Parameters.AddWithValue("@QtdBateladas", producao.quantidadeBateladas);
                    Command.Parameters.AddWithValue("@TempoPreMistura", producao.tempoPreMistura);
                    Command.Parameters.AddWithValue("@TempoPosMistura", producao.tempoPosMistura);
                    Command.Parameters.AddWithValue("@PesoTotalProducao", producao.pesoTotalProducao);
                    Command.Parameters.AddWithValue("@PesoTotalProduzido", producao.pesoTotalProduzido);
                    Command.Parameters.AddWithValue("@VolumeTotalProducao", producao.volumeTotalProducao);
                    Command.Parameters.AddWithValue("@VolumeTotalProduzido", producao.volumeTotalProduzido);
                    Command.Parameters.AddWithValue("@CodigoProdutoDosagemAutomaticaSilo1", producao.CodigoProdutoDosagemAutomaticaSilo1);
                    Command.Parameters.AddWithValue("@CodigoProdutoDosagemAutomaticaSilo2", producao.CodigoProdutoDosagemAutomaticaSilo2);
                    Command.Parameters.AddWithValue("@DataInicioProducao", producao.dateTimeInicioProducao);
                    Command.Parameters.AddWithValue("@DataFimProducao", producao.dateTimeFimProducao);
                    Command.Parameters.AddWithValue("@IniciouProducao", producao.IniciouProducao);
                    Command.Parameters.AddWithValue("@FinalizouProducao", producao.FinalizouProducao);
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

        public static int IntoDate_Table_Bateladas(ref Utilidades.Producao producao)
        {
            int ret = -1;

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    Call.Open();

                    foreach (var bateladas in producao.batelada)
                    {
                        foreach (var ProdBateladas in bateladas.produtos)
                        {
                            

                            string query = "INSERT into Bateladas (" +
                                "IdProducao, " +
                                "CodigoProduto, " +
                                "ValorDesejado, " +
                                "ValorDosado, " +
                                "NumeroBatelada) VALUES (" +
                                "@IdProducao, " +
                                "@CodigoProduto, " +
                                "@ValorDesejado, " +
                                "@ValorDosado, " +
                                "@NumeroBatelada)";

                            dynamic Command = SqlGlobalFuctions.ReturnCommand(query, Call);
                            Command.Parameters.AddWithValue("@IdProducao", producao.id);
                            Command.Parameters.AddWithValue("@CodigoProduto", ProdBateladas.codigo);
                            Command.Parameters.AddWithValue("@ValorDesejado", ProdBateladas.pesoDesejado);
                            Command.Parameters.AddWithValue("@ValorDosado", ProdBateladas.pesoDosado);
                            Command.Parameters.AddWithValue("@NumeroBatelada", bateladas.numeroBatelada);

                            ret = Command.ExecuteNonQuery();
                            
                            ret = 0;
                        }
                    }

                    Call.Close();

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

        public static int AddProducao(Utilidades.Producao producao)
        {
            Utilidades.messageBox inputDialog;
            int ret = -1;

            if (IntoDate_Table_Producao(ref producao) != -1)
            {
                if (IntoDate_Table_Bateladas(ref producao) != -1)
                {
                    ret = 0;
                }
                else
                {
                    inputDialog = new Utilidades.messageBox("Falha inserir DB", "Falha ao inserir dados na tabela de bateladas!", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

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

        public static DataTable getBateladaFromIdProducaoANDNumeroBatelada(int IdProducao, int NumeroBatelada)
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM Bateladas WHERE IdProducao = '"+IdProducao+"' AND NumeroBatelada = '"+ NumeroBatelada+"'";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return Data;
        }

        public static void AtualizaProducaoEmExecucao()
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM Producao WHERE IniciouProducao = 'True' AND FinalizouProducao = 'False'";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            if (!DBNull.Value.Equals(Data))
            {
                if (Data.Rows.Count >=1)
                {
                    functions.DataRow_To_Producao(Data.Rows[0], ref VariaveisGlobais.ProducaoReceita);
                }
                else
                {
                    VariaveisGlobais.ProducaoReceita = new Producao();
                }

            }
            else
            {
                VariaveisGlobais.ProducaoReceita = new Producao();
            }
                 
        }

        public static int Update_PesoDosado_ProdutoBatelada(int idProducao, int numeroBatelada, string codigoProduto, float valorDosado)
        {
            int ret = -1;
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string valDosado = valorDosado.ToString(CultureInfo.GetCultureInfo("en-US"));

                    string CommandString = "UPDATE Bateladas SET ValorDosado = '" + valDosado + "' WHERE IdProducao = " + idProducao + " AND " +
                        "CodigoProduto = "+codigoProduto+" AND " +
                        "NumeroBatelada = "+numeroBatelada+";";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);
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

        public static int Update_Finaliza_Producao(float pesoTotalProduzido, float volumeTotalProduzido)
        {
            int ret = -1;
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string pesoTotalProd = pesoTotalProduzido.ToString(CultureInfo.GetCultureInfo("en-US"));
                    dynamic DTnow = new DateTime();
                    DTnow = DateTime.Now;

                    DTnow = DTnow.ToString("yyyyMMdd") + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;

                    string CommandString = "UPDATE Producao SET FinalizouProducao = 'true', DataFimProducao = '"+ DTnow + "', PesoTotalProduzido = '"+ pesoTotalProd + "', VolumeTotalProduzido = '"+ volumeTotalProduzido + "' WHERE FinalizouProducao = 'false';";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);
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

        public static Int32 getCoutReceitaBase(int IdReceitaBase)
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT COUNT(*) FROM Producao WHERE IdReceitaBase = '" + IdReceitaBase + "'";


                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return Convert.ToInt32(Data.Rows[0][0]);
        }

        public static int getLast_ID_Producao()
        {
            DataTable Data = new DataTable();

            int id = -1;

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT MAX(Id) AS maxid FROM Producao";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    Adapter.Fill(Data);

                    //last id is null?
                    if (!(DBNull.Value.Equals(Data.Rows[0][0])))
                        id = Convert.ToInt32(Data.Rows[0][0]);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return id;
        }

        public static int getID_Receita_Base_From_Id_Producao(int ID_Producao)
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT IdReceitaBase FROM Producao Where Id = '" + ID_Producao + "'";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS);

                    Adapter.Fill(Data);

                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return Convert.ToInt32(Data.Rows[0][0]);
        }

    }
}
