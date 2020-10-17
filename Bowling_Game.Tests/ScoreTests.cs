using Bowling_Game;
using System;
using Xunit;

namespace Bowling_Game.Tests
{
  public class ScoreTests
  {
    [Fact]
    public void OneRollProducesOneScore()
    {
      Game g = new Game();

      g.roll(4);

      int score = g.score();

      Assert.Equal(4, score);

    }

    [Fact]
    public void AllOnesProducesScoreOf20()
    {
      Game g = new Game();
      for(int i = 0; i<20; i++)
      {
        g.roll(1);
      }

      int score = g.score();

      Assert.Equal(20, score);

    }

    [Fact]
    public void StrikeInFrameOneAddsNextTwoRolls()
    {
      Game g = new Game();

      g.roll(10);
      g.roll(3);
      g.roll(4);

      int score = g.score();

      Assert.Equal(24, score);
    }

    [Fact]
    public void SpareInFrameOneAddNextOneRoll()
    {
      Game g = new Game();

      g.roll(7);
      g.roll(3);
      g.roll(4);
      g.roll(4);

      int score = g.score();

      Assert.Equal(22, score);
    }

    [Fact]
    public void AllSparesResultsIn190Score()
    {
      Game g = new Game();
      // Frame 1
      RoleFrame(g, 9, 1);
      // Frame 2
      RoleFrame(g, 9, 1);
      // Frame 3
      RoleFrame(g, 9, 1);
      // Frame 4
      RoleFrame(g, 9, 1);
      // Frame 5
      RoleFrame(g, 9, 1);
      // Frame 6
      RoleFrame(g, 9, 1);
      // Frame 7
      RoleFrame(g, 9, 1);
      // Frame 8
      RoleFrame(g, 9, 1);
      // Frame 9
      RoleFrame(g, 9, 1);
      // Frame 10 - last frame has 3 rolls.
      RollFinalFrame(g, 9, 1, 9);

      int score = g.score();

      Assert.Equal(190, score);

    }

    

    [Fact]
    public void AllStrikesResultsIn300()
    {
      Game g = new Game();
      // Frame 1
      g.roll(10);
      // Frame 2
      g.roll(10);
      // Frame 3
      g.roll(10);
      // Frame 4
      g.roll(10);
      // Frame 5
      g.roll(10);
      // Frame 6
      g.roll(10);
      // Frame 7
      g.roll(10);
      // Frame 8
      g.roll(10);
      // Frame 9
      g.roll(10);
      // Frame 10 - last frame has 3 rolls.
      g.roll(10);
      g.roll(10);
      g.roll(10);

      int score = g.score();

      Assert.Equal(300, score);
    }
  

    [Fact]
    public void CannotRollMoreThan10PinsAtOneTime()
    {
      Game g = new Game();

      Exception ex = Assert.Throws<RollException>(() => g.roll(11));
           
      Assert.Equal("Pin Error", ex.Message);

    }

    [Fact]
    public void SecondRollCannotPutTotalOver10ForFrame()
    {
      Game g = new Game();

      g.roll(5);

      Exception ex = Assert.Throws<RollException>(() => g.roll(6));

      Assert.Equal("Pin Error", ex.Message);

    }
  
    [Fact]
    public void AutoRollExceptionCheck()
    {
      Game g = new Game();

      Exception ex = Assert.Throws<RollException>(() => RoleFrame(g, 4, 8));

      Assert.Equal("Pin Error", ex.Message);
    }


    private static void RollFinalFrame(Game g, int r1, int r2, int r3)
    {
      g.roll(r1);
      g.roll(r2);
      g.roll(r3);
    }

    private static void RoleFrame(Game g, int roll1, int roll2)
    {
      g.roll(roll1);
      g.roll(roll2);
    }
  
  }
}
