using DG.Tweening;
using UnityEngine;

public class LoginUIManager : MonoBehaviour {

	public float SpeedToAnimate = 0.7f;

	public float LogoPositionInSignin;
	public float LogoPositionInSignup;
	public float LogoPositionInForgot;

	private bool isSigninShowed = false;
	private bool isSignupShowed = false;
	private bool isForgotShowed = false;

	public RectTransform AppLogo;
	public RectTransform GroupSignin;
	public RectTransform GroupSignup;
	public RectTransform GroupForgot;

	#region AnimateUI
	public void AnimateSignUpGroup() {
		AppLogo.DOAnchorPosY(LogoPositionInSignup, SpeedToAnimate - 0.3f);
		ToggleSignin();
		ToggleSignup();
	}

	public void AnimateForgotGroup() {
		AppLogo.DOAnchorPosY(LogoPositionInForgot, SpeedToAnimate);
		ToggleSignin();
		ToggleForgot();
	}

	private void AnimateSigninGroup() {
		AppLogo.DOAnchorPosY(LogoPositionInSignin, SpeedToAnimate);
		ToggleSignin();
		if (isSignupShowed) {
			ToggleSignup();
		} else if (isForgotShowed) {
			ToggleForgot();
		}
	}

	private void ToggleSignin() {
		if (isSigninShowed) {
			GroupSignin.DOAnchorPosX(-2000, SpeedToAnimate);
		} else {
			GroupSignin.DOAnchorPosX(0, SpeedToAnimate);
		}
		isSigninShowed = !isSigninShowed;
	}
	private void ToggleSignup() {
		if (isSignupShowed) {
			GroupSignup.DOAnchorPosX(2000, SpeedToAnimate);
		} else {
			GroupSignup.DOAnchorPosX(0, SpeedToAnimate);
		}
		isSignupShowed = !isSignupShowed;
	}
	private void ToggleForgot() {
		if (isForgotShowed) {
			GroupForgot.DOAnchorPosY(-2000, SpeedToAnimate);
		} else {
			GroupForgot.DOAnchorPosY(0, SpeedToAnimate);
		}
		isForgotShowed = !isForgotShowed;
	}

	#endregion

	private void Start() {
		AnimateSigninGroup();
	}

	private void Update() {

		// Animate to Sign in.
		if (Input.GetKeyDown(KeyCode.Escape)) {
			AnimateSigninGroup();
		}
	}
}
