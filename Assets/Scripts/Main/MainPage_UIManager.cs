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
		AndroidNativeFunctions.ShowAlert("ยังไม่ได้ทำ สัส",
					"ท่ดๆ แมวพิมพ์",
					"Got it", "", "",
					(action) => {
						print("Hello adb !");
					}
		);
	}
}
