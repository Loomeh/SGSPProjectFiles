using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000011 RID: 17
[ExecuteInEditMode]
[RequireComponent(typeof(WaterBase))]
public class PlanarReflection : MonoBehaviour
{
	// Token: 0x0600002E RID: 46 RVA: 0x00002DEC File Offset: 0x00000FEC
	public void Start()
	{
		this.sharedMaterial = ((WaterBase)base.gameObject.GetComponent(typeof(WaterBase))).sharedMaterial;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002E14 File Offset: 0x00001014
	private Camera CreateReflectionCameraFor(Camera cam)
	{
		string name = base.gameObject.name + "Reflection" + cam.name;
		GameObject gameObject = GameObject.Find(name);
		if (!gameObject)
		{
			gameObject = new GameObject(name, new Type[]
			{
				typeof(Camera)
			});
		}
		if (!gameObject.GetComponent(typeof(Camera)))
		{
			gameObject.AddComponent(typeof(Camera));
		}
		Camera component = gameObject.GetComponent<Camera>();
		component.backgroundColor = this.clearColor;
		component.clearFlags = (this.reflectSkybox ? CameraClearFlags.Skybox : CameraClearFlags.Color);
		this.SetStandardCameraParameter(component, this.reflectionMask);
		if (!component.targetTexture)
		{
			component.targetTexture = this.CreateTextureFor(cam);
		}
		return component;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002EDB File Offset: 0x000010DB
	private void SetStandardCameraParameter(Camera cam, LayerMask mask)
	{
		cam.cullingMask = (mask & ~(1 << LayerMask.NameToLayer("Water")));
		cam.backgroundColor = Color.black;
		cam.enabled = false;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002F0C File Offset: 0x0000110C
	private RenderTexture CreateTextureFor(Camera cam)
	{
		return new RenderTexture(Mathf.FloorToInt((float)cam.pixelWidth * 0.5f), Mathf.FloorToInt((float)cam.pixelHeight * 0.5f), 24)
		{
			hideFlags = HideFlags.DontSave
		};
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002F44 File Offset: 0x00001144
	public void RenderHelpCameras(Camera currentCam)
	{
		if (this.helperCameras == null)
		{
			this.helperCameras = new Dictionary<Camera, bool>();
		}
		if (!this.helperCameras.ContainsKey(currentCam))
		{
			this.helperCameras.Add(currentCam, false);
		}
		if (this.helperCameras[currentCam])
		{
			return;
		}
		if (!this.reflectionCamera)
		{
			this.reflectionCamera = this.CreateReflectionCameraFor(currentCam);
		}
		this.RenderReflectionFor(currentCam, this.reflectionCamera);
		this.helperCameras[currentCam] = true;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002FC2 File Offset: 0x000011C2
	public void LateUpdate()
	{
		if (this.helperCameras != null)
		{
			this.helperCameras.Clear();
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002FD7 File Offset: 0x000011D7
	public void WaterTileBeingRendered(Transform tr, Camera currentCam)
	{
		this.RenderHelpCameras(currentCam);
		if (this.reflectionCamera && this.sharedMaterial)
		{
			this.sharedMaterial.SetTexture(this.reflectionSampler, this.reflectionCamera.targetTexture);
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003016 File Offset: 0x00001216
	public void OnEnable()
	{
		Shader.EnableKeyword("WATER_REFLECTIVE");
		Shader.DisableKeyword("WATER_SIMPLE");
	}

	// Token: 0x06000036 RID: 54 RVA: 0x0000302C File Offset: 0x0000122C
	public void OnDisable()
	{
		Shader.EnableKeyword("WATER_SIMPLE");
		Shader.DisableKeyword("WATER_REFLECTIVE");
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003044 File Offset: 0x00001244
	private void RenderReflectionFor(Camera cam, Camera reflectCamera)
	{
		if (!reflectCamera)
		{
			return;
		}
		if (this.sharedMaterial && !this.sharedMaterial.HasProperty(this.reflectionSampler))
		{
			return;
		}
		reflectCamera.cullingMask = (this.reflectionMask & ~(1 << LayerMask.NameToLayer("Water")));
		this.SaneCameraSettings(reflectCamera);
		reflectCamera.backgroundColor = this.clearColor;
		reflectCamera.clearFlags = (this.reflectSkybox ? CameraClearFlags.Skybox : CameraClearFlags.Color);
		if (this.reflectSkybox && cam.gameObject.GetComponent(typeof(Skybox)))
		{
			Skybox skybox = (Skybox)reflectCamera.gameObject.GetComponent(typeof(Skybox));
			if (!skybox)
			{
				skybox = (Skybox)reflectCamera.gameObject.AddComponent(typeof(Skybox));
			}
			skybox.material = ((Skybox)cam.GetComponent(typeof(Skybox))).material;
		}
		GL.SetRevertBackfacing(true);
		Transform transform = base.transform;
		Vector3 eulerAngles = cam.transform.eulerAngles;
		reflectCamera.transform.eulerAngles = new Vector3(-eulerAngles.x, eulerAngles.y, eulerAngles.z);
		reflectCamera.transform.position = cam.transform.position;
		Vector3 position = transform.transform.position;
		position.y = transform.position.y;
		Vector3 up = transform.transform.up;
		float w = -Vector3.Dot(up, position) - this.clipPlaneOffset;
		Vector4 plane = new Vector4(up.x, up.y, up.z, w);
		Matrix4x4 matrix4x = Matrix4x4.zero;
		matrix4x = PlanarReflection.CalculateReflectionMatrix(matrix4x, plane);
		this.oldpos = cam.transform.position;
		Vector3 position2 = matrix4x.MultiplyPoint(this.oldpos);
		reflectCamera.worldToCameraMatrix = cam.worldToCameraMatrix * matrix4x;
		Vector4 clipPlane = this.CameraSpacePlane(reflectCamera, position, up, 1f);
		reflectCamera.projectionMatrix = cam.CalculateObliqueMatrix(clipPlane);
		reflectCamera.transform.position = position2;
		Vector3 eulerAngles2 = cam.transform.eulerAngles;
		reflectCamera.transform.eulerAngles = new Vector3(-eulerAngles2.x, eulerAngles2.y, eulerAngles2.z);
		reflectCamera.Render();
		GL.SetRevertBackfacing(false);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x0000329F File Offset: 0x0000149F
	private void SaneCameraSettings(Camera helperCam)
	{
		helperCam.depthTextureMode = DepthTextureMode.None;
		helperCam.backgroundColor = Color.black;
		helperCam.clearFlags = CameraClearFlags.Color;
		helperCam.renderingPath = RenderingPath.Forward;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000032C4 File Offset: 0x000014C4
	private static Matrix4x4 CalculateReflectionMatrix(Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
		reflectionMat.m01 = -2f * plane[0] * plane[1];
		reflectionMat.m02 = -2f * plane[0] * plane[2];
		reflectionMat.m03 = -2f * plane[3] * plane[0];
		reflectionMat.m10 = -2f * plane[1] * plane[0];
		reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
		reflectionMat.m12 = -2f * plane[1] * plane[2];
		reflectionMat.m13 = -2f * plane[3] * plane[1];
		reflectionMat.m20 = -2f * plane[2] * plane[0];
		reflectionMat.m21 = -2f * plane[2] * plane[1];
		reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
		reflectionMat.m23 = -2f * plane[3] * plane[2];
		reflectionMat.m30 = 0f;
		reflectionMat.m31 = 0f;
		reflectionMat.m32 = 0f;
		reflectionMat.m33 = 1f;
		return reflectionMat;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000347C File Offset: 0x0000167C
	private static float sgn(float a)
	{
		if (a > 0f)
		{
			return 1f;
		}
		if (a < 0f)
		{
			return -1f;
		}
		return 0f;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000034A0 File Offset: 0x000016A0
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 point = pos + normal * this.clipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(point);
		Vector3 vector = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(vector.x, vector.y, vector.z, -Vector3.Dot(lhs, vector));
	}

	// Token: 0x0400003B RID: 59
	public LayerMask reflectionMask;

	// Token: 0x0400003C RID: 60
	public bool reflectSkybox;

	// Token: 0x0400003D RID: 61
	public Color clearColor = Color.grey;

	// Token: 0x0400003E RID: 62
	public string reflectionSampler = "_ReflectionTex";

	// Token: 0x0400003F RID: 63
	public float clipPlaneOffset = 0.07f;

	// Token: 0x04000040 RID: 64
	private Vector3 oldpos = Vector3.zero;

	// Token: 0x04000041 RID: 65
	private Camera reflectionCamera;

	// Token: 0x04000042 RID: 66
	private Material sharedMaterial;

	// Token: 0x04000043 RID: 67
	private Dictionary<Camera, bool> helperCameras;
}
