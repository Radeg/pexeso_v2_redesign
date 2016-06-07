using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AnimatedButton : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
	[Serializable]
	public class ButtonClickedEvent : UnityEvent { }

	[SerializeField]
	private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

	private Animator m_animator;

	private bool m_pointerInside = false;
	private bool m_pointerPressed = false;

	override protected void Start()
	{
		base.Start();
		m_animator = GetComponent<Animator>();
	}

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
		Unpress();
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		m_pointerPressed = false;
		Unpress();
		if (m_pointerInside)
			m_OnClick.Invoke();
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		m_pointerPressed = true;
		Press();
	}

	private void Press()
	{
		if (!IsActive())
			return;

		if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Normal"))
			m_animator.Play("Pressed");
	}

	private void Unpress()
	{
		if (!IsActive())
			return;

		if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Pressed"))
			m_animator.CrossFade("Unpressed", 0.3f);
	}

	public void OnPressedAnimationFinished()
	{
		m_OnClick.Invoke();
	}

	public void ResetToNormalState()
	{
		m_animator.Play("Normal");
	}
}
