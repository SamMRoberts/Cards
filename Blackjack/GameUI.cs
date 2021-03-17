using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blackjack
{
    public partial class GameUI : Form
    {
        Environment environment = new Environment();
        public GameUI()
        {            
            InitializeComponent();
        }

        private void GameUI_Load(object sender, EventArgs e)
        {
            
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            environment.CheckInput(inputBox.Text);
        }
    }
}
