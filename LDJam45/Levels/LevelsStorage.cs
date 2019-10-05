using System;
using System.Collections.Generic;

namespace LDJam45
{
    public static class LevelsStorage
    {
        public static List<Level> levels;

        public static void GenerateLevels()
        {
            // Init
            levels = new List<Level>();

            // Level 1 : NOTHING
            Level nothing = new Level("NOTHING");
            levels.Add(nothing);
            nothing.Add(1, 1, 0, 100, 1);
            nothing.Add(2, 2, 0, 100, 2);
            nothing.Add(3, 3, 0, 100, 3);

            // Level 2 : IS

            //Level 3 : IMPOSSIBLE
        }

        public static Level GetNextLevel()
        {
            Level nextLvl = levels[0];
            levels.Remove(nextLvl);
            return nextLvl;
        }
    }

    public struct Level
    {
        public string word;
        public List<Spawn> spawns;

        public Level(string word)
        {
            this.word = word;
            spawns = new List<Spawn>();
        }

        public void Add(float time, int number, int decim, int speed, int line)
        {
            spawns.Add(new Spawn(time, number, decim, speed, line));
        }
    }

    public struct Spawn
    {
        public float time;
        public int number;
        public int decim;
        public int speed;
        public int line;

        public Spawn(float time, int number, int decim, int speed, int line)
        {
            this.time = time;
            this.number = number;
            this.decim = decim;
            this.speed = speed;
            this.line = line;
        }
    }
}

