using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using MiniExcelLibs;
using MyConsole.dao;
using MyConsole.domain;
using WPFDialogs;
using XLabs;

namespace Infor.model
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Tex> Info { get; set; }
        
        public ICommand OpenDirectoryCommand { get; }
        
        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue();

        public MainWindowViewModel()
        {
            //列表展示初始化数据
            LoadData();
            BindingOperations.EnableCollectionSynchronization(Info, new object());
            //按钮 导出Excel执行命令
            OpenDirectoryCommand = new RelayCommand(OnOpenDirectory);
        }
        private void LoadData()
        {
            //加载数据展示在列表上
            Info = new ObservableCollection<Tex>();
            TexDao texDao = new TexDao();
            var texList = texDao.GetTexList();
            foreach (Tex tex in texList)
            {
                Info.Add(tex);
            }
        }
        
        private void OnOpenDirectory()
        {
            //将Excel保存至文件夹
            var dialog = new FolderBrowserDialog()
            {
                Description = "导出Excel"
            };

            if (dialog.ShowDialog() == true)
            {
                Task.Run(() => ExportExcel(dialog.SelectedPath));
            }
        }

        private void ExportExcel(string dirPath)
        {
            try
            {
                //从数据库查询数据，做成Excel对应的模板输出格式
                TexDao texDao = new TexDao();
                var texList = texDao.GetTexList();
                var value = new
                {
                    tex = texList
                };
                //MiniExcel导出模板的使用方式
                MiniExcel.SaveAsByTemplate(dirPath+"//个人信息.xlsx","template.xlsx",value);
                //弹出Message提示信息
                MessageBox.Show("导出表格成功");
            }
            catch (Exception e)
            {
                MessageBox.Show("导出表格成功");
                Console.WriteLine(e);
                throw;
            }
            
        }
    }

}