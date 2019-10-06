using System;
using System.Collections.Generic;

namespace LDJam45
{
    public static class LevelStorage
    {
        public static List<Level> levels;
        public static int currentLevel = -1;
        public static int currentEvent = -1;
        public static bool generated = false;

        public static void GenerateLevels()
        {
            generated = true;

            // Init
            levels = new List<Level>();
            // time | number | decimal | speed | line

            // Tuto : COMMANDER
            /*
            Level tuto = CreateLevel("COMMANDER");
            tuto.Add(0, "Welcome to the army soldiers !");
            tuto.Add(0, "Today you will fight our \n worst enemy : Numbers");
            tuto.Add(0, "We, Letters, should not let \n    them pass !");
            tuto.Add(0, "Each of you will \n help stopping them,");
            tuto.Add(0, "Let's start with NOTHING");
            */

            // Level 1 : NOTHING
            Level nothing = CreateLevel("NOTHING");
            nothing.Add(0, "Press the LETTER KEYS \n to shoot with your letters");
            nothing.Add(0, "Don't let them touch you");
            nothing.Add(1, 1, 0, 500, 1);
            nothing.Add(1, 1, 0, 100, 6);
            nothing.Add(2, 2, 0, 100, 2);
            nothing.Add(3, 3, 0, 100, 3);
            nothing.Add(4, 4, 0, 100, 4);
            nothing.Add(5, 5, 0, 100, 5);
            nothing.Add(6, 6, 0, 100, 1);
            nothing.Add(6, 6, 0, 100, 6);
            nothing.Add(7, 7, 0, 100, 7);
            nothing.Add(8, 6, 0, 100, 6);
            nothing.Add(8, 6, 0, 100, 1);
            nothing.Add(9, 5, 0, 100, 5);
            nothing.Add(10, 4, 0, 100, 4);
            nothing.Add(11, 3, 0, 100, 3);
            nothing.Add(12, 2, 0, 100, 2);
            nothing.Add(13, 1, 0, 100, 1);
            nothing.Add(13, 1, 0, 100, 6);
            nothing.Add(20, "Next ones will be fast, \n we need a shorter guy \n for the next wave");

            // Level 2 : IS
            Level est = CreateLevel("IS");
            est.Add(1, 1, 0, 250, 1);
            est.Add(1.5f, 1, 0, 250, 2);
            est.Add(2, 1, 0, 250, 1);
            est.Add(2.5f, 1, 0, 250, 1);
            est.Add(3, 1, 0, 250, 2);

            //Level 3 : IMPOSSIBLE
            Level impossible = CreateLevel("IMPOSSIBLE");
            impossible.Add(1, 1, 0, 100, 1);
            impossible.Add(2, 1, 0, 100, 1);
            impossible.Add(3, 1, 0, 100, 1);
            impossible.Add(4, 1, 0, 100, 1);
            impossible.Add(5, 1, 0, 100, 1);
        }

        private static Level CreateLevel(string word)
        {
            Level lvl = new Level(word);
            levels.Add(lvl);
            return lvl;
        }

        public static Level GetNextLevel()
        {
            // End of level
            if (currentLevel == levels.Count)
                return new Level(""); // No more events;
            currentLevel++;
            currentEvent = -1;
            return levels[currentLevel];
        }

        public static Level GetCurrentLevel()
        {
            currentEvent = -1;
            return levels[currentLevel];
        }
    }

    public struct Level
    {
        public string word;
        public List<Event> events;

        public Level(string word)
        {
            this.word = word;
            events = new List<Event>();
        }

        public void Add(float time, int number, int decim, int speed, int line)
        {
            events.Add(new Event(time, number, decim, speed, line));
        }

        public void Add(float time, string text)
        {
            events.Add(new Event(time, text));
        }

        public Event GetNextEvent()
        {
            if (LevelStorage.currentEvent == events.Count)
                return new Event(0, ""); // No more events
            LevelStorage.currentEvent += 1;
            return events[LevelStorage.currentEvent];
        }
    }

    public struct Event
    {
        public float time;
        public string text;
        public int number;
        public int decim;
        public int speed;
        public int line;

        public Event(float time, int number, int decim, int speed, int line)
        {
            this.time = time;
            this.number = number;
            this.decim = decim;
            this.speed = speed;
            this.line = line;
            this.text = "";
        }

        public Event(float time, string text)
        {
            this.time = time;
            this.text = text;
            this.number = this.decim = this.speed = this.line = 0;
        }
    }
}

