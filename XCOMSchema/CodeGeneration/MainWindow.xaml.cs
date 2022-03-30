using CodeGeneration.Model;
using CodeGeneration.Service;
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
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.SQLClient;
using XCOM.Schema.EDapper.Utility;
using XCOM.Schema.Standard.Configuration;

namespace CodeGeneration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string dbKey = "";

        private IGenerationServer generationServer;

        public MainWindow(IGenerationServer generationServer)
        {
            InitializeComponent();
            this.generationServer = generationServer;
            this.Load();
        }

        private void Load()
        {
            this.ddlDbKey.ItemsSource = XMDBConfig.ConfigSetting.DBConnectionList;
            this.ddlDbKey.DisplayMemberPath = "Key";
            this.ddlDbKey.SelectedValuePath = "Key";
            this.txtOutput.Text = this.generationServer.GetTest();
                //XMConfiguration.GetConfigOrDefault("Appsettings:FileOutput");


        }

        private void btnSure_Click(object sender, RoutedEventArgs e)
        {
            if (this.ddlDbKey.SelectedItem == null)
            {
                MessageBox.Show("请选择dbKey", "提示");
                return;
            }
            var selectDbKey = (DBConnection)this.ddlDbKey.SelectedItem;
            this.dbKey = selectDbKey.Key;

            var cmd = new XMSqlCommand($"getDataBase_{this.dbKey}");
            var resultDataList = cmd.Query<string>();
            if (resultDataList == null || resultDataList.Count() == 0)
            {
                MessageBox.Show("该dbKey无数据库", "提示");
                return;
            }
            this.ddlDB.ItemsSource = resultDataList;
        }
        private void btnDBSure_Click(object sender, RoutedEventArgs e)
        {
            if (this.ddlDB.SelectedItem == null)
            {
                MessageBox.Show("请选择数据库", "提示");
                return;
            }
            var dataList = GetTableEntities();
            this.noselectContent.ItemsSource = dataList;
            this.noselectContent.DisplayMemberPath = "TableName";
        }

        private void btnLeftAll_Click(object sender, RoutedEventArgs e)
        {
            //if (this.noselectContent.Items == null
            //    || this.noselectContent.Items.Count == 0)
            //{
            //    return;
            //}
            //if (this.selectContent.Items != null && this.selectContent.Items.Count > 0)
            //{
            //    selectContent.Items.Clear();
            //}
            //tableDataSource.Clear();
            //this.noselectContent.Items.Clear();
            //this.selectContent.ItemsSource = data;
            //this.noselectContent.Items.Clear();

        }

        private void btnLeftOne_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRightOne_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRightAll_Click(object sender, RoutedEventArgs e)
        {
            //if (this.selectContent.Items == null
            //      || this.selectContent.Items.Count == 0)
            //{
            //    return;
            //}
            //if (this.noselectContent.Items != null && this.noselectContent.Items.Count > 0)
            //{
            //    noselectContent.Items.Clear();
            //}
            //this.noselectContent.ItemsSource = this.selectContent.ItemsSource;
            //this.selectContent.Items.Clear();

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            var dealTableData = GetTableEntities();
            if (dealTableData != null && dealTableData.Count > 0)
            {

            }
        }

        private List<TableEntity> GetTableEntities()
        {
            var cmd = new XMSqlCommand($"getTable_{this.dbKey}");
            cmd.SetParameter("@dbName", this.ddlDB.SelectedItem);
            return cmd.Query<TableEntity>().ToList();
        }

    }
}
