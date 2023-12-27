using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

class MyForm : Form
{
    private Label topnglabel;
    private Button fileselect;
    private PictureBox banner;
    private Panel dropPanel;
    private Label droplabel;
    private ToolStripButton toolStripButton1;
    private ToolStripButton toolStripButton2;
    private ToolStrip toolStrip1;



    public MyForm()
    {
        this.Text = "ToPNG";
        this.Size = new System.Drawing.Size(780, 700);

        topnglabel = new Label();
        topnglabel.Text = "TO PNG";
        topnglabel.Font = new Font(topnglabel.Font.FontFamily, 30);
        topnglabel.ForeColor = Color.Black;
        topnglabel.AutoSize = false;
        topnglabel.Location = new Point(300, 5);
        topnglabel.Size = new System.Drawing.Size(200, 50);

        fileselect = new Button();
        fileselect.Text = "ファイルを選択";
        fileselect.Font = new Font(fileselect.Font.FontFamily, 13);
        fileselect.ForeColor = Color.Black;
        fileselect.AutoSize = false;
        fileselect.Location = new Point(300, 100);
        fileselect.Size = new System.Drawing.Size(150, 30);
        fileselect.Click += fileselect_Click;

        banner = new PictureBox();
        banner.ImageLocation = "banner.png";
        banner.Size = new System.Drawing.Size(780, 700);
        banner.Location = new Point(0, 520);
        banner.AutoSize = false;

        dropPanel = new Panel();
        dropPanel.BorderStyle = BorderStyle.FixedSingle;
        dropPanel.AllowDrop = true;
        dropPanel.Location = new Point(200, 200);
        dropPanel.Size = new Size(350, 100);
        dropPanel.DragEnter += DropPanel_DragEnter;
        dropPanel.DragDrop += DropPanel_DragDrop;

        droplabel = new Label();
        droplabel.Text = "画像をドロップできます";
        droplabel.Font = new Font(droplabel.Font.FontFamily, 20);
        droplabel.ForeColor = Color.Black;
        droplabel.Location = new Point(250, 300);
        droplabel.Size = new System.Drawing.Size(300, 50);

        toolStrip1 = new System.Windows.Forms.ToolStrip();
        toolStripButton1 = new System.Windows.Forms.ToolStripButton();
        toolStripButton2 = new System.Windows.Forms.ToolStripButton();
        toolStrip1.SuspendLayout();
        SuspendLayout();

        toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripButton1,
            toolStripButton2});
            toolStrip1.Location = new System.Drawing.Point(0,0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Text = "toolStrip1";

            //toolStripButton1を作成
            toolStripButton1.Name = "readme閲覧";
            toolStripButton1.Text = "&New";
            toolStripButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            toolStripButton1.Click += toolStripButton1_Click;

            //toolStripButton2を作成
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Text = "&Open";
            toolStripButton2.Click += toolStripButton2_Click;

            //Formの設定
            toolStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        

        Controls.Add(topnglabel);
        Controls.Add(fileselect);
        Controls.Add(banner);
        Controls.Add(dropPanel);
        Controls.Add(droplabel);
        Controls.Add(toolStrip1);
    }

    private void fileselect_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.FileName = "";
        ofd.InitialDirectory = @"C:\";
        ofd.Filter = "画像ファイル(*.jpg;*.jpeg;*.webp;*.svg;*.bmp;*.raw;*.cr2;*.tiff;*.gif;*.pdf)|*.jpg;*.jpeg;*.webp;*.svg;*.bmp;*.raw;*.cr2;*.tiff;*.gif;*.pdf|全てのファイル(*.*)|*.*";
        ofd.FilterIndex = 1;
        ofd.Multiselect = true;

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            foreach (string fileName in ofd.FileNames)
            {
                    try
                    {
                        Console.WriteLine(System.IO.Path.GetFileNameWithoutExtension(fileName));
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string newFileName = System.IO.Path.Combine(desktopPath, System.IO.Path.ChangeExtension(System.IO.Path.GetFileName(fileName), "png"));

                        using (Image image = Image.FromFile(fileName))
                        {
                            image.Save(newFileName, System.Drawing.Imaging.ImageFormat.Png);
                        }

                        MessageBox.Show("変換しました！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("エラーが発生しました : {0} (画像ではありません)", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    private void DropPanel_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effect = DragDropEffects.Copy;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private void DropPanel_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Effect == DragDropEffects.Copy)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                try
                {
                    Console.WriteLine(System.IO.Path.GetFileNameWithoutExtension(file));
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string newFileName = System.IO.Path.Combine(desktopPath, System.IO.Path.ChangeExtension(System.IO.Path.GetFileName(file), "png"));

                    using (Image image = Image.FromFile(file))
                    {
                        image.Save(newFileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    MessageBox.Show("変換しました！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("エラーが発生しました : {0} (画像ではありません)", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start("notepad.exe", "readme.md");
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
        MessageBox.Show("まだ");
    }

}

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        MyForm form = new MyForm();
        Application.Run(form);
    }
}
