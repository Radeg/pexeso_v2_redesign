﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TintedButton : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
	[Serializable]
	public class ButtonClickedEvent : UnityEvent { }

	[SerializeField]
	private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

	private const float m_colorOffset = 0.2f;

	private bool m_pointerInside = false;
	private bool m_pointerPressed = false;

	public ButtonClickedEvent onClick
	{
		get { return m_OnClick; }
		set { m_OnClick = value; }
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		m_pointerInside = true;
		if (m_pointerPressed)
			Press();
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		m_pointerInside = false;
		if (m_pointerPressed)
			Unpress();
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		m_pointerPressed = false;
		if (m_pointerInside)
		{
			Unpress();
			m_OnClick.Invoke();
		}
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		m_pointerPressed = true;
		if (m_pointerInside)
			Press();
	}

	private void Press()
	{
		if (!IsActive())
			return;

		Darken();
	}

	private void Unpress()
	{
		if (!IsActive())
			return;

		Brighten();
	}

	private void Darken()
	{
		var images = GetComponentsInChildren<Image>();
		foreach (var image in images)
			StartCoroutine(RunDarkenColor(image));
	}

	private void Brighten()
	{
		var images = GetComponentsInChildren<Image>();
		foreach (var image in images)
			StartCoroutine(RunBrightenColor(image));
	}

	private IEnumerator RunDarkenColor(Image image)
	{
		for (int i = 0; i < 20; i++)
		{
			var newColor = image.color;
			newColor.r -= 0.01f;
			newColor.g -= 0.01f;
			newColor.b -= 0.01f;
			image.color = newColor;
			yield return new WaitForSeconds(0.01f);
		}
	}

	private IEnumerator RunBrightenColor(Image image)
	{
		for (int i = 0; i < 20; i++)
		{
			var newColor = image.color;
			newColor.r += 0.01f;
			newColor.g += 0.01f;
			newColor.b += 0.01f;
			image.color = newColor;
			yield return new WaitForSeconds(0.01f);
		}
	}
}
