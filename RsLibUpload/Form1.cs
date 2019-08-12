using RsLib;
using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace RsLibUpload
{
    public partial class Form1 : Form
    {
        //private JyDoc _doc;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        private JyDoc _doc;
        private string extractPath;

        private  Upload m_Analysis;
        private void button1_Click(object sender, EventArgs e)
        {
            m_Analysis = new Upload();
            OpenFileDialog dlg = new OpenFileDialog();      // 定义打开文件
            dlg.InitialDirectory = Application.StartupPath; // 初始路径,这里设置的是程序的起始位置，可自由设置    
            dlg.Filter = "All files (*.*)|*.*";             //（这是全部类型文件）
            dlg.FilterIndex = 2;                            // 文件类型的显示顺序（上一行.txt设为第二位）
            dlg.RestoreDirectory = true;                    // 对话框记忆之前打开的目录
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.FileName.ToString();    // 获得完整路径在textBox1中显示

                try
                {
                    if (Path.GetExtension(dlg.FileName) == ".zip")
                    {
                        // 解压zip压缩文件
                        m_Analysis.Decompression(dlg.FileName);

                        extractPath = dlg.FileName + @".Extract";
                        // 删除非指定文件格式文件
                        m_Analysis.DeleteFile(extractPath, comboBox1.SelectedItem.ToString());
                        // 获取指定文件夹下所有文件的完整路径集合
                        // var list = m_Analysis.GetFilePath(extractPath);
                        m_Analysis.GetAllFillPath(extractPath);
                        textBox4.Text += m_Analysis.allFillPath[0];
                        foreach (var file in m_Analysis.allFillPath)
                        {
                            textBox2.Text += file + "\r\n";
                        }

                        // 打开此目录
                        //System.Diagnostics.Process.Start(dlg.FileName + @".Extract");
                    }
                    else
                    {
                        // 单文件
                        // 从Jy文件中读取数据并构造 JyDoc 对象
                        _doc = new JyDoc(File.ReadAllBytes(textBox4.Text.ToString()));
                    }
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Read_Button_Click(object sender, EventArgs e)
        {          
            // 从Jy文件中读取数据并构造 JyDoc 对象
             _doc = new JyDoc(File.ReadAllBytes(textBox4.Text.ToString()));
        }

        /// <summary>
        /// 删除解压后文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            // 删除整个文件夹
            Directory.Delete(extractPath, true);

            //// 删除文件
            //string path = extractPath;
            //DirectoryInfo di = new DirectoryInfo(path);
            //FileAttributes attr = File.GetAttributes(path);
            //if (di.Exists)
            //{
            //    Directory.Delete(path, true);
            //}         
        }
    }
}
