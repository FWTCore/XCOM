using CodeGeneration.Model;
using CodeGeneration.Utility;
using System;
using System.Collections;
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
using XCOM.Schema.Standard.Utility;

namespace CodeGeneration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DBConnection _selectDB;
        private static List<TableEntity> _tableData;
        private static string _outFilePath;

        public MainWindow()
        {
            InitializeComponent();
            this.Load();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result =
                MessageBox.Show(
                  "你确定要关闭软件？",
                  "提示",
                  MessageBoxButton.YesNo,
                  MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Load()
        {
            this.ddlDbKey.ItemsSource = XMDBConfig.ConfigSetting.DBConnectionList;
            this.ddlDbKey.DisplayMemberPath = "Key";
            this.ddlDbKey.SelectedValuePath = "Key";
            _outFilePath = System.IO.Path.Combine(ToolConstant.PatchBaseFolder, "FileOutput");
            XMDirectory.CreateDirectory(_outFilePath);
            this.txtOutput.Text = _outFilePath;

        }

        private void btnSure_Click(object sender, RoutedEventArgs e)
        {
            if (this.ddlDbKey.SelectedItem == null)
            {
                MessageBox.Show("请选择dbKey", "提示");
                return;
            }
            this._selectDB = (DBConnection)this.ddlDbKey.SelectedItem;

            var cmd = new XMSqlCommand($"GetDataBase_{this._selectDB.DBType}", this._selectDB.Key);
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
            this.noselectContent.ItemsSource = dataList.Select(e => e.TableName).ToList();
            this.selectContent.ItemsSource = null;
        }

        private void btnLeftAll_Click(object sender, RoutedEventArgs e)
        {
            var dataSource = selectContent.ItemsSource;
            if (dataSource == null)
            {
                MessageBox.Show("没有选项需要全部左移");
                return;
            }
            selectContent.ItemsSource = null;
            noselectContent.ItemsSource = _tableData.Select(e => e.TableName).OrderBy(e => e);
        }



        private void btnLeftOne_Click(object sender, RoutedEventArgs e)
        {
            var selectItem = selectContent.SelectedItem;
            if (selectItem == null)
            {
                MessageBox.Show("请选择需要左移的选项");
                return;
            }

            var resultData = UploadSource(selectContent.ItemsSource, noselectContent.ItemsSource, (string)selectItem);

            selectContent.ItemsSource = resultData.Item1.OrderBy(e => e);
            noselectContent.ItemsSource = resultData.Item2.OrderBy(e => e);
        }

        private void btnRightOne_Click(object sender, RoutedEventArgs e)
        {
            var selectItem = noselectContent.SelectedItem;
            if (selectItem == null)
            {
                MessageBox.Show("请选择需要右移的选项");
                return;
            }
            var resultData = UploadSource(noselectContent.ItemsSource, selectContent.ItemsSource, (string)selectItem);
            noselectContent.ItemsSource = resultData.Item1.OrderBy(e => e);
            selectContent.ItemsSource = resultData.Item2.OrderBy(e => e);

        }

        private void btnRightAll_Click(object sender, RoutedEventArgs e)
        {
            var dataSource = noselectContent.ItemsSource;
            if (dataSource == null)
            {
                MessageBox.Show("没有选项需要全部右移");
                return;
            }
            noselectContent.ItemsSource = null;
            selectContent.ItemsSource = _tableData.Select(e => e.TableName).OrderBy(e => e);

        }

        private Tuple<List<string>, List<string>> UploadSource(IEnumerable dataCource, IEnumerable targetCource, string itemData)
        {
            var dataCourceList = new List<string>();
            var dataTargetList = new List<string>();
            if (dataCource != null)
            {
                foreach (string item in dataCource)
                {
                    dataCourceList.Add(item);
                }
            }
            if (targetCource != null)
            {

                foreach (string item in targetCource)
                {
                    dataTargetList.Add(item);
                }
            }
            dataCourceList.Remove(itemData);
            dataTargetList.Add(itemData);

            return new Tuple<List<string>, List<string>>(dataCourceList, dataTargetList);

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            BuilderType builderType = BuilderType.NONE;
            if (this.cbEntity.IsChecked != null && this.cbEntity.IsChecked == true)
            {
                builderType |= BuilderType.BuilderEntity;
                XMDirectory.CreateDirectory(System.IO.Path.Combine(_outFilePath, ToolConstant.PatchEntityFile));
            }
            if (this.cbRepository.IsChecked != null && this.cbRepository.IsChecked == true)
            {
                builderType |= BuilderType.BuilderRepository;
                XMDirectory.CreateDirectory(System.IO.Path.Combine(_outFilePath, ToolConstant.PatchRepositoryFile));
                XMDirectory.CreateDirectory(System.IO.Path.Combine(_outFilePath, ToolConstant.PatchIRepositoryFile));
            }
            if (this.cbServer.IsChecked != null && this.cbServer.IsChecked == true)
            {
                builderType |= BuilderType.BuilderService;
                XMDirectory.CreateDirectory(System.IO.Path.Combine(_outFilePath, ToolConstant.PatchServiceFile));
                XMDirectory.CreateDirectory(System.IO.Path.Combine(_outFilePath, ToolConstant.PatchIServiceFile));
            }
            if (builderType == BuilderType.NONE)
            {
                MessageBox.Show("请选择生成对象", "提示");
                return;
            }
            var generatorDataList = selectContent.Items;
            if (generatorDataList == null || generatorDataList.Count == 0)
            {
                MessageBox.Show("请设置需要生成的表");
                return;
            }
            var generatorTableList = new List<string>();
            foreach (string item in generatorDataList)
            {
                generatorTableList.Add(item);
            }

            OperationEnabledStatus(false);
            ToolCode.BuilderCode(this._selectDB, (string)this.ddlDB.SelectedItem, generatorTableList, _tableData, builderType, $"{txtNameSpace.Text.Trim()}.", txtOutput.Text.Trim());
            OperationEnabledStatus(true);

            MessageBox.Show("代码生成完成");
        }

        private List<TableEntity> GetTableEntities()
        {
            var cmd = new XMSqlCommand($"GetTable_{this._selectDB.DBType}", this._selectDB.Key);
            var dbName = (string)this.ddlDB.SelectedItem;
            cmd.SetParameter("@dbName", dbName);
            _tableData = cmd.Query<TableEntity>().ToList();
            return _tableData;
        }

        private void OperationEnabledStatus(bool isEnabled)
        {
            this.Dispatcher.Invoke(new Action(delegate
            {
                ddlDbKey.IsEnabled = isEnabled;
                btnSure.IsEnabled = isEnabled;

                ddlDB.IsEnabled = isEnabled;
                btnDBSure.IsEnabled = isEnabled;

                txtNameSpace.IsEnabled = isEnabled;

                cbEntity.IsEnabled = isEnabled;
                cbRepository.IsEnabled = isEnabled;
                cbServer.IsEnabled = isEnabled;

                btnStart.IsEnabled = isEnabled;

                noselectContent.IsEnabled = isEnabled;
                btnLeftAll.IsEnabled = isEnabled;
                btnLeftOne.IsEnabled = isEnabled;
                btnRightOne.IsEnabled = isEnabled;
                btnRightAll.IsEnabled = isEnabled;
                selectContent.IsEnabled = isEnabled;
            }));
        }
    }
}
