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
        private void Clear_SQL(SQLiteConnection conn)
        {
            string stm = "DELETE from reactors;";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            cmd.ExecuteNonQuery();
            stm = "DELETE from buildings;";
            cmd = new SQLiteCommand(stm, conn);
            cmd.ExecuteNonQuery();
            stm = "DELETE from processes;";
            cmd = new SQLiteCommand(stm, conn);
            cmd.ExecuteNonQuery();
            stm = "DELETE from reactants;";
            cmd = new SQLiteCommand(stm, conn);
            cmd.ExecuteNonQuery();
            stm = "DELETE from process_reactants;";
            cmd = new SQLiteCommand(stm, conn);
            cmd.ExecuteNonQuery();
        }
    }
}
