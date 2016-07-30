using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS
{

    class GameScene : asd.Scene
    {
        #region オブジェクト回り

        private Dictionary<string, asd.TextureObject2D> ObjectDict;
        private asd.Keys[] SelectKeys = { asd.Keys.A, asd.Keys.S, asd.Keys.D, asd.Keys.J, asd.Keys.K, asd.Keys.L };

        asd.Layer2D Layer = new asd.Layer2D();

        private void LoadImagePack()
        {
            var imgPack = asd.Engine.Graphics.CreateImagePackage(@"Resources\Game.aip");
            ObjectDict = new Dictionary<string, asd.TextureObject2D>();
            for (int i = 0; i < imgPack.ImageCount; i++)
            {
                var obj = new asd.TextureObject2D();
                obj.Texture = imgPack.GetImage(i);
                obj.Position = imgPack.GetImageArea(i).Position.To2DF();
                var name = imgPack.GetImageName(i);

                ObjectDict.Add(name, obj);
            }
        }

        private void SetPriority(string key, bool isDrawn, ref int priority)
        {
            ObjectDict[key].IsDrawn = isDrawn;
            ObjectDict[key].DrawingPriority = priority++;
        }

        private void SetPriority2(string prefix, bool isDrawn, ref int priority)
        {
            foreach (var x in ObjectDict.Where(y => y.Key.Contains(prefix)))
            {
                x.Value.IsDrawn = isDrawn;
                x.Value.DrawingPriority = priority++;
            }
        }

        private void RegisterTextureObjects()
        {
            foreach (var obj in ObjectDict)
            {
                Layer.AddObject(obj.Value);
            }

            int priority = 0;
            SetPriority("background", true, ref priority);
            SetPriority("Objects", true, ref priority);
            SetPriority("charactorbox", true, ref priority);
            SetPriority("timebox", true, ref priority);
            SetPriority("timebar", true, ref priority);//複製するやつか？！
            SetPriority("Objects", true, ref priority);
            SetPriority2("button", false, ref priority);
            SetPriority2("pushbutton", true, ref priority);
            SetPriority("key", true, ref priority);
            SetPriority("talkbox", false, ref priority);
            SetPriority2("_light", false, ref priority);
            SetPriority2("bluelight", false, ref priority);
            SetPriority2("redlight", false, ref priority);
        }

        private void FillTimeBar()
        {
            var source = ObjectDict["timebar"];
            var texture = source.Texture;
            var position = source.Position - new asd.Vector2DF(0, 8);
            var increment = texture.Size.Y;
            var priority = source.DrawingPriority;
            for (int i = 0; i < 15; i++)
            {
                var obj = new asd.TextureObject2D();
                obj.Texture = texture;
                obj.Position = position;
                obj.DrawingPriority = priority;
                obj.IsDrawn = false;
                ObjectDict.Add("timebar" + i, obj);
                Layer.AddObject(obj);

                position.Y += increment;
            }
            Layer.RemoveObject(ObjectDict["timebar"]);
            ObjectDict.Remove("timebar");
        }

        private void UpdateTimeBar(int elapsed)
        {
            for (int i = 0; i < elapsed; i++)
            {
                ObjectDict["timebar" + i].IsDrawn = true;
            }
            for (int i = elapsed; i < 15; i++)
            {
                ObjectDict["timebar" + i].IsDrawn = false;
            }
        }

        private void UpdateButton(char button, bool isLighting)
        {
            ObjectDict["button" + button].IsDrawn = isLighting;
            ObjectDict["pushbutton" + button].IsDrawn = !isLighting;
        }

        private void UpdateLights(string prefix, bool isDrawn)
        {
            foreach (var x in ObjectDict.Where(y => y.Key.Contains(prefix)))
            {
                x.Value.IsDrawn = isDrawn;
            }
        }

        #endregion

        private int WinFlag = -1;
        private int WinCount = 0;
        private int[] Select = { -1, -1 };
        int TimeCount = 0;
        Action CurrentPhase;

        public GameScene()
        {
            LoadImagePack();
            RegisterTextureObjects();
            FillTimeBar();

            this.AddLayer(Layer);

            CurrentPhase = PhaseWaitSelect;
        }

        protected override void OnUpdated()
        {
            CurrentPhase();
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
                    break;
                case 1://右の勝ち
                    WinFlag = 1;
                    WinCount = 0;
                    break;
                case -1://あいこ
                    WinCount++;
                    if (WinFlag != -1 &&  WinCount!=3) UpdateLights("_light", true);
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
    }
}
