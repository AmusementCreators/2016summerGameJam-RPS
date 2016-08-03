using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS
{
    class CharWord
    {
        private event Action<bool, asd.Color, string> UpdateTalkBox;
        private void Story()
        {
            int WordCount = 0;

            var Black = new asd.Color(0,0,0);
            var Blue = new asd.Color(0, 255, 0);
            var Red = new asd.Color(255, 0, 0);

            switch (WordCount)
            {
                case 0:
                    UpdateTalkBox(true, Black ,"スペースキーを押してください");
                    break;
                case 1:
                    UpdateTalkBox(true,Red,"ちょっと、二コラ！これはどういうことなの！？");
                    break;
                case 2:
                    UpdateTalkBox(true,Blue,"はいはいはーい！\nテスラ姉さん、いったいどうしました？");
                    break;
                case 3:
                    UpdateTalkBox(true,Red,"建物中のライトが全然点かないじゃない！\nまたなんかしたでしょう！");
                    break;
                case 4:
                    UpdateTalkBox(true,Blue,"そんな、人を悪者みたいに！悪いことはなにもしてないよ！\n……好奇心でちょっと回路をいじっただけ");
                    break;
                case 5:
                    UpdateTalkBox(true,Red,"してるじゃない！まったくもう、早く直してちょうだい\nこれじゃなにも見えないわ！");
                    break;
                case 6:
                    UpdateTalkBox(true,Blue,"もちろん、直すための手は打ってあるよ！\n明日のお昼に部品が届く予定！");
                    break;
                case 7:
                    UpdateTalkBox(true,Red,"明日の昼って……今夜はどうするのよ！\nあと、さっきから話す時だけライトが点くのはなぜ？");
                    break;
                case 8:
                    UpdateTalkBox(true,Blue,"頑張ったんだけど、どちらかのライトしか点かなくなっちゃったの");
                    break;
                case 9:
                    UpdateTalkBox(true,Red,"あらそう、じゃあ私の部屋だけ点けてちょうだい\n今夜は観たいドラマがあるの");
                    break;
                case 10:
                    UpdateTalkBox(true,Blue,"ダメ！！今夜は観たいアニメがあるんだもん！\nね、じゃんけんしよう？");
                    break;
                case 11:
                    UpdateTalkBox(true,Blue,"って、なると思ったから、スイッチをグー・チョキ・パーにしてみました！\n勝った方の部屋しかライトは点きません！");
                    break;
                case 12:
                    UpdateTalkBox(true,Black,"真ん中に表示される制限時間内に、出したい手のスイッチを押してください。");
                    break;
                case 13:
                    UpdateTalkBox(true,Red,"はあ！？……付き合っていられないわ\n仕方がない、スポットライトは借りるから");
                    break;
                case 14:
                    UpdateTalkBox(true,Blue,"ざーんねーんでしたー！スポットライトもお部屋の回路と繋がっています！\nじゃんけんに勝った状態であいこにならないと点きません！");
                    break;
                case 15:
                    UpdateTalkBox(true,Black,"あいこになると、スポットライトが勝っているほうの色に光ります。\nしかし、連続であいこにならないと消えてしまいます。");
                    break;
                case 16:
                    UpdateTalkBox(true,Red,"なによ、それ！もう頭にきた！絶対にすべてのライトを点けてやるわ\n二コラの部屋以外！");
                    break;
                case 17:
                    UpdateTalkBox(true,Black,"３回連続であいこを出し、相手以外のライトを点けたほうの勝利となります。");
                    break;
                case 18:
                    UpdateTalkBox(true,Blue,"ふふーん、臨むところ！");
                    break;

            }

            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Push)
            {
                WordCount = WordCount + 1;
            }
        }
    }
}
