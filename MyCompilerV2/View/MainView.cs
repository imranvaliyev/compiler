using FastColoredTextBoxNS;
using MyCompilerV2.Model;
using MyCompilerV2.View;
using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;

namespace MyCompilerV2
{
    public partial class MainView : Form, IMainView
    {
        SpeechRecognitionEngine speechRecognition = new SpeechRecognitionEngine();
        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
        public event EventHandler<CreateSubDirectoryEventArgs> CreateProjectEvent;
        public event EventHandler<CodeEventArgs> SaveFocusedFileEvent;
        public event EventHandler<PathEventArgs> OpenProjectEvent;
        public event EventHandler<PathEventArgs> CreateNewFileEvent;
        public event EventHandler<PathEventArgs> OpenSelectebFileEvent;
        public event EventHandler<EventArgs> CloseProgramEvent;
        public event EventHandler<RemoveProjectEventArgs> RemoveProjectEvent;
        public event EventHandler<TextEventArgs> BuildAndRunEvent;
        public event EventHandler<TextEventArgs> Build;

        private static int tabCount = 0;
        public void Splash()
        {
            Application.Run(new SplashScreen());
        }
        public MainView()
        {
            Thread t = new Thread(new ThreadStart(Splash));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();
        }
        public void OpenSelectebFileNode(FileInformation file, string code)
        {
            TabPage tabPage = new TabPage(file.Name);
            FastColoredTextBox fastColored = new FastColoredTextBox();
            fastColored.ContextMenuStrip = contextMenuStripMain;
            fastColored.Text = code;
            fastColored.Dock = DockStyle.Fill;
            fastColored.Language = Language.CSharp;
            tabPage.Tag = file.Path + @"\" + file.Name;
            tabPage.Controls.Add(fastColored);
            tabControl_code.TabPages.Add(tabPage);
            tabControl_code.SelectedTab = tabPage;
        }

        public void SetSettings(FileInformation newProject)
        {
            string fullpath = newProject.Path + @"\" + newProject.Name;
            treeView_project.Tag = fullpath;
            treeView_project.ContextMenuStrip = contextMenuStripEditTree;
            treeView_project.ImageList = new ImageList();
            treeView_project.ImageList.Images.Add("proj", Image.FromFile(@"..\..\Resources\proj.png"));
            treeView_project.ImageList.Images.Add("CS", Image.FromFile(@"..\..\Resources\CS.png"));

            TreeNode _mainTreeNode = treeView_project.Nodes.Add("treeNode", newProject.Name, "proj");

            DirectoryInfo check = new DirectoryInfo(fullpath);
            treeView_project.Nodes[0].Tag = check.FullName;
            if (check.Exists)
            {
                foreach (FileInfo file in new DirectoryInfo(fullpath).GetFiles("*.cs"))
                {
                    TreeNode fileNode = new TreeNode(file.Name) { ImageKey = "CS", SelectedImageKey = "CS" };
                    fileNode.Tag = file.FullName;
                    _mainTreeNode.Nodes.Add(fileNode);
                }
            }
            else
            {

                fullpath = newProject.Path;
                foreach (FileInfo file in new DirectoryInfo(fullpath).GetFiles("*.cs"))
                {
                    TreeNode fileNode = new TreeNode(file.Name) { ImageKey = "CS", SelectedImageKey = "CS" };
                    fileNode.Tag = file.FullName;
                    _mainTreeNode.Nodes.Add(fileNode);
                }
            }
        }

        private void createProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProjectEvent?.Invoke(this, new CreateSubDirectoryEventArgs { });
        }

        bool IView.ShowDialog()
        {
            return ShowDialog() == DialogResult.OK;
        }

        private void saveFocusedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string code = tabControl_code.SelectedTab.Controls[0].Text;
            string pathtofile = tabControl_code.SelectedTab.Tag as string;

