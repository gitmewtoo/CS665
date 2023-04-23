using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRUD.Forms
{
    /// <summary>
    /// Interaction logic for FormularyWindow.xaml
    /// </summary>
    public partial class FormularyWindow : Window
    {
        int processId = 0;
        ObservableCollection<Reactant> ocReactant = new();
        ObservableCollection<ProcessReactant> ocProcessReactant = new();

        public FormularyWindow(SQLiteConnection conn)
        {
            InitializeComponent();
            Connection = conn;
            dgReactants.ItemsSource = ocReactant;
            dgProcessReactants.ItemsSource = ocProcessReactant;

            string stm = "SELECT id, desc from processes";
            SQLiteCommand cmd = new SQLiteCommand(stm, Connection);
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Process p = new Process(dr.GetInt16(0), dr.GetString(1), 0, 0);
                cmbProcesses.Items.Add(p.Id.ToString() + ": " + p.Desc);
            }
            cmbProcesses.SelectedIndex = 0;
            string sData = cmbProcesses.SelectedItem.ToString();
            string[] sParts = sData.Split(":");
            processId = Convert.ToInt32(sParts[0]);

            Show_Data();
        }
        public FormularyWindow()
        {
            InitializeComponent();
        }
        public SQLiteConnection Connection { get; set; }
        private void Show_Data()
        {
            ocReactant.Clear();
            string stm = "SELECT * from reactants";
            SQLiteCommand cmd = new SQLiteCommand(stm, Connection);
            SQLiteDataReader dr3 = cmd.ExecuteReader();

            while (dr3.Read())
            {
                ocReactant.Add(new Reactant(dr3.GetInt16(0), dr3.GetString(1), dr3.GetFloat(2), dr3.GetFloat(3)));
            }

            ocProcessReactant.Clear();
            stm = "SELECT process_reactants.process_id, process_reactants.reactant_id, reactants.name, process_reactants.temp, process_reactants.volume from process_reactants, reactants WHERE process_reactants.process_id = " + processId.ToString() + " AND reactants.id = process_reactants.reactant_id";
            cmd = new SQLiteCommand(stm, Connection);
            dr3 = cmd.ExecuteReader();

            while (dr3.Read())
            {
                ocProcessReactant.Add(new ProcessReactant(dr3.GetInt16(0), dr3.GetInt16(1), dr3.GetString(2), dr3.GetFloat(3), dr3.GetFloat(4)));
            }
        }

        private void cmbProcesses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sData = cmbProcesses.SelectedItem.ToString();
            string[] sParts = sData.Split(":");
            processId = Convert.ToInt32(sParts[0]);
            Show_Data();

        }

        private void btnCreateReactant_Click(object sender, RoutedEventArgs e)
        {
            string sID = "SELECT MAX(id) from reactants;";
            SQLiteCommand cmd = new SQLiteCommand(sID, Connection);
            Int64 result = (long)cmd.ExecuteScalar();
            int iID = Convert.ToInt32(result) + 1;

            ReactantWindow win = new ReactantWindow();
            win.txtID.Text = iID.ToString();
            win.txtName.Text = "Enter a name.";
            win.txtQuantity.Text = "0";
            win.txtOrderPoint.Text = "5000";
            if ((bool)win.ShowDialog())
            {
                Reactant r = new Reactant();
                r.Name = win.txtName.Text;
                r.Quantity = (float)Convert.ToInt32(win.txtQuantity.Text);
                r.OrderPoint = (float)Convert.ToInt32(win.txtOrderPoint.Text);

                string stm = "INSERT INTO reactants ( id, name, onhand, orderpoint) VALUES (\"" + iID + "\", \"" + r.Name + "\", \"" + r.Quantity + "\", \"" + r.OrderPoint + "\");";
                cmd = new SQLiteCommand(stm, Connection);
                int rows = cmd.ExecuteNonQuery();

            }
            Show_Data();


        }

        private void btnReadReactant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateReactant_Click(object sender, RoutedEventArgs e)
        {
            if (dgReactants.SelectedItems.Count != 0)
            {
                Reactant? r = dgReactants.SelectedItems[0] as Reactant;
                if (r != null)
                {
                    ReactantWindow win = new ReactantWindow();
                    win.txtID.Text = r.Id.ToString();
                    win.txtName.Text = r.Name;
                    win.txtQuantity.Text = r.Quantity.ToString();
                    win.txtOrderPoint.Text = r.OrderPoint.ToString();
                    if ((bool)win.ShowDialog())
                    {
                        r.Name = win.txtName.Text;
                        r.Quantity = (float)Convert.ToInt32(win.txtQuantity.Text);
                        r.OrderPoint = (float)Convert.ToInt32(win.txtOrderPoint.Text);

                        string stm = "UPDATE processes set desc=\"" + r.Name + "\", onhand=\"" + r.Quantity + "\", orderpoint=\"" + r.OrderPoint + "\" where id=" + r.Id + ";";
                        SQLiteCommand cmd = new SQLiteCommand(stm, Connection);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            Show_Data();

        }

        private void btnDeleteReactant_Click(object sender, RoutedEventArgs e)
        {
            if (dgReactants.SelectedItems.Count != 0)
            {
                Reactant? r = dgReactants.SelectedItems[0] as Reactant;
                if (r != null)
                {
                    ReactantWindow win = new ReactantWindow();
                    win.txtID.Text = r.Id.ToString();
                    win.txtName.Text = r.Name;
                    win.txtQuantity.Text = r.Quantity.ToString();
                    win.txtOrderPoint.Text = r.OrderPoint.ToString();
                    if ((bool)win.ShowDialog())
                    {
                        string stm = "DELETE FROM reactants WHERE id=\"" + win.txtID.Text + "\";";
                        SQLiteCommand cmd = new SQLiteCommand(stm, Connection);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            Show_Data();

        }

        private void btnCreateFormula_Click(object sender, RoutedEventArgs e)
        {
            if (dgReactants.SelectedItems.Count != 0)
            {
                Reactant? r = dgReactants.SelectedItems[0] as Reactant;
                if (r != null)
                {
                    string stm = "INSERT INTO process_reactants (process_id, reactant_id, temp, volume) VALUES (\"" + processId.ToString() + "\", \"" + r.Id.ToString() + "\", \"" + 85 + "\", \"" + 10 + "\")";
                    SQLiteCommand cmd = new SQLiteCommand(stm, Connection);
                    int rows = cmd.ExecuteNonQuery();
                }
            }
            Show_Data();
        }

        private void btnDeleteFormula_Click(object sender, RoutedEventArgs e)
        {
            if (dgProcessReactants.SelectedItems.Count != 0)
            {
                ProcessReactant? r = dgProcessReactants.SelectedItems[0] as ProcessReactant;
                if (r != null)
                {
                    string stm = "DELETE FROM process_reactants WHERE process_id = " + processId.ToString() + " AND reactant_id = " + r.ReagentId.ToString() + ";";
                    SQLiteCommand cmd = new SQLiteCommand(stm, Connection);
                    int rows = cmd.ExecuteNonQuery();
                }
            }
            Show_Data();
        }

        private void btnReadFormula_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateFormula_Click(object sender, RoutedEventArgs e)
        {
            if (dgProcessReactants.SelectedItems.Count != 0)
            {
                ProcessReactant? r = dgProcessReactants.SelectedItems[0] as ProcessReactant;
                if (r != null)
                {
                    ProcessReactantWindow win = new ProcessReactantWindow();
                    win.txtProcessID.Text = r.ProcessId.ToString();
                    win.txtReactantID.Text = r.ReagentId.ToString();
                    win.txtName.Text = r.Name;
                    win.txtTemp.Text = r.Temp.ToString();
                    win.txtVolume.Text = r.Volume.ToString();
                    if ((bool)win.ShowDialog())
                    {
                        r.Name = win.txtName.Text;
                        r.Temp = (float)Convert.ToInt32(win.txtTemp.Text);
                        r.Volume = (float)Convert.ToInt32(win.txtVolume.Text);

                        string stm = "UPDATE process_reactants set temp=\"" + r.Temp + "\", volume=\"" + r.Volume + "\" where process_id=" + r.ProcessId + " AND reactant_id=" + r.ReagentId + ";";
                        SQLiteCommand cmd = new SQLiteCommand(stm, Connection);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            Show_Data();

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
