using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS
{
    class Tutorial : asd.Scene
    {
        private void Story()
        {
         int WordCount = 0;
            var nicoraWord = new asd.TextObject2D();
            var tesraWord = new asd.TextObject2D();
            var Word = new asd.TextObject2D();

         switch (WordCount)
            {
                case 0:
                    Word.Text = "スペースキーを押してください";
                    break;
                case 1:
                    tesraWord.Text = "ちょっと、二コラ！これはどういうことなの！？";
                    break;
                case 2:
                    nicoraWord.Text = "はいはいはーい！\nテスラ姉さん、いったいどうしました？";
                    break;
                case 3:
                    tesraWord.Text = "建物中のライトが全然点かないじゃない！\nまたなんかしたでしょう！";
                    break;
                case 4:
                    nicoraWord.Text = "そんな、人を悪者みたいに！悪いことはなにもしてないよ！\n……好奇心でちょっと回路をいじっただけ";
                    break;
                case 5:
                    tesraWord.Text = "してるじゃない！まったくもう、早く直してちょうだい\nこれじゃなにも見えないわ！";
                    break;
                case 6:
                    nicoraWord.Text = "もちろん、直すための手は打ってあるよ！\n明日のお昼に部品が届く予定！";
                    break;
                case 7:
                    tesraWord.Text = "明日の昼って……今夜はどうするのよ！\nあと、さっきから話す時だけライトが点くのはなぜ？";
                    break;
                case 8:
                    nicoraWord.Text = "頑張ったんだけど、どちらかのライトしか点かなくなっちゃったの";
                    break;
                case 9:
                    tesraWord.Text = "あらそう、じゃあ私の部屋だけ点けてちょうだい\n今夜は観たいドラマがあるの";
                    break;
                case 10:
                    nicoraWord.Text = "ダメ！！今夜は観たいアニメがあるんだもん！\nね、じゃんけんしよう？";
                    break;
                case 11:
                    nicoraWord.Text = "って、なると思ったから、スイッチをグー・チョキ・パーにしてみました！\n勝った方の部屋しかライトは点きません！";
                    break;
                case 12:
                    Word.Text = "真ん中に表示される制限時間内に、出したい手のスイッチを押してください。";
                    break;
                case 13:
                    tesraWord.Text = "はあ！？……付き合っていられないわ\n仕方がない、スポットライトは借りるわね";
                    break;
                case 14:
                    nicoraWord.Text = "ざーんねーんでしたー！スポットライトもお部屋の回路と繋がっています！\nじゃんけんに勝った状態であいこにならないと点きません！";
                    break;
                case 15:
                    Word.Text = "あいこになると、スポットライトが勝っているほうの色に光ります。\nしかし、連続であいこにならないと消えてしまいます。";
                    break;
                case 16:
                    tesraWord.Text = "なによ、それ！もう頭にきた！絶対にすべてのライトを点けてやるわ\n二コラの部屋以外！";
                    break;
                case 17:
                    Word.Text = "３回連続であいこを出し、相手以外のライトを点けたほうの勝利となります。";
                    break;
                case 18:
                    nicoraWord.Text = "ふふーん、臨むところ！";
                    break;

            }

            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Push)
            {
                asd.Engine.AddObject2D(nicoraWord);
                asd.Engine.AddObject2D(tesraWord);
                asd.Engine.AddObject2D(Word);
                WordCount = WordCount + 1;
            }
    }
        


    }
}
