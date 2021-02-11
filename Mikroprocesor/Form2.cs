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
    public partial class Pomoc : Form
    {
        public Pomoc()
        {
            InitializeComponent();
        }

        private void Pomoc_Load(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(asm.GetManifestResourceStream("Mikroprocesor.Pomoc.html"));
            webBrowser1.DocumentText = reader.ReadToEnd();
        }
    }
}
