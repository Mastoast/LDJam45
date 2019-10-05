using System;
using System.Collections.Generic;

namespace LDJam45
{
    public static class LevelStorage
    {
        public static List<Level> levels;
        public static bool generated = false;

        public static void GenerateLevels()
        {
            generated = true;

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
            if (levels.Count == 0)
                return new Level(""); // No more events
            Level nextLvl = levels[0];
            levels.RemoveAt(0);
            return nextLvl;
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

        public Event GetNextEvent()
        {
            if (events.Count == 0)
                return new Event(0, ""); // No more events
            Event nextEvent = events[0];
            events.RemoveAt(0);
            return nextEvent;
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

