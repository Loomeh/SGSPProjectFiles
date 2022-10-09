using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class ObjectiveManager : MonoBehaviour
{
	// Token: 0x060000D4 RID: 212 RVA: 0x0000572B File Offset: 0x0000392B
	private void Awake()
	{
		ObjectiveManager.instance = this;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00005733 File Offset: 0x00003933
	public static ObjectiveManager Get()
	{
		return ObjectiveManager.instance;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000573C File Offset: 0x0000393C
	private void Start()
	{
		if (ObjectiveManager.currentObjective >= 14)
		{
			PauseManager.Get().currentobjective.text = "Congratulations! You have no new objectives. Maybe see a therapist...";
			return;
		}
		PauseManager.Get().currentobjective.text = "Current Objective: " + ObjectiveManager.objectives[ObjectiveManager.currentObjective];
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0000255D File Offset: 0x0000075D
	private void Update()
	{
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0000578B File Offset: 0x0000398B
	public void CompleteObjective()
	{
		ObjectiveManager.currentObjective++;
		SaveManager.Save();
		this.UpdateObjective();
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x000057A4 File Offset: 0x000039A4
	public void UpdateObjective()
	{
		if (ObjectiveManager.currentObjective >= 14)
		{
			PauseManager.Get().currentobjective.text = "Congratulations! You have no new objectives. Maybe see a therapist...";
			return;
		}
		HUDManager.Get().objectiveText.text = "New Objective: " + ObjectiveManager.objectives[ObjectiveManager.currentObjective];
		HUDManager.Get().objectiveText.gameObject.SetActive(true);
		PauseManager.Get().currentobjective.text = "Current Objective: " + ObjectiveManager.objectives[ObjectiveManager.currentObjective];
	}

	// Token: 0x040000ED RID: 237
	private static ObjectiveManager instance;

	// Token: 0x040000EE RID: 238
	public static int currentObjective;

	// Token: 0x040000EF RID: 239
	public static string[] objectives = new string[]
	{
		"Go to the Krusty Krab",
		"Go to Squidward's house and collect the overdue ketamine debt",
		"Kill Squidward",
		"Go to Patrick's house to collect the overdue drug money",
		"Snap Patrick's neck",
		"Go to Goo Lagoon and assassinate Larry for his unpaid steroids",
		"Defend yourself from Larry's sidemen",
		"Shoot up Shady Shoals",
		"Return to the Krusty Krab to speak with Mr. Krabs",
		"Kill Sandy in her meth lab to even out the drug trade competition",
		"Kill narcs in Downtown Bikini Bottom",
		"Go to the bank and collect Mr. Krabs's laundering funds",
		"Return the laundering money to Mr. Krabs at the Krusty Krab",
		"Kill Mr. Krabs in his true form"
	};
}
