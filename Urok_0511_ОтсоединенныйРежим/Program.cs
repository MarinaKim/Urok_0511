using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urok_0511_ОтсоединенныйРежим
{
    class Program
    {
        static void Main(string[] args)
        {

            string connectionString = "192.168.110.7 Usier=sa password=";

            //1
            SqlConnection con = new SqlConnection();
            
            //2
            DataSet ds = new DataSet();

            //3
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM [User]; SELECT * FROM [AccessTab]";
            cmd.Connection = con;
            //4
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [User] where intUserId=1009;",con);//сам открывает соединение и закрывает, если соединение есть, то использует его и закрывает

            da.Fill(ds);// если использовать  еще раз филл, данные дозапишутся
            //для создания новой строки
            DataRow newRow = ds.Tables[0].Rows[0];
            newRow["SID"] = "SID";
            newRow["UserFirstName"] = "UserFirstName";
            newRow["UserLastName"] = "UserLastName";
            newRow["email"] = 1;
            newRow["locatId"] = 1;
            ds.Tables[0].Rows.Add(newRow);
            //для изменения данных
            DataRow updateRow = ds.Tables[0].Rows[0];
            updateRow["SID"] = "updateSID";
            updateRow["UserFirstName"] = "updateUserFirstName";
            updateRow["UserLastName"] = "updateUserLastName";
            updateRow["email"] = 2;
            updateRow["locatId"] = 2;

            SqlCommandBuilder b = new SqlCommandBuilder(da);//создаются запросы
            da.Update(ds);

            ds.Tables[0].TableName = "User";
            ds.Tables[1].TableName = "AccessTab";

            foreach (DataTable table in ds.Tables)
            {
                Console.WriteLine(table.TableName);
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine(row[0] + "-"+ row[1]+"-"+row[2]);
                    row[2] = "new Name";
                }
            }
            SqlCommandBuilder comBuild = new SqlCommandBuilder(da);
            var test= comBuild.GetUpdateCommand();
            int changeSubmitted = da.Update(ds);

        }
    }
}
