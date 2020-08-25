using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling_Game
{
  public class Game
  {
    private int MAX_FRAMES = 10;
    private int TENTH_FRAME = 9;
    private int MAGIC_FRAME = 8; // index of the frame where scores may reach into the 10th frame

    public Frame[] Frames { get; set; }

    int currentFrame;       // Should be 0 to 9 (10 frames)
    int currentRollInFrame; // either 1 or 2 (array index 0 or 1) except 10th

    public Game()
    {
      Frames = new Frame[MAX_FRAMES];
      for(int i = 0; i< MAX_FRAMES-1; i++)
      {
        Frames[i] = new Frame();
      }
      // Add the tenth "Special" frame
      Frames[MAX_FRAMES - 1] = new TenthFrame();


      currentFrame = 0;
      currentRollInFrame = 1; 
    }

    public void roll(int pins)
    {
      Frames[currentFrame].Pins[currentRollInFrame-1] = pins;

      // special case for 10th frame
      if (currentFrame == TENTH_FRAME)
      {
        currentRollInFrame++;
        return;
      }

      // Strike automatically advances frame number.
      if (pins == 10)
      {
        currentFrame++;
        return;
      }

      // state manipulation
      if (++currentRollInFrame == 3)
      {
        currentFrame++;
        currentRollInFrame = 1;
      }

    }

    public int score()
    {
      int score = 0;
      for (int i = 0; i < MAX_FRAMES-1; i++) // Only score frames 1-9 (index 0-8)
      {
        score += Frames[i].Pins[0];
        
        // strike bonus - add next two roles as bonus
        if (IsStrikeBonus(i))
        {
          // add the next frame's fist roll
          score += Frames[i + 1].Pins[0];

          // Special logic for the 2nd bonus roll.
          if (Frames[i + 1].Pins[0] == 10 && i != MAGIC_FRAME)  // add roll from 2nd frame
            score += Frames[i + 2].Pins[0];
          else if (Frames[i + 1].Pins[0] == 10 && i == MAGIC_FRAME) // add roll from 2nd roll in 10th frame if 2nd roll strike
            score += Frames[i + 1].Pins[1];
          else
            score += Frames[i + 1].Pins[1]; // if 2nd roll non-strike, add 2nd roll for next frame.
          continue;
        }

        // add the second role in the frame
        score += Frames[i].Pins[1];
        // Spare bonus - frame total of 10 adds next single roll
        if (IsSpareBonus(i))
        {
          score += Frames[i + 1].Pins[0];
        }

      }

      // Process 10th frame
      score += ScoreTenthFrame();

      return score;
    }

    private int ScoreTenthFrame()
    {
      int score = 0;

      score += Frames[MAX_FRAMES-1].Pins[0];
      score += Frames[MAX_FRAMES-1].Pins[1];
      score += Frames[MAX_FRAMES-1].Pins[2];

      return score;
    }


    private bool IsStrikeBonus(int frameNum)
    {
      return (Frames[frameNum].Pins[0] == 10);
    }

    private bool IsSpareBonus(int frameNum)
    {
      return (Frames[frameNum].Pins[0] + Frames[frameNum].Pins[1] == 10);
    }
  }
}
