using UnityEngine;
using System.Collections;
using System;
using HotWheels.GUI;

public class MainMenuView : MonoBehaviour {


	
	#region Fields

	private MainMenuViewModel model = new MainMenuViewModel();
	public GUISkin mySkin;	


	#endregion

	#region Funkcje MonoBehaviour

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#endregion

	void DoMyWindow(int windowID)
	{
		model.AddSpikes(model.width);
		GUILayout.BeginVertical();
		GUILayout.Label(model.menuTitle);
		switch (model.currentMenu)
		{
		case MainMenuEnum.None:
			break;
			
		case MainMenuEnum.Main:
			//model.FancyTop(model.width);
			mainMenu();
			break;
			
		case MainMenuEnum.Play:
			playMenu();
			break;
			
		case MainMenuEnum.MapEditor:
			mapEditorMenu();
			break;
			
		case MainMenuEnum.Settings:
			settingsMenu();
			break;
			
		case MainMenuEnum.About:
			aboutMenu();
			//model.WaxSeal(model.width,model.height);
			break;
		}

		GUILayout.EndVertical();
	}

	void OnGUI()
	{
		GUI.skin = mySkin;
		model.tekstStyle = new GUIStyle(GUI.skin.box);

		//GUI.color = Color.green;

		reCalcVars();
		Rect Box1 = new Rect(model.boxX, model.boxY, model.width, model.height);
		GUI.Window(0, Box1, DoMyWindow, String.Empty); //, model.tekstStyle


		//GUILayout.BeginVertical();
	

		//GUILayout.BeginVertical();
		//GUILayout.Space(8);
		//GUILayout.Label("", "Divider");//-------------------------------- custom
		//GUILayout.Label("Standard Label");


		//GUILayout.EndVertical();

		
	}

	#region Funkcje Menu

	private void mainMenu()
	{
		if (GUILayout.Button(MainMenuResX.ButtonPlay))
		{
			model.currentMenu = MainMenuEnum.Play;
		}
		if (GUILayout.Button(MainMenuResX.ButtonMapEditor))
		{
			model.currentMenu = MainMenuEnum.MapEditor;
		}
		if (GUILayout.Button(MainMenuResX.ButtonSettings))
		{
			model.currentMenu = MainMenuEnum.Settings;
		}
		if (GUILayout.Button(MainMenuResX.ButtonsAbout))
		{
			model.currentMenu = MainMenuEnum.About;
		}
		GUILayout.Space(10);
		if (addButtonBottom(MainMenuResX.ButtonsExit))
		{
			model.currentMenu = MainMenuEnum.None;
		}
	}

	private void aboutMenu()
	{
		GUILayout.Box(MainMenuResX.LabelAbout);
		if (addButtonBottom(MainMenuResX.ButtonsExit))
		{
			model.currentMenu = MainMenuEnum.Main;
		}
	}
	
	private void settingsMenu()
	{
        Settings.backCameraOn = GUILayout.Toggle(Settings.backCameraOn, "Lusterko");
        Settings.miniMapOn = GUILayout.Toggle(Settings.miniMapOn, "Mini mapa");


		if (addButtonBottom(MainMenuResX.ButtonsExit))
		{
			model.currentMenu = MainMenuEnum.Main;
		}
	}

	private void mapEditorMenu()
	{
		if (GUILayout.Button(MainMenuResX.ButtonNewMap))
		{
			throw new NotImplementedException();
		}
		if (GUILayout.Button(MainMenuResX.ButtonLoadMap))
		{
			throw new NotImplementedException();
		}
		if (addButtonBottom(MainMenuResX.ButtonsExit))
		{
			model.currentMenu = MainMenuEnum.Main;
		}
	}
	
	private void playMenu()
	{
		if (GUILayout.Button(MainMenuResX.ButtonDevelopement))
		{
			SceneLoaderService.changeScene(SceneLoaderService.Scene.scPlay);
		}
		if (GUILayout.Button(MainMenuResX.ButtonSinglePlayer))
		{

		}

		if (addButtonBottom(MainMenuResX.ButtonsExit))
		{
			model.currentMenu = MainMenuEnum.Main;
		}
	}

	
	#endregion	
	
	
	#region Funkcje prywatne
	private void reCalcVars()
	{
		model.buttonX = 0;
		model.buttonY = 0;
		model.boxX = Screen.width / 2 - 300;
		model.boxY = Screen.height / 2 - 200;
		model.width = 600;
		model.height = 500;
		switch (model.currentMenu)
		{
			case MainMenuEnum.None:
				break;
				
			case MainMenuEnum.Main:
			model.menuTitle = MainMenuResX.MenuTitleMain;
				break;
				
			case MainMenuEnum.Play:
			model.menuTitle = MainMenuResX.ButtonPlay;
				break;
				
			case MainMenuEnum.MapEditor:
			model.menuTitle = MainMenuResX.MenuTitleMapEditor;
				break;
				
			case MainMenuEnum.Settings:
			model.menuTitle = MainMenuResX.MenuTitleSettings;
				break;
				
			case MainMenuEnum.About:
			model.menuTitle = MainMenuResX.MenuTitleAbout;
				break;
		}
	}
	
	private bool addButtonBottom(string text)
	{
		model.buttonY = 0;
		model.buttonY += model.height - 50 - 10 - 25 -50 ;
		return addButton(text);
	}
	
	private bool addButton(string text)
	{
		model.buttonY += 10 + 25;
		Rect position = new Rect(model.width / 2 - 100, model.buttonY, 200, 25);
		return GUI.Button(position, text);
	}

	
	#endregion
}
