using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS
{
    class TitleScene : asd.Scene
    {
        #region オブジェクト回り

        private Dictionary<string, asd.TextureObject2D> ObjectDict;

        asd.Layer2D Layer = new asd.Layer2D();

        private void LoadImagePack()
        {
            var imgPack = Program.TitleUI;
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

        private void RegisterTextureObjects()
        {
            foreach (var obj in ObjectDict)
            {
                Layer.AddObject(obj.Value);
            }

            int priority = 0;
            SetPriority("haikei", true, ref priority);
            SetPriority("copyright", true, ref priority);
            SetPriority("tutorial", true, ref priority);
            SetPriority("playgame", true, ref priority);
        }

        #endregion

        public TitleScene()
        {
            LoadImagePack();
            RegisterTextureObjects();
            this.AddLayer(Layer);
        }

        protected override void OnUpdated()
        {
            // Kキーが押されるのを待つ
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.K) == asd.KeyState.Push)
            {
                asd.Engine.ChangeSceneWithTransition(new GameScene(false), new asd.TransitionFade(0.5f, 0.5f));
            }

            // Jキーが押されるのを待つ
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.J) == asd.KeyState.Push)
            {
                asd.Engine.ChangeSceneWithTransition(new GameScene(true), new asd.TransitionFade(0.5f, 0.5f));
            }

            // Lキーが押されるのを待つ
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.L) == asd.KeyState.Push)
            {
                asd.Engine.ChangeSceneWithTransition(new CopyRightScene(), new asd.TransitionFade(0.5f, 0.5f));
            }

        }

    }
}
