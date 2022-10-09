using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class MaterialType : MonoBehaviour
{
	// Token: 0x0400002E RID: 46
	public MaterialType.MaterialTypeEnum TypeOfMaterial;

	// Token: 0x020000B1 RID: 177
	[Serializable]
	public enum MaterialTypeEnum
	{
		// Token: 0x040003D5 RID: 981
		Plaster,
		// Token: 0x040003D6 RID: 982
		Metall,
		// Token: 0x040003D7 RID: 983
		Folliage,
		// Token: 0x040003D8 RID: 984
		Rock,
		// Token: 0x040003D9 RID: 985
		Wood,
		// Token: 0x040003DA RID: 986
		Brick,
		// Token: 0x040003DB RID: 987
		Concrete,
		// Token: 0x040003DC RID: 988
		Dirt,
		// Token: 0x040003DD RID: 989
		Glass,
		// Token: 0x040003DE RID: 990
		Water
	}
}
