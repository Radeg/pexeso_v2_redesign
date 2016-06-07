using UnityEngine;
using UnityEngine.UI;

public class StartGamePopupOpener : PopupOpener
{
    public int starsObtained = 0;

	public override void OpenPopup()
	{
		var popup = Instantiate(popupPrefab) as GameObject;
		popup.SetActive(true);
		popup.transform.localScale = Vector3.zero;
		popup.transform.SetParent(m_canvas.transform, false);

		var startGamePopup = popup.GetComponent<StartGamePopup>();
		startGamePopup.Open();
		startGamePopup.SetAchievedStars(starsObtained);
	}
}
