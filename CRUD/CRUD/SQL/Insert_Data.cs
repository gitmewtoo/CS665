﻿using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRUD
{
    public partial class MainWindow
    {

        //This function and the many iterations shown here are being used to populate the tables used in the presentation.
        //of course we start off with a large amount of prepopulated fields.
        private void Insert_SQL(SQLiteConnection conn)
        {
            string stm = "INSERT INTO reactors ( id, name, building_id, temp, volume) VALUES (\"1\", \"Reactor I\", \"1\", \"200\", \"5000\");";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            int rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO reactors ( id, name, building_id, temp, volume) VALUES (\"2\", \"Reactor II - Hi Temp\", \"1\", \"400\", \"5000\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO reactors ( id, name, building_id, temp, volume) VALUES (\"3\", \"Reactor III\", \"2\", \"100\", \"2500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO reactors ( id, name, building_id, temp, volume) VALUES (\"4\", \"Reactor IV - Low Temp\", \"3\", \"50\", \"10000\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO reactors ( id, name, building_id, temp, volume) VALUES (\"5\", \"Reactor V\", \"4\", \"100\", \"1000\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "INSERT INTO buildings ( id, name, address, city, state, zip, phone) VALUES (\"1\", \"River Facility\", \"123 S Fourth, East Range\", \"Wichita\", \"KS\", \"67212\", \"3162434444\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO buildings ( id, name, address, city, state, zip, phone) VALUES (\"2\", \"Downing Facility\", \"123 S Fourth, West Range\", \"Wichita\", \"KS\", \"67212\", \"3162434446\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO buildings ( id, name, address, city, state, zip, phone) VALUES (\"3\", \"Uptown Facility\", \"246 E Ninth\", \"Bethesda\", \"MD\", \"21345\", \"8182554499\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO buildings ( id, name, address, city, state, zip, phone) VALUES (\"4\", \"Twin Rivers Facility A\", \"668 Main St\", \"Westerlie\", \"MO\", \"45345\", \"4162554499\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO buildings ( id, name, address, city, state, zip, phone) VALUES (\"5\", \"Twin Rivers Facility B\", \"669 Main St\", \"Westerlie\", \"MO\", \"45345\", \"4162554499\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "INSERT INTO processes ( id, desc, temp, volume) VALUES (\"1\", \"Red Kool Aid\", \"75\", \"5000\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO processes ( id, desc, temp, volume) VALUES (\"2\", \"Green Kool Aid\", \"75\", \"2500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO processes ( id, desc, temp, volume) VALUES (\"3\", \"Blue Kool Aid\", \"120\", \"5000\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO processes ( id, desc, temp, volume) VALUES (\"4\", \"Brown Kool Aid\", \"85\", \"5000\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO processes ( id, desc, temp, volume) VALUES (\"5\", \"Yellow Kool Aid\", \"150\", \"1500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "INSERT INTO reactants ( id, name, onhand, orderpoint) VALUES (\"1\", \"Red Dye\", \"5000\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO reactants ( id, name, onhand, orderpoint) VALUES (\"2\", \"Yellow Dye\", \"5000\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO reactants ( id, name, onhand, orderpoint) VALUES (\"3\", \"Blue Dye\", \"5000\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO reactants ( id, name, onhand, orderpoint) VALUES (\"4\", \"Black Dye\", \"5000\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO reactants ( id, name, onhand, orderpoint) VALUES (\"5\", \"Water\", \"5000\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO reactants ( id, name, onhand, orderpoint) VALUES (\"6\", \"Sugar\", \"5000\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO reactants ( id, name, onhand, orderpoint) VALUES (\"7\", \"Fake Fruit Flavor\", \"50000\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"1\", \"1\", \"1\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"2\", \"1\", \"5\", \"50\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"3\", \"1\", \"6\", \"50\", \"250\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"4\", \"1\", \"7\", \"65\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"5\", \"2\", \"2\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"6\", \"2\", \"3\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"7\", \"2\", \"5\", \"50\", \"1000\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"8\", \"2\", \"6\", \"50\", \"250\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"9\", \"2\", \"7\", \"65\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"10\", \"3\", \"3\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"11\", \"3\", \"5\", \"50\", \"1000\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"12\", \"3\", \"6\", \"50\", \"250\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"13\", \"3\", \"7\", \"65\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"14\", \"4\", \"1\", \"50\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"15\", \"4\", \"1\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"16\", \"4\", \"2\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"17\", \"4\", \"4\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"18\", \"4\", \"5\", \"50\", \"1000\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"19\", \"4\", \"6\", \"50\", \"250\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"20\", \"4\", \"7\", \"65\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"21\", \"5\", \"2\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"22\", \"5\", \"5\", \"50\", \"1000\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"23\", \"5\", \"6\", \"50\", \"250\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( id, process_id, reactant_id, temp, volume) VALUES (\"24\", \"5\", \"7\", \"65\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
        }
    }
}
