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
        //This function will trigger an event after one of the dependent tables is deleted, the combined elements
        //such as process+reactants will be deleted.
        //One the trigger is called we automatically use the function delete_process_reactants, delete_process_reactants2, or delete_reactors
        //We trigger an event where the ones that are deleted are the objects with an ID == to the OLD.id
        //Or the held ID of the combined table.
        private void Create_Triggers(SQLiteConnection conn)
        {
            string stm = "CREATE TRIGGER delete_process_reactants " +
                "AFTER DELETE ON reactants " +
                "FOR EACH ROW " +
                "BEGIN " +
                "DELETE FROM process_reactants " +
                "WHERE process_reactants.reactant_id = OLD.id; " +
                "END;";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            cmd.ExecuteNonQuery();

            stm = "CREATE TRIGGER delete_process_reactants_2 " +
                "AFTER DELETE ON processes " +
                "FOR EACH ROW " +
                "BEGIN " +
                "DELETE FROM process_reactants " +
                "WHERE process_reactants.process_id = OLD.id; " +
                "END;";
            cmd = new SQLiteCommand(stm, conn);
            cmd.ExecuteNonQuery();

            stm = "CREATE TRIGGER delete_reactors " +
                "AFTER DELETE ON buildings " +
                "FOR EACH ROW " +
                "BEGIN " +
                "DELETE FROM reactors " +
                "WHERE reactors.building_id = OLD.id; " +
                "END;";
            cmd = new SQLiteCommand(stm, conn);
            cmd.ExecuteNonQuery();
        }

    }
}
