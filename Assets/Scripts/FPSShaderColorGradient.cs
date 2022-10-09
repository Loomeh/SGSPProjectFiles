using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class FPSShaderColorGradient : MonoBehaviour
{
	// Token: 0x06000018 RID: 24 RVA: 0x000026F4 File Offset: 0x000008F4
	private void Start()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			Material[] materials = component.materials;
			if (this.MaterialID >= materials.Length)
			{
				Debug.Log("ShaderColorGradient: Material ID more than shader materials count.");
			}
			this.matInstance = materials[this.MaterialID];
		}
		else
		{
			Projector component2 = base.GetComponent<Projector>();
			Material material = component2.material;
			if (!material.name.EndsWith("(Instance)"))
			{
				this.matInstance = new Material(material)
				{
					name = material.name + " (Instance)"
				};
			}
			else
			{
				this.matInstance = material;
			}
			component2.material = this.matInstance;
		}
		if (!this.matInstance.HasProperty(this.ShaderProperty))
		{
			Debug.Log("ShaderColorGradient: Shader not have \"" + this.ShaderProperty + "\" property");
		}
		this.propertyID = Shader.PropertyToID(this.ShaderProperty);
		this.oldColor = this.matInstance.GetColor(this.propertyID);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000027E7 File Offset: 0x000009E7
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000027FC File Offset: 0x000009FC
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			Color a = this.Color.Evaluate(num / this.TimeMultiplier);
			this.matInstance.SetColor(this.propertyID, a * this.oldColor);
		}
		if (num >= this.TimeMultiplier)
		{
			this.canUpdate = false;
		}
	}

	// Token: 0x0400001C RID: 28
	public string ShaderProperty = "_TintColor";

	// Token: 0x0400001D RID: 29
	public int MaterialID;

	// Token: 0x0400001E RID: 30
	public Gradient Color = new Gradient();

	// Token: 0x0400001F RID: 31
	public float TimeMultiplier = 1f;

	// Token: 0x04000020 RID: 32
	private bool canUpdate;

	// Token: 0x04000021 RID: 33
	private Material matInstance;

	// Token: 0x04000022 RID: 34
	private int propertyID;

	// Token: 0x04000023 RID: 35
	private float startTime;

	// Token: 0x04000024 RID: 36
	private Color oldColor;
}
