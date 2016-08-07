using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS
{
	class TutorialController : IController
	{
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

		public void OnUpdate()
		{
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Release)
            {
                if (Words.Story()) asd.Engine.ChangeSceneWithTransition(new TitleScene(),new asd.TransitionFade(0.5f, 0.5f));
            }
        }

	}
}
