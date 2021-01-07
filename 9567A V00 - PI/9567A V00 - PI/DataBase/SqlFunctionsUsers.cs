using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace _9567A_V00___PI.DataBase
{
    public class SqlFunctionsUsers
    {

        //Return to DBLogin this created 
        public static bool Initialize_ProgramDBCA()
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);
                    Call.Open();
                    Data = Call.GetSchema("Tables");
                    Call.Close();
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();

                }
            }

            if (Data.Rows.Count >= 1)
            {
                return true;
            }
            else
            {
                if (!ExistTableDBCA("Automasul"))
                {
                    CreateTableDBCA("Automasul");
                    IntoDateDBCA("Automasul", MD5Cryptography("8887"), "Administrador", "automacao@automasul.com", "Criou");
                }
                return false;
            }
        }

        public static DataTable GetAllTablesDBCA()
        {
            DataTable Data = new DataTable();


            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM information_schema.tables;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return Data;
        }

        public static DataTable GetTableDBCA(string TableName)
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM " + TableName + "";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }

            }

            return Data;
        }

        public static DataTable Get_Table(string TableName, DateTime dtIn, DateTime dtOut)
        {
            DataTable Data1 = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {

                    dynamic DTIn;
                    dynamic DTOut;

                    if (Utilidades.VariaveisGlobais.SQLCe_GS)
                    {
                        DTIn = dtIn.ToString("yyyyMMdd") + " " + dtIn.Hour + ":" + dtIn.Minute;
                        DTOut = dtOut.ToString("yyyyMMdd") + " " + dtOut.Hour + ":" + dtOut.Minute;
                    }
                    else
                    {
                        DTIn = dtIn;
                        DTOut = dtOut;
                    }



                    string CommandString = "SELECT * FROM " + TableName + " WHERE DateNow >= '" + DTIn + "' AND DateNow <= '" + DTOut + "'";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);
                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    Adapter.Fill(Data1);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return Data1;
        }


        public static DataTable GetTableDBCA_DescID(string TableName)
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM " + TableName + "ORDER BY IdLogin DESC;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return Data;
        }

        //Insert value in DBLogin
        public static void IntoDateDBCA(string TableName, string Password, string GroupUser, string Email, string EventLog)
        {
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    int IdLogin;

                    string CommandString = "SELECT MAX(IdLogin) AS maxid FROM " + TableName + "";
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Users_GS);
                    DataTable Data = new DataTable();
                    Adapter.Fill(Data);

                    //last id is null?
                    if (DBNull.Value.Equals(Data.Rows[0][0]))
                        IdLogin = 0;
                    else
                        IdLogin = Convert.ToInt32(Data.Rows[0][0]) + 1;


                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);

                    //parametros
                    Command.CommandText = "INSERT into " + TableName + " (IdLogin, Password, GroupUser, EventLog, Email, Time_actual, DateNow) VALUES (@IdLogin, @Password, @GroupUser,@EventLog,@Email,@Time_actual, @DateNow)";
                    Command.Parameters.AddWithValue("@IdLogin", IdLogin);
                    Command.Parameters.AddWithValue("@Password", Password);
                    Command.Parameters.AddWithValue("@GroupUser", GroupUser);
                    Command.Parameters.AddWithValue("@EventLog", EventLog);
                    Command.Parameters.AddWithValue("@Email", Email);
                    Command.Parameters.AddWithValue("@Time_actual", DateTime.Now.ToString());
                    Command.Parameters.AddWithValue("@DateNow", DateTime.Now);
                    Call.Open();
                    Command.ExecuteNonQuery();
                    Call.Close();
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }
        }

        //==== BUT FIRST REMEMBER TO CHECK IF THERE IS A TABLE WITH THE NAME YOU WANT TO CREATE====
        public static void CreateTableDBCA(string TableName)
        {
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "CREATE TABLE " + TableName + "(IdLogin int not null primary key,Password nvarchar(100),GroupUser nvarchar(30),EventLog nvarchar(30),Email nvarchar(50),Time_actual nvarchar(50), DateNow datetime);";
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);
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

        //Verify if exist table in DBCronograma_Automasul
        public static bool ExistTableDBCA(string TableName)
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    //Metodo para verificar se existe a tabela pesquisada

                    string CommandString = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + TableName + "'";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);
                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            if (Data.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get last value from Table in column especific
        public static string GetLastValueTableDBCA(string TableName, string column)
        {
            DataTable Data = new DataTable();
            int IdLogin = 0;

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {

                    string CommandString = "SELECT * FROM " + TableName + "";

                    //DataBase.OpenDataBase();
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    Adapter.Fill(Data);

                    //pega a ultima linha da tabela
                    IdLogin = (Data.Rows.Count - 1);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }
            //seleciona a linha atraves do IdLogin que recebeu a ultima linha
            DataRow linha = Data.Rows[IdLogin];

            return (linha[column].ToString());
        }

        //Return password correct or no
        public static bool CheckPasswordDBCA(string TableName, string Password)
        {
            DataTable Data = new DataTable();
            int IdLogin = 0;

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM " + TableName + "";

                    //DataBase.OpenDataBase();
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    Adapter.Fill(Data);

                    //pega a ultima linha da tabela
                    IdLogin = (Data.Rows.Count - 1);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            //seleciona a linha atraves do IdLogin que recebeu a ultima linha
            DataRow linha = Data.Rows[IdLogin];

            //verifica se a senha recebida confere com a ultima senha do banco de dados cadastrada.
            if ((linha["Password"].ToString()) == MD5Cryptography(Password))
            {
                return true;
            }
            {
                return false;
            }

        }

        //Return value encrypted
        public static string MD5Cryptography(string Pass)
        {

            StringBuilder Password = new StringBuilder();

            try
            {
                MD5 CreateCryptography = MD5.Create();

                byte[] Input = Encoding.ASCII.GetBytes(Pass);
                byte[] hash = CreateCryptography.ComputeHash(Input);


                for (int i = 0; i < hash.Length; i++)
                {
                    Password.Append(hash[i].ToString("X2"));
                }
            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }

            return Password.ToString();
        }

        public static bool DropTableDBCA(string TableName)
        {
            string CommandString = "DROP TABLE " + TableName + "";

            try
            {
                dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);
                Call.Open();

                dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);
                Command.ExecuteNonQuery();

                Call.Close();
                return true;

            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();

                return false;

            }
        }

        //Função principal para pegar todas as planilhas de usuários e dar o merge
        public static DataTable GetAllTables(DateTime DTIn, DateTime DTOut)
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                DataTable Data_Schema = new DataTable();

                DataTable DataAux = new DataTable();

                dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);
                Call.Open();

                Data_Schema = Call.GetSchema("Tables");

                Call.Close();

                foreach (DataRow Row in Data_Schema.Rows)
                {
                    Data.Merge(GetAllTablesAux((string)Row.ItemArray[2], DTIn, DTOut));
                }
            }
            return Data;
        }

        public static DataTable GetAllTablesAux(string TableName, DateTime DTIn, DateTime DTOut)
        {
            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM " + TableName + " WHERE DateNow >= '" + DTIn + "' AND DateNow <= '" + DTOut + "' ORDER BY IdLogin DESC;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, (Utilidades.VariaveisGlobais.Connection_DB_Users_GS));

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }

                Data.Columns.Add("User", typeof(System.String));
                Data.Columns.Remove("Password");
                Data.Columns.Remove("Email");
                Data.Columns.Remove("IdLogin");
                Data.Columns.Remove("Time_actual");

                Data.Columns["User"].SetOrdinal(0);

                foreach (DataRow Row in Data.Rows)
                {
                    Row["User"] = TableName;
                }

            }

            return Data;
        }

        //Adiciona a coluna DateNow (datetime) em todas as planilhas e copia o valor da Time_actual para o DateNow.
        public static void AddColumnTime()
        {
            try
            {
                if (Utilidades.VariaveisGlobais.DB_Connected_GS)
                {
                    DataTable Data_Schema = new DataTable();
                    DataTable Data = new DataTable();

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Users_GS);
                    Call.Open();

                    Data_Schema = Call.GetSchema("Tables");

                    foreach (DataRow Row in Data_Schema.Rows)
                    {

                        string CommandString = "ALTER TABLE " + Row["TABLE_NAME"] + " ADD DateNow datetime;";

                        SqlCommand Comand = new SqlCommand(CommandString, Call);

                        Comand.ExecuteNonQuery();

                        CommandString = "SELECT * FROM " + Row["TABLE_NAME"] + " ";


                        SqlDataAdapter Adapter = new SqlDataAdapter(CommandString, (Utilidades.VariaveisGlobais.Connection_DB_Users_GS));

                        Adapter.Fill(Data);


                        foreach (DataRow RowAux in Data.Rows)
                        {
                            Comand.CommandText = "UPDATE " + Row["TABLE_NAME"] + " SET DateNow = '" + Convert.ToDateTime((string)RowAux.ItemArray[5]) + "' WHERE IdLogin = " + RowAux.ItemArray[0] + ";";

                            Comand.ExecuteNonQuery();
                        }

                    }

                    Call.Close();
                }
            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }
        }

    }
}
