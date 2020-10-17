using System;

namespace Bowling_Game
{
  public class RollException : Exception
  {
    public RollException(String message) : base(message)
    { }

  }
}