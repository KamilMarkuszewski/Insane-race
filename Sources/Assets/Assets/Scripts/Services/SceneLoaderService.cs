using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


	public class SceneLoaderService
	{
		public enum Scene { None, scGameMenu, scCreateMatch, scPlay, scPoints, scMapEditor };
		public static Scene current = Scene.scGameMenu;
		
		private static string getSceneName(Scene sc)
		{
			switch (sc)
			{
				case Scene.scGameMenu:
					return "scMainMenu";
					
				case Scene.scCreateMatch:
					return "";
					
				case Scene.scPlay:
					return "scDev";
					
				case Scene.scPoints:
					return "";
					
				case Scene.scMapEditor:
					return "";
					
				default:
					throw new NotImplementedException();
			}
		}
		
		public static void changeScene(Scene sc)
		{
			current = sc;
			Application.LoadLevel(getSceneName(sc));
		}
		
		
	}

