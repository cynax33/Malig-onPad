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

        string orig;

        public Form1()
        {
            InitializeComponent();

            toolStripStatusLabel1.Text = "Ready";

            orig = richTextBox1.Text;
        }

        private void updateStatLabel(string status)
        {
            toolStripStatusLabel1.Text = status;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Open");

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "open";
            op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                unsavedChanges = false;

                richTextBox1.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                string flName = Path.GetFileName(op.FileName);
                richTextBox1.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                this.Text = flName + " - Malig-on_Pad";

                orig = richTextBox1.Text;

                name = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Save");

            SaveFileDialog op = new SaveFileDialog();
            op.Title = "Save";
            op.Filter = "Text Document(*.txt)|*.txt| All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                unsavedChanges = false;

                richTextBox1.SaveFile(op.FileName, RichTextBoxStreamType.PlainText);
                string flName = Path.GetFileName(op.FileName);
                richTextBox1.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                this.Text = flName + " - Malig-on_Pad";

                orig = richTextBox1.Text;

                name = false;
            }     
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatLabel("Exit");

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
            if (unsavedChanges is true)
            {
                var result = MessageBox.Show("Save changes before closing?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

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
            if(richTextBox1.Text == orig)
            {
                unsavedChanges = false;
                this.Text = this.Text.Trim('*');
                name = false;
            }
            else if(!name)
            {
                this.Text = '*' + this.Text;
                name = true;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unsavedChanges is true)
            {
                var result = MessageBox.Show("Save changes before closing?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

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
                }

                this.Hide();
                Form1 f1 = new Form1();
                f1.Show();
            }
            else
            {
                this.Hide();
                Form1 f1 = new Form1();
                f1.Show();
            }
        }
    }
}
