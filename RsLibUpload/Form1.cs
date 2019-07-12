using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RsLibUpload
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private  RsLibUpload m_Analysis;
        private void button1_Click(object sender, EventArgs e)
        {
            m_Analysis = new RsLibUpload();
            OpenFileDialog dlg = new OpenFileDialog();      // 定义打开文件
            dlg.InitialDirectory = Application.StartupPath; // 初始路径,这里设置的是程序的起始位置，可自由设置    
            dlg.Filter = "All files (*.*)|*.*";             //（这是全部类型文件）
            dlg.FilterIndex = 2;                            // 文件类型的显示顺序（上一行.txt设为第二位）
            dlg.RestoreDirectory = true;                    // 对话框记忆之前打开的目录
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.FileName.ToString();    // 获得完整路径在textBox1中显示
                
                if (System.IO.Path.GetExtension(dlg.FileName)==".zip")
                {
                    // 解压zip压缩文件
                    m_Analysis.Decompression(dlg.FileName);

                    string extractPath = dlg.FileName + @".Extract";
                    // 删除非指定文件格式文件
                    m_Analysis.DeleteFile(extractPath, textBox3.Text.ToString());
                    // 获取指定文件夹下所有文件的完整路径集合
                    var list = m_Analysis.GetFilePath(extractPath);
                   
                    foreach (var file in list)
                    {
                        textBox2.Text += file + "\r\n";
                    }

                    // 打开此目录
                    //System.Diagnostics.Process.Start(dlg.FileName + @".Extract");
                }          
                else
                {
                    // 单文件

                }



            }
        }
    }
}
