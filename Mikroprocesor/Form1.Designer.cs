using Mikroprocesor.Kompilator;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Mikroprocesor
{
    partial class Mikroprocesor
    {
        /// <summary>
        /// Wymagana zmienna projektanta.

        //ZROBIC NUMERACJE LINII PARSOWANIE LINII KOLEJNOSC LINII POPRAWIC ODEJMOWANIE

        // REJESTRY
        public short AX = 0, 
                    BX = 0, 
                    CX = 0, 
                    DX = 0;

        public sbyte AH = 0,
                    AL = 0,
                    BH = 0,
                    BL = 0,
                    CH = 0,
                    CL = 0,
                    DH = 0,
                    DL = 0;


        //Stos
        public Stack<short> stos = new Stack<short>();

        //Czytanie znaku
        //public bool readChar = false;


        //Kod programu
        private List<string> linieKodu = new List<string>();

        //TIMER TICK
        private int timer = 0;
        private bool _blad;
        public bool Blad { get => _blad; set { _blad = false; } }

        //FILE PATH
        string filePath = null;

        //KURSOR
        Cursor kursor = Cursor.Current;

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TA_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rozkaz_ComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rejDO_ComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rejZ_ComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.VALUE_TextBox = new System.Windows.Forms.TextBox();
            this.DO_Button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.CLR_Button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.zapiszToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszJakoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wczytajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ADD_Button = new System.Windows.Forms.Button();
            this.AHReg_TextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.BHReg_TextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CHReg_TextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.DHReg_TextBox = new System.Windows.Forms.TextBox();
            this.STEP_Timer = new System.Windows.Forms.Timer(this.components);
            this.ALL_radioButton = new System.Windows.Forms.RadioButton();
            this.STEP_radioButton = new System.Windows.Forms.RadioButton();
            this.IMM_radioButton = new System.Windows.Forms.RadioButton();
            this.REG_radioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CLRREG_Button = new System.Windows.Forms.Button();
            this.ALReg_TextBox = new System.Windows.Forms.TextBox();
            this.BLReg_TextBox = new System.Windows.Forms.TextBox();
            this.CLReg_TextBox = new System.Windows.Forms.TextBox();
            this.DLReg_TextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.KOD_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.LineNumbers_For_RichTextBox1 = new LineNumbers.LineNumbers_For_RichTextBox();
            this.AXINT_TextBox = new System.Windows.Forms.TextBox();
            this.BXINT_TextBox = new System.Windows.Forms.TextBox();
            this.CXINT_TextBox = new System.Windows.Forms.TextBox();
            this.DXINT_TextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.INT_ComboBox = new System.Windows.Forms.ComboBox();
            this.ADDINT_button = new System.Windows.Forms.Button();
            this.DEMO_checkbox = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TA_label
            // 
            this.TA_label.AutoSize = true;
            this.TA_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TA_label.Location = new System.Drawing.Point(115, 44);
            this.TA_label.Name = "TA_label";
            this.TA_label.Size = new System.Drawing.Size(168, 25);
            this.TA_label.TabIndex = 0;
            this.TA_label.Text = "Tryb adresowania";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(103, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rozkaz";
            // 
            // rozkaz_ComboBox
            // 
            this.rozkaz_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rozkaz_ComboBox.FormattingEnabled = true;
            this.rozkaz_ComboBox.Items.AddRange(new object[] {
            "ADD",
            "SUB",
            "MOV"});
            this.rozkaz_ComboBox.Location = new System.Drawing.Point(108, 199);
            this.rozkaz_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rozkaz_ComboBox.Name = "rozkaz_ComboBox";
            this.rozkaz_ComboBox.Size = new System.Drawing.Size(131, 24);
            this.rozkaz_ComboBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(53, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "DO:";
            // 
            // rejDO_ComboBox
            // 
            this.rejDO_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rejDO_ComboBox.Items.AddRange(new object[] {
            "AX",
            " AH",
            " AL",
            "BX",
            " BH",
            " BL",
            "CX",
            " CH",
            " CL",
            "DX",
            " DH",
            " DL"});
            this.rejDO_ComboBox.Location = new System.Drawing.Point(108, 252);
            this.rejDO_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rejDO_ComboBox.Name = "rejDO_ComboBox";
            this.rejDO_ComboBox.Size = new System.Drawing.Size(53, 24);
            this.rejDO_ComboBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(194, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Z:";
            // 
            // rejZ_ComboBox
            // 
            this.rejZ_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rejZ_ComboBox.FormattingEnabled = true;
            this.rejZ_ComboBox.Items.AddRange(new object[] {
            "AX",
            " AH",
            " AL",
            "BX",
            " BH",
            " BL",
            "CX",
            " CH",
            " CL",
            "DX",
            " DH",
            " DL"});
            this.rejZ_ComboBox.Location = new System.Drawing.Point(230, 252);
            this.rejZ_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rejZ_ComboBox.Name = "rejZ_ComboBox";
            this.rejZ_ComboBox.Size = new System.Drawing.Size(53, 24);
            this.rejZ_ComboBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(52, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Wartość";
            // 
            // VALUE_TextBox
            // 
            this.VALUE_TextBox.Enabled = false;
            this.VALUE_TextBox.Location = new System.Drawing.Point(153, 308);
            this.VALUE_TextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VALUE_TextBox.Name = "VALUE_TextBox";
            this.VALUE_TextBox.ShortcutsEnabled = false;
            this.VALUE_TextBox.Size = new System.Drawing.Size(147, 22);
            this.VALUE_TextBox.TabIndex = 10;
            this.VALUE_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VALUE_TextBox_KeyPress);
            // 
            // DO_Button
            // 
            this.DO_Button.Location = new System.Drawing.Point(547, 539);
            this.DO_Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DO_Button.Name = "DO_Button";
            this.DO_Button.Size = new System.Drawing.Size(216, 39);
            this.DO_Button.TabIndex = 11;
            this.DO_Button.Text = "WYKONAJ";
            this.DO_Button.UseVisualStyleBackColor = true;
            this.DO_Button.Click += new System.EventHandler(this.DO_Button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(542, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "KOD PROGRAMU";
            // 
            // CLR_Button
            // 
            this.CLR_Button.Location = new System.Drawing.Point(876, 539);
            this.CLR_Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CLR_Button.Name = "CLR_Button";
            this.CLR_Button.Size = new System.Drawing.Size(107, 39);
            this.CLR_Button.TabIndex = 14;
            this.CLR_Button.Text = "WYCZYŚĆ";
            this.CLR_Button.UseVisualStyleBackColor = true;
            this.CLR_Button.Click += new System.EventHandler(this.CLR_Button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(480, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Tryb wykonania";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zapiszToolStripMenuItem,
            this.zapiszJakoToolStripMenuItem,
            this.wczytajToolStripMenuItem,
            this.pomocToolStripMenuItem,
            this.oProgramieToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1068, 28);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // zapiszToolStripMenuItem
            // 
            this.zapiszToolStripMenuItem.Name = "zapiszToolStripMenuItem";
            this.zapiszToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.zapiszToolStripMenuItem.Text = "Zapisz";
            this.zapiszToolStripMenuItem.Click += new System.EventHandler(this.zapiszToolStripMenuItem_Click);
            // 
            // zapiszJakoToolStripMenuItem
            // 
            this.zapiszJakoToolStripMenuItem.Name = "zapiszJakoToolStripMenuItem";
            this.zapiszJakoToolStripMenuItem.Size = new System.Drawing.Size(107, 24);
            this.zapiszJakoToolStripMenuItem.Text = "Zapisz jako...";
            this.zapiszJakoToolStripMenuItem.Click += new System.EventHandler(this.zapiszJakoToolStripMenuItem_Click);
            // 
            // wczytajToolStripMenuItem
            // 
            this.wczytajToolStripMenuItem.Name = "wczytajToolStripMenuItem";
            this.wczytajToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.wczytajToolStripMenuItem.Text = "Wczytaj";
            this.wczytajToolStripMenuItem.Click += new System.EventHandler(this.wczytajToolStripMenuItem_Click);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            this.pomocToolStripMenuItem.Click += new System.EventHandler(this.pomocToolStripMenuItem_Click);
            // 
            // oProgramieToolStripMenuItem
            // 
            this.oProgramieToolStripMenuItem.Name = "oProgramieToolStripMenuItem";
            this.oProgramieToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.oProgramieToolStripMenuItem.Text = "O programie";
            this.oProgramieToolStripMenuItem.Click += new System.EventHandler(this.oProgramieToolStripMenuItem_Click);
            // 
            // ADD_Button
            // 
            this.ADD_Button.Location = new System.Drawing.Point(85, 354);
            this.ADD_Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ADD_Button.Name = "ADD_Button";
            this.ADD_Button.Size = new System.Drawing.Size(216, 48);
            this.ADD_Button.TabIndex = 20;
            this.ADD_Button.Text = "DODAJ ROZKAZ";
            this.ADD_Button.UseVisualStyleBackColor = true;
            this.ADD_Button.Click += new System.EventHandler(this.ADD_Button_Click);
            // 
            // AHReg_TextBox
            // 
            this.AHReg_TextBox.Enabled = false;
            this.AHReg_TextBox.Location = new System.Drawing.Point(139, 458);
            this.AHReg_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.AHReg_TextBox.Name = "AHReg_TextBox";
            this.AHReg_TextBox.ReadOnly = true;
            this.AHReg_TextBox.Size = new System.Drawing.Size(112, 22);
            this.AHReg_TextBox.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(52, 418);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 25);
            this.label7.TabIndex = 27;
            this.label7.Text = "Wartość rejestrów";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(52, 458);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 25);
            this.label8.TabIndex = 28;
            this.label8.Text = "AX:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(52, 490);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 25);
            this.label9.TabIndex = 30;
            this.label9.Text = "BX:";
            // 
            // BHReg_TextBox
            // 
            this.BHReg_TextBox.Enabled = false;
            this.BHReg_TextBox.Location = new System.Drawing.Point(139, 490);
            this.BHReg_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.BHReg_TextBox.Name = "BHReg_TextBox";
            this.BHReg_TextBox.ReadOnly = true;
            this.BHReg_TextBox.Size = new System.Drawing.Size(112, 22);
            this.BHReg_TextBox.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(52, 522);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 25);
            this.label10.TabIndex = 32;
            this.label10.Text = "CX:";
            // 
            // CHReg_TextBox
            // 
            this.CHReg_TextBox.Enabled = false;
            this.CHReg_TextBox.Location = new System.Drawing.Point(139, 522);
            this.CHReg_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.CHReg_TextBox.Name = "CHReg_TextBox";
            this.CHReg_TextBox.ReadOnly = true;
            this.CHReg_TextBox.Size = new System.Drawing.Size(112, 22);
            this.CHReg_TextBox.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(52, 554);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 25);
            this.label11.TabIndex = 34;
            this.label11.Text = "DX:";
            // 
            // DHReg_TextBox
            // 
            this.DHReg_TextBox.Enabled = false;
            this.DHReg_TextBox.Location = new System.Drawing.Point(139, 554);
            this.DHReg_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.DHReg_TextBox.Name = "DHReg_TextBox";
            this.DHReg_TextBox.ReadOnly = true;
            this.DHReg_TextBox.Size = new System.Drawing.Size(112, 22);
            this.DHReg_TextBox.TabIndex = 33;
            // 
            // STEP_Timer
            // 
            this.STEP_Timer.Interval = 1000;
            this.STEP_Timer.Tick += new System.EventHandler(this.STEP_Timer_Tick);
            // 
            // ALL_radioButton
            // 
            this.ALL_radioButton.AutoSize = true;
            this.ALL_radioButton.Checked = true;
            this.ALL_radioButton.Location = new System.Drawing.Point(32, 15);
            this.ALL_radioButton.Margin = new System.Windows.Forms.Padding(4);
            this.ALL_radioButton.Name = "ALL_radioButton";
            this.ALL_radioButton.Size = new System.Drawing.Size(99, 21);
            this.ALL_radioButton.TabIndex = 37;
            this.ALL_radioButton.TabStop = true;
            this.ALL_radioButton.Text = "Całościowo";
            this.ALL_radioButton.UseVisualStyleBackColor = true;
            // 
            // STEP_radioButton
            // 
            this.STEP_radioButton.AutoSize = true;
            this.STEP_radioButton.Location = new System.Drawing.Point(207, 15);
            this.STEP_radioButton.Margin = new System.Windows.Forms.Padding(4);
            this.STEP_radioButton.Name = "STEP_radioButton";
            this.STEP_radioButton.Size = new System.Drawing.Size(83, 21);
            this.STEP_radioButton.TabIndex = 38;
            this.STEP_radioButton.Text = "Krokowo";
            this.STEP_radioButton.UseVisualStyleBackColor = true;
            // 
            // IMM_radioButton
            // 
            this.IMM_radioButton.AutoSize = true;
            this.IMM_radioButton.Location = new System.Drawing.Point(12, 46);
            this.IMM_radioButton.Margin = new System.Windows.Forms.Padding(4);
            this.IMM_radioButton.Name = "IMM_radioButton";
            this.IMM_radioButton.Size = new System.Drawing.Size(130, 21);
            this.IMM_radioButton.TabIndex = 36;
            this.IMM_radioButton.Text = "Natychmiastowy";
            this.IMM_radioButton.UseVisualStyleBackColor = true;
            this.IMM_radioButton.CheckedChanged += new System.EventHandler(this.IMM_radioButton_CheckedChanged);
            // 
            // REG_radioButton
            // 
            this.REG_radioButton.AutoSize = true;
            this.REG_radioButton.Checked = true;
            this.REG_radioButton.Location = new System.Drawing.Point(12, 17);
            this.REG_radioButton.Margin = new System.Windows.Forms.Padding(4);
            this.REG_radioButton.Name = "REG_radioButton";
            this.REG_radioButton.Size = new System.Drawing.Size(98, 21);
            this.REG_radioButton.TabIndex = 35;
            this.REG_radioButton.TabStop = true;
            this.REG_radioButton.Text = "Rejestrowy";
            this.REG_radioButton.UseVisualStyleBackColor = true;
            this.REG_radioButton.CheckedChanged += new System.EventHandler(this.REG_radioButton_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.REG_radioButton);
            this.panel1.Controls.Add(this.IMM_radioButton);
            this.panel1.Location = new System.Drawing.Point(85, 71);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 87);
            this.panel1.TabIndex = 39;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ALL_radioButton);
            this.panel2.Controls.Add(this.STEP_radioButton);
            this.panel2.Location = new System.Drawing.Point(641, 36);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(332, 50);
            this.panel2.TabIndex = 40;
            // 
            // CLRREG_Button
            // 
            this.CLRREG_Button.Location = new System.Drawing.Point(59, 594);
            this.CLRREG_Button.Margin = new System.Windows.Forms.Padding(4);
            this.CLRREG_Button.Name = "CLRREG_Button";
            this.CLRREG_Button.Size = new System.Drawing.Size(280, 42);
            this.CLRREG_Button.TabIndex = 41;
            this.CLRREG_Button.Text = "WYCZYŚĆ REJESTRY";
            this.CLRREG_Button.UseVisualStyleBackColor = true;
            this.CLRREG_Button.Click += new System.EventHandler(this.CLRREG_Button_Click);
            // 
            // ALReg_TextBox
            // 
            this.ALReg_TextBox.Enabled = false;
            this.ALReg_TextBox.Location = new System.Drawing.Point(304, 458);
            this.ALReg_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.ALReg_TextBox.Name = "ALReg_TextBox";
            this.ALReg_TextBox.ReadOnly = true;
            this.ALReg_TextBox.Size = new System.Drawing.Size(112, 22);
            this.ALReg_TextBox.TabIndex = 42;
            // 
            // BLReg_TextBox
            // 
            this.BLReg_TextBox.Enabled = false;
            this.BLReg_TextBox.Location = new System.Drawing.Point(304, 490);
            this.BLReg_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.BLReg_TextBox.Name = "BLReg_TextBox";
            this.BLReg_TextBox.ReadOnly = true;
            this.BLReg_TextBox.Size = new System.Drawing.Size(112, 22);
            this.BLReg_TextBox.TabIndex = 43;
            // 
            // CLReg_TextBox
            // 
            this.CLReg_TextBox.Enabled = false;
            this.CLReg_TextBox.Location = new System.Drawing.Point(304, 522);
            this.CLReg_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.CLReg_TextBox.Name = "CLReg_TextBox";
            this.CLReg_TextBox.ReadOnly = true;
            this.CLReg_TextBox.Size = new System.Drawing.Size(112, 22);
            this.CLReg_TextBox.TabIndex = 44;
            // 
            // DLReg_TextBox
            // 
            this.DLReg_TextBox.Enabled = false;
            this.DLReg_TextBox.Location = new System.Drawing.Point(304, 554);
            this.DLReg_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.DLReg_TextBox.Name = "DLReg_TextBox";
            this.DLReg_TextBox.ReadOnly = true;
            this.DLReg_TextBox.Size = new System.Drawing.Size(112, 22);
            this.DLReg_TextBox.TabIndex = 45;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(104, 464);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 17);
            this.label12.TabIndex = 46;
            this.label12.Text = "AH";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(269, 462);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(25, 17);
            this.label13.TabIndex = 47;
            this.label13.Text = "AL";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(104, 496);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 17);
            this.label14.TabIndex = 48;
            this.label14.Text = "BH";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(104, 528);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 17);
            this.label15.TabIndex = 49;
            this.label15.Text = "CH";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(104, 560);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 17);
            this.label16.TabIndex = 50;
            this.label16.Text = "DH";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(269, 496);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(25, 17);
            this.label17.TabIndex = 51;
            this.label17.Text = "BL";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(269, 528);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(25, 17);
            this.label18.TabIndex = 52;
            this.label18.Text = "CL";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(268, 560);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(26, 17);
            this.label19.TabIndex = 53;
            this.label19.Text = "DL";
            // 
            // KOD_RichTextBox
            // 
            this.KOD_RichTextBox.Location = new System.Drawing.Point(547, 139);
            this.KOD_RichTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.KOD_RichTextBox.Name = "KOD_RichTextBox";
            this.KOD_RichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.KOD_RichTextBox.Size = new System.Drawing.Size(433, 389);
            this.KOD_RichTextBox.TabIndex = 54;
            this.KOD_RichTextBox.Text = "";
            // 
            // sfd
            // 
            this.sfd.Filter = "text file|*.txt";
            // 
            // ofd
            // 
            this.ofd.Filter = "Text file|*.txt";
            // 
            // LineNumbers_For_RichTextBox1
            // 
            this.LineNumbers_For_RichTextBox1._SeeThroughMode_ = false;
            this.LineNumbers_For_RichTextBox1.AutoSizing = true;
            this.LineNumbers_For_RichTextBox1.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LineNumbers_For_RichTextBox1.BackgroundGradient_BetaColor = System.Drawing.Color.White;
            this.LineNumbers_For_RichTextBox1.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.LineNumbers_For_RichTextBox1.BorderLines_Color = System.Drawing.Color.AliceBlue;
            this.LineNumbers_For_RichTextBox1.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.LineNumbers_For_RichTextBox1.BorderLines_Thickness = 1F;
            this.LineNumbers_For_RichTextBox1.DockSide = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Left;
            this.LineNumbers_For_RichTextBox1.GridLines_Color = System.Drawing.Color.Azure;
            this.LineNumbers_For_RichTextBox1.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.LineNumbers_For_RichTextBox1.GridLines_Thickness = 1F;
            this.LineNumbers_For_RichTextBox1.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
            this.LineNumbers_For_RichTextBox1.LineNrs_AntiAlias = true;
            this.LineNumbers_For_RichTextBox1.LineNrs_AsHexadecimal = false;
            this.LineNumbers_For_RichTextBox1.LineNrs_ClippedByItemRectangle = true;
            this.LineNumbers_For_RichTextBox1.LineNrs_LeadingZeroes = false;
            this.LineNumbers_For_RichTextBox1.LineNrs_Offset = new System.Drawing.Size(0, 0);
            this.LineNumbers_For_RichTextBox1.Location = new System.Drawing.Point(525, 139);
            this.LineNumbers_For_RichTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.LineNumbers_For_RichTextBox1.MarginLines_Color = System.Drawing.Color.SlateGray;
            this.LineNumbers_For_RichTextBox1.MarginLines_Side = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
            this.LineNumbers_For_RichTextBox1.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.LineNumbers_For_RichTextBox1.MarginLines_Thickness = 1F;
            this.LineNumbers_For_RichTextBox1.Name = "LineNumbers_For_RichTextBox1";
            this.LineNumbers_For_RichTextBox1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LineNumbers_For_RichTextBox1.ParentRichTextBox = this.KOD_RichTextBox;
            this.LineNumbers_For_RichTextBox1.Show_BackgroundGradient = true;
            this.LineNumbers_For_RichTextBox1.Show_BorderLines = true;
            this.LineNumbers_For_RichTextBox1.Show_GridLines = false;
            this.LineNumbers_For_RichTextBox1.Show_LineNrs = true;
            this.LineNumbers_For_RichTextBox1.Show_MarginLines = true;
            this.LineNumbers_For_RichTextBox1.Size = new System.Drawing.Size(21, 389);
            this.LineNumbers_For_RichTextBox1.TabIndex = 57;
            // 
            // AXINT_TextBox
            // 
            this.AXINT_TextBox.Enabled = false;
            this.AXINT_TextBox.Location = new System.Drawing.Point(425, 458);
            this.AXINT_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.AXINT_TextBox.Name = "AXINT_TextBox";
            this.AXINT_TextBox.ReadOnly = true;
            this.AXINT_TextBox.Size = new System.Drawing.Size(60, 22);
            this.AXINT_TextBox.TabIndex = 58;
            // 
            // BXINT_TextBox
            // 
            this.BXINT_TextBox.Enabled = false;
            this.BXINT_TextBox.Location = new System.Drawing.Point(425, 490);
            this.BXINT_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.BXINT_TextBox.Name = "BXINT_TextBox";
            this.BXINT_TextBox.ReadOnly = true;
            this.BXINT_TextBox.Size = new System.Drawing.Size(60, 22);
            this.BXINT_TextBox.TabIndex = 59;
            // 
            // CXINT_TextBox
            // 
            this.CXINT_TextBox.Enabled = false;
            this.CXINT_TextBox.Location = new System.Drawing.Point(425, 522);
            this.CXINT_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.CXINT_TextBox.Name = "CXINT_TextBox";
            this.CXINT_TextBox.ReadOnly = true;
            this.CXINT_TextBox.Size = new System.Drawing.Size(60, 22);
            this.CXINT_TextBox.TabIndex = 60;
            // 
            // DXINT_TextBox
            // 
            this.DXINT_TextBox.Enabled = false;
            this.DXINT_TextBox.Location = new System.Drawing.Point(425, 554);
            this.DXINT_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.DXINT_TextBox.Name = "DXINT_TextBox";
            this.DXINT_TextBox.ReadOnly = true;
            this.DXINT_TextBox.Size = new System.Drawing.Size(60, 22);
            this.DXINT_TextBox.TabIndex = 61;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(440, 438);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(30, 17);
            this.label20.TabIndex = 62;
            this.label20.Text = "INT";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label21.Location = new System.Drawing.Point(318, 162);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(163, 25);
            this.label21.TabIndex = 63;
            this.label21.Text = "Dodaj przerwanie";
            // 
            // INT_ComboBox
            // 
            this.INT_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.INT_ComboBox.DropDownWidth = 230;
            this.INT_ComboBox.FormattingEnabled = true;
            this.INT_ComboBox.Items.AddRange(new object[] {
            "Wolna pamięć dyskowa",
            "Obecny dysk domyślny",
            "Odczytanie znaku z wejścia",
            "Wersja DOS",
            "Ścieżka pliku wykonywalnego",
            "Zegar czasu rzeczywistego",
            "Data zegaru czasu rzeczywistego",
            "Pokaż wskaźnik myszy",
            "Ukryj wskaźnik myszy",
            "Pozycja wskaźnika myszy i stan przycisków",
            "Czekaj"});
            this.INT_ComboBox.Location = new System.Drawing.Point(323, 199);
            this.INT_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.INT_ComboBox.Name = "INT_ComboBox";
            this.INT_ComboBox.Size = new System.Drawing.Size(158, 24);
            this.INT_ComboBox.TabIndex = 64;
            // 
            // ADDINT_button
            // 
            this.ADDINT_button.Location = new System.Drawing.Point(315, 249);
            this.ADDINT_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ADDINT_button.Name = "ADDINT_button";
            this.ADDINT_button.Size = new System.Drawing.Size(178, 35);
            this.ADDINT_button.TabIndex = 65;
            this.ADDINT_button.Text = "DODAJ PRZERWANIE";
            this.ADDINT_button.UseVisualStyleBackColor = true;
            this.ADDINT_button.Click += new System.EventHandler(this.ADDINT_button_Click);
            // 
            // DEMO_checkbox
            // 
            this.DEMO_checkbox.AutoSize = true;
            this.DEMO_checkbox.Location = new System.Drawing.Point(818, 112);
            this.DEMO_checkbox.Name = "DEMO_checkbox";
            this.DEMO_checkbox.Size = new System.Drawing.Size(162, 21);
            this.DEMO_checkbox.TabIndex = 66;
            this.DEMO_checkbox.Text = "Tryb demonstracyjny";
            this.DEMO_checkbox.UseVisualStyleBackColor = true;
            this.DEMO_checkbox.CheckedChanged += new System.EventHandler(this.DEMO_checkbox_CheckedChanged);
            // 
            // Mikroprocesor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 654);
            this.Controls.Add(this.DEMO_checkbox);
            this.Controls.Add(this.ADDINT_button);
            this.Controls.Add(this.INT_ComboBox);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.DXINT_TextBox);
            this.Controls.Add(this.CXINT_TextBox);
            this.Controls.Add(this.BXINT_TextBox);
            this.Controls.Add(this.AXINT_TextBox);
            this.Controls.Add(this.LineNumbers_For_RichTextBox1);
            this.Controls.Add(this.KOD_RichTextBox);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.DLReg_TextBox);
            this.Controls.Add(this.CLReg_TextBox);
            this.Controls.Add(this.BLReg_TextBox);
            this.Controls.Add(this.ALReg_TextBox);
            this.Controls.Add(this.CLRREG_Button);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.DHReg_TextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.CHReg_TextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.BHReg_TextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AHReg_TextBox);
            this.Controls.Add(this.ADD_Button);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CLR_Button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DO_Button);
            this.Controls.Add(this.VALUE_TextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rejZ_ComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rejDO_ComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rozkaz_ComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TA_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Mikroprocesor";
            this.Text = "Mikroprocesor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mikroprocesor_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TA_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox rozkaz_ComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox rejDO_ComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox rejZ_ComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox VALUE_TextBox;
        private System.Windows.Forms.Button DO_Button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button CLR_Button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem zapiszToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wczytajToolStripMenuItem;
        private System.Windows.Forms.Button ADD_Button;
        private System.Windows.Forms.TextBox AHReg_TextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox BHReg_TextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox CHReg_TextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox DHReg_TextBox;
        public System.Windows.Forms.Timer STEP_Timer;
        private System.Windows.Forms.RadioButton ALL_radioButton;
        private System.Windows.Forms.RadioButton STEP_radioButton;
        private System.Windows.Forms.RadioButton IMM_radioButton;
        private System.Windows.Forms.RadioButton REG_radioButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button CLRREG_Button;
        private System.Windows.Forms.TextBox ALReg_TextBox;
        private System.Windows.Forms.TextBox BLReg_TextBox;
        private System.Windows.Forms.TextBox CLReg_TextBox;
        private System.Windows.Forms.TextBox DLReg_TextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RichTextBox KOD_RichTextBox;
        private LineNumbers.LineNumbers_For_RichTextBox LineNumbers_For_RichTextBox1;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.OpenFileDialog ofd;
        private ToolStripMenuItem zapiszJakoToolStripMenuItem;
        private ToolStripMenuItem pomocToolStripMenuItem;
        private ToolStripMenuItem oProgramieToolStripMenuItem;
        private TextBox AXINT_TextBox;
        private TextBox BXINT_TextBox;
        private TextBox CXINT_TextBox;
        private TextBox DXINT_TextBox;
        private Label label20;
        private Label label21;
        private ComboBox INT_ComboBox;
        private Button ADDINT_button;
        private CheckBox DEMO_checkbox;
    }
}

