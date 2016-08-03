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
		private int WordCount = 0;
		public CharWord(Action<bool, asd.Color, string> updateTalkBox)
		{
			UpdateTalkBox = updateTalkBox;

		}

		public bool Story()
		{
			var Black = new asd.Color(0, 0, 0);
			var Blue = new asd.Color(0, 0, 255);
			var Red = new asd.Color(255, 0, 0);

            var Color = new[]
            {
                Black,Red,Blue,Red,Blue,Red,Blue,Red,Blue,Red,Blue,Blue,Black,Red,Blue,Black,Red,Black,Blue
            };

            string[] Text = new string[]
            {
                "スペースキーを押してください",
                "ちょっと、二コラ！これはどういうことなの！？",
                "はいはいはーい！\nテスラ姉さん、いったいどうしました？",
                "建物中のライトが全然点かないじゃない！\nまたなんかしたでしょう！",
                "そんな、人を悪者みたいに！悪いことはなにもしてないよ！\n……好奇心でちょっと回路をいじっただけ",
                "してるじゃない！まったくもう、早く直してちょうだい\nこれじゃなにも見えないわ！",
                "もちろん、直すための手は打ってあるよ！\n明日のお昼に部品が届く予定！",
                "明日の昼って……今夜はどうするのよ！\nあと、さっきから話す時だけライトが点くのはなぜ？",
                "頑張ったんだけど、どちらかのライトしか点かなくなっちゃったの",
                "あらそう、じゃあ私の部屋だけ点けてちょうだい\n今夜は観たいドラマがあるの",
                "ダメ！！今夜は観たいアニメがあるんだもん！\nね、じゃんけんしよう？",
                "って、なると思ったから、スイッチをグー・チョキ・パーにしてみました！\n勝った方の部屋しかライトは点きません！",
                "真ん中に表示される制限時間内に、出したい手のスイッチを押してください。",
                "はあ！？……付き合っていられないわ\n仕方がない、スポットライトは借りるから",
                "ざーんねーんでしたー！スポットライトもお部屋の回路と繋がっています！\nじゃんけんに勝った状態であいこにならないと点きません！",
                "あいこになると、スポットライトが勝っているほうの色に光ります。\nしかし、連続であいこにならないと消えてしまいます。",
                "なによ、それ！もう頭にきた！絶対にすべてのライトを点けてやるわ\n二コラの部屋以外！",
                "３回連続であいこを出し、相手以外のライトを点けたほうの勝利となります。",
                "ふふーん、臨むところ！"
        };

            UpdateTalkBox(true, Color[WordCount], Text[WordCount]);
            WordCount = WordCount + 1;
			if (WordCount == 20) return true;
			return false;
		}
	}
}
