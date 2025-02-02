﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RPS
{
	static class Program
	{
		public static string Title = "ライトニングじゃんけん！";
		public static asd.ImagePackage TitleUI, GameUI;

		[System.STAThread]
		static void Main(string[] args)
		{
			// Altseedを初期化する。
			asd.Engine.Initialize(Title, 1280, 720, new asd.EngineOption());

			TitleUI = asd.Engine.Graphics.CreateImagePackage(@"Resources\GameTitle.aip");
			GameUI = asd.Engine.Graphics.CreateImagePackage(@"Resources\Game.aip");

			asd.Engine.ChangeScene(new TitleScene());

			// Altseedのウインドウが閉じられていないか確認する。
			while (asd.Engine.DoEvents())
			{
				// Altseedを更新する。
				asd.Engine.Update();
			}

			// Altseedの終了処理をする。
			asd.Engine.Terminate();
		}
	}

	interface IController
	{
		void OnUpdate();
	}
}
