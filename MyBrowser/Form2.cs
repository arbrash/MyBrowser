using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MyBrowser
{
    public partial class Form2 : Form
    {
        public Form2(string path)
        {
            InitializeComponent();
            if (Directory.Exists(path))
            {
                if (path == @"我的电脑")
                {
                    return;
                }
                else if (path == @"C:\"||path == @"D:\"||path == @"E:\"||path == @"F:\"||path == @"G:\"||path == @"H:\"||path == @"I:\")
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    this.DirName = dir.Name;
                    this.DirType = "驱动";
                    this.DirLocation = "无";
                    this.DirCreationTime = dir.CreationTime.ToString();
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    this.DirName = dir.Name;
                    this.DirType = "文件夹";
                    this.DirLocation = (dir.Parent != null) ? dir.Parent.FullName : null;
                    this.DirCreationTime = dir.CreationTime.ToString();
                }
            }
            this.Show();
        }
        public string DirName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string DirType
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        public string DirLocation
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public string DirCreationTime
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
    }
}
