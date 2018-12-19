using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
namespace ChangeFileName
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text =  ConfigurationManager.AppSettings["defaultDir"].ToString();
            textBox3.Text = ConfigurationManager.AppSettings["newDir"].ToString();
            StringBuilder text = new StringBuilder();
            text.AppendLine("1.请先点击【目录】选择要修改的文件夹");
            text.AppendLine("2.请在新名称框中输入要修改的名称（只需输入新名称，不需要完整路径）");
            text.Append("3.点击【一键修改】完成新名称的修改！");
            label4.Text = text.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = this.folderBrowserDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string dir = folderBrowserDialog1.SelectedPath;
                textBox1.Text = dir;
            }
            else
            {
                textBox1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请选择路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("新名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string oldPath = textBox1.Text;
            string parentPath = Directory.GetParent(oldPath).FullName;
            string newPath = System.IO.Path.Combine(parentPath, textBox3.Text);

            if (!Directory.Exists(oldPath))
            {
                MessageBox.Show("原文件夹不存在，请检查是否已经被删除！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Directory.Exists(newPath))
            {
                MessageBox.Show("新文件夹已存在，请重新填写！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //重命名
            try
            {
                var directInfo = new DirectoryInfo(oldPath);
                directInfo.MoveTo(newPath);
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改失败！\r\n错误原因:"+ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
