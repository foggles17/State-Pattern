using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace State_Pattern
{
    public partial class StartUp : Form
    {
        CheckBox[] birth;
        CheckBox[] survival;
        public StartUp()
        {
            InitializeComponent();
            birth = new CheckBox[9];
            birth[0] = birth0;
            birth[1] = birth1;
            birth[2] = birth2;
            birth[3] = birth3;
            birth[4] = birth4;
            birth[5] = birth5;
            birth[6] = birth6;
            birth[7] = birth7;
            birth[8] = birth8;

            survival = new CheckBox[9];
            survival[0] = survival0;
            survival[1] = survival1;
            survival[2] = survival2;
            survival[3] = survival3;
            survival[4] = survival4;
            survival[5] = survival5;
            survival[6] = survival6;
            survival[7] = survival7;
            survival[8] = survival8;

            birth[3].Checked = true;
            survival[2].Checked = true;
            survival[3].Checked = true;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            bool[] birthConditions = new bool[9];
            for(int i = 0; i < birthConditions.Length; i++)
            {
                birthConditions[i] = birth[i].Checked;
            }
            bool[] survivalConditions = new bool[9];
            for (int i = 0; i < survivalConditions.Length; i++)
            {
                survivalConditions[i] = survival[i].Checked;
            }
            Game game = new Game((int)widthNUD.Value, (int)heightNUD.Value, birthConditions, survivalConditions);
            game.Show();
        }
    }
}
