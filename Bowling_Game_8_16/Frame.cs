using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling_Game
{
  // The frame class represents a single frame of a bowling game
  public class Frame
  {
    public int[] Pins { get; set; }

    public Frame()
    {
      Pins = new int[2]; // normal frames.  How to specify 3 for the tenth?
    }


  }
}
