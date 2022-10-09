using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class FPSShaderFloatCurves : MonoBehaviour
{
	// Token: 0x0600001C RID: 28 RVA: 0x00002888 File Offset: 0x00000A88
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
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002964 File Offset: 0x00000B64
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002978 File Offset: 0x00000B78
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			float value = this.FloatPropertyCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphScaleMultiplier;
			this.matInstance.SetFloat(this.propertyID, value);
		}
		if (num >= this.GraphTimeMultiplier)
		{
			this.canUpdate = false;
		}
	}

	// Token: 0x04000025 RID: 37
	public string ShaderProperty = "_BumpAmt";

	// Token: 0x04000026 RID: 38
	public int MaterialID;

	// Token: 0x04000027 RID: 39
	public AnimationCurve FloatPropertyCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x04000028 RID: 40
	public float GraphTimeMultiplier = 1f;

	// Token: 0x04000029 RID: 41
	public float GraphScaleMultiplier = 1f;

	// Token: 0x0400002A RID: 42
	private bool canUpdate;

	// Token: 0x0400002B RID: 43
	private Material matInstance;

	// Token: 0x0400002C RID: 44
	private int propertyID;

	// Token: 0x0400002D RID: 45
	private float startTime;
}
