using System;
using System.Linq;
using System.Collections.Generic;

public struct Game
{
    public int Score;

    public DateTime Time;
}

public static class Leaderboard
{
    public static List<Game> Games = new List<Game>();

    public static void Order() => 
        Games = Games.OrderByDescending(g => g.Score).ToList();

}