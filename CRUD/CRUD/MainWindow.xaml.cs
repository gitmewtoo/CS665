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
        private bool refresh = true;
        private bool buildingRefresh = true;
        private bool internalOperation = false;
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

            Show_Data();
        }

        private void Show_Data()
        {
            int count = 0;
            SQLiteCommand cmd = null;
            SQLiteDataReader dr = null;
            string stm = string.Empty;
            try
            {
                if (refresh)
                {
                    refresh = false;
                    ocBuildings.Clear();
                    ocReactors.Clear();
                    ocProcesses.Clear();
                    if (buildingRefresh)
                    {
                        buildingRefresh = false;
                        internalOperation = true;
                        cmbBuildings.Items.Clear();
                        stm = "SELECT id, name, address, city, state, zip, phone from buildings";
                        cmd = new SQLiteCommand(stm, conn);
                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            Building b = new Building(dr.GetInt16(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6));
                            cmbBuildings.Items.Add(b.Id.ToString() + ": " + b.Name);
                            count++;
                        }
                        if (count > 0)
                        {
                            cmbBuildings.SelectedIndex = 0;
                        }
                        if (cmbBuildings.SelectedItem != null)
                        {
                            string sData = cmbBuildings.SelectedItem.ToString();
                            string[] sParts = sData.Split(":");
                            buildingId = Convert.ToInt32(sParts[0]);
                        }
                        internalOperation = false;
                    }
                    stm = "SELECT * from reactors WHERE building_id = " + buildingId.ToString();
                    cmd = new SQLiteCommand(stm, conn);
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ocReactors.Add(new Reactor(dr.GetInt16(0), dr.GetString(1), dr.GetInt16(2), dr.GetFloat(3), dr.GetFloat(4)));
                    }

                    stm = "SELECT * from processes";
                    cmd = new SQLiteCommand(stm, conn);
                    SQLiteDataReader dr2 = cmd.ExecuteReader();

                    while (dr2.Read())
                    {
                        ocProcesses.Add(new Process(dr2.GetInt16(0), dr2.GetString(1), dr2.GetFloat(2), dr2.GetFloat(3)));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Create database first!", "NO DATABASE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnShowData_Click(object sender, RoutedEventArgs e)
        {
            Show_Data();
        }

        private void Add_Process(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Creates all of the database tables.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void Create_Tables(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            Create_SQL(conn);
            Create_Triggers(conn);
            Insert_SQL(conn);
            refresh = true;
            buildingRefresh = true;
            Show_Data();
            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Clears out all data from the tables.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void Clear_Tables(object sender, RoutedEventArgs e)
        {
            Clear_SQL(conn);
            refresh = true;
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

            ReactorWindow win = new ReactorWindow();
            win.txtID.Text = iID.ToString();
            win.txtName.Text = "Enter a name.";
            win.txtBuildingId.Text = buildingId.ToString();
            win.txtTemp.Text = "0.0";
            win.txtVolume.Text = "5000.0";
            if ((bool)win.ShowDialog())
            {
                Reactor r = new Reactor();
                r.Name = win.txtName.Text;
                r.Temp = (float)Convert.ToDouble(win.txtTemp.Text);
                r.Volume = (float)Convert.ToDouble(win.txtVolume.Text);

                string stm = "INSERT INTO reactors ( id, name, building_id, temp, volume) VALUES (\"" + iID + "\", \"" + r.Name + "\", \"" + buildingId + "\", \"" + r.Temp + "\", \"" + r.Volume + "\");";
                cmd = new SQLiteCommand(stm, conn);
                cmd.ExecuteNonQuery();
                refresh = true;
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
                        cmd.ExecuteNonQuery();
                        refresh = true;
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
                        cmd.ExecuteNonQuery();
                        refresh = true;
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
                cmd.ExecuteNonQuery();
                refresh = true;
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
                        cmd.ExecuteNonQuery();
                        refresh = true;
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
                        cmd.ExecuteNonQuery();
                        refresh = true;
                    }
                }
            }
            Show_Data();

        }

        private void cmbBuildings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (internalOperation) return;
            if (cmbBuildings.SelectedItem != null)
            {
                string sData = cmbBuildings.SelectedItem.ToString();
                string[] sParts = sData.Split(":");
                buildingId = Convert.ToInt32(sParts[0]);
            }
            refresh = true;
            Show_Data();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FormularyWindow win = new FormularyWindow(conn);
            win.ShowDialog();
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
                            
                            MessageBox.Show("Batch of " + p.Desc + " being made in " + r.Name + ".", "COOKING", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Selected reactor cannot reach required temp!", "REACTOR UNSUITABLE", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select a larger reactor!", "REACTOR TOO SMALL", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Select a process first!", "NO PROCESS SELECTED", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Select a reactor first!", "NO REACTOR SELECTED", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Delete_Tables(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            Delete_SQL(conn);
            buildingRefresh = true;
            refresh = true;
            Show_Data();
            Cursor = Cursors.Arrow;
        }

        private void btnCreateBuilding_Click(object sender, RoutedEventArgs e)
        {
            string sID = "SELECT MAX(id) from buildings;";
            SQLiteCommand cmd = new SQLiteCommand(sID, conn);
            Int64 result = (long)cmd.ExecuteScalar();
            int iID = Convert.ToInt32(result) + 1;
            int iBuildingID = 0;

            FacilityWindow win = new FacilityWindow();
            win.txtID.Text = iID.ToString();
            win.txtName.Text = "Enter a name.";
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

                string stm = "INSERT INTO buildings ( id, name, address, city, state, zip, phone) VALUES (\"" + iID + "\", \"" + b.Name + "\", \"" + b.Address + "\", \"" + b.City + "\", \"" + b.State + "\", \"" + b.Zip + "\", \"" + b.Phone + "\");";
                cmd = new SQLiteCommand(stm, conn);
                cmd.ExecuteNonQuery();
                refresh = true;
                buildingRefresh = true;
            }
            Show_Data();
        }

        private void btnUpdateBuilding_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBuildings.SelectedItem != null)
            {
                string stm = "SELECT id, name, address, city, state, zip, phone from buildings where id=" + buildingId.ToString();
                SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                SQLiteDataReader dr = cmd.ExecuteReader();

                dr.Read();
                Building b = new Building(dr.GetInt16(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6));

                FacilityWindow win = new FacilityWindow();
                win.txtID.Text = buildingId.ToString();
                win.txtName.Text = b.Name;
                win.txtAddress.Text = b.Address;
                win.txtCity.Text = b.City;
                win.txtState.Text = b.State;
                win.txtZip.Text = b.Zip;
                win.txtPhone.Text = b.Phone;
                if ((bool)win.ShowDialog())
                {
                    b.Name = win.txtName.Text;
                    b.Address = win.txtAddress.Text;
                    b.City = win.txtCity.Text;
                    b.State = win.txtState.Text;
                    b.Zip = win.txtZip.Text;
                    b.Phone = win.txtPhone.Text;

                    stm = "UPDATE buildings set name=\"" + b.Name + "\", address=\"" + b.Address + "\", city=\"" + b.City + "\", state=\"" + b.State + "\", zip=\"" + b.Zip + "\", phone=\"" + b.Phone + "\" WHERE id =" + buildingId.ToString() + ";";
                    cmd = new SQLiteCommand(stm, conn);
                    cmd.ExecuteNonQuery();
                    refresh = true;
                    buildingRefresh = true;
                }
                Show_Data();
            }
        }

        private void btnDeleteBuilding_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBuildings.SelectedItem != null)
            {
                string stm = "SELECT id, name, address, city, state, zip, phone from buildings where id=" + buildingId.ToString();
                SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                SQLiteDataReader dr = cmd.ExecuteReader();

                dr.Read();
                Building b = new Building(dr.GetInt16(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6));

                FacilityWindow win = new FacilityWindow();
                win.txtID.Text = buildingId.ToString();
                win.txtName.Text = b.Name;
                win.txtAddress.Text = b.Address;
                win.txtCity.Text = b.City;
                win.txtState.Text = b.State;
                win.txtZip.Text = b.Zip;
                win.txtPhone.Text = b.Phone;
                if ((bool)win.ShowDialog())
                {
                    b.Name = win.txtName.Text;
                    b.Address = win.txtAddress.Text;
                    b.City = win.txtCity.Text;
                    b.State = win.txtState.Text;
                    b.Zip = win.txtZip.Text;
                    b.Phone = win.txtPhone.Text;

                    stm = "DELETE FROM buildings WHERE id =" + buildingId.ToString() + ";";
                    cmd = new SQLiteCommand(stm, conn);
                    cmd.ExecuteNonQuery();
                    refresh = true;
                    buildingRefresh = true;
                }
                Show_Data();
            }
        }
    }
}
