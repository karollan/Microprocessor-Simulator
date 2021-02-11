using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikroprocesor
{
    public partial class ReadKey : Form
    {
        public ReadKey(Mikroprocesor procesor, bool check)
        {
            InitializeComponent();
            this.procesor = procesor;
            this.check = check;
        }

        Mikroprocesor procesor;
        bool check;

        private void ReadKey_KeyDown(object sender, KeyEventArgs e)
        {
            procesor.AL = (sbyte)e.KeyValue;
            procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF); ;
            procesor.aktualizujRejestry();
            procesor.Enabled = true;
            if (check)
            {
                procesor.STEP_Timer.Start();
            }
            this.Close();
        }
    }
}
