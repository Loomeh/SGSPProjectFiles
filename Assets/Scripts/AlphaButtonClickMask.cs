using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000016 RID: 22
public class AlphaButtonClickMask : MonoBehaviour, ICanvasRaycastFilter
{
	// Token: 0x06000048 RID: 72 RVA: 0x000037E8 File Offset: 0x000019E8
	public void Start()
	{
		this._image = base.GetComponent<Image>();
		Texture2D texture = this._image.sprite.texture;
		bool flag = false;
		if (texture != null)
		{
			try
			{
				texture.GetPixels32();
				goto IL_41;
			}
			catch (UnityException ex)
			{
				Debug.LogError(ex.Message);
				flag = true;
				goto IL_41;
			}
		}
		flag = true;
		IL_41:
		if (flag)
		{
			Debug.LogError("This script need an Image with a readbale Texture2D to work.");
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003854 File Offset: 0x00001A54
	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
	{
		Vector2 vector;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this._image.rectTransform, sp, eventCamera, out vector);
		Vector2 pivot = this._image.rectTransform.pivot;
		Vector2 vector2 = new Vector2(pivot.x + vector.x / this._image.rectTransform.rect.width, pivot.y + vector.y / this._image.rectTransform.rect.height);
		Vector2 vector3 = new Vector2(this._image.sprite.rect.x + vector2.x * this._image.sprite.rect.width, this._image.sprite.rect.y + vector2.y * this._image.sprite.rect.height);
		vector3.x /= (float)this._image.sprite.texture.width;
		vector3.y /= (float)this._image.sprite.texture.height;
		return this._image.sprite.texture.GetPixelBilinear(vector3.x, vector3.y).a > 0.1f;
	}

	// Token: 0x0400004F RID: 79
	protected Image _image;
}
