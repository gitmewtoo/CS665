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
        private void Delete_SQL(SQLiteConnection conn)
        {
            string stm = "DROP TABLE reactors;";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            int rows = cmd.ExecuteNonQuery();

            stm = "DROP TABLE buildings;";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "DROP TABLE processes;";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "DROP TABLE process_reactants;";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "DROP TABLE reactants;";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

        }

    }
}
