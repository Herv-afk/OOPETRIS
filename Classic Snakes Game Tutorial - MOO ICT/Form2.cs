using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPETRIS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            MusicPlayer.StartMusic(); // starts looping music
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Form1 game = new Form1();   // create game screen,
            game.Show();                // show game screen
            this.Hide();
            this.ActiveControl = null;   // removes focus from the Start button
            this.Focus();                // focuses the form so it can receive key presses
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 game = Application.OpenForms["Form1"] as Form1;
            if (game != null)
            {
                game.StopGame();   // stop running timers
                game.Close();      // close game form safely
            }

            Application.Exit();     // exit program normally
            MusicPlayer.StopMusic(); // stop before exiting
        }
    }
}
