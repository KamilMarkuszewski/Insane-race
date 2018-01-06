using UnityEngine;
using System.Collections;

public class MainMenuViewModel  {

	#region Fields

	
	public int boxX;
	public int boxY;
	public int width;
	public int height;
	public int buttonX;
	public int buttonY;
	public MainMenuEnum currentMenu = MainMenuEnum.Main;
	public string menuTitle;
	
	public GUIStyle tekstStyle;
	#endregion

	private int spikeCount;	
	private int leafOffset;
	private int frameOffset;
	private int skullOffset;
	private int RibbonOffsetX;
	private int FrameOffsetX;
	private int SkullOffsetX;
	private int RibbonOffsetY;
	private int FrameOffsetY;
	private int SkullOffsetY;	
	private int WSwaxOffsetX;
	private int WSwaxOffsetY;
	private int WSribbonOffsetX;
	private int WSribbonOffsetY;

	
	public void AddSpikes(int winX)
	{
		spikeCount = (int)(Mathf.Floor(winX - 152)/22);
		GUILayout.BeginHorizontal();
		GUILayout.Label ("", "SpikeLeft");//-------------------------------- custom
		for (int i = 0; i < spikeCount; i++)
		{
			GUILayout.Label ("", "SpikeMid");//-------------------------------- custom
		}
		GUILayout.Label ("", "SpikeRight");//-------------------------------- custom
		GUILayout.EndHorizontal();
	}

	public void FancyTop(int topX)
	{
		leafOffset = (topX/2)-64;
		frameOffset = (topX/2)-27;
		skullOffset = (topX/2)-20;
		GUI.Label(new Rect(leafOffset, 18, 0, 0), "", "GoldLeaf");//-------------------------------- custom	
		GUI.Label(new Rect(frameOffset, 3, 0, 0), "", "IconFrame");//-------------------------------- custom	
		GUI.Label(new Rect(skullOffset, 12, 0, 0), "", "Skull");//-------------------------------- custom	
	}

	public void  WaxSeal(int x,int y)
	{
		WSwaxOffsetX = x - 120;
		WSwaxOffsetY = y - 115;
		WSribbonOffsetX = x - 114;
		WSribbonOffsetY = y - 83;
		
		GUI.Label(new Rect(WSribbonOffsetX, WSribbonOffsetY, 0, 0), "", "RibbonBlue");//-------------------------------- custom	
		GUI.Label(new Rect(WSwaxOffsetX, WSwaxOffsetY, 0, 0), "", "WaxSeal");//-------------------------------- custom	
	}
	
	public void  DeathBadge(int x,int y)
	{
		RibbonOffsetX = x;
		FrameOffsetX = x+3;
		SkullOffsetX = x+10;
		RibbonOffsetY = y+22;
		FrameOffsetY = y;
		SkullOffsetY = y+9;
		
		GUI.Label(new Rect(RibbonOffsetX, RibbonOffsetY, 0, 0), "", "RibbonRed");//-------------------------------- custom	
		GUI.Label(new Rect(FrameOffsetX, FrameOffsetY, 0, 0), "", "IconFrame");//-------------------------------- custom	
		GUI.Label(new Rect(SkullOffsetX, SkullOffsetY, 0, 0), "", "Skull");//-------------------------------- custom	
	}

}

public enum MainMenuEnum { None, Main, Play, MapEditor, Settings, About };