            SaveFocusedFileEvent?.Invoke(this, new CodeEventArgs { Code = code, PathToFile = pathtofile });
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = OpenProjectPath();
            OpenProjectEvent?.Invoke(this, new PathEventArgs { Path = path });
        }

        public string OpenProjectPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            return openFileDialog.FileName;
        }

        private void treeView_project_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectebFileEvent?.Invoke(this, new PathEventArgs
            {
                Path = treeView_project.Tag as string
                + @"\" + Path.GetFileName(treeView_project.SelectedNode.FullPath)
            });
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewFileEvent?.Invoke(this, new PathEventArgs { Path = treeView_project.Tag as string });
            string name = "new tab" + $"({++tabCount }).cs";

            treeView_project.Nodes[0].Nodes.Add(new TreeNode(name) { ImageKey = "CS", SelectedImageKey = "CS" });
        }

        private void openFIleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = OpenProjectPath();
            OpenSelectebFileEvent?.Invoke(this, new PathEventArgs { Path = path });
        }

        private void saveAllFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string code;
            string pathtofile;
            for (int i = 0; i < tabControl_code.TabCount; i++)
            {
                code = tabControl_code.TabPages[i].Controls[0].Text;
                pathtofile = tabControl_code.TabPages[i].Tag as string;
                SaveFocusedFileEvent?.Invoke(this, new CodeEventArgs { Code = code, PathToFile = pathtofile });
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Do you want to save files?", "Caption of window", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                string code;
                string pathtofile;
                for (int i = 0; i < tabControl_code.TabCount; i++)
                {
                    code = tabControl_code.TabPages[i].Controls[0].Text;
                    pathtofile = tabControl_code.TabPages[i].Tag as string;
                    SaveFocusedFileEvent?.Invoke(this, new CodeEventArgs { Code = code, PathToFile = pathtofile });
                }
                CloseProgramEvent?.Invoke(this, new EventArgs());
            }
            else
            {
                CloseProgramEvent?.Invoke(this, new EventArgs());
            }
        }

        public void CloseProgram()
        {
            this.Close();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Cut();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("I have nothing to cut");
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Copy();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("I have nothing to copy");
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Paste();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("I have nothing to paste");
            }
        }

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)tabControl_code.SelectedTab.Controls[0];
                Build?.Invoke(this, new TextEventArgs { Text = fastColoredTextBox.Text });
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Open the project first");
            }
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)tabControl_code.SelectedTab.Controls[0];
            BuildAndRunEvent?.Invoke(this, new TextEventArgs { Text = fastColoredTextBox.Text});
        }
        private void checkedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView_project.Visible = false;
            tabControl_code.Dock = DockStyle.Fill;
            checkedToolStripMenuItem.CheckState = CheckState.Checked;
            uncheckedToolStripMenuItem.CheckState = CheckState.Unchecked;
        }

        private void errorsBlockCollapsedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (errorsBlockCollapsedToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                tabControl_debug.Visible = false;
                treeView_project.Height = 430;
                tabControl_code.Height = 430;
                errorsBlockCollapsedToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else if (errorsBlockCollapsedToolStripMenuItem.CheckState == CheckState.Checked)
            {
                tabControl_code.Visible = true;
                treeView_project.Height = 311;
                tabControl_code.Height = 311;
                errorsBlockCollapsedToolStripMenuItem.CheckState = CheckState.Unchecked;
                tabControl_debug.Visible = true;
            }
        }

        private void uncheckedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView_project.Visible = true;
            tabControl_code.Dock = DockStyle.None;
            checkedToolStripMenuItem.CheckState = CheckState.Unchecked;
            uncheckedToolStripMenuItem.CheckState = CheckState.Checked;
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            CreateProjectEvent?.Invoke(this, new CreateSubDirectoryEventArgs { });
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenProjectEvent?.Invoke(this, new PathEventArgs { Path = OpenProjectPath()});
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Paste();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Cut();
        }

        public void GetErrors(List<Error> errors)
        {
            listView1.Items.Clear();
            if (errors != null)
            {
                foreach (Error error in errors)
                {
                    ListViewItem lvi = new ListViewItem
                    {
                        Text = error.Code
                    };
                    lvi.SubItems.Add(error.Text);
                    lvi.SubItems.Add(error.Line.ToString());
                    lvi.SubItems.Add(error.File);
                    listView1.Items.Add(lvi);
                    richTextBox1.Text = "This project has errors";
                }
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
            SaveFocusedFileEvent?.Invoke(this, new CodeEventArgs { Code = tabControl_code.SelectedTab.Controls[0].Text, PathToFile = tabControl_code.SelectedTab.Tag.ToString() });
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Nothing to save");
            }
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Copy();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nothing to copy");
            }
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Paste();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nothing to paste");
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Cut();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nothing to paste");
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is Compiler Studio. Create or open existing file");
        }
        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl_code.TabPages.Remove(tabControl_code.SelectedTab);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveProjectEvent?.Invoke(this, new RemoveProjectEventArgs() { SelectedItem = treeView_project.SelectedNode.Tag as string });
            treeView_project.SelectedNode.Remove();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "Newproject", "Openproject", "Build", "Compile", "Copy", "Cut", "Paste", "Exit", "Turnof"});
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(commands);
            Grammar grammar = new Grammar(grammarBuilder);
            speechRecognition.LoadGrammarAsync(grammar);
            speechRecognition.SetInputToDefaultAudioDevice();
            speechRecognition.SpeechRecognized += SpeechRecognition_SpeechRecognized;
        }

        private void SpeechRecognition_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
                switch (e.Result.Text)
                {
                    case "Newproject":
                        CreateProjectEvent?.Invoke(this, new CreateSubDirectoryEventArgs { });
                        break;
                    case "Openproject":
                        string path = OpenProjectPath();
                        OpenProjectEvent?.Invoke(this, new PathEventArgs { Path = path });
                        break;
                case "Turnof":
                    
                        speechRecognition.RecognizeAsyncStop();
                        speechSynthesizer.Speak("I cant hear you now");
                    break;
                    case "Build":
                    try
                    {
                        FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)tabControl_code.SelectedTab.Controls[0];
                        Build?.Invoke(this, new TextEventArgs { Text = fastColoredTextBox.Text });
                    }
                    catch (NullReferenceException)
                    {
                        speechSynthesizer.Speak("Open the project first");
                    }
                    break;
                case "Compile":

                    try
                    {
                        FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)tabControl_code.SelectedTab.Controls[0];
                        BuildAndRunEvent?.Invoke(this, new TextEventArgs { Text = fastColoredTextBox.Text });
                    }
                    catch (NullReferenceException)
                    {
                        speechSynthesizer.Speak("Open the project first");
                    }
                    break;
                    case "Cut":
                    try
                    {
                        (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Cut();
                    }
                    catch(NullReferenceException)
                    {
                        speechSynthesizer.Speak("Open the project first");
                    }
                        break;
                    case "Copy":
                    try {
                    (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Copy();
                    }
                    catch (NullReferenceException)
                    {
                        speechSynthesizer.Speak("Open the project first");
                    }
                    break;
                    case "Paste":
                        (tabControl_code.SelectedTab.Controls[0] as FastColoredTextBox).Paste();
                        break;
                    case "Exit":
                        Application.Exit();
                        break;
           }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            speechRecognition.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            speechRecognition.RecognizeAsyncStop();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)tabControl_code.SelectedTab.Controls[0];
                BuildAndRunEvent?.Invoke(this, new TextEventArgs { Text = fastColoredTextBox.Text });
            }
            catch
            {
                MessageBox.Show("Нет проекта");
            }
            
        }

        public void GetWarnings(List<Warning> warnings)
        {
            listView2.Items.Clear();
            if (warnings != null)
            {
                foreach (Warning warning in warnings)
                {
                    ListViewItem lvi = new ListViewItem
                    {
                        Text = warning.Code
                    };
                    lvi.SubItems.Add(warning.Text);
                    lvi.SubItems.Add(warning.Line.ToString());
                    lvi.SubItems.Add(warning.File);
                    listView1.Items.Add(lvi);
                    richTextBox1.Text = "This project has not built succesfully";
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)tabControl_code.SelectedTab.Controls[0];
                Build?.Invoke(this, new TextEventArgs { Text = fastColoredTextBox.Text });
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Сначала запустите проект");
            }
        }
    }
}