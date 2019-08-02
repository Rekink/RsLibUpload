using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace RsLibUpload
{   
    public class Upload
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 文件格式
        /// </summary>
        public string fileFormat { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string fileExtension { get; set; }

        /// <summary>
        /// 解压文件的完整路径
        /// </summary>
        public string extractPath { get; set; }

        public List<string> allFillPath { get;set; }

        /// <summary>
        /// jy实例
        /// </summary>
        //private JyDoc _doc;


        public Upload()
        {
            allFillPath = new List<string>();
        }
        /// <summary>
        /// 获取文件格式
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string CheckFileType(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string bx = " ";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                bx = buffer.ToString();
                buffer = r.ReadByte();
                bx += buffer.ToString();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            r.Close();
            fs.Close();
            // 真实的文件类型
            fileExtension = bx;
            // 文件格式
            fileFormat = System.IO.Path.GetExtension(path);
            return fileFormat;
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public enum FileExtension
        {
            JPG = 255216,
            GIF = 7173,
            BMP = 6677,
            PNG = 13780,
            COM = 7790,
            EXE = 7790,
            DLL = 7790,
            RAR = 8297,
            ZIP = 8075,
            XML = 6063,
            HTML = 6033,
            ASPX = 239187,
            CS = 117115,
            JS = 119105,
            TXT = 210187,
            SQL = 255254,
            BAT = 64101,
            BTSEED = 10056,
            RDP = 255254,
            PSD = 5666,
            PDF = 3780,
            CHM = 7384,
            LOG = 70105,
            REG = 8269,
            HLP = 6395,
            DOC = 208207,
            XLS = 208207,
            DOCX = 208207,
            XLSX = 208207,
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="fileName">文件所在目录完整路径</param>
        public void Decompression(string filePath)
        {
            if (!File.Exists(filePath))
                return;
            // 指定解压路径
            extractPath = filePath + @".Extract";
            if (!Directory.Exists(extractPath))
            {
                // 解压 
                ZipFile.ExtractToDirectory(filePath, extractPath);
            
                // 打开此目录
                //System.Diagnostics.Process.Start(extractPath);
            }
        }

        /// <summary>
        /// 删除指定文件夹下非指定格式的文件
        /// </summary>
        /// <param name="filePath">指定文件夹路径</param>
        /// <param name="format">指定文件格式'.'</param>
        public void DeleteFile(string filePath,string format)
        {     
            if (!Directory.Exists(filePath))
                return;
            DirectoryInfo folder = new DirectoryInfo(filePath);
            FileInfo[] fileList = folder.GetFiles();
            foreach (FileInfo file in fileList)
            {            
                if (file.Extension != format)
                {
                    file.Delete();  // 删除
                }
            }      
        }

        /// <summary>
        /// 返回指定文件下所有文件的完整路径集合
        /// </summary>
        /// <param name="filePath">指定文件夹路径</param>
        /// <returns></returns>
        public List<string> GetFilePath(string filePath)
        {
            var path = new List<string>();         
            DirectoryInfo folder = new DirectoryInfo(filePath);
            FileInfo[] fileList = folder.GetFiles();
            foreach (FileInfo file in fileList)
            {        
                path.Add(file.FullName);           
            }
            return path;
        }

        /// <summary>
        /// 递归获取文件夹下所有的文件完整路径路径集合
        /// </summary>
        /// <param name="fillPath"></param>
        public void GetAllFillPath(string fillPath)
        {         
            DirectoryInfo dir = new DirectoryInfo(fillPath);
            FileInfo[] allFile = dir.GetFiles();
            foreach (FileInfo fi in allFile)
            {
                allFillPath.Add(fi.FullName);
            }
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                GetAllFillPath(d.FullName);
            }      
        }

        public void ReadFile(string filePath)
        {
            try
            {
                // 从Jy文件中读取数据   
                //_doc = new JyDoc(File.ReadAllBytes(filePath));         
            }
            catch (Exception ex)
            {           
              
            }
        }
        

    }
}
