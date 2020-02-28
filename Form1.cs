using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Caesar_s_cypher
{
    public partial class Form : System.Windows.Forms.Form
    {
        const string ALPHABET_LOWER = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        const string ALPHABET_UPPER = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";


        public Form()
        {
            InitializeComponent();
            numericUpDownKey.Maximum = ALPHABET_LOWER.Length - 1;
            comboBoxAction.SelectedIndex = 0;
        }

        private void buttonDo_Click(object sender, EventArgs e)
        {
            CaesarCipher();
        }

        static char NewCharCipher(char oldChar, int shift)
        {
            if (ALPHABET_LOWER.Contains(oldChar.ToString()))
                return ALPHABET_LOWER[(shift + ALPHABET_LOWER.IndexOf(oldChar)) % ALPHABET_LOWER.Length];
            if (ALPHABET_UPPER.Contains(oldChar.ToString()))
                return ALPHABET_UPPER[(shift + ALPHABET_UPPER.IndexOf(oldChar)) % ALPHABET_UPPER.Length];
            return oldChar;
        }

        static char NewCharDecrypt(char oldChar, int shift)
        {
            if (ALPHABET_LOWER.Contains(oldChar.ToString()))
                return ALPHABET_LOWER[(ALPHABET_LOWER.Length - shift + ALPHABET_LOWER.IndexOf(oldChar)) % ALPHABET_LOWER.Length];
            if (ALPHABET_UPPER.Contains(oldChar.ToString()))
                return ALPHABET_UPPER[(ALPHABET_UPPER.Length - shift + ALPHABET_UPPER.IndexOf(oldChar)) % ALPHABET_UPPER.Length];
            return oldChar;
        }

        void CaesarCipher()
        {
            StringBuilder stringBuilder = new StringBuilder(richTextBox.Text);
            int shift = Decimal.ToInt32(numericUpDownKey.Value);
            for (int i = 0; i < stringBuilder.Length; i++)
                if (comboBoxAction.SelectedItem != null)
                    switch (comboBoxAction.SelectedItem.ToString())
                    {
                        case "Расшифровать": stringBuilder[i] = NewCharDecrypt(stringBuilder[i], shift); break;
                        case "Зашифровать": stringBuilder[i] = NewCharCipher(stringBuilder[i], shift); break;
                    }
            richTextBox.Clear();
            richTextBox.Text = stringBuilder.ToString();
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Text Documents|*.txt;*.rtf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = openFileDialog.OpenFile();
                richTextBox.LoadFile(stream, RichTextBoxStreamType.PlainText);
                stream.Close();
            }
        }

        private void toolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            MemoryStream memorystream = new MemoryStream();
            Stream filestream;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filestream = saveFileDialog.OpenFile();
                memorystream.Position = 0;
                richTextBox.SaveFile(memorystream, RichTextBoxStreamType.PlainText);
                memorystream.WriteTo(filestream);
                filestream.Close();
            }
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItemBackgroundColor_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            this.BackColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
        }

        private void toolStripMenuItemButtonStyle_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            buttonDo.BackColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
        }
    }
}
