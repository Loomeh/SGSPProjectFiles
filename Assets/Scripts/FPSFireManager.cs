using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class FPSFireManager : MonoBehaviour
{
	// Token: 0x06000008 RID: 8 RVA: 0x00002438 File Offset: 0x00000638
	private void Update()
	{
		RaycastHit raycastHit;
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(new Ray(base.transform.position, base.transform.forward), out raycastHit, this.BulletDistance))
		{
			GameObject impactEffect = this.GetImpactEffect(raycastHit.transform.gameObject);
			if (impactEffect == null)
			{
				return;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(impactEffect, raycastHit.point, default(Quaternion));
			this.ImpactEffect.SetActive(false);
			this.ImpactEffect.SetActive(true);
			gameObject.transform.LookAt(raycastHit.point + raycastHit.normal);
			Object.Destroy(gameObject, 4f);
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000024F0 File Offset: 0x000006F0
	private GameObject GetImpactEffect(GameObject impactedGameObject)
	{
		MaterialType component = impactedGameObject.GetComponent<MaterialType>();
		if (component == null)
		{
			return null;
		}
		foreach (FPSFireManager.ImpactInfo impactInfo in this.ImpactElemets)
		{
			if (impactInfo.MaterialType == component.TypeOfMaterial)
			{
				return impactInfo.ImpactEffect;
			}
		}
		return null;
	}

	// Token: 0x0400000D RID: 13
	public FPSFireManager.ImpactInfo[] ImpactElemets = new FPSFireManager.ImpactInfo[0];

	// Token: 0x0400000E RID: 14
	public float BulletDistance = 100f;

	// Token: 0x0400000F RID: 15
	public GameObject ImpactEffect;

	// Token: 0x020000B0 RID: 176
	[Serializable]
	public class ImpactInfo
	{
		// Token: 0x040003D2 RID: 978
		public MaterialType.MaterialTypeEnum MaterialType;

		// Token: 0x040003D3 RID: 979
		public GameObject ImpactEffect;
	}
}
