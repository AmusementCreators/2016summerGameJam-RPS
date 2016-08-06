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
        private Dictionary<string, asd.RectI> RectDict;

        asd.GeometryObject2D LightBox;

        asd.RectangleShape rect_l;

        asd.RectangleShape rect_r;

        asd.TextObject2D TextBox;

        asd.Layer2D Layer = new asd.Layer2D();

        asd.Layer2D Layer2 = new asd.Layer2D();

        private void LoadImagePack()
        {
            var imgPack = Program.GameUI;
            ObjectDict = new Dictionary<string, asd.TextureObject2D>();
            RectDict = new Dictionary<string, asd.RectI>();

            for (int i = 0; i < imgPack.ImageCount; i++)
            {
                var obj = new asd.TextureObject2D();
                obj.Texture = imgPack.GetImage(i);
                obj.Position = imgPack.GetImageArea(i).Position.To2DF();
                var name = imgPack.GetImageName(i);

                ObjectDict.Add(name, obj);
                RectDict.Add(name, imgPack.GetImageArea(i));
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

        private void RegisterObjects()
        {
            foreach (var obj in ObjectDict)
            {
                Layer.AddObject(obj.Value);
            }

            int priority = 0;
            SetPriority("background", true, ref priority);
            SetPriority2("characterbox", true, ref priority);
            InitializeCharLight(ref priority);
            SetPriority("timebox", true, ref priority);
            SetPriority("timebar", true, ref priority);
            SetPriority("spotlights", true, ref priority);
            LoadAnimations(ref priority);
            SetPriority2("bright", false, ref priority);
            SetPriority2("dark", true, ref priority);
            SetPriority("key", true, ref priority);
            SetPriority("talkbox", false, ref priority);
            CreateTalkBox(ref priority);
            SetPriority2("whitelight", false, ref priority);
            SetPriority2("bluelight", false, ref priority);
            SetPriority2("redlight", false, ref priority);
            CreateLast(ref priority);
            string namae = "";
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

        private void UpdateCharLight(bool winFlag)
        {
            if (winFlag) LightBox.Shape = rect_l;
            else LightBox.Shape = rect_r;
        }

        private void InitializeCharLight(ref int priority)
        {
            LightBox = new asd.GeometryObject2D();
            LightBox.DrawingPriority = priority++;
            Layer.AddObject(LightBox);

            rect_l = new asd.RectangleShape();
            rect_l.DrawingArea = new asd.RectF(ObjectDict["characterbox_l"].Position.X, ObjectDict["characterbox_l"].Position.Y, RectDict["characterbox_l"].Size.X, RectDict["characterbox_l"].Size.Y);
            rect_r = new asd.RectangleShape();
            rect_r.DrawingArea = new asd.RectF(ObjectDict["characterbox_r"].Position.X, ObjectDict["characterbox_r"].Position.Y, RectDict["characterbox_r"].Size.X, RectDict["characterbox_r"].Size.Y);
        }

        private void CreateTalkBox(ref int priority)
        {
            TextBox = new asd.TextObject2D();
            TextBox.Position = ObjectDict["talkbox"].Position + new asd.Vector2DF(20, 20);
            TextBox.Font = asd.Engine.Graphics.CreateDynamicFont("", 25, new asd.Color(255, 255, 255), 0, new asd.Color(0, 0, 0));
            TextBox.DrawingPriority = priority++;
            Layer.AddObject(TextBox);
        }

        private void UpdateTalkBox(bool isDrawn, asd.Color color, string text)
        {
            ObjectDict["talkbox"].IsDrawn = isDrawn;
            TextBox.IsDrawn = isDrawn;
            TextBox.Color = color;
            TextBox.Text = text;
        }

            private void CreateLast(ref int priority)
        {
            asd.TextureObject2D obj = new asd.TextureObject2D();
            obj.DrawingPriority = priority++;
            string namae = "";
            asd.Texture2D texture = asd.Engine.Graphics.CreateTexture2D("Resources/"+ namae + " _win.png");
            obj.Texture = texture;
            obj.CenterPosition = new asd.Vector2DF(640, 360);
            Layer.AddObject(obj);
        }

        private void UpdateLast(bool Finish)
        {
            string namae = "";
            if (true) namae = "nicora";
            else namae = "tesra";
        }

        private void LoadAnimations(ref int priority)
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
                    obj.DrawingPriority = priority++;
                    var scale = new asd.Vector2DF();
                    scale.X = (float)ObjectDict["characterbox_l"].Texture.Size.X / obj.Texture.Size.X;
                    scale.Y = (float)ObjectDict["characterbox_l"].Texture.Size.Y / obj.Texture.Size.Y;
                    obj.Scale = scale;
                }
                else
                {
                    obj.Position = ObjectDict["characterbox_r"].Position;
                    var scale = new asd.Vector2DF();
                    obj.DrawingPriority = priority++;
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

        private IController Ctrl;

        public GameScene(bool isTutorial)
        {
            LoadImagePack();
            RegisterObjects();
            FillTimeBar();
            this.AddLayer(Layer);

            if (!isTutorial)
                Ctrl = new GameController(UpdateTimeBar, UpdateButton, UpdateCharLight, UpdateLights,UpdateLast);
            else
                Ctrl = new TutorialController(UpdateTimeBar, UpdateButton, UpdateLights, UpdateTalkBox);
        }

        protected override void OnUpdated()
        {
            Ctrl.OnUpdate();
        }
    }
}
