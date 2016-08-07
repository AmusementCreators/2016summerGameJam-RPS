using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS
{
	class TutorialController : IController
	{
<<<<<<< HEAD
=======
		private asd.Keys[] SelectKeys = { asd.Keys.A, asd.Keys.S, asd.Keys.D, asd.Keys.J, asd.Keys.K, asd.Keys.L };

		private Action CurrentPhase;
		private int WinFlag = -1;
		private int WinCount = 0;
		private int[] Select = { 3, 3 };
		private int TimeCount = 0;

		private event Action<int> UpdateTimeBar;
		private event Action<char, bool> UpdateButton;
		private event Action<string, bool> UpdateLights;
>>>>>>> e33cbd3f3595fc811ab997ee8c92924d8c6adbc0
		private event Action<bool, asd.Color, string> UpdateTalkBox;
        private event Action<bool> UpdateCharLight;
        private event Action<string, string> UpdateCharAnimation;
        private CharWord Words;

		public TutorialController(Action<bool, asd.Color, string> updateTalkBox, Action<bool> updateCharLight,Action<string, string> updateCharAnimation)
		{
			UpdateTalkBox = updateTalkBox;
            UpdateCharLight = updateCharLight;
			UpdateCharAnimation = updateCharAnimation;

			Words = new CharWord(UpdateTalkBox,UpdateCharLight, UpdateCharAnimation);
			Words.Story();
		}

<<<<<<< HEAD
=======
		private void PhaseStoryMode()
		{
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Release)
			{
				if (Words.Story()) CurrentPhase = PhaseWaitSelect;
			}
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

			int[,] table = new int[4, 4] { { 1, 0, -1, 0 }, { 0, -1, 1, 0 }, { -1, 1, 0, 0 }, { 1, 1, 1, -2 } };//勝敗テーブル
			switch (table[Select[0], Select[1]])
			{
				case 0://左の勝ち
					WinFlag = 0;
					WinCount = 0;
					break;
				case 1://右の勝ち
					WinFlag = 1;
					WinCount = 0;
					break;
				case -1://あいこ
					WinCount++;
					if (WinFlag != -1 && WinCount != 3) UpdateLights("_light", true);
					break;
			}

			if (WinCount == 3)
			{
				if (WinFlag == 0) UpdateLights("redlight", true);
				else if (WinFlag == 1) UpdateLights("bluelight", true);
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




>>>>>>> e33cbd3f3595fc811ab997ee8c92924d8c6adbc0
		public void OnUpdate()
		{
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Release)
            {
                if (Words.Story()) asd.Engine.ChangeSceneWithTransition(new TitleScene(),new asd.TransitionFade(0.5f, 0.5f));
            }
        }

	}
}
