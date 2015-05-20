using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PI_AF.Sinus;

namespace PI_AF.Sinus
{
    public partial class RandomConfigEditor : Form
    {
        public RandomConfigEditor()
        {
            InitializeComponent();
        }

        private RandomDataReference editedRandomDataReference;

        public RandomConfigEditor(RandomDataReference obj, bool readOnly): this()
        {
            editedRandomDataReference = obj;
            var cfg = RandomConfig.FromConfigString(editedRandomDataReference.ConfigString);
            if (cfg != null)
            {
                this.cbCounter.Checked = cfg.UseCounter;
                this.numFreq.Value = cfg.Frequency;
            }

        }

        private void bOK_Click(object sender, EventArgs e)
        {
            RandomConfig cfg = new RandomConfig();
            cfg.Frequency =(int) this.numFreq.Value;
            cfg.UseCounter = this.cbCounter.Checked;

            editedRandomDataReference.ConfigString = cfg.GetConfigString();
            this.Close();
        }
    }
}
