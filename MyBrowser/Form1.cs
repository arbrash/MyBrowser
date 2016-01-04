using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBrowser
{
    public partial class Form1 : Form
    {
        private Stack<string> hPath;//存储上一结点路径
        private Stack<string> sPath;//存储下一结点路径
        private string cPath = "";
        private string[] sources = new string[200];
        private bool IsMove = false;

        public Form1()
        {
            InitializeComponent();
            LoadDrivers();
        }

        void LoadDrivers()
        {
            listView1.Clear();
            TreeNode NodeDir = new TreeNode("我的电脑", 3, 3);
            treeView1.Nodes.Add(NodeDir);
            DriveInfo[] drives = DriveInfo.GetDrives();
            cPath = "我的电脑";
            toolStripComboBox1.Text = cPath;
            foreach (DriveInfo drive in drives)
            {
                ListViewItem item = listView1.Items.Add(drive.Name);
                if (drive.DriveType == DriveType.CDRom)
                {                   
                    item.Name = drive.Name;
                    TreeNode objNode = new TreeNode(drive.Name, 2, 2);
                    NodeDir.Nodes.Add(objNode);
                    Icon drvIcon = GetSystemIcon.GetIconByFileName(drive.Name);
                    imageList2.Images.Add(drive.Name, drvIcon);
                    imageList3.Images.Add(drive.Name, drvIcon);
                    item.ImageKey = drive.Name;
                }
                else if (drive.DriveType == DriveType.Fixed)
                {
                    item.Name = drive.Name;
                    TreeNode objNode = new TreeNode(drive.Name, 1, 1);
                    NodeDir.Nodes.Add(objNode);
                    Icon drvIcon = GetSystemIcon.GetIconByFileName("c:\\");
                    imageList2.Images.Add(drive.Name, drvIcon);
                    imageList3.Images.Add(drive.Name, drvIcon);
                    item.ImageKey = drive.Name;
                }
                else if (drive.DriveType == DriveType.Removable)
                {
                    item.Name = drive.Name;
                    TreeNode objNode = new TreeNode(drive.Name, 4, 4);
                    NodeDir.Nodes.Add(objNode);
                    Icon drvIcon = GetSystemIcon.GetIconByFileName(drive.Name);
                    imageList2.Images.Add(drive.Name, drvIcon);
                    imageList3.Images.Add(drive.Name, drvIcon);
                    item.ImageKey = drive.Name;
                }
                item.SubItems.Add("");
                item.SubItems.Add("驱动");
                item.SubItems.Add("");
                this.listView1.Columns.Add("名称", this.listView1.Width / 4, HorizontalAlignment.Center);
                this.listView1.Columns.Add("大小", this.listView1.Width / 4, HorizontalAlignment.Center);
                this.listView1.Columns.Add("类型", this.listView1.Width / 4, HorizontalAlignment.Center);
                this.listView1.Columns.Add("修改时间", this.listView1.Width / 4, HorizontalAlignment.Center);
                imageList2.ColorDepth = ColorDepth.Depth32Bit;
            }
        }

        void AddNodes(TreeNode aNode)
        {
            try
            {
                string str = aNode.FullPath.Remove(0, @"我的电脑\".Length);
                DirectoryInfo dir = new DirectoryInfo(str);
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo di in dirs)
                {
                    aNode.Nodes.Add(di.Name);
                }
            }
            catch { }
        }

        void ListDrive()
        {
            listView1.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                ListViewItem item = listView1.Items.Add(drive.Name);
                if (drive.DriveType == DriveType.CDRom)
                {
                    item.Name = drive.Name;
                    Icon drvIcon = GetSystemIcon.GetIconByFileName(drive.Name);
                    imageList2.Images.Add(item.Name, drvIcon);
                    imageList3.Images.Add(item.Name, drvIcon);
                    item.ImageKey = drive.Name;
                }
                else if (drive.DriveType == DriveType.Fixed)
                {
                    item.Name = drive.Name;
                    Icon drvIcon = GetSystemIcon.GetIconByFileName("c:\\");
                    imageList2.Images.Add(item.Name, drvIcon);
                    imageList3.Images.Add(item.Name, drvIcon);
                    item.ImageKey = drive.Name;
                }
                else if (drive.DriveType == DriveType.Removable)
                {
                    item.Name = drive.Name;
                    Icon drvIcon = GetSystemIcon.GetIconByFileName(drive.Name);
                    imageList2.Images.Add(item.Name, drvIcon);
                    imageList3.Images.Add(item.Name, drvIcon);
                    item.ImageKey = drive.Name;
                }
                item.SubItems.Add("");
                item.SubItems.Add("驱动");
                item.SubItems.Add("");
                imageList2.ColorDepth = ColorDepth.Depth32Bit;
            }
            this.listView1.Columns.Add("名称", this.listView1.Width / 4, HorizontalAlignment.Center);
            this.listView1.Columns.Add("大小", this.listView1.Width / 4, HorizontalAlignment.Center);
            this.listView1.Columns.Add("类型", this.listView1.Width / 4, HorizontalAlignment.Center);
            this.listView1.Columns.Add("修改时间", this.listView1.Width / 4, HorizontalAlignment.Center);
            cPath = "我的电脑";
            toolStripComboBox1.Text = cPath;
            toolStripStatusLabel1.Text = listView1.Items.Count + "个对象";
        }
        void ListShow(string path)
        {
            listView1.Items.Clear();
            try
            {
                DirectoryInfo currentDir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = currentDir.GetDirectories();
                FileInfo[] files = currentDir.GetFiles();
                foreach (DirectoryInfo dir in dirs)
                {
                    ListViewItem dirItem = listView1.Items.Add(dir.Name);
                    Icon dirIcon = GetSystemIcon.GetDirectoryIcon();
                    imageList2.Images.Add(dir.Name, dirIcon);
                    imageList3.Images.Add(dir.Name, dirIcon);
                    dirItem.SubItems.Add("");
                    dirItem.SubItems.Add("文件夹");
                    dirItem.SubItems.Add(dir.LastAccessTimeUtc.ToString());
                    dirItem.ImageKey = dir.Name;
                    dirItem.Name = dir.FullName;
                }
                foreach (FileInfo file in files)
                {
                    ListViewItem fileItem = listView1.Items.Add(file.Name);
                    Icon fileIcon = GetSystemIcon.GetIconByFileName(file.FullName);
                    imageList2.Images.Add(file.Name, fileIcon);
                    imageList3.Images.Add(file.Name, fileIcon);
                    fileItem.SubItems.Add(file.Length.ToString() + "字节");
                    fileItem.SubItems.Add(file.Extension);
                    fileItem.SubItems.Add(file.LastWriteTimeUtc.ToString());
                    fileItem.ImageKey = file.Name;
                    fileItem.Name = file.FullName;
                }
                this.listView1.Columns.Add("名称", this.listView1.Width / 4, HorizontalAlignment.Center);
                this.listView1.Columns.Add("大小", this.listView1.Width / 4, HorizontalAlignment.Center);
                this.listView1.Columns.Add("类型", this.listView1.Width / 4, HorizontalAlignment.Center);
                this.listView1.Columns.Add("修改时间", this.listView1.Width / 4, HorizontalAlignment.Center);
                imageList2.ColorDepth = ColorDepth.Depth32Bit;
                cPath = path;
                toolStripComboBox1.Text = cPath;
                toolStripStatusLabel1.Text = listView1.Items.Count + "个对象";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.toolStripButton8.Enabled = true;
                
                e.Node.Expand();
                if (e.Node.FullPath == "我的电脑")
                {
                    hPath.Push("我的电脑");
                    ListDrive();
                }
                else
                {
                    string str = e.Node.FullPath.Remove(0, @"我的电脑\".Length);
                    hPath.Push(str);
                    ListShow(str);
                }
            }
            catch { }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (e.Node.Nodes[0].Nodes.Count == 0)
                {
                    foreach (TreeNode tn in e.Node.Nodes)
                    {
                        AddNodes(tn);
                    }
                }
                else { }
            }
            catch { }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            this.toolStripButton8.Enabled = true;
            hPath.Push(cPath);
            string path = listView1.SelectedItems[0].Name;
            try
            {
                if (Directory.Exists(path))
                {
                    ListShow(path);
                }
                else
                {
                    Process.Start(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------拖放--------------------------------------------
        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
        }
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
        }
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
        }
        //-------------------------------窗口--------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            hPath = new Stack<string>();
            sPath = new Stack<string>();
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                toolStripComboBox1.Width = this.Width - 120;
                this.listView1.Columns.Clear();
                this.listView1.Columns.Add("名称", this.listView1.Width / 4, HorizontalAlignment.Center);
                this.listView1.Columns.Add("大小", this.listView1.Width / 4, HorizontalAlignment.Center);
                this.listView1.Columns.Add("类型", this.listView1.Width / 4, HorizontalAlignment.Center);
                this.listView1.Columns.Add("修改时间", this.listView1.Width / 4, HorizontalAlignment.Center);
                splitContainer1.SplitterDistance = this.toolStrip1.Height + this.toolStrip2.Height + this.menuStrip1.Height;
            }
            catch { }
        }
        //--------------------各种函数------------------------------------------------------
        void CreatDir()
        {
            try
            {
                if (cPath == @"我的电脑")
                {
                    MessageBox.Show("无法新建文件夹！");
                    return;
                }
                string path = Path.Combine(cPath, "新建文件夹");
                int i = 1;
                string newPath = path;
                while (Directory.Exists(newPath))
                {
                    newPath = path + i;
                    i++;
                }
                Directory.CreateDirectory(newPath);
                DirectoryInfo dir = new DirectoryInfo(newPath);
                ListViewItem dirItem = listView1.Items.Add("新建文件夹" + (i - 1 == 0 ? "" : (i - 1).ToString()));
                dirItem.SubItems.Add("新建文件夹" + (i - 1 == 0 ? "" : (i - 1).ToString()));
                Icon dirIcon = GetSystemIcon.GetDirectoryIcon();
                imageList2.Images.Add(dir.Name, dirIcon);
                dirItem.ImageKey = dir.Name;
                dirItem.Name = dir.FullName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void CopyFile(string fName)
        {
            try
            {
                FileInfo file = new FileInfo(fName);
                string destination = Path.Combine(cPath, file.Name);
                if (destination == cPath)
                    return;

                if (IsMove)
                    file.MoveTo(destination);
                else
                    file.CopyTo(destination);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void CopyFileInDir(DirectoryInfo fName, DirectoryInfo tName)
        {
            FileInfo[] files = fName.GetFiles();
            DirectoryInfo[] dirs = fName.GetDirectories();
            if (Directory.Exists(tName.FullName) == false)
            {
                Directory.CreateDirectory(tName.FullName);
            }
            foreach (FileInfo fi in files)
            {
                fi.CopyTo(Path.Combine(tName.ToString(), fi.Name));
            }
            foreach (DirectoryInfo diSourceSubDir in dirs)
            {
                DirectoryInfo nextTargetSubDir = tName.CreateSubdirectory(diSourceSubDir.Name);
                CopyFileInDir(diSourceSubDir, nextTargetSubDir);
            }
        }
        void CopyDir(string sName)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sName);
                string destination = Path.Combine(cPath, dir.Name);
                if (destination == sName)
                    return;

                if (IsMove)
                    dir.MoveTo(destination);
                else
                {
                    CopyFileInDir(dir, new DirectoryInfo(destination));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void Cut()
        {
            if (cPath == @"我的电脑")
            {
                MessageBox.Show("无法剪切！");
                return;
            }
            Copy();
            IsMove = true;
        }
        void Copy()
        {
            if (cPath == @"我的电脑")
            {
                MessageBox.Show("无法复制！");
                return;
            }
            if (listView1.SelectedItems.Count == 0)
                return;
            sources = new string[200];
            int i = 0;
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                sources[i++] = item.Name;
            }
            IsMove = false;
        }
        void Paste()
        {
            if (sources[0] == null)
            {
                MessageBox.Show("请先剪切或复制文件！");
                return;
            }
            if (!Directory.Exists(cPath))
                return;
            if (cPath == @"我的电脑")
            {
                MessageBox.Show("无法粘贴！");
                return;
            }
            for (int i = 0; sources[i] != null; i++)
            {
                if (File.Exists(sources[i]))
                {
                    CopyFile(sources[i]);
                }
                else if (Directory.Exists(sources[i]))
                {
                    CopyDir(sources[i]);
                }
            }
            ListShow(cPath);
            sources = new string[200];
        }
        void Delete()
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            if (cPath == @"我的电脑")
            {
                MessageBox.Show("无法删除！");
                return;
            }
            try
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    string path = item.Name;
                    if (File.Exists(path))
                    {
                        string fileName = Path.GetFileName(path);
                        DialogResult result = MessageBox.Show("确定要删除文件" + "“" + item.SubItems[0].Text + "”" + "吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.No)
                            return;
                        File.Delete(path);
                        MessageBox.Show("成功删除！");
                        listView1.Items.Remove(item);
                    }
                    else if (Directory.Exists(path))
                    {
                        DialogResult result = MessageBox.Show("确定要删除文件夹" + "“" + item.SubItems[0].Text + "”" + "吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.No)
                            return;
                        Directory.Delete(path, true);
                        MessageBox.Show("成功删除！");
                        listView1.Items.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Attributes()
        {
            if (listView1.SelectedItems.Count == 0)
            {
                Form2 FormAttributes1 = new Form2(cPath);
            }
            else
            {
                if (Directory.Exists(listView1.SelectedItems[0].Name))
                {
                    Form2 FormAttributes1 = new Form2(listView1.SelectedItems[0].Name);
                }
                else if (File.Exists(listView1.SelectedItems[0].Name))
                {
                    Form3 FormAttributes1 = new Form3(listView1.SelectedItems[0].Name);
                }
            }
        }
        //--------------------工具栏------------------------------------------------------------
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string Path = toolStripComboBox1.Text;
            if (Path == "")
                return;
            ListShow(Path);
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            this.toolStripButton2.Enabled = true;
            sPath.Push(cPath);
            if (hPath.Count > 0)
            {
                string path = hPath.Pop();
                if (path == "我的电脑")
                {
                    ListDrive();
                }
                else
                {

                    DirectoryInfo dir = new DirectoryInfo(path);
                    if (dir != null)
                    {
                        ListShow(dir.FullName);
                    }
                    else { }
                }
            }
            else
            {
                MessageBox.Show("无法后退!");
                this.toolStripButton8.Enabled = false;
                return;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.toolStripButton8.Enabled = true;
            hPath.Push(cPath);
            if (sPath.Count > 0)
            {
                string path = sPath.Pop();
                if (path == "我的电脑")
                {
                    ListDrive();
                }
                else
                {

                    DirectoryInfo dir = new DirectoryInfo(path);
                    if (dir != null)
                    {
                        ListShow(dir.FullName);
                    }
                    else { }
                }
            }
            else
            {
                MessageBox.Show("无法前进!");
                this.toolStripButton2.Enabled = false;
                return;
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (cPath == "我的电脑")
            {
                MessageBox.Show("已到根目录无法向上！");
                return;
            }
            this.toolStripButton8.Enabled = true;
            hPath.Push(cPath);
            sPath.Clear();
            
            DirectoryInfo dir = new DirectoryInfo(cPath);
            if (dir.Parent != null)
            {
                ListShow(dir.Parent.FullName);
            }
            else
            {
                ListDrive();
            }
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Delete();
        }
        //----------------------------右键菜单--------------------------------------------------------
        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
        }
        private void 新建文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatDir();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void 新建文件夹ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CreatDir();
        }
        //---------------------------------菜单栏------------------------------------------------
        private void ClearCheck()
        {
            平铺ToolStripMenuItem.Checked = false;
            图标ToolStripMenuItem.Checked = false;
            详细信息ToolStripMenuItem.Checked = false;
        }
        private void 工具栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = !toolStrip1.Visible;
        }

        private void 菜单栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip2.Visible = !toolStrip2.Visible;
        }

        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = !statusStrip1.Visible;
        }

        private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Attributes();
        }

        private void 平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearCheck();
            平铺ToolStripMenuItem.Checked = true;
            listView1.View = View.LargeIcon;
        }
        private void 图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearCheck();
            图标ToolStripMenuItem.Checked = true;
            listView1.View = View.SmallIcon;
            
        }
        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearCheck();
            详细信息ToolStripMenuItem.Checked = true;
            this.listView1.Columns.Add("名称", this.listView1.Width / 4, HorizontalAlignment.Center);
            this.listView1.Columns.Add("大小", this.listView1.Width / 4, HorizontalAlignment.Center);
            this.listView1.Columns.Add("类型", this.listView1.Width / 4, HorizontalAlignment.Center);
            this.listView1.Columns.Add("修改时间", this.listView1.Width / 4, HorizontalAlignment.Center);
            listView1.View = View.Details;
        }
    }
}
