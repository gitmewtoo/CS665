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
        private void Create_SQL(SQLiteConnection conn)
        {
            string stm = "CREATE TABLE reactors ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, building_id INTEGER REFERENCES buildings(id), temp FLOAT, volume FLOAT );";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            int rows = cmd.ExecuteNonQuery();

            stm = "CREATE TABLE buildings ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, address TEXT, city TEXT, state TEXT, zip TEXT, phone TEXT );";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "CREATE TABLE processes ( id INTEGER PRIMARY_KEY, desc TEXT NOT NULL, temp FLOAT, volume FLOAT );";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "CREATE TABLE process_reactants ( id INTEGER PRIMARY_KEY, process_id INTEGER REFERENCES processes(id), reactant_id INTEGER REFERENCES reactants(id), temp FLOAT, volume FLOAT );";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "CREATE TABLE reactants ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, onhand FLOAT, orderpoint FLOAT );";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

        }

    }
}
