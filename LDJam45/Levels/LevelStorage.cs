using System;
using System.Collections.Generic;

namespace LDJam45
{
    public static class LevelStorage
    {
        public static List<Level> levels;
        public static int currentLevel = 3;
        public static int currentEvent = -1;
        public static bool generated = false;

        public static void GenerateLevels()
        {
            generated = true;

            // Init
            levels = new List<Level>();
            // time | number | decimal | speed | line

            // Tuto : COMMANDER
            Level tuto = CreateLevel("COMMANDER");
            tuto.Add(0, " Welcome to the army soldiers !\n(Press the space key to continue)");
            tuto.Add(0, "Today you will fight our \n worst enemy : Numbers");
            tuto.Add(0, "We, Letters, should not let \n        them pass !");
            tuto.Add(0, "Each of you will help to stop them");
            tuto.Add(0, "Let's start with NOTHING");

            // Level 1 : NOTHING
            Level nothing = CreateLevel("NOTHING");
            nothing.Add(0, "Press the LETTER KEYS \n to shoot with your letters");
            nothing.Add(0, "Don't let numbers get to you");
            nothing.Add(1, 1, 0, 100, 1);
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
            nothing.Add(0, "Be careful to not throw bullets\naway for nothing\nit may cost our energy");
            nothing.Add(0, "Okay, the next wave is comming fast, \n we need a shorter guy \n for the job");

            // Level 2 : IS
            Level est = CreateLevel("IS");
            est.Add(0, "Get ready !");
            est.Add(1f, 1, 0, 400, 1);
            est.Add(1f, 1, 0, 400, 2);
            est.Add(1.5f, 2, 0, 400, 1);
            est.Add(1.5f, 1, 0, 400, 2);
            est.Add(2f, 1, 0, 400, 1);
            est.Add(2f, 2, 0, 400, 2);
            est.Add(2.5f, 2, 0, 400, 1);
            est.Add(2.5f, 1, 0, 400, 2);
            est.Add(3f, 1, 0, 400, 1);
            est.Add(3f, 2, 0, 400, 2);
            est.Add(3f, 1, 0, 600, 1);
            est.Add(3f, 2, 0, 600, 2);
            est.Add(3.5f, 2, 0, 400, 1);
            est.Add(3.5f, 1, 0, 400, 2);
            est.Add(4f, 1, 0, 400, 1);
            est.Add(4f, 2, 0, 400, 2);
            est.Add(4.5f, 2, 0, 800, 1);
            est.Add(4.5f, 1, 0, 800, 2);
            est.Add(0, "Keep going !");
            est.Add(7f, 5, 0, 1000, 1);
            est.Add(7.8f, 7, 0, 1000, 1);
            est.Add(8.4f, 9, 0, 1000, 1);
            est.Add(9.1f, 3, 0, 1000, 2);
            est.Add(9.8f, 2, 0, 1000, 2);
            est.Add(10.5f, 6, 0, 1000, 1);
            est.Add(11.2f, 8, 0, 1000, 2);
            est.Add(12f, 8, 0, 1000, 1);

            est.Add(0, "Well done, next !");

            //Level 3 : SURE
            Level sure = CreateLevel("SURE");
            sure.Add(0, "the next wave looks stronger");
            sure.Add(0, "you will need more than one bullet for these");
            sure.Add(1, 1238, 0, 250, 2);
            sure.Add(2.5f, 1614, 0, 250, 3);
            sure.Add(4, 1974, 0, 250, 4);
            sure.Add(5.5f, 1617, 0, 250, 2);
            sure.Add(5.5f, 8491, 0, 250, 1);
            sure.Add(0, "oh no, there are decimals\n" +
                "    hidding among them   ");
            sure.Add(0, "watch for surprise attacks");
            sure.Add(7f, 91, 5, 250, 1);
            sure.Add(13f, 34, 8, 250, 2);
            sure.Add(17f, 5, 8768, 250, 3);
            //
            sure.Add(23f, 832, 8, 250, 1);
            sure.Add(25f, 75, 89, 250, 2);
            sure.Add(27f, 13, 87, 250, 3);
            sure.Add(29f, 1, 2, 250, 4);
            sure.Add(0, "Well done, next !");

            // Level 4 : 
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
            if (currentLevel == levels.Count -1)
                return new Level(""); // No more events;
            currentLevel += 1;
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
            if (LevelStorage.currentEvent == events.Count - 1)
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

