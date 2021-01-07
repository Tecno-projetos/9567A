using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace _9567A_V00___PI.DataBase
{
    public class SqlFunctionsEquips
    {

        public static bool Ping_DB_Success = false;

        public static string Nome = "";

        public static bool Enable_Read_Alarm = true;


        #region Checks whether tables exist and creates them

        //============================================================================================== Checks whether tables exist and creates them ====================================================================================================
        public static void ExistTable()
        {

            DataTable Data_EquipAlarmEvent = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString_EquipAlarmEvent = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EquipAlarmEvent';";

                    dynamic Call_EquipAlarmEvent = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);
                    dynamic Adapter_EquipAlarmEvent = SqlGlobalFuctions.ReturnAdapter(CommandString_EquipAlarmEvent, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    Adapter_EquipAlarmEvent.Fill(Data_EquipAlarmEvent);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }

                if (!(Data_EquipAlarmEvent.Rows.Count > 0))
                {
                    Create_Table_EquipAlarmEvent();
                }
            }
        }


        #endregion

        #region All control of the table EquipAlarmEvent

        //============================================================================================== All control of the table EquipAlarmEvent ========================================================================================================

        private static void Create_Table_EquipAlarmEvent()
        {
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    //==== BUT BEFORE REMEMBER TO CHECK IF THERE IS A JA TABLE WITH THE NAME YOU WANT TO ====
                    string CommandString = "CREATE TABLE EquipAlarmEvent (Id int not null primary key,TagEquip nvarchar(50), Description nvarchar(100),Event bit,Active bit,Priority int,Ack bit, UserAck nvarchar(100), UserLogged nvarchar(100), GroupUserLogged nvarchar(100), DateIn nvarchar(50), DateOut nvarchar(50), DateAck nvarchar(50), DateNow datetime, Obs nvarchar(150));";
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);
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

        public static void IntoDate_Table_EquipAlarmEvent(string TagEquip, string Description, bool Event, bool Active, int Priority, bool Ack, string UserAck, string UserLogged, string GroupUserLogged, string DateIn, string DateOut, string DateAck, DateTime DateNow, string Obs)
        {
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    int Id;

                    string CommandString = "SELECT MAX(Id) AS maxid FROM EquipAlarmEvent";
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);


                    //last id
                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);
                    DataTable Data = new DataTable();
                    Adapter.Fill(Data);

                    //last id is null?
                    if (DBNull.Value.Equals(Data.Rows[0][0]))
                        Id = 0;
                    else
                        Id = Convert.ToInt32(Data.Rows[0][0]) + 1;


                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);

                    //parametros
                    Command.CommandText = "INSERT into EquipAlarmEvent (Id, TagEquip, Description, Event, Active, Priority, Ack, UserAck, UserLogged, GroupUserLogged, DateIn, DateOut, DateAck, DateNow, Obs) VALUES (@Id, @TagEquip, @Description, @Event, @Active, @Priority, @Ack, @UserAck, @UserLogged, @GroupUserLogged, @DateIn, @DateOut, @DateAck, @DateNow, @Obs)";
                    Command.Parameters.AddWithValue("@Id", Id);
                    Command.Parameters.AddWithValue("@TagEquip", TagEquip);
                    Command.Parameters.AddWithValue("@Description", Description);
                    Command.Parameters.AddWithValue("@Event", Event);
                    Command.Parameters.AddWithValue("@Active", Active);
                    Command.Parameters.AddWithValue("@Priority", Priority);
                    Command.Parameters.AddWithValue("@Ack", Ack);
                    Command.Parameters.AddWithValue("@UserAck", UserAck);
                    Command.Parameters.AddWithValue("@UserLogged", UserLogged);
                    Command.Parameters.AddWithValue("@GroupUserLogged", GroupUserLogged);
                    Command.Parameters.AddWithValue("@DateIn", DateIn);
                    Command.Parameters.AddWithValue("@DateOut", DateOut);
                    Command.Parameters.AddWithValue("@DateAck", DateAck);
                    Command.Parameters.AddWithValue("@DateNow", DateNow);
                    Command.Parameters.AddWithValue("@Obs", Obs);
                    Call.Open();
                    Command.ExecuteNonQuery();
                    Call.Close();

                    Enable_Read_Alarm = true;
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }
        }

        public static bool VerifyRowExist_Table_EquipAlarmEvent(string TagEquip)
        {

            DataTable Data = new DataTable();
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT MAX(Id) AS maxid FROM EquipAlarmEvent WHERE TagEquip = '" + TagEquip + "'";
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);
                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);
                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            //last id is null?
            if (DBNull.Value.Equals(Data.Rows[0][0]))
                return false;
            else
                return true;
        }

        public static bool GetBoolLastRowActive_Table_EquipAlarmEvent(string TagEquip, string Description)
        {

            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM EquipAlarmEvent WHERE TagEquip = '" + TagEquip + "' AND Description = '" + Description + "' ORDER BY Id DESC;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);
                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }



            if (!(Data.Rows.Count > 0))
            {
                return false;
            }
            else
            {
                DataRow Linha = Data.Rows[0];

                if (Linha.ItemArray[4].ToString() == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static int GetIntLastRowActive_Table_EquipAlarmEvent(string TagEquip, string Description)
        {

            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM EquipAlarmEvent WHERE TagEquip = '" + TagEquip + "' AND Description = '" + Description + "' ORDER BY Id DESC;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);
                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }


            DataRow Linha = Data.Rows[0];

            return Convert.ToInt32(Linha.ItemArray[0]);

        }

        public static bool UpdateActiveFalse_Table_EquipAlarmEvent(string TagEquip, int Id)
        {

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "UPDATE EquipAlarmEvent SET Active = 'False', DateOut = '" + DateTime.Now.ToString() + "' WHERE Id = " + Id + ";";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);
                    Call.Open();

                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);
                    Command.ExecuteNonQuery();

                    Call.Close();

                    Enable_Read_Alarm = true;
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return true;

        }

        public static bool UpdateAckTrue_Table_EquipAlarmEvent(string TagEquip, string UserLogged, int Id)
        {
            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "UPDATE EquipAlarmEvent SET Ack = 'True', DateAck = '" + DateTime.Now.ToString() + "', UserAck = '" + UserLogged + "' WHERE Id = " + Id + ";";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);
                    Call.Open();

                    dynamic Command = SqlGlobalFuctions.ReturnCommand(CommandString, Call);
                    Command.ExecuteNonQuery();

                    Call.Close();

                    Enable_Read_Alarm = true;
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }


            return true;
        }

        public static DataTable GetGridAlarm_Table_EquipAlarmEvent()
        {

            DataTable Data = new DataTable();

            if (Utilidades.VariaveisGlobais.DB_Connected_GS)
            {
                try
                {
                    string CommandString = "SELECT * FROM EquipAlarmEvent WHERE Event = 'False' AND Active = 'true' ORDER BY Id DESC;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    Adapter.Fill(Data);

                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return RemoveUnderscoreAndSubscribeDate(Data);
        }

        public static DataTable GetReportAlarm_Table_EquipAlarmEvent(DateTime dtIn, DateTime dtOut)
        {

            DataTable Data = new DataTable();

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


                    string CommandString = "SELECT * FROM EquipAlarmEvent WHERE Event = 'False' AND DateNow >= '" + DTIn + "' AND DateNow <= '" + DTOut + "' ORDER BY Id DESC;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return RemoveUnderscoreAndSubscribeDate(Data);
        }

        public static DataTable GetReportAlarm_Table_EquipAlarmEvent(DateTime dtIn, DateTime dtOut, string Equip)
        {
            DataTable Data = new DataTable();

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

                    string CommandString = "SELECT * FROM EquipAlarmEvent WHERE Event = 'False' AND DateNow >= '" + DTIn + "' AND DateNow <= '" + DTOut + "' AND TagEquip = '" + Equip + "' ORDER BY Id DESC;";
                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return RemoveUnderscoreAndSubscribeDate(Data);
        }

        public static DataTable GetReportEvent_Table_EquipAlarmEvent(DateTime dtIn, DateTime dtOut)
        {
            DataTable Data = new DataTable();

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


                    string CommandString = "SELECT * FROM EquipAlarmEvent WHERE Event = 'True' AND DateNow >= '" + DTIn + "' AND DateNow <= '" + DTOut + "' ORDER BY Id DESC;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return RemoveUnderscoreAndSubscribeDate(Data);
        }

        public static DataTable GetReportEvent_Table_EquipAlarmEvent(DateTime dtIn, DateTime dtOut, string Equip)
        {

            DataTable Data = new DataTable();

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

                    string CommandString = "SELECT * FROM EquipAlarmEvent WHERE Event = 'True' AND DateNow >= '" + DTIn + "' AND DateNow <= '" + DTOut + "' AND TagEquip = '" + Equip + "' ORDER BY Id DESC;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return RemoveUnderscoreAndSubscribeDate(Data);
        }

        public static DataTable GetReportAlarmEvent_Table_EquipAlarmEvent(DateTime dtIn, DateTime dtOut)
        {

            DataTable Data = new DataTable();

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


                    string CommandString = "SELECT * FROM EquipAlarmEvent WHERE DateNow >= '" + DTIn + "' AND DateNow <= '" + DTOut + "' ORDER BY Id DESC;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return RemoveUnderscoreAndSubscribeDate(Data);
        }

        public static DataTable GetReportAlarmEvent_Table_EquipAlarmEvent(DateTime dtIn, DateTime dtOut, string Equip)
        {

            DataTable Data = new DataTable();

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


                    string CommandString = "SELECT * FROM EquipAlarmEvent WHERE DateNow >= '" + DTIn + "' AND DateNow <= '" + DTOut + "' AND TagEquip = '" + Equip + "' ORDER BY Id DESC;";

                    dynamic Call = SqlGlobalFuctions.ReturnCall(Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    dynamic Adapter = SqlGlobalFuctions.ReturnAdapter(CommandString, Utilidades.VariaveisGlobais.Connection_DB_Equip_GS);

                    Adapter.Fill(Data);
                }
                catch (Exception ex)
                {
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                }
            }

            return RemoveUnderscoreAndSubscribeDate(Data);
        }

        private static DataTable RemoveUnderscoreAndSubscribeDate(DataTable Data)
        {

            try
            {

                int count1 = 0;


                foreach (DataRow row in Data.Rows)
                {
                    string Tag = (string)row[1];
                    string Tag_1 = "";

                    foreach (char item in Tag)
                    {
                        if (item != '_')
                        {
                            Tag_1 += item;
                        }
                    }

                    Data.Rows[count1][1] = Tag_1;

                    Nome = Tag_1;
                    count1 += 1;
                }

            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }

            return Data;
        }

        public static string Name_Motor_GS
        {
            get { return Nome; }

        }

        #endregion

    }
}
