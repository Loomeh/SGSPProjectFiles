using System;
using System.IO;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class SaveManager : MonoBehaviour
{
	// Token: 0x06000116 RID: 278 RVA: 0x00006E31 File Offset: 0x00005031
	public static void CheckSave()
	{
		if (!File.Exists(SaveManager.savePath))
		{
			SaveManager.ClearSave();
		}
		SaveManager.LoadSave();
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00006E4C File Offset: 0x0000504C
	public static void Save()
	{
		using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(SaveManager.savePath)))
		{
			binaryWriter.Write(Screen.width);
			binaryWriter.Write(Screen.height);
			binaryWriter.Write(Screen.fullScreen);
			binaryWriter.Write(PauseManager.musicVolume);
			binaryWriter.Write(PauseManager.soundvolume);
			binaryWriter.Write(MouseLook.mouseSensitivity);
			binaryWriter.Write(PlayerMovement.autoSprint);
			binaryWriter.Write(ObjectiveManager.currentObjective);
			binaryWriter.Write(PlayerMovement.money);
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00006EE8 File Offset: 0x000050E8
	public static void LoadSave()
	{
		using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(SaveManager.savePath)))
		{
			int width = binaryReader.ReadInt32();
			int height = binaryReader.ReadInt32();
			bool fullscreen = binaryReader.ReadBoolean();
			Screen.SetResolution(width, height, fullscreen);
			PauseManager.musicVolume = binaryReader.ReadSingle();
			PauseManager.soundvolume = binaryReader.ReadSingle();
			MouseLook.mouseSensitivity = binaryReader.ReadSingle();
			PlayerMovement.autoSprint = binaryReader.ReadBoolean();
			ObjectiveManager.currentObjective = binaryReader.ReadInt32();
			PlayerMovement.money = (float)binaryReader.ReadInt32();
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00006F80 File Offset: 0x00005180
	public static void ClearSave()
	{
		using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(SaveManager.savePath)))
		{
			binaryWriter.Write(Screen.width);
			binaryWriter.Write(Screen.height);
			binaryWriter.Write(Screen.fullScreen);
			binaryWriter.Write(PauseManager.musicVolume);
			binaryWriter.Write(PauseManager.soundvolume);
			binaryWriter.Write(MouseLook.mouseSensitivity);
			binaryWriter.Write(PlayerMovement.autoSprint);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
		}
		SaveManager.LoadSave();
	}

	// Token: 0x0400013F RID: 319
	public static string savePath = Application.persistentDataPath + "/save.bin";
}
