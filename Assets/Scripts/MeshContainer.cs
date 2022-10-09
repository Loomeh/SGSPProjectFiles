using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class MeshContainer
{
	// Token: 0x0600002C RID: 44 RVA: 0x00002DA1 File Offset: 0x00000FA1
	public MeshContainer(Mesh m)
	{
		this.mesh = m;
		this.vertices = m.vertices;
		this.normals = m.normals;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002DC8 File Offset: 0x00000FC8
	public void Update()
	{
		this.mesh.vertices = this.vertices;
		this.mesh.normals = this.normals;
	}

	// Token: 0x04000038 RID: 56
	public Mesh mesh;

	// Token: 0x04000039 RID: 57
	public Vector3[] vertices;

	// Token: 0x0400003A RID: 58
	public Vector3[] normals;
}
