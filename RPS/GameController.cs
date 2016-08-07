using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS
{
    class GameController : IController
    {
        private asd.Keys[] SelectKeys = { asd.Keys.A, asd.Keys.S, asd.Keys.D, asd.Keys.J, asd.Keys.K, asd.Keys.L };

        private Action CurrentPhase;
        private int WinFlag = -1;
        private int WinCount = 0;
        private int[] Select = { -1, -1 };
        private int TimeCount = 0;

        private event Action<int> UpdateTimeBar;
        private event Action<char, bool> UpdateButton;
        private event Action<bool> UpdateCharLight;
        private event Action<string, bool> UpdateLights;
        private event Action<bool> UpdateLast;
        private event Action<string, string> UpdateCharAnimation;


        public GameController(Action<int> updateTimeBar, Action<char, bool> updateButton, Action<bool> updateCharLight, Action<string, bool> updateLights, Action<bool> updateLast, Action<string, string> updateCharAnimation)
        {
            CurrentPhase = PhaseWaitSelect;
            UpdateTimeBar = updateTimeBar;
            UpdateButton = updateButton;
            UpdateCharLight = updateCharLight;
            UpdateLights = updateLights;
            UpdateLast = updateLast;
            UpdateCharAnimation = updateCharAnimation;
        }

        private void PhaseWaitSelect()
        {
            TimeCount++;
            if (TimeCount > 225)
            {
                CurrentPhase = PhaseResult;
                ShiftToResult();
                return;
            }
            if (TimeCount % 15 == 0) UpdateTimeBar(TimeCount / 15);

            for (int i = 0; i < 6; i++)
            {
                if (asd.Engine.Keyboard.GetKeyState(SelectKeys[i]) == asd.KeyState.Push) Select[i / 3] = i % 3;
            }
        }

        private void ShiftToResult()
        {
            UpdateButton('A', Select[0] == 0);
            UpdateButton('S', Select[0] == 1);
            UpdateButton('D', Select[0] == 2);
            UpdateButton('J', Select[1] == 0);
            UpdateButton('K', Select[1] == 1);
            UpdateButton('L', Select[1] == 2);

            int[,] table = new int[3, 3] { { 1, 0, -1 }, { 0, -1, 1 }, { -1, 1, 0 } };//勝敗テーブル
            switch (table[Select[0], Select[1]])
            {
                case 0://左の勝ち
                    WinFlag = 0;
                    WinCount = 0;
                    UpdateCharLight(true);
                    break;
                case 1://右の勝ち
                    WinFlag = 1;
                    WinCount = 0;
                    UpdateCharLight(false);
                    break;
                case -1://あいこ
                    WinCount++;
                    if (WinFlag != -1 && WinCount != 3) UpdateLights("_light", true);
                    break;
            }

            if (WinFlag == 0) UpdateCharLight(true);
            else if (WinFlag == 1) UpdateCharLight(false);

            if (WinCount == 3)
            {
                if (WinFlag == 0)
                {
                    UpdateLights("bluelight", true);
                    UpdateLast(true);
                }
                else if (WinFlag == 1)
                {
                    UpdateLights("redlight", true);
                    UpdateLast(false);
                }
            }
        }

        private void PhaseResult()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Enter) == asd.KeyState.Release)
            {
                CurrentPhase = PhaseWaitSelect;
                ShiftToWaitSelect();
            }
        }

        private void ShiftToWaitSelect()
        {
            UpdateLights("_light", false);
            UpdateLights("redlight", false);
            UpdateLights("bluelight", false);
            TimeCount = 0;
        }

        public void OnUpdate()
        {
            CurrentPhase();
        }

    }
}
