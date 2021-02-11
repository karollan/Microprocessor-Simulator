using Mikroprocesor.Kompilator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikroprocesor
{
    public partial class Mikroprocesor : Form
    {
        public Mikroprocesor()
        {
            InitializeComponent();

            aktualizujRejestry();
        }

        /*
         * AKTUALIZUJ REJESTRY
         * 
        */

        public void aktualizujRejestry()
        {

            int16ToBinary(AX, AHReg_TextBox, ALReg_TextBox);
            int16ToBinary(BX, BHReg_TextBox, BLReg_TextBox);
            int16ToBinary(CX, CHReg_TextBox, CLReg_TextBox);
            int16ToBinary(DX, DHReg_TextBox, DLReg_TextBox);
            justINT(AX, AXINT_TextBox);
            justINT(BX, BXINT_TextBox);
            justINT(CX, CXINT_TextBox);
            justINT(DX, DXINT_TextBox);

        }

        /*
         * AKTUALIZUJ REJESTRY
         * 
        */

        private void justINT(short dec, TextBox text)
        {
            text.Clear();
            text.Text = Convert.ToString(dec);
        }

        private void int16ToBinary(int dec, TextBox textH, TextBox textL)
        {
            textH.Clear(); textL.Clear();

            /*            for (int i = 7; i >= 0; i--)
                        {
                            textL.Text += Convert.ToString(((byte)dec2 >> i) % 2);
                            textH.Text += Convert.ToString(((byte)dec1 >> i) % 2);
                        }*/

            for (int i = 15; i >= 0; i--)
            {
                if (i <= 7) textL.Text += Convert.ToString(((ushort)dec >> i) % 2);
                else textH.Text += Convert.ToString(((ushort)dec >> i) % 2);
            }

        }

        /*
         * Checkboxes 
         * 
        */

        private void DEMO_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (DEMO_checkbox.Checked == true)
            {
                STEP_radioButton.Checked = true;
                ALL_radioButton.Enabled = false;
                STEP_Timer.Interval = 2000;
            } else if (DEMO_checkbox.Checked == false)
            {
                ALL_radioButton.Enabled = true;
                STEP_Timer.Interval = 1000;
            }
        }

        /*
         * RadioButtons
         * 
        */

        //TRYBY ADRESOWANIA

        //Rejestrowy
        private void REG_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (REG_radioButton.Checked == true)
            {
                rejDO_ComboBox.Enabled = true;
                rejZ_ComboBox.Enabled = true;
                VALUE_TextBox.Enabled = false;
            }
        }

        //Natychmiastowy
        private void IMM_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (IMM_radioButton.Checked == true)
            {
                rejDO_ComboBox.Enabled = true;
                rejZ_ComboBox.Enabled = false;
                VALUE_TextBox.Enabled = true;
            }
        }

        /*
        * Buttons
        * 
        */

        //Dodaj rozkaz do konsoli z kodem
        private void ADD_Button_Click(object sender, EventArgs e)
        {
            //Tryb rejestrowy
            if (REG_radioButton.Checked == true)
            {
                //Sprawdz czy poprawnie wypełniono
                if (rozkaz_ComboBox.Text.Length == 0 || rejDO_ComboBox.Text.Length == 0 || rejZ_ComboBox.Text.Length == 0)
                {
                    MessageBox.Show("Musisz wybrać rodzaj rozkazu i oba rejestry aby dodać nowy rozkaz!");
                }
                else
                {
                    KOD_RichTextBox.Text += "REG" + " " + rozkaz_ComboBox.Text + " " + rejDO_ComboBox.Text.Replace(" ", string.Empty) + ", " + rejZ_ComboBox.Text.Replace(" ", string.Empty) + ";" + "\r\n";
                }
            }

            //Tryb natychmiastowy
            if (IMM_radioButton.Checked == true)
            {
                //Sprawdz czy poprawnie wypełniono
                if (rozkaz_ComboBox.Text.Length == 0 || rejDO_ComboBox.Text.Length == 0 || VALUE_TextBox.Text.Length == 0)
                {
                    MessageBox.Show("Musisz wybrać rodzaj rozkazu i rejestr oraz wpisać wartość!");
                }
                else
                {
                    KOD_RichTextBox.Text += "IMM" + " " + rozkaz_ComboBox.Text + " " + rejDO_ComboBox.Text.Replace(" ", string.Empty) + ", " + VALUE_TextBox.Text + ";" + "\r\n";
                }
            }
        }

        //Dodaj przerwanie
        private void ADDINT_button_Click(object sender, EventArgs e)
        {
            if (INT_ComboBox.Text.Length != 0)
            {
                switch (INT_ComboBox.SelectedIndex)
                {
                    case 0:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nPUSH BX;\r\nPUSH CX;\r\nPUSH DX;\r\nIMM MOV AH, 36;\r\nINT21;\r\nPOP DX;\r\nPOP CX;\r\nPOP BX;\r\nPOP AX;\r\n";
                        break;
                    case 1:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nIMM MOV AH, 19;\r\nINT21;\r\nPOP AX;\r\n";
                        break;
                    case 2:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nIMM MOV AH, 7;\r\nINT21;\r\nPOP AX;\r\n";
                        break;
                    case 3:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nPUSH CX;\r\nPUSH DX;\r\nIMM MOV AH, 30;\r\nINT21;\r\nPOP DX;\r\nPOP CX;\r\nPOP AX;\r\n";
                        break;
                    case 4:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nIMM MOV AH, 47;\r\nINT21;\r\nPOP AX;\r\n";
                        break;
                    case 5:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nPUSH CX;\r\nPUSH DX;\r\nIMM MOV AH, 2;\r\nINT1A;\r\nPOP DX;\r\nPOP CX;\r\nPOP AX;\r\n";
                        break;
                    case 6:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nPUSH BX;\r\nPUSH CX;\r\nPUSH DX;\r\nIMM MOV AH, 4;\r\nINT1A;\r\nPOP DX;\r\nPOP CX;\r\nPOP BX;\r\nPOP AX;\r\n";
                        break;
                    case 7:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nIMM MOV AX, 1;\r\nINT33;\r\nPOP AX;\r\n";
                        break;
                    case 8:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nIMM MOV AX, 2;\r\nINT33;\r\nPOP AX;\r\n";
                        break;
                    case 9:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nPUSH BX;\r\nPUSH CX;\r\nPUSH DX;\r\nIMM MOV AX, 3;\r\nINT33;\r\nPOP DX;\r\nPOP CX;\r\nPOP BX;\r\nPOP AX;\r\n";
                        break;
                    case 10:
                        KOD_RichTextBox.Text += $"PUSH AX;\r\nPUSH CX;\r\nPUSH DX;\r\nIMM MOV CX, BITY STARSZE;\r\nIMM MOV DX, BITY MŁODSZE;\r\nIMM MOV AH, 86;\r\nINT15;\r\nPOP DX;\r\nPOP CX;\r\nPOP AX;\r\n";
                        break;
                    default:
                        MessageBox.Show("Brakuje automatycznego dodania tego przerwania!");
                        break;
                }
            } else
            {
                MessageBox.Show("Musisz wybrać rodzaj przerwania, które chcesz dodać!");

            }
        }

        //Wykonaj rozkazy
        private void DO_Button_Click(object sender, EventArgs e)
        {
            ResetLinesColor();
            foreach(string line in KOD_RichTextBox.Lines)
            {
                if (line.Length != 0 && line[0] != '\n')
                {
                    linieKodu.Add(line);
                }
            }

            if (ALL_radioButton.Checked == true)
            {
                Kompiluj(linieKodu);
            }

            if (STEP_radioButton.Checked == true)
            {
                STEP_Timer.Start();
            }
        }

        //Wyczyść rozkazy
        private void CLR_Button_Click(object sender, EventArgs e)
        {
            if (filePath == null && KOD_RichTextBox.Text.Length != 0)
            {
                if (MessageBox.Show("Czy chcesz zapisać kod przed wyczyszczeniem edytora?", "Mikroprocesor", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    zapiszToolStripMenuItem.PerformClick();

                }
            }
            else if (filePath != null && KOD_RichTextBox.Text != File.ReadAllText(sfd.FileName))
            {
                if (MessageBox.Show("Czy chcesz zapisać wprowadzone zmiany przed wyczyszczeniem edytora?", "Mikroprocesor", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    zapiszToolStripMenuItem.PerformClick();
                }
            }
            KOD_RichTextBox.Text = "";
        }

        //Wyczyść rejestry
        private void CLRREG_Button_Click(object sender, EventArgs e)
        {
            AX = 0;
            BX = 0;
            CX = 0;
            DX = 0;
            AL = 0;
            AH = 0;
            BH = 0;
            BL = 0;
            CH = 0;
            CL = 0;
            DH = 0;
            DL = 0;
            aktualizujRejestry();
        }

        //ZAPISZ
        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            Cursor.Current = Cursors.WaitCursor;
            if (filePath == null)
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    File.WriteAllText(filePath, KOD_RichTextBox.Text);
                }
            } else
            {
                File.WriteAllText(filePath, KOD_RichTextBox.Text);
            }
            Cursor.Current = kursor;
        }

        //ZAPISZ JAKO
        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
                File.WriteAllText(sfd.FileName, KOD_RichTextBox.Text);
            }
        }

        //ZAPISZ PRZED WYJSCIEM
        private void Mikroprocesor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (filePath == null && KOD_RichTextBox.Text.Length != 0)
            {
                if (MessageBox.Show("Czy chcesz zapisać kod przed zakończeniem pracy?", "Mikroprocesor",  MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    zapiszToolStripMenuItem.PerformClick();
                }
            } else if (filePath != null && KOD_RichTextBox.Text != File.ReadAllText(sfd.FileName))
            {
                if (MessageBox.Show("Czy chcesz zapisać wprowadzone zmiany przed zakończeniem pracy?", "Mikroprocesor", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    zapiszToolStripMenuItem.PerformClick();
                }
            }

        }

        //WCZYTAJ
        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                sfd.FileName = filePath;
                string readFile = File.ReadAllText(filePath);
                KOD_RichTextBox.Text = readFile;
            }
        }

        //POMOC
        private void pomocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pomoc pomoc = new Pomoc();
            pomoc.ShowDialog();
            
        }

        //O PROGRAMIE
        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        /*
         * Timer
         * 
        */
        private void STEP_Timer_Tick(object sender, EventArgs e)
        {

            if (timer >= linieKodu.Count)
            {
                timer = 0;
                Kompilator.Kompilator.ResetKompilator();
                linieKodu.Clear();
                STEP_Timer.Stop();
            }
            else if (!Kompilator.Kompilator.Evaluate(linieKodu[timer], this, timer++))
            {
                timer = 0;
                linieKodu.Clear();
                STEP_Timer.Stop();
                Kompilator.Kompilator.ShowMistakes();
                Kompilator.Kompilator.ResetKompilator();
            }
            
        }

        /*
         * TextBoxes
         * 
        */

        //Wpisz wartość przyjmuje tylko liczby
        private void VALUE_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void KOD_RichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Text.Length != 0)
            {
                DO_Button.Enabled = true;
            }
            else DO_Button.Enabled = false;
        }

        //RichTextBox ZMIANY KOLORU LINII;

        public void ChangeLineForeColor(Color color, string expression, int numerLinii)
        {
            int start = KOD_RichTextBox.GetFirstCharIndexFromLine(numerLinii);
            int koniec = expression.Length;
            KOD_RichTextBox.SelectionStart = start;
            KOD_RichTextBox.SelectionLength = koniec;
            KOD_RichTextBox.SelectionColor = color;
        }

        public void ResetLinesColor()
        {
            KOD_RichTextBox.SelectAll();
            KOD_RichTextBox.SelectionColor = Color.Black;
        }

        /*
         * KOMPILACJA
         * 
        */

        private void Kompiluj(List<string> linieKodu)
        {
            for (int i = 0; i < linieKodu.Count; i++)
            {
                if (!Kompilator.Kompilator.Evaluate(linieKodu[i], this, i))
                {
                    Kompilator.Kompilator.ShowMistakes();
                    break;
                }

            }
            Kompilator.Kompilator.ResetKompilator();

            linieKodu.Clear();
        }

        /*
         * Read Character
         * 
        */
        public async void ReadChar()
        {

            this.Enabled = false;
            if (STEP_radioButton.Checked == true)
            {
                STEP_Timer.Stop();
            }
            ReadKey rk = new ReadKey(this, STEP_radioButton.Checked);
            rk.ShowDialog();
        }


    }
}
