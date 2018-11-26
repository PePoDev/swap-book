using DG.Tweening;
using UnityEngine;

public class MainPage_UIManager : MonoBehaviour {

	public float speed;
	public RectTransform[] PageItem;

	private int currentIndex;

	private void Start() {
		currentIndex = 0;
	}

	public void SwithPage(int index) {

		// Don't anything if touch same page.
		if (currentIndex == index)
			return;
		
		PageItem[currentIndex].DOAnchorPosX(PageItem[index].localPosition.x > 0 ? -1925 : 1925, speed);
		currentIndex = index;
		PageItem[currentIndex].DOAnchorPosX(0, speed);

	}

	public void OnClick_MenuButton() {
		
	}

	public void ShowBookDetail() {
		PageItem[4].DOAnchorPosX(0, speed);
	}
	public void HideBookDetail() {
		PageItem[4].DOAnchorPosX(1925, speed);
	}
	public void OnClick_Offer() {
		PageItem[5].DOAnchorPosX(0, speed);
		HideBookDetail();
	}
	public void HideSelectBook() {
		PageItem[5].DOAnchorPosX(1925, speed);
	}
	public void ShowDialog() {
		HideSelectBook();
		PageItem[6].gameObject.SetActive(true);
	}
	public void HideDialog() {
		PageItem[6].gameObject.SetActive(false);
	}
	public void ShowConfirmation() {
		PageItem[7].DOAnchorPosX(0, speed);
	}
	public void HideConfirmation() {
		PageItem[7].DOAnchorPosX(1925, speed);
	}
	public void ShowAddNew() {
		PageItem[8].DOAnchorPosX(0, speed);
	}
	public void HideAddNew() {
		PageItem[8].DOAnchorPosX(1925, speed);
	}
	public void ShowReceipt() {
		PageItem[9].DOAnchorPosX(0, speed);
	}
	public void HideReceipt() {
		PageItem[9].DOAnchorPosX(1925, speed);
	}
}
