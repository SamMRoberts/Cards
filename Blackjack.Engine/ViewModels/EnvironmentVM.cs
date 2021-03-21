using System;
using System.Collections.Generic;
using System.Text;
using Blackjack.Engine.Models;

namespace Blackjack.Engine.ViewModels
{
    public class EnvironmentVM
    {
        public Models.Environment NewEnvironment { get; set; }
        public EnvironmentVM()
        {
            NewEnvironment = new Models.Environment();
        }

        public void Setup()
        {
            NewEnvironment.Setup();
        }
        public void Hit()
        {
            NewEnvironment.PlayerHit();
        }
        public void Stand()
        {
            NewEnvironment.PlayerStand();
        }

        public void Deal()
        {
            NewEnvironment.Deal();
        }

    }
}
