using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRUD
{
    public partial class MainWindow
    {
        //Simple delete process WITH a WHERE to target specific parameters.
        //It will NOT clear the entire tables contents, but not the table itself.
        //This command will clear a column not a row.
        //The cmd.paramter.AddWithValue expression allows us to have a place holder value
        //parameter which is then filled in with AddWithValue.
        //
        private void Delete_Single_SQL(SQLiteConnection conn, string input)
        {
            string stm = "DELETE from reactors WHERE column_name = @input ;";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            cmd.Parameters.AddWithValue("@input", input);
            cmd.ExecuteNonQuery();
             string stm = "DELETE from buildings WHERE column_name = @input ;";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            cmd.Parameters.AddWithValue("@input", input);
            cmd.ExecuteNonQuery();
             string stm = "DELETE from processes WHERE column_name = @input ;";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            cmd.Parameters.AddWithValue("@input", input);
            cmd.ExecuteNonQuery();
             string stm = "DELETE from process_reactants WHERE column_name = @input ;";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            cmd.Parameters.AddWithValue("@input", input);
            cmd.ExecuteNonQuery();
             string stm = "DELETE from reactants WHERE column_name = @input ;";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            cmd.Parameters.AddWithValue("@input", input);
            cmd.ExecuteNonQuery();
        }
    }
}
