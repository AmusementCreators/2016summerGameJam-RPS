using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS
{
    class CopyRightScene : asd.Scene
    {
        asd.Layer2D layer = null;

        private void Copyright()
        {
            // フォントを生成する。
            var font = asd.Engine.Graphics.CreateDynamicFont("", 24, new asd.Color(255, 255, 255), 0, new asd.Color(0, 0, 0));

            // 文字描画オブジェクトを生成する。
            var obj = new asd.TextObject2D();

            // 描画に使うフォントを設定する。
            obj.Font = font;

            // 描画位置を指定する。
            obj.Position = new asd.Vector2DF(200, 72);

            // 描画する文字列を指定する。
            obj.Text = " --spotlight image-- \n Freepik(http://jp.freepik.com/free-vector/ベクター画像_780623.htm) \n \n --background image-- \n Design by MysticEmma(http://zooll.com/goodies-pastel-triangle-patterns/) \n \n --animation-- \n Live2D(http://www.live2d.com/ja/) \n \n --game engine-- \n Altseeed(http://altseed.github.io/) \n \n This game is created by wisteria, KengPeng. \n © 2016 AmusementCreators \n \n \n PUSH　SPACE-key TO BACK";

            layer = new asd.Layer2D();
            AddLayer(layer);

            // 文字描画オブジェクトのインスタンスをエンジンへ追加する。
            layer.AddObject(obj);
        }

        public CopyRightScene()
        {
            Copyright();
        }
        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Push)
            {
                asd.Engine.ChangeSceneWithTransition(new TitleScene(), new asd.TransitionFade(0.5f, 0.5f));
            }
        }
    }
}
