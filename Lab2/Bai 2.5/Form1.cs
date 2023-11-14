using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bai_2._5
{
    public partial class Form1 : Form
    {
        string path = @"D:\";
        public Form1()
        {
            InitializeComponent();
            if (Directory.Exists(path))
            {
                TreeNode root = new TreeNode(path) { Text = path };
                treeView1.Nodes.Add(root);
                LoadTreeView(root);
            }
            LoadListView();
        }

        private void LoadTreeView(TreeNode root)
        {

            treeView1.AfterSelect += treeView1_AfterSelect;

            try
            {


                var folderList = new DirectoryInfo(root.FullPath).GetDirectories();

                if (folderList.Count() == 0)
                {
                    return;
                }
                foreach (DirectoryInfo item in folderList)
                {
                    TreeNode node = new TreeNode(item.Name);
                    root.Nodes.Add(node);
                    LoadTreeView(node);
                    
                }
            }
            catch
            {
                return;
            }
        }

        ImageList imgListLarge;
        ImageList imgListSmall;

        void LoadImageList()
        {
            imgListLarge = new ImageList() { ImageSize = new Size(68, 68) };
            imgListLarge.Images.Add(new Bitmap(Application.StartupPath + "\\Images\\FileImage.png"));
            imgListLarge.Images.Add(new Bitmap(Application.StartupPath + "\\Images\\FolderImage.png"));

            imgListSmall = new ImageList() { ImageSize = new Size(16, 16) };
            imgListSmall.Images.Add(new Bitmap(Application.StartupPath + "\\Images\\FileImage.png"));
            imgListSmall.Images.Add(new Bitmap(Application.StartupPath + "\\Images\\FolderImage.png"));
        }
        void LoadListView()
        {
            LoadImageList();
            listView1.LargeImageList = imgListLarge;
            listView1.SmallImageList = imgListSmall;    
            listView1.AfterLabelEdit += ListView1_AfterLabelEdit;
            listView1.Columns.Add("Name", 200);
            listView1.Columns.Add("Type", 100);
            listView1.Columns.Add("Size", 100);
            listView1.Columns.Add("Date Modified", 150);
        }
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    listView1.SelectedItems[0].BeginEdit();
                }
            }
            catch
            {

            } 
            
        }

        private void ListView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            try
            {
                if (e.Label != null) 
                {
                    string oldPath = Path.Combine(path, e.Item.ToString());
                    string newPath = Path.Combine(path, e.Label);

                    if (File.Exists(oldPath))
                    {
                        File.Move(oldPath, newPath);
                    }
                    else if (Directory.Exists(oldPath))
                    {
                        Directory.Move(oldPath, newPath);
                    }

                    UpdateListView(path);
                }
            }
            catch
            {

            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            path = e.Node.FullPath;
            UpdateListView(path);
        }

        private void UpdateListView(string path)
        {
            listView1.Items.Clear();
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (DirectoryInfo info in directoryInfo.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(info.FullName);
                    item.SubItems.Add("Folder");
                    item.SubItems.Add("");
                    item.SubItems.Add(info.LastWriteTime.ToString());
                    listView1.Items.Add(item);
                }
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    item.SubItems.Add("File");
                    item.SubItems.Add(file.Length.ToString());
                    item.SubItems.Add(file.LastWriteTime.ToString());
                    listView1.Items.Add(item);
                }
            }
            catch
            {
                return;
            }
        }


        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        private void smallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }

        private void listIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }



        int i = 0;
        private void view_ts_Click(object sender, EventArgs e)
        {
            if (i > 4)
                i = 0;    
            switch(i)
            {
                case 0:
                    listView1.View = View.LargeIcon;
                    break;
                case 1:
                    listView1.View = View.SmallIcon;
                    break;
                case 2:
                    listView1.View = View.Tile;
                    break;
                case 3:
                    listView1.View = View.List;
                    break;
                case 4:
                    listView1.View = View.Details;
                    break;
            }
            i++; 
        }
        private void Refresh_ts_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateListView(path);
            }
            catch
            { 
            
            }
        }
        private void up_ts_Click(object sender, EventArgs e)
        {
            try
            {
                if(Directory.GetParent(path) != null)
                {
                    DirectoryInfo parentPath = Directory.GetParent(path);
                    path = parentPath.FullName;
                    UpdateListView(path);
                }    
            }
            catch
            {
            
            }
        }


        private FileSystemInfo selectedFileSystemItem;
        private void copy_ts_Click(object sender, EventArgs e)
        {
            try
            {
                if(listView1.SelectedItems.Count > 0)
                {
                    string selectedPath = Path.Combine(path, listView1.SelectedItems[0].Text);
                    if (Directory.Exists(selectedPath))
                    {
                        selectedFileSystemItem = new DirectoryInfo(selectedPath);
                    }
                    else if (File.Exists(selectedPath))
                    {
                        selectedFileSystemItem = new FileInfo(selectedPath);
                    }
                }    
            }
            catch { }
        }


        private FileSystemInfo cutFileSystemItem;
        private void cut_ts_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string selectedPath = Path.Combine(path, listView1.SelectedItems[0].Text);

                    if (Directory.Exists(selectedPath))
                    {
                        cutFileSystemItem = new DirectoryInfo(selectedPath);
                    }
                    else if (File.Exists(selectedPath))
                    {
                        cutFileSystemItem = new FileInfo(selectedPath);
                    }

                    selectedFileSystemItem = null;
                }
            }
            catch
            {

            }
        }
        private void paste_ts_Click(object sender, EventArgs e)
        {
            try
            {
                if(selectedFileSystemItem != null)
                {
                    string destinationPath = Path.Combine(path, selectedFileSystemItem.Name);

                    int count = 1;
                    while (Directory.Exists(destinationPath) || File.Exists(destinationPath))
                    {
                        destinationPath = Path.Combine(path, $"{selectedFileSystemItem.Name} ({count++})");
                    }

                    if (selectedFileSystemItem is DirectoryInfo)
                    {
                        Directory.CreateDirectory(destinationPath);
                    }
                    else if (selectedFileSystemItem is FileInfo)
                    {
                        File.Copy(selectedFileSystemItem.FullName, destinationPath);
                    }

                    UpdateListView(path);
                }    
            }
            catch { }
        }

        private void delete_ts_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string selectedPath = Path.Combine(path, listView1.SelectedItems[0].Text);
                    if (Directory.Exists(selectedPath))
                    {
                        Directory.Delete(selectedPath, true);
                    }
                    else if (File.Exists(selectedPath))
                    {
                        File.Delete(selectedPath);
                    }

                    UpdateListView(path);
                }
            }
            catch { }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string selectedPath = Path.Combine(path, listView1.SelectedItems[0].Text);
                    if (Directory.Exists(selectedPath))
                    {
                        selectedFileSystemItem = new DirectoryInfo(selectedPath);
                    }
                    else if (File.Exists(selectedPath))
                    {
                        selectedFileSystemItem = new FileInfo(selectedPath);
                    }
                }
            }
            catch { }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string selectedPath = Path.Combine(path, listView1.SelectedItems[0].Text);

                    if (Directory.Exists(selectedPath))
                    {
                        cutFileSystemItem = new DirectoryInfo(selectedPath);
                    }
                    else if (File.Exists(selectedPath))
                    {
                        cutFileSystemItem = new FileInfo(selectedPath);
                    }

                    selectedFileSystemItem = null;
                }
            }
            catch
            {

            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedFileSystemItem != null)
                {
                    string destinationPath = Path.Combine(path, selectedFileSystemItem.Name);

                    int count = 1;
                    while (Directory.Exists(destinationPath) || File.Exists(destinationPath))
                    {
                        destinationPath = Path.Combine(path, $"{selectedFileSystemItem.Name} ({count++})");
                    }

                    if (selectedFileSystemItem is DirectoryInfo)
                    {
                        Directory.CreateDirectory(destinationPath);
                    }
                    else if (selectedFileSystemItem is FileInfo)
                    {
                        File.Copy(selectedFileSystemItem.FullName, destinationPath);
                    }

                    UpdateListView(path);
                }
            }
            catch { }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string selectedPath = Path.Combine(path, listView1.SelectedItems[0].Text);
                    if (Directory.Exists(selectedPath))
                    {
                        Directory.Delete(selectedPath, true);
                    }
                    else if (File.Exists(selectedPath))
                    {
                        File.Delete(selectedPath);
                    }

                    UpdateListView(path);
                }
            }
            catch { }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
