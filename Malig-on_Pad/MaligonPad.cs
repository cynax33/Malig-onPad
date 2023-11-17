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

namespace Malig_on_Pad
{
    public partial class Form1 : Form
    {
        private bool unsavedChanges = false;

        private bool name = false;

        int flag;

        string orig;

        public Form1()
        {
            InitializeComponent();

            updateStatLabel("Ready");

            orig = richTextBox1.Text;
        }

        private void updateStatLabel(string status)
        {
            toolStripStatusLabel1.Text = status;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Save");

            SaveFileDialog op = new SaveFileDialog();
            op.Title = "Save";
            op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(op.FileName, RichTextBoxStreamType.PlainText);
                string flName = Path.GetFileName(op.FileName);
                richTextBox1.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                this.Text = flName;

                orig = richTextBox1.Text;

                unsavedChanges = false;

                name = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Exit");

            if (unsavedChanges)
            {
                saveChanges();
            }

            DialogResult result = MessageBox.Show("Do you want to close this window?", "Close Window", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Sige2", "Exit", MessageBoxButtons.OK);
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Undo");
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Redo");
            richTextBox1.Redo();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Copy");
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Paste");
            richTextBox1.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Cut");
            richTextBox1.Cut();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Select All");
            richTextBox1.SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Date/Time");
            richTextBox1.Text = DateTime.Now.ToString();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Font");
            FontDialog op = new FontDialog();
            if (op.ShowDialog() == DialogResult.OK)
                richTextBox1.Font = op.Font;

        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Color");
            ColorDialog op = new ColorDialog();
            if (op.ShowDialog() == DialogResult.OK)
                richTextBox1.ForeColor = op.Color;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string saveText = this.Text.Trim('*');

            if (unsavedChanges is true)
            {
                var result = MessageBox.Show("Do you want to save changes to " + saveText + "?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFileDialog op = new SaveFileDialog();
                        op.Title = "Save";
                        op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
                        if (op.ShowDialog() == DialogResult.OK)
                            richTextBox1.SaveFile(op.FileName, RichTextBoxStreamType.PlainText);
                        this.Text = op.FileName;
                        unsavedChanges = false;
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            unsavedChanges = true;
            if (richTextBox1.Text.Count() == orig.Count())
            {
                unsavedChanges = false;
                this.Text = this.Text.Trim('*');
                name = false;
            }
            else if (!name)
            {
                unsavedChanges = true;
                this.Text = '*' + this.Text;
                name = true;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unsavedChanges is true)
            {
                saveChanges();
            }
            else
            {
                flag = 1;
            }

            if (flag == 1)
            {
                this.Hide();
                Form1 f1 = new Form1();
                f1.Show();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unsavedChanges)
            {
                saveChanges();
            }
            else
            {
                flag = 1;
            }

            if (flag == 1)
            {
                updateStatLabel("Open");

                OpenFileDialog op = new OpenFileDialog();
                op.Title = "open";
                op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                    string flName = Path.GetFileName(op.FileName);
                    this.Text = flName;

                    orig = richTextBox1.Text;

                    unsavedChanges = false;

                    name = false;
                }
            }
        }

        private void saveChanges()
        {
            string saveText = this.Text.Trim('*');

            if (unsavedChanges is true)
            {
                var result = MessageBox.Show("Do you want to save changes to " + saveText + "?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveFileDialog op = new SaveFileDialog();
                    op.Title = "Save";
                    op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.SaveFile(op.FileName, RichTextBoxStreamType.PlainText);
                    }
                        
                    this.Text = op.FileName;
                    unsavedChanges = false;
                    flag = 1;
                }
                else if (result == DialogResult.No)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
            }
            else
            {
                flag = 0;
            }
        }
    }
}
