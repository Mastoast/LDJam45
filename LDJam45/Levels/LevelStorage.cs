using System;
using System.Collections.Generic;

namespace LDJam45
{
    public static class LevelStorage
    {
        public static List<Level> levels;
        // TODO change to 0
        public static int currentLevel = 0;
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
            tuto.Add(0, "Today you will fight our \n" +
                        "   worst enemy : Numbers  ");
            tuto.Add(0, "We, Letters, should not let \n" +
                        "        them pass !");
            tuto.Add(0, "Each of you will fight to stop them");
            tuto.Add(0, "Let's start with NOTHING");

            // Level 1 : NOTHING
            Level nothing = CreateLevel("NOTHING");
            nothing.Add(0, "  Press the LETTER KEYS  \n" +
                            "to shoot with your letters");
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
            nothing.Add(0, "Be careful to not throw bullets\n" +
                           "       away for nothing        \n" +
                           "    it may cost our energy    ");
            nothing.Add(0, "Okay, the next wave is comming fast");
            nothing.Add(0, "we need a shorter guy for the job");

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
            sure.Add(0, "    you will need more   \n" +
                "than one bullet for these");
            sure.Add(1, 1238, 0, 250, 2);
            sure.Add(2.5f, 1614, 0, 250, 3);
            sure.Add(4, 1974, 0, 250, 4);
            sure.Add(5.5f, 1617, 0, 250, 2);
            sure.Add(5.5f, 8491, 0, 250, 1);
            sure.Add(0, "oh no, there are decimals\n" +
                "    hiding among them   ");
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

            // Level 4 : VVVVVV
            Level vi = CreateLevel("VVVVVV");
            vi.Add(0, "Wait ! Who are you ?");
            vi.Add(1, 4687, 0, 350, 1);
            vi.Add(1, 6175, 0, 350, 2);
            vi.Add(1, 1238, 0, 350, 3);
            vi.Add(1, 6986, 0, 350, 4);
            vi.Add(1, 7618, 0, 350, 5);
            vi.Add(1, 3197, 0, 350, 6);
            vi.Add(0, "well, that's pretty impressive");
            vi.Add(0, "please continue");
            vi.Add(5, 4687, 0, 350, 1);
            vi.Add(5, 6175, 0, 350, 2);
            vi.Add(5, 1238, 0, 350, 3);
            vi.Add(5, 6986, 0, 350, 4);
            vi.Add(5, 7618, 0, 350, 5);
            vi.Add(5, 3197, 0, 350, 6);
            //
            vi.Add(7.5f, 4687, 0, 350, 1);
            vi.Add(7.5f, 6175, 0, 350, 2);
            vi.Add(7.5f, 1238, 0, 350, 3);
            vi.Add(7.5f, 6986, 0, 350, 4);
            vi.Add(7.5f, 7618, 0, 350, 5);
            vi.Add(7.5f, 3197, 0, 350, 6);
            //
            vi.Add(10, 36, 4, 350, 6);
            vi.Add(13, 7, 49, 350, 6);
            vi.Add(0, "ok stop now");
            vi.Add(0, " you will just kill yourself \n" +
                "  if you keep that up");
            vi.Add(0, "Next !");

            // Level 5 : EASY
            Level easy = CreateLevel("EASY");
            easy.Add(0, "GET READY !");
            //easy.Add(1, , , , 1);

            // Level 6 : DIFFICULT
            Level diff = CreateLevel("DIFFICULT");
            diff.Add(0, " ");

            // Level 7 : PI
            Level pi = CreateLevel("MATH");
            pi.Add(0, "The big one is coming");
            pi.Add(3, 3, 1415926535, 200, 1);
            pi.Add(0, "Wait, there are still some decimals");
            pi.Add(10, 0, 89, 315, 1);
            pi.Add(11, 0, 79, 300, 2);
            pi.Add(12, 0, 32, 315, 3);
            pi.Add(13, 0, 384, 300, 4);
            pi.Add(0, "What's the ...");
            pi.Add(15, 0, 62643, 315, 2);
            pi.Add(19, 0, 3832, 300, 1);
            pi.Add(0, "I think this was the last one");
            pi.Add(0, "Wait that's not");
            pi.Add(23, 0, 79502, 315, 1);
            pi.Add(27, 0, 8841, 315, 2);
            pi.Add(0, "It will stop one day");
            pi.Add(31, 0, 9716, 300, 3);
            pi.Add(31, 0, 93993, 300, 1);
            pi.Add(0, "Good job soldier that's a victory");
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

