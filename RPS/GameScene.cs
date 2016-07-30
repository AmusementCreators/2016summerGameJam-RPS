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
            SetPriority2("characterbox", true, ref priority);
            SetPriority("timebox", true, ref priority);
            SetPriority("timebar", true, ref priority);//複製するやつか？！
            SetPriority("spotlights", true, ref priority);
            SetPriority2("bright", false, ref priority);
            SetPriority2("dark", true, ref priority);
            SetPriority("key", true, ref priority);
            SetPriority("talkbox", false, ref priority);
            SetPriority2("whitelight", false, ref priority);
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
            ObjectDict["bright" + button].IsDrawn = isLighting;
            ObjectDict["dark" + button].IsDrawn = !isLighting;
        }

        private void UpdateLights(string prefix, bool isDrawn)
        {
            foreach (var x in ObjectDict.Where(y => y.Key.Contains(prefix)))
            {
                x.Value.IsDrawn = isDrawn;
            }
        }

        private void LoadAnimations()
        {
            string[] list = { @"Resources\nicora_lose\", @"Resources\nicora_normal\", @"Resources\nicora_talk\", @"Resources\nicora_win\",
                @"Resources\tesra_lose\", @"Resources\tesra_normal\",@"Resources\tesra_talk\", @"Resources\tesra_win\" };

            foreach (var n in list)
            {
                var obj = new AnimationObject(n, 30, null, true);
                obj.TextureFilterType = asd.TextureFilterType.Linear;
                if (n.Contains("nicora"))
                {
                    obj.Position = ObjectDict["characterbox_l"].Position;
                    var scale = new asd.Vector2DF();
                    scale.X = (float)ObjectDict["characterbox_l"].Texture.Size.X / obj.Texture.Size.X;
                    scale.Y = (float)ObjectDict["characterbox_l"].Texture.Size.Y / obj.Texture.Size.Y;
                    obj.Scale = scale;
                }
                else
                {
                    obj.Position = ObjectDict["characterbox_r"].Position;
                    var scale = new asd.Vector2DF();
                    scale.X = (float)ObjectDict["characterbox_r"].Texture.Size.X / obj.Texture.Size.X;
                    scale.Y = (float)ObjectDict["characterbox_r"].Texture.Size.Y / obj.Texture.Size.Y;
                    obj.Scale = scale;
                }

                if (!n.Contains("normal")) obj.IsDrawn = false;
                obj.DrawingPriority = 100;
                ObjectDict.Add(n, obj);
                Layer.AddObject(obj);
            }
        }

        #endregion

        private GameController Controller;

        public GameScene()
        {
            LoadImagePack();
            RegisterTextureObjects();
            FillTimeBar();
            LoadAnimations();
            this.AddLayer(Layer);

            Controller = new GameController(UpdateTimeBar, UpdateButton, UpdateLights);
        }

        protected override void OnUpdated()
        {
            Controller.OnUpdate();
        }
    }
}
