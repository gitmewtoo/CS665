using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using System.Security.Principal;
using System.IO;
using System.Security.Cryptography;
using CRUD.Forms;

namespace CRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = "test.db";
        string cs = @"URI=file:" + "test.db";
        SQLiteConnection conn;
        private int buildingId = 0;

        ObservableCollection<Building> ocBuildings = new();
        ObservableCollection<Process> ocProcesses = new();
        ObservableCollection<Reactor> ocReactors = new();

        public MainWindow()
        {
            InitializeComponent();
            dgReactors.ItemsSource = ocReactors;
            dgProcesses.ItemsSource = ocProcesses;

            conn = new SQLiteConnection(cs);
            conn.Open();
            string stm = "SELECT id, name, address, city, state, zip, phone from buildings";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Building b = new Building(dr.GetInt16(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6));
                cmbBuildings.Items.Add(b.Id.ToString() + ": " + b.Name);
            }

            cmbBuildings.SelectedIndex = 0;
            string sData = cmbBuildings.SelectedItem.ToString();
            string[] sParts = sData.Split(":");
            buildingId = Convert.ToInt32(sParts[0]);
            Show_Data();
        }

        private void Show_Data()
        {
            ocReactors.Clear();
            string stm = "SELECT * from reactors WHERE building_id = " + buildingId.ToString();
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ocReactors.Add(new Reactor(dr.GetInt16(0), dr.GetString(1), dr.GetInt16(2), dr.GetFloat(3), dr.GetFloat(4)));
            }

            ocProcesses.Clear();
            stm = "SELECT * from processes";
            cmd = new SQLiteCommand(stm, conn);
            SQLiteDataReader dr2 = cmd.ExecuteReader();

            while (dr2.Read())
            {
                ocProcesses.Add(new Process(dr2.GetInt16(0), dr2.GetString(1), dr2.GetFloat(2), dr2.GetFloat(3)));
            }
        }

        private void btnShowData_Click(object sender, RoutedEventArgs e)
        {
            Show_Data();
        }

        /// <summary>
        /// Creates all of the database tables.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void Create_Tables(object sender, RoutedEventArgs e)
        {
            string stm = "CREATE TABLE reactors ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, building_id INTEGER, temp FLOAT, volume FLOAT );";
            SQLiteCommand cmd = new SQLiteCommand(stm, conn);
            int rows = cmd.ExecuteNonQuery();

            stm = "CREATE TABLE buildings ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, address TEXT, city TEXT, state TEXT, zip TEXT, phone TEXT );";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "CREATE TABLE processes ( id INTEGER PRIMARY_KEY, desc TEXT NOT NULL, temp FLOAT, volume FLOAT );";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "CREATE TABLE process_reactants ( process_id INTEGER, reactant_id INTEGER, temp FLOAT, volume FLOAT );";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            stm = "CREATE TABLE reactants ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, onhand FLOAT, orderpoint FLOAT );";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            Show_Data();
        }

        /// <summary>
        /// Inserts entries into all tables for testing.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void Insert_Starting_Data(object sender, RoutedEventArgs e)
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

            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"1\", \"1\", \"50\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"1\", \"5\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"1\", \"6\", \"50\", \"250\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"1\", \"7\", \"65\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"2\", \"2\", \"50\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"2\", \"3\", \"50\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"2\", \"5\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"2\", \"6\", \"50\", \"250\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"2\", \"7\", \"65\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"3\", \"3\", \"50\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"3\", \"5\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"3\", \"6\", \"50\", \"250\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"3\", \"7\", \"65\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"4\", \"1\", \"50\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"4\", \"1\", \"50\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"4\", \"2\", \"50\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"4\", \"4\", \"50\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"4\", \"5\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"4\", \"6\", \"50\", \"250\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"4\", \"7\", \"65\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"5\", \"2\", \"50\", \"500\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"5\", \"5\", \"50\", \"1\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"5\", \"6\", \"50\", \"250\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();
            stm = "INSERT INTO process_reactants ( process_id, reactant_id, temp, volume) VALUES (\"5\", \"7\", \"65\", \"2\");";
            cmd = new SQLiteCommand(stm, conn);
            rows = cmd.ExecuteNonQuery();

            Show_Data();
        }

        private void Add_Process(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Clears out all data from the tables.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void Clear_Tables(object sender, RoutedEventArgs e)
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

            Show_Data();
        }

        /// <summary>
        /// Dialog to create a reactor.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void btnCreateReactor_Click(object sender, RoutedEventArgs e)
        {
            string sID = "SELECT MAX(id) from reactors;";
            SQLiteCommand cmd = new SQLiteCommand(sID, conn);
            Int64 result = (long)cmd.ExecuteScalar();
            int iID = Convert.ToInt32(result) + 1;
            int iBuildingID = 0;

            ReactorWindow win = new ReactorWindow();
            win.txtID.Text = iID.ToString();
            win.txtName.Text = "Enter a name.";
            win.txtTemp.Text = "0.0";
            win.txtVolume.Text = "5000.0";
            if ((bool)win.ShowDialog())
            {
                Reactor r = new Reactor();
                r.Name = win.txtName.Text;
                r.Temp = (float)Convert.ToDouble(win.txtTemp.Text);
                r.Volume = (float)Convert.ToDouble(win.txtVolume.Text);

                string stm = "INSERT INTO reactors ( id, name, building_id, temp, volume) VALUES (\"" + iID + "\", \"" + r.Name + "\", \"" + iBuildingID + "\", \"" + r.Temp + "\", \"" + r.Volume + "\");";
                cmd = new SQLiteCommand(stm, conn);
                int rows = cmd.ExecuteNonQuery();

            }
            Show_Data();
        }

        /// <summary>
        /// Dialog to update a reactor.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void btnUpdateReactor_Click(object sender, RoutedEventArgs e)
        {
            if (dgReactors.SelectedItems.Count != 0)
            {
                Reactor? r = dgReactors.SelectedItems[0] as Reactor;
                if (r != null)
                {
                    ReactorWindow win = new ReactorWindow();
                    win.txtID.Text = r.Id.ToString();
                    win.txtBuildingId.Text = r.BuildingId.ToString();
                    win.txtName.Text = r.Name;
                    win.txtTemp.Text = r.Temp.ToString();
                    win.txtVolume.Text = r.Volume.ToString();
                    if ((bool)win.ShowDialog())
                    {
                        r.Name = win.txtName.Text;
                        r.Temp = (float)Convert.ToDouble(win.txtTemp.Text);
                        r.Volume = (float)Convert.ToDouble(win.txtVolume.Text);

                        string stm = "UPDATE reactors set name=\"" + r.Name + "\", building_id=\"" + r.BuildingId + "\", temp=\"" + r.Temp + "\", volume=\"" + r.Volume + "\" where id=" + r.Id + ";";
                        SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            Show_Data();
        }

        /// <summary>
        /// Dialog to delete a reactor.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void btnDeleteReactor_Click(object sender, RoutedEventArgs e)
        {
            if (dgReactors.SelectedItems.Count != 0)
            {
                Reactor? r = dgReactors.SelectedItems[0] as Reactor;
                if (r != null)
                {
                    ReactorWindow win = new ReactorWindow();
                    win.txtID.Text = r.Id.ToString();
                    win.txtBuildingId.Text = r.BuildingId.ToString();
                    win.txtName.Text = r.Name;
                    win.txtTemp.Text = r.Temp.ToString();
                    win.txtVolume.Text = r.Volume.ToString();
                    if ((bool)win.ShowDialog())
                    {
                        string stm = "DELETE FROM reactors WHERE id=\"" + win.txtID.Text + "\";";
                        SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            Show_Data();
        }

        private void btnReadReactor_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Dialog to create a process.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void btnCreateProcess_Click(object sender, RoutedEventArgs e)
        {
            string sID = "SELECT MAX(id) from processes;";
            SQLiteCommand cmd = new SQLiteCommand(sID, conn);
            Int64 result = (long)cmd.ExecuteScalar();
            int iID = Convert.ToInt32(result) + 1;

            ProcessWindow win = new ProcessWindow();
            win.txtID.Text = iID.ToString();
            win.txtName.Text = "Enter a name.";
            win.txtTemp.Text = "0.0";
            win.txtVolume.Text = "5000.0";
            if ((bool)win.ShowDialog())
            {
                Reactor r = new Reactor();
                r.Name = win.txtName.Text;
                r.Temp = (float)Convert.ToDouble(win.txtTemp.Text);
                r.Volume = (float)Convert.ToDouble(win.txtVolume.Text);

                string stm = "INSERT INTO processes ( id, desc, temp, volume) VALUES (\"" + iID + "\", \"" + r.Name + "\", \"" + r.Temp + "\", \"" + r.Volume + "\");";
                cmd = new SQLiteCommand(stm, conn);
                int rows = cmd.ExecuteNonQuery();

            }
            Show_Data();

        }

        private void btnReadProcess_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Dialog to update a process.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void btnUpdateProcess_Click(object sender, RoutedEventArgs e)
        {
            if (dgProcesses.SelectedItems.Count != 0)
            {
                Process? r = dgProcesses.SelectedItems[0] as Process;
                if (r != null)
                {
                    ProcessWindow win = new ProcessWindow();
                    win.txtID.Text = r.Id.ToString();
                    win.txtName.Text = r.Desc;
                    win.txtTemp.Text = r.Temp.ToString();
                    win.txtVolume.Text = r.Volume.ToString();
                    if ((bool)win.ShowDialog())
                    {
                        r.Desc = win.txtName.Text;
                        r.Temp = (float)Convert.ToDouble(win.txtTemp.Text);
                        r.Volume = (float)Convert.ToDouble(win.txtVolume.Text);

                        string stm = "UPDATE processes set desc=\"" + r.Desc + "\", temp=\"" + r.Temp + "\", volume=\"" + r.Volume + "\" where id=" + r.Id + ";";
                        SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            Show_Data();

        }

        /// <summary>
        /// Dialog to delete a process.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void btnDeleteProcess_Click(object sender, RoutedEventArgs e)
        {
            if (dgProcesses.SelectedItems.Count != 0)
            {
                Process? r = dgProcesses.SelectedItems[0] as Process;
                if (r != null)
                {
                    ProcessWindow win = new ProcessWindow();
                    win.txtID.Text = r.Id.ToString();
                    win.txtName.Text = r.Desc;
                    win.txtTemp.Text = r.Temp.ToString();
                    win.txtVolume.Text = r.Volume.ToString();
                    if ((bool)win.ShowDialog())
                    {
                        string stm = "DELETE FROM processes WHERE id=\"" + win.txtID.Text + "\";";
                        SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            Show_Data();

        }

        private void cmbBuildings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sData = cmbBuildings.SelectedItem.ToString();
            string[] sParts = sData.Split(":");
            buildingId = Convert.ToInt32(sParts[0]);
            Show_Data();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FormularyWindow win = new FormularyWindow(conn);
            win.ShowDialog();
        }

        private void FacilityCreate_Click(object sender, RoutedEventArgs e)
        {
            string sID = "SELECT MAX(id) from buildings;";
            SQLiteCommand cmd = new SQLiteCommand(sID, conn);
            Int64 result = (long)cmd.ExecuteScalar();
            int iID = Convert.ToInt32(result) + 1;
            FacilityWindow win = new FacilityWindow();
            win.txtID.Text = iID.ToString();
            win.txtName.Text = "";
            win.txtAddress.Text = "";
            win.txtCity.Text = "";
            win.txtState.Text = "";
            win.txtZip.Text = "";
            win.txtPhone.Text = "";

            if ((bool)win.ShowDialog())
            {
                Building b = new Building();
                b.Name = win.txtName.Text;
                b.Address = win.txtAddress.Text;
                b.City = win.txtCity.Text;
                b.State = win.txtState.Text;
                b.Zip = win.txtZip.Text;
                b.Phone = win.txtPhone.Text;

                string stm = "INSERT INTO buildings (id, name, address, city, state, zip, phone) VALUES " +
                    "(\"" + iID + "\", \"" + b.Name + "\", \"" + b.Address + "\", \"" + b.City + "\", \"" +
                    b.State + "\", \"" + b.Zip + "\", \"" + b.Phone + "\");";
                cmd = new SQLiteCommand(stm, conn);
                int rows = cmd.ExecuteNonQuery();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgReactors.SelectedItems.Count != 0)
            {
                Reactor? r = dgReactors.SelectedItems[0] as Reactor;
                if (dgProcesses.SelectedItems.Count != 0)
                {
                    Process? p = dgProcesses.SelectedItems[0] as Process;
                    if (p.Volume <= r.Volume)
                    {
                        if (p.Temp <= r.Temp)
                        {
                            
                            MessageBox.Show("Batch of " + p.Desc + " being made in " + r.Name + ".", "COOKING");
                        }
                        else
                        {
                            MessageBox.Show("Selected reactor cannot reach required temp!", "REACTOR UNSUITABLE");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select a larger reactor!", "REACTOR TOO SMALL");
                    }
                }
                else
                {
                    MessageBox.Show("Select a process first!", "NO PROCESS SELECTED");
                }
            }
            else
            {
                MessageBox.Show("Select a reactor first!", "NO REACTOR SELECTED");
            }
        }
    }
}
