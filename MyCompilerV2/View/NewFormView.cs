using MyCompilerV2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCompilerV2.View
{
    public partial class NewFormView : Form, INewFormView
    {

        public NewFormView()
        {
            InitializeComponent();
        }

        public event EventHandler<NameAndPathEventArgs> GetPath;
        public event EventHandler<NamePathCheckboxEventArgs> CreateProject;


        public void SetPathInBox(string path)
        {
            textBox_path.Text = path;
        }

        private void button_chooseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                GetPath?.Invoke(this, new NameAndPathEventArgs { Path = openFileDialog.SelectedPath, Name = textBox_name.Text });
            }
        }

        bool IView.ShowDialog()
        {
            return ShowDialog() == DialogResult.OK;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox_path.Text) && textBox_name.Text != "")
            {
                this.DialogResult = DialogResult.OK;
                bool checkbox=checkBox_subdirectory.Checked;
                CreateProject?.Invoke(this, new NamePathCheckboxEventArgs { Path = textBox_path.Text, Name = textBox_name.Text,Subdirectory=checkbox } );
            }
        }


        public void CloseWindow()
        {
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            textBox_name.Text = "";
            textBox_path.Text = "";
            Close();
        }
    }
}
