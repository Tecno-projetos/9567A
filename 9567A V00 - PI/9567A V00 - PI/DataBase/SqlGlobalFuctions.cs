using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9567A_V00___PI.DataBase
{ 

    public class SqlGlobalFuctions
    {
    #region SqlConnection, SqlCeCommand, SqlCeDataAdapter

    /// <summary>
    /// Função para retornar a SqlConnection que está lidando com a parte da comunicação física entre o aplicativo C# e o banco de dados do SQL Server.
    /// </summary>
    /// <param name="Connection">Enviar a conexão do Banco de dados (SQLExpress ou SQLCe)</param>
    /// <returns> Retorna o tipo de SQL Connection </returns>
    public static dynamic ReturnCall(string Connection)
    {
        if (Utilidades.VariaveisGlobais.SQLCe_GS)
        {
            SqlCeConnection Call = new SqlCeConnection(Connection);
            return Call;
        }
        else
        {
            SqlConnection Call = new SqlConnection(Connection);
            return Call;
        }
    }

    /// <summary>
    /// Função para Retornar comandos SQL em um banco de dados. Ele envia um comando SQL para um banco de dados especificado por um objeto SqlConnection(Call).
    /// </summary>
    /// <param name="CommandString">Envio do comando em SQL</param>
    /// <param name="Call">Tipo de conexão SqlConnection ou SqlCeConnection</param>
    /// <returns>Retorno do command do SQL</returns>
    public static dynamic ReturnCommand(string CommandString, dynamic Call)
    {
        if (Utilidades.VariaveisGlobais.SQLCe_GS)
        {
            SqlCeCommand Command = new SqlCeCommand(CommandString, Call);
            return Command;
        }
        else
        {
            SqlCommand Command = new SqlCommand(CommandString, Call);
            return Command;
        }
    }

    /// <summary>
    /// Função para Retornar o Adapter que fornece a comunicação entre o conjunto de dados e o banco de dados SQL.
    /// </summary>
    /// <param name="CommandString">Envio do comando em SQL</param>
    /// <param name="Connection">Enviar a conexão do Banco de dados (SQLExpress ou SQLCe)</param>
    /// <returns>Retorna o Adapter do SQL</returns>
    public static dynamic ReturnAdapter(string CommandString, string Connection)
    {
        if (Utilidades.VariaveisGlobais.SQLCe_GS)
        {
            SqlCeDataAdapter Adapter = new SqlCeDataAdapter(CommandString, Connection);
            return Adapter;
        }
        else
        {
            SqlDataAdapter Adapter = new SqlDataAdapter(CommandString, Connection);
            return Adapter;
        }
    }

    #endregion

    #region AutoDelete
    /// <summary>
    /// Função para auto deletar dados de um Banco de dados, Chamda em um Dispatcher Timer
    /// </summary>
    /// <param name="TableName">Tabela que deseja deletar dados a cada X tempo</param>
    /// <param name="Connection">Conexão do banco de dados que se encontra a tabela citada no parâmentro anterior </param>
    /// <param name="Month">Quantidade de meses para apagar dados antigos Valores = 1/6/12/24(1 mes, 6 meses, 12 meses, 24 meses) ou mais </param>
    private static void AutoDelete(string TableName, string Connection, int Month)
    {
        try
        {
            //Month
            //day
            string CommandString = "DELETE FROM " + TableName + " WHERE DateNow < DATEADD(Month," + Month * -1 + ", GETDATE())";

            dynamic Call = ReturnCall(Connection);

            dynamic Command = ReturnCommand(CommandString, Call);

            Call.Open();
            Command.ExecuteNonQuery();
            Call.Close();

        }
        catch (Exception ex)
        {
            Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
        }
    }

    /// <summary>
    /// Função para deletar a partir do ID da produção
    /// </summary>
    /// <param name="TableName">Tabela que deseja deletar dados a cada X tempo</param>
    /// <param name="Connection">Conexão do banco de dados que se encontra a tabela citada no parâmentro anterior </param>
    /// <param name="IdProducao">Id da produção que deseja excluir.</param>
    /// <param name="NomeIDparaDeletar">Nome da coluna que possui o Id da produção.</param>
    private static void AutoDeleteFromIDProducao(string TableName, string Connection, int IdProducao, string NomeIDparaDeletar)
    {
        try
        {
            string CommandString = "DELETE FROM " + TableName + "  WHERE " + NomeIDparaDeletar + "  = '" + IdProducao + "' ";

            dynamic Call = ReturnCall(Connection);

            dynamic Command = ReturnCommand(CommandString, Call);

            Call.Open();
            Command.ExecuteNonQuery();
            Call.Close();

        }
        catch (Exception ex)
        {
            Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
        }
    }

    /// <summary>
    /// Seleciona todas as produção apartir do setPoint desejado para apagar.
    /// </summary>
    /// <param name="TableName">Nome da tabela que deseja apagar</param>
    /// <param name="Connection">Conexão do banco de dados que se encontra a tabela citada no parâmentro anterior </param>
    /// <param name="Month">Quantidade de meses para apagar dados antigos Valores = 1/6/12/24(1 mes, 6 meses, 12 meses, 24 meses) ou mais </param>
    /// <returns>Retorna um datatable com todas as produções</returns>
    private static DataTable AutoSelectProducao(string TableName, string Connection, int Month)
    {
        DataTable Data = new DataTable();

        try
        {
            //Month
            //day
            string CommandString = "SELECT * FROM " + TableName + " WHERE DataFimProducao < DATEADD(Month," + Month * -1 + ", GETDATE())";

            dynamic Call = SqlGlobalFuctions.ReturnCall(Connection);

            dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Connection);

            Adapter.Fill(Data);
        }
        catch (Exception ex)
        {
            Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
        }

        return Data;

    }

    /// <summary>
    /// Função para deletar as tabelas a partir do mes selecionado
    /// </summary>
    /// <param name="meses">Quantidade de meses que quer apagar</param>
    public static void AutoDelete(int meses)
    {
        try
        {
            DataTable Data = new DataTable();

            //Data = DataBase.SqlGlobalFuctions.AutoSelectProducao("Producao", Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS, meses);

            //foreach (DataRow row in Data.Rows)
            //{
            //    int DeletarID;
            //    DeletarID = Convert.ToInt32(row["Id"]);

            //    DataBase.SqlGlobalFuctions.AutoDeleteFromIDProducao("Ensaques", Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS, DeletarID, "IdEnsaque");
            //    DataBase.SqlGlobalFuctions.AutoDeleteFromIDProducao("ProducaoEnsaque", Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS, DeletarID, "IdProducao");
            //    DataBase.SqlGlobalFuctions.AutoDeleteFromIDProducao("Bateladas", Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS, DeletarID, "IdProducao");
            //    DataBase.SqlGlobalFuctions.AutoDeleteFromIDProducao("Producao", Utilidades.VariaveisGlobais.Connection_DB_Receitas_GS, DeletarID, "Id");
            //}

            //Limpa Historio de Alarmes
            DataBase.SqlGlobalFuctions.AutoDelete("EquipAlarmEvent", Utilidades.VariaveisGlobais.Connection_DB_Equip_GS, meses);

            //Limpa dicionario de Logs
            DataBase.SqlGlobalFuctions.deleteLogs(meses);
        }
        catch (Exception ex)
        {

            Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
        }
    }

    /// <summary>
    /// Deleta os arquicos da pasta Logs apartir de um tempo selecionado
    /// </summary>
    /// <param name="meses">Meses * 30 = Apaga arquivos maiores ou igual a essa data</param>
    public static void deleteLogs(int meses)
    {
        try
        {
            meses = meses * 30;

            DirectoryInfo d = new DirectoryInfo(@"C:\Logs");

            foreach (FileInfo item in d.GetFiles())
            {
                //Console.WriteLine(item.Name);
                //Pega a diferença(tempo) entre a ultima data de escrita e hoje
                TimeSpan t = DateTime.Now.Subtract(item.LastWriteTime);
                //Diferença em dias
                //Console.WriteLine(t.Days);
                // Se ultima vez que o arq foi escrito for maior q 10 dias, deleta o arq
                if (t.Days >= meses)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = "Apagado o LOG com o nome: " + item.Name;
                    item.Delete();

                }

            }
        }
        catch (Exception ex)
        {

            Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
        }
    }
    #endregion

    #region Create and Exist DB SQLExpress or SQLCe

    /// <summary>
    /// Função para criar Banco de Dados SQLCe e  SQL Express
    /// Se a opção SQLCe estiver selecionada o irá criar umas pasta chamda SQLCe onde encontra todos os banco.
    /// </summary>
    /// <param name="NameDB">Nome para o Banco de dados que irá ser criado.</param>
    public static void Create_DB(string NameDB)
    {
        try
        {
            if (Utilidades.VariaveisGlobais.SQLCe_GS)
            {
                string folder = @"C:\SQLCe"; //nome do diretorio a ser criado
                                             //Se o diretório não existir...
                if (!Directory.Exists(folder))
                {
                    //Criamos um com o nome folder
                    Directory.CreateDirectory(folder);

                }

                if (Directory.Exists(folder))
                {
                    //String utilizada para criar o Banco SQLCe
                    string connStr = @"Data Source =" + folder + "\\" + NameDB + ".sdf";

                    //Caminho para Verificar se existe o banco
                    string Caminho = folder + "\\" + NameDB + ".sdf";

                    if (!(File.Exists(Caminho)))
                    {
                        SqlCeEngine engine = new SqlCeEngine(connStr);
                        engine.CreateDatabase();
                    }
                }
            }
            else
            {
                if (!Exist_DB(NameDB))
                {
                    string str;

                    SqlConnection myConn = new SqlConnection(Utilidades.VariaveisGlobais.Connection_DB_Create_GS);

                    str = "CREATE DATABASE " + NameDB;

                    SqlCommand myCommand = new SqlCommand(str, myConn);

                    myConn.Open();
                    myCommand.ExecuteNonQuery();

                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
        }
    }

    /// <summary>
    /// Função para criar Banco de Dados APENAS SQL Express
    /// </summary>
    /// <param name="NameDB">Nome para o Banco de dados que irá ser criado.</param>
    /// <param name="Connection">Conexão do banco de dados para ser criado</param>      
    public static void Create_DB(string NameDB, string Connection)
    {
        if (!Exist_DB(NameDB, Connection))
        {
            string str;
            SqlConnection myConn = new SqlConnection(Connection);

            str = "CREATE DATABASE " + NameDB;

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();

            }
            catch (System.Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }

        }
    }

    /// <summary>
    /// Função para verificar se já existe Banco de dadops SQL Express 
    /// </summary>
    /// <param name="NameDB">Nome para verificar se já existe esse banco</param>
    /// <returns></returns>
    public static bool Exist_DB(string NameDB)
    {
        bool exist = false;
        try
        {
            string str;

            SqlConnection myConn = new SqlConnection(Utilidades.VariaveisGlobais.Connection_DB_Create_GS);

            str = "SELECT count(*) FROM sysdatabases WHERE name= " + "'" + NameDB + "'";

            SqlCommand myCommand = new SqlCommand(str, myConn);

            myConn.Open();

            exist = Convert.ToBoolean(myCommand.ExecuteScalar());

        }
        catch (Exception ex)
        {
            Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
        }

        if (exist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Função para verificar se já existe Banco de dadops SQL Express 
    /// </summary>
    /// <param name="NameDB">Nome para o Banco de dados que irá ser criado.</param>
    /// <param name="Connection">Conexão do banco de dados para ser criado</param>
    /// <returns></returns>
    public static bool Exist_DB(string NameDB, string Connection)
    {
        bool exist = false;
        try
        {
            string str;

            SqlConnection myConn = new SqlConnection(Connection);
            str = "SELECT count(*) FROM sysdatabases WHERE name= " + "'" + NameDB + "'";
            SqlCommand myCommand = new SqlCommand(str, myConn);

            myConn.Open();

            exist = Convert.ToBoolean(myCommand.ExecuteScalar());
        }
        catch (Exception ex)
        {
            Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
        }

        if (exist)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    #endregion

}
}
