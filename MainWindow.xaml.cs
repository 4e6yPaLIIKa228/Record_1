using System;
using System.Collections.Generic;
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
using Record.Connection;
using System.Data;

namespace Record
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DisplayData();
            CbFill();
            SetFormatting();
            Groubs();
            CbMonth.IsEnabled = false;
        }
        private void SetFormatting()
        {
            //this.DGStudents.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //this.DGStudents.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //this.DGStudents.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            //this.DGStudents.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        public void DisplayData()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
          //          connection.Open();
          //          string query = $@"SELECT Devices.ID, Types.Class, Titles.Title, Devices.Number, Conditions.Condition ,NumberKabs.NumKab ,Devices.StartWork,Users.Login, Brands.Brand, Models.Model
          //                              FROM Devices JOIN  Types
          //                              ON Devices.IDType = Types.ID
          //                              JOIN  Conditions
          //                              ON Devices.IDCondition = Conditions.ID
          //                              JOIN  NumberKabs
          //                              ON Devices.IDKabuneta = NumberKabs.ID
          //                              JOIN Titles
          //                              ON Devices.IDTitle = Titles.ID
										//JOIN Users
										//ON Devices.IDAddUser = Users.ID
										//JOIN Brands
										//ON Devices.IDBrand = Brands.ID					
										//JOIN Models
										//ON Devices.IDModel = Models.ID;";
          //          SQLiteCommand cmd = new SQLiteCommand(query, connection);
          //          DataTable DT = new DataTable("Devices");
          //          SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
          //          SDA.Fill(DT);
          //          DGStudents.ItemsSource = DT.DefaultView;
          //          cmd.ExecuteNonQuery();
                    //Login.Text = $"Ваш логин: " + Saver.Login;


                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }
        public void CbFill()  //Данные для комбобоксов 
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query1 = $@"SELECT * FROM Groups"; // Группы
                    string query2 = $@"SELECT * FROM Months"; // Типы

                    //----------------------------------------------
                    SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);

                    //----------------------------------------------
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    SQLiteDataAdapter SDA2 = new SQLiteDataAdapter(cmd2);
                    //----------------------------------------------
                    DataTable dt1 = new DataTable("Groups");
                    DataTable dt2 = new DataTable("Months");
                    //----------------------------------------------
                    SDA1.Fill(dt1);
                    SDA2.Fill(dt2);
                    //----------------------------------------------
                    CbGroups.ItemsSource = dt1.DefaultView;
                    CbGroups.DisplayMemberPath = "NameGroup";
                    CbGroups.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CbMonth.ItemsSource = dt2.DefaultView;
                    CbMonth.DisplayMemberPath = "Month";
                    CbMonth.SelectedValuePath = "ID";


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e) //Группы
        {
            if (CbGroups.SelectedIndex != -1) { CbMonth.IsEnabled = true; } else { CbMonth.IsEnabled = true; }
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    // MessageBox.Show(CbGroups.DisplayMemberPath.ToString());//нет
                    // MessageBox.Show(CbGroups.Items[CbGroups.SelectedIndex].ToString());// почти
                    String str = ((DataRowView)CbGroups.SelectedItem)["NameGroup"].ToString(); //Вывел
                    bool resultClass = int.TryParse(CbGroups.SelectedValue.ToString(), out Saver.idGroup);
                    // MessageBox.Show(str);
                    Saver.groups = str ;
                    ////MessageBox.Show (Saver.groups);
                   
                    //connection.Open();
                    ////string query = $@"SELECT Groups.ID, Groups.NameGroup, Students.NSM
                    ////            FROM Groups JOIN Students ON Groups.IDStudent = Students.ID 
                    ////            Where Groups.NameGroup = '{str}';";
                    //string query = $@"SELECT Students.NSM,Groups.NameGroup, Months.Month, Traffics.Day1,Traffics.Day2 FROM Students 
                    //            JOIN Months on Traffics.ID = Months.ID
                    //            JOIN Traffics on Students.IDTraffic = Traffics.ID
                    //            JOIN Groups on Students.IDGroup = Groups.ID
                    //            WHERE Groups.NameGroup = '{Saver.groups}' and Traffics.IDMonth = '{Saver.month}';";
                    //SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    ////cmd.Parameters.AddWithValue("@Number", TbNumber.Text);
                    //DataTable DT = new DataTable("Students");
                    //SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    //SDA.Fill(DT);
                    //DGStudents.ItemsSource = DT.DefaultView;
                    //cmd.ExecuteNonQuery();
                    //Traffics();

                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
        }
        public void Groubs()//Студенты
        {
            
        }
        public void Traffics()//дни
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    string query = $@"SELECT Students.NSM,Traffics.Day1 FROM Traffics
                                    JOIN Students On Traffics.IDStudents = Students.ID";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    //cmd.Parameters.AddWithValue("@Number", TbNumber.Text);
                    DataTable DT = new DataTable("Students");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    DGStudents.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }


            
        }

        private void CbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e) //Месяцы
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    // MessageBox.Show(CbGroups.DisplayMemberPath.ToString());//нет
                    // MessageBox.Show(CbGroups.Items[CbGroups.SelectedIndex].ToString());// почти
                    String str = ((DataRowView)CbMonth.SelectedItem)["Month"].ToString(); //Вывел
                    bool resultClass = int.TryParse(CbMonth.SelectedValue.ToString(), out Saver.idmonth);
                    Saver.month = str;
                    //MessageBox.Show(Saver.month);

                    //connection.Open();
                    //string query = $@"SELECT Students.NSM,Groups.NameGroup, Months.Month, Traffics.Day1,Traffics.Day2 FROM Students 
                    //            JOIN Months on Traffics.ID = Months.ID
                    //            JOIN Traffics on Students.IDTraffic = Traffics.ID
                    //            JOIN Groups on Students.IDGroup = Groups.ID
                    //            WHERE Groups.NameGroup = '{Saver.groups}' and Traffics.IDMonth = '{Saver.month}';";
                    //SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    ////cmd.Parameters.AddWithValue("@Number", TbNumber.Text);
                    //DataTable DT = new DataTable("Students");
                    //SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    //SDA.Fill(DT);
                    //DGStudents.ItemsSource = DT.DefaultView;
                    //cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)//поиск
        {
            if ((CbGroups.SelectedIndex != -1) && (CbMonth.SelectedIndex != -1)) 
            {
                try
                {
                    using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                    {
                        // MessageBox.Show(CbGroups.DisplayMemberPath.ToString());//нет
                        // MessageBox.Show(CbGroups.Items[CbGroups.SelectedIndex].ToString());// почти
                        //String str = ((DataRowView)CbMonth.SelectedItem)["Month"].ToString(); //Вывел
                        //Saver.month = str;
                        //MessageBox.Show(Saver.month);
                        connection.Open();
                        //MessageBox.Show(Saver.groups);
                        //MessageBox.Show(Saver.month);
                        string query = $@"SELECT Students.ID,Students.NSM,Groups.NameGroup, Months.Month, Traffics.Day1,Traffics.Day2,Traffics.Day3,Traffics.Day4,Traffics.Day5,Traffics.Day6,Traffics.Day7,Traffics.Day8,Traffics.Day9,Traffics.Day10,Traffics.Day11,Traffics.Day12,Traffics.Day13,Traffics.Day14,Traffics.Day15,Traffics.Day16,Traffics.Day17,Traffics.Day18,Traffics.Day19,Traffics.Day20,Traffics.Day21,Traffics.Day22,Traffics.Day23,Traffics.Day24,Traffics.Day25,Traffics.Day26,Traffics.Day27,Traffics.Day28,Traffics.Day29,Traffics.Day31,Traffics.Day31  FROM Students  
                                        JOIN Traffics on Students.IDTraffic = Traffics.ID
                                        JOIN Groups on Students.IDGroup = Groups.ID
                                        JOIN Months on Traffics.IDMonth = Months.ID
                        WHERE  Groups.ID = '{Saver.idGroup}' and Traffics.IDMonth = '{Saver.idmonth}'";//Дописать Дни
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        DataTable DT = new DataTable("Students");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DGStudents.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception exp) { MessageBox.Show(exp.Message); }
            }
            else
            {
                MessageBox.Show("----");
            }
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)//обновляем данные при их изменениях(по кнопке)
        {
            Stroka();
        }

        private void DGStudents_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)//обновляем данные при их изменениях(enter, ....
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    if (DGStudents.SelectedItems.Count > 0)
                    {
                       DataRowView row = (DataRowView)DGStudents.SelectedItems[0];
                        connection.Open();
                        Stroka();
                        TbNumber.Text = row["Day1"].ToString();
                        Saver.a = row["Day1"].ToString();
                        if (Saver.a == "Н" ) { MessageBox.Show("Увидел"); } else { MessageBox.Show("-"); }
                        MessageBox.Show(row["Day1"].ToString());
                        string query = $@"UPDATE  Traffics 
                        SET  Day1 = '{Saver.a}'
                        WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' ";//Дописать Дни
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                    }                   

                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); } 
        }
        public void Stroka()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))//Узнаем кого выбрали + его айди(в студентах)
            {
                try
                {

                    foreach (var item in DGStudents.SelectedItems.Cast<DataRowView>())
                    {
                        DataRowView row = (DataRowView)DGStudents.SelectedItems[0];
                        Saver.afv = item["NSM"];
                        Saver.NameFirst = row["NSM"].ToString(); 
                         MessageBox.Show(Saver.NameFirst);
                        //MessageBox.Show(Saver.afv);
                        connection.Open();
                        string query = $@"SELECT ID FROM  Students  WHERE NSM=@NSM ";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        cmd.Parameters.AddWithValue("@NSM", Saver.NameFirst);
                        int countID = Convert.ToInt32(cmd.ExecuteScalar());
                        Saver.IDNSM = countID;
                        //MessageBox.Show('{Saver.IDNSM}');
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }
    }  
}
