using PePo.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageController : MonoBehaviour {

	public Transform MarketPage;
	public Transform MyBookPage;
	public Transform NotificationPage;
	public Transform MyBookSelectPage;

	public GameObject BookItem;
	public GameObject MyBookItem;
	public GameObject NotificationItem;

	public InputField SearchField;

	public Image BookDetail_BookPicture;
	public Text BookDetail_BookName;
	public Text BookDetail_BookDescription;
	public Image BookDetail_ProfilePicture;
	public Text BookDetail_ProfileName;

	public Image MyProfile;
	public Text MyName;
	public Text MyUsername;
	public Text MyBookNumber;

	public Image confirmation_offerPicture;
	public Image confirmation_mePicture;
	public Image confirmation_userPicture;
	public Text confirmation_OfferBookName;
	public Text confirmation_OfferBookDescription;
	public Text confirmation_OfferName;
	public Text confirmation_MeBookName;
	public Text confirmation_MeBookDescription;

	public Image AddNew_Photo;
	public Text AddNew_BookName;
	public Text AddNew_Description;

	public Image Receipt_otherBook;
	public Image Receipt_userBook;
	public Text Receipt_otherBookName;
	public Text Receipt_userBookName;
	public Text Receipt_otherTele;
	public Text Receipt_userTele;
	public Text Receipt_otherName;
	public Text Receipt_userName;

	public Text TextButtonSelectBook;

	public Sprite ProfilePicture;
	public Sprite PhotoPicture;

	private MainPage_UIManager UIManager;
	private float timeToCloseApp = 0;
	private int currentBookID;
	private int currentUserID;
	private int numberOfTransaction;
	private string _path;

	private void Awake() {
		UIManager = GameObject.Find("MainPage_UIManager").GetComponent<MainPage_UIManager>();
		LoadBook();
		StartCoroutine(LoadNotificationAPI(request => {
			print(request.downloadHandler.text);
			var notis = JsonUtility.FromJson<Notifications>(request.downloadHandler.text);
			if (notis != null) {
				foreach (var noti in notis.list) {
					var item = Instantiate(NotificationItem);
					item.transform.SetParent(NotificationPage, false);

					var TemList = new List<Transform>();
					for (int i = 0; i < 2; i++) {
						TemList.Add(item.transform.GetChild(i));
					}
					foreach (var listObj in TemList) {
						switch (listObj.name) {
							case "Image:BookPhoto":
								StartCoroutine(LoadBookPicture(noti.image, action => {
									var texture = DownloadHandlerTexture.GetContent(action);
									listObj.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
								}));
								break;
							case "Text:Detail":
								listObj.GetComponent<Text>().text = $"{noti.username}";
								break;
						}
					}
					item.GetComponent<Button>().onClick.AddListener(() => OnClick_NotificationItem(int.Parse(noti.book_id)));
				}
			}
		}));

		MyName.text = PlayerPrefs.GetString("name");
		MyUsername.text = PlayerPrefs.GetString("username");

	}
	private void LoadBook() {
		StartCoroutine(LoadMarketAPI(request => {
			print(request.downloadHandler.text);
			var books = JsonUtility.FromJson<Books>(request.downloadHandler.text);
			if (books != null) {
				if (books.list.Count > 6) {
					var x = books.list.Count - 5;
					x /= 2;
					MarketPage.GetComponent<RectTransform>().sizeDelta = new Vector2(MarketPage.GetComponent<RectTransform>().sizeDelta.x, 3560 + 1200 * x);
				}
				foreach (var book in books.list) {
					var item = Instantiate(BookItem);
					item.transform.SetParent(MarketPage, false);

					var TemList = new List<Transform>();
					for (int i = 0; i < 3; i++) {
						TemList.Add(item.transform.GetChild(i));
					}
					foreach (var listObj in TemList) {
						switch (listObj.name) {
							case "Image:BookPhoto":
								StartCoroutine(LoadBookPicture(book.image, action => {
									var texture = DownloadHandlerTexture.GetContent(action);
									listObj.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
								}));
								break;
							case "Text:Title":
								listObj.GetComponent<Text>().text = $"{book.book_name}";
								break;
							case "Text:Detail":
								listObj.GetComponent<Text>().text = book.description;
								break;
						}
					}
					item.GetComponent<Button>().onClick.AddListener(() => OnClick_BookItem(int.Parse(book.book_id)));
				}
			}
		}));
		StartCoroutine(LoadMyBookAPI(request => {
			var books = JsonUtility.FromJson<Books>(request.downloadHandler.text);
			if (books != null) {
				MyBookNumber.text = books.list.Count.ToString(); ;
				if (books.list.Count > 4) {
					var x = books.list.Count - 3;
					x /= 2;
					MyBookSelectPage.GetComponent<RectTransform>().sizeDelta = new Vector2(MyBookSelectPage.GetComponent<RectTransform>().sizeDelta.x, 2326.8f + 1200 * x);
				}
				foreach (var book in books.list) {
					var itemX = Instantiate(MyBookItem);
					itemX.transform.SetParent(MyBookSelectPage, false);

					var TemListX = new List<Transform>();
					for (int i = 0; i < 2; i++) {
						TemListX.Add(itemX.transform.GetChild(i));
					}
					foreach (var listObj in TemListX) {
						switch (listObj.name) {
							case "Image:BookPhoto":
								StartCoroutine(LoadBookPicture(book.image, action => {
									var texture = DownloadHandlerTexture.GetContent(action);
									listObj.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
								}));
								break;
							case "Text:Detail":
								listObj.GetComponent<Text>().text = book.book_name;
								break;
						}
					}
					itemX.GetComponent<Button>().onClick.AddListener(() => OnClick_MyBookItem(int.Parse(book.book_id)));

					var item = Instantiate(BookItem);
					item.transform.SetParent(MyBookPage, false);

					var TemList = new List<Transform>();
					for (int i = 0; i < 3; i++) {
						TemList.Add(item.transform.GetChild(i));
					}
					foreach (var listObj in TemList) {
						switch (listObj.name) {
							case "Image:BookPhoto":
								StartCoroutine(LoadBookPicture(book.image, action => {
									var texture = DownloadHandlerTexture.GetContent(action);
									listObj.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
								}));
								break;
							case "Text:Title":
								listObj.GetComponent<Text>().text = $"{book.book_name}";
								break;
							case "Text:Detail":
								listObj.GetComponent<Text>().text = book.description;
								break;
						}
					}
					item.GetComponent<Button>().onClick.AddListener(() => OnClick_MyBookSelfItem(int.Parse(book.book_id)));
				}
			} else {
				MyBookNumber.text = "0";
			}
		}));
	}
	private void Update() {

		if (timeToCloseApp > (Time.time - 1.2f)) {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				Application.Quit();
			}
		} else {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				timeToCloseApp = Time.time;
				AndroidNativeFunctions.ShowToast("double tab to exit app . . .", true);
			}
		}
	}
	public void OnClick_NotificationItem(int i) {
		AndroidNativeFunctions.ShowProgressDialog("Loading . . .");
		StartCoroutine(SeeOfferAPI(i.ToString(), action => {
			print(action.downloadHandler.text);
			var con = JsonUtility.FromJson<confirmation>(action.downloadHandler.text);
			numberOfTransaction = int.Parse(con.me.number);
			confirmation_OfferBookName.text = con.offer.book_name;
			confirmation_OfferBookDescription.text = con.offer.description;
			confirmation_OfferName.text = con.offer.name;
			confirmation_MeBookDescription.text = con.me.description;
			confirmation_MeBookName.text = con.me.book_name;
			StartCoroutine(LoadBookPicture(con.offer.image, pic => {
				var texture = DownloadHandlerTexture.GetContent(pic);
				confirmation_offerPicture.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
				StartCoroutine(LoadBookPicture(con.offer.user_image, pic2 => {
					var texture1 = DownloadHandlerTexture.GetContent(pic2);
					confirmation_userPicture.sprite = Sprite.Create(texture1, new Rect(0.0f, 0.0f, texture1.width, texture1.height), new Vector2(0.5f, 0.5f), 100.0f);
					StartCoroutine(LoadBookPicture(con.me.image, pic3 => {
						var texture2 = DownloadHandlerTexture.GetContent(pic3);
						confirmation_mePicture.sprite = Sprite.Create(texture2, new Rect(0.0f, 0.0f, texture2.width, texture2.height), new Vector2(0.5f, 0.5f), 100.0f);
						UIManager.ShowConfirmation();
						AndroidNativeFunctions.HideProgressDialog();
					}));
				}));
			}));

		}));
	}
	public void OnClick_BookItem(int bookId) {
		AndroidNativeFunctions.ShowProgressDialog("Loading . . .");
		currentBookID = bookId;
		TextButtonSelectBook.text = "Select Your book to make offer";
		StartCoroutine(LoadBookDetail(bookId, action1 => {
			print(action1.downloadHandler.text);
			var bookDetail = JsonUtility.FromJson<BookData>(action1.downloadHandler.text);
			StartCoroutine(LoadBookPicture(bookDetail.image, action3 => {
				var texture = DownloadHandlerTexture.GetContent(action3);
				BookDetail_BookPicture.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
			}));
			BookDetail_BookName.text = bookDetail.book_name;
			BookDetail_BookDescription.text = bookDetail.description;
			currentUserID = int.Parse(bookDetail.user_creator);
			StartCoroutine(LoadUserProfile(currentUserID, action2 => {
				var userProfile = JsonUtility.FromJson<UserProfile>(action2.downloadHandler.text);
				StartCoroutine(LoadBookPicture(userProfile.image, action3 => {
					var texture = DownloadHandlerTexture.GetContent(action3);
					BookDetail_ProfilePicture.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

					AndroidNativeFunctions.HideProgressDialog();
					UIManager.ShowBookDetail();
				}));
				BookDetail_ProfileName.text = userProfile.name;
			}));
		}));
	}
	public void OnClick_MyBookItem(int bookId) {
		var OfferData = new offerData(){
			book_deal = currentBookID.ToString(),
			book_offer = bookId.ToString(),
			user_deal = currentUserID.ToString()
		};
		AndroidNativeFunctions.ShowProgressDialog("Loading . . .");
		StartCoroutine(SentOfferAPI(OfferData, action => {
			var TemList = new List<Transform>();
			int x = MarketPage.childCount;
			for (int i = 0; i < x; i++) {
				TemList.Add(MarketPage.GetChild(i));
			}
			for (int i = 0; i < x; i++) {
				Destroy(TemList[i].gameObject);
			}
			TemList.Clear();
			x = MyBookPage.childCount;
			for (int i = 0; i < x; i++) {
				TemList.Add(MyBookPage.GetChild(i));
			}
			for (int i = 0; i < x; i++) {
				Destroy(TemList[i].gameObject);
			}
			TemList.Clear();
			x = MyBookSelectPage.childCount;
			for (int i = 0; i < x; i++) {
				TemList.Add(MyBookSelectPage.GetChild(i));
			}
			for (int i = 0; i < x; i++) {
				Destroy(TemList[i].gameObject);
			}
			LoadBook();
			UIManager.ShowDialog();

			AndroidNativeFunctions.HideProgressDialog();
		}));
	}
	public void OnClick_MyBookSelfItem(int bookId) {
		TextButtonSelectBook.text = "Delete this book";
		AndroidNativeFunctions.ShowProgressDialog("Loading . . .");
		currentBookID = bookId;
		StartCoroutine(LoadBookDetail(bookId, action1 => {
			var bookDetail = JsonUtility.FromJson<BookData>(action1.downloadHandler.text);
			StartCoroutine(LoadBookPicture(bookDetail.image, action3 => {
				var texture = DownloadHandlerTexture.GetContent(action3);
				BookDetail_BookPicture.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
			}));
			BookDetail_BookName.text = bookDetail.book_name;
			BookDetail_BookDescription.text = bookDetail.description;
			StartCoroutine(LoadUserProfile(int.Parse(bookDetail.user_creator), action2 => {
				print(action2.downloadHandler.text);
				var userProfile = JsonUtility.FromJson<UserProfile>(action2.downloadHandler.text);
				StartCoroutine(LoadBookPicture(userProfile.image, action3 => {
					var texture = DownloadHandlerTexture.GetContent(action3);
					BookDetail_ProfilePicture.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

					AndroidNativeFunctions.HideProgressDialog();
					UIManager.ShowBookDetail();
				}));
				BookDetail_ProfileName.text = userProfile.name;
			}));
		}));
	}
	public void OnClick_SelectBook() {
		if (TextButtonSelectBook.text.Equals("Delete this book")) {
			AndroidNativeFunctions.ShowAlert("This action will delete your book information and orther people can't acess it", "Remove book from SwapBook", "Sure", "Cancel", "", action => {
				if (action == DialogInterface.Positive) {
					StartCoroutine(DeleteBookAPI(currentBookID.ToString(), callback => {
						var TemList = new List<Transform>();
						int x = MarketPage.childCount;
						for (int i = 0; i < x; i++) {
							TemList.Add(MyBookPage.GetChild(i));
						}
						for (int i = 0; i < x; i++) {
							Destroy(TemList[i].gameObject);
						}
						TemList.Clear();
						x = MyBookSelectPage.childCount;
						for (int i = 0; i < x; i++) {
							TemList.Add(MyBookSelectPage.GetChild(i));
						}
						for (int i = 0; i < x; i++) {
							Destroy(TemList[i].gameObject);
						}
						LoadBook();
						UIManager.HideBookDetail();
					}));
				}
			});
			return;
		}
		if (int.Parse(MyBookNumber.text) > 0) {
			UIManager.OnClick_Offer();
		} else {
			AndroidNativeFunctions.ShowToast("Your don't have any book to swap !");
		}
	}
	public void OnClick_ChangePassword() {
		// StartCoroutine();
	}
	public void OnClick_AddNew() {
		StartCoroutine(AddBookAPI(action=> {
			UIManager.HideAddNew();
		}));
	}
	public void OnClick_AddPicture() {
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) => {
			 if( path != null ) {
				_path = path;
				AddNew_BookName.text = _path;
				Texture2D texture = NativeGallery.LoadImageAtPath( path, 1024 );
				if( texture == null ) {
					Debug.Log( "Couldn't load texture from " + path );
					return;
				}
				AddNew_Photo.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
			}
		}, "Select a PNG image");
	}
	public void AcceptOffer() {
		StartCoroutine(AcceptOfferAPI(numberOfTransaction.ToString(), action => {
			print(action.downloadHandler.text);
			var r = JsonUtility.FromJson<Receipt>(action.downloadHandler.text);
			Receipt_otherBookName.text = r.other_book.book_name;
			Receipt_otherName.text = r.other_user.name;
			Receipt_otherTele.text = r.other_user.telephone;
			Receipt_userBookName.text = r.book_me.book_name;
			Receipt_userName.text = r.user_me.name;
			Receipt_userTele.text = r.user_me.telephone;
			StartCoroutine(LoadBookPicture(r.other_book.image, pic => {
				var texture = DownloadHandlerTexture.GetContent(pic);
				Receipt_otherBook.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
				StartCoroutine(LoadBookPicture(r.book_me.image, pic2 => {
					texture = DownloadHandlerTexture.GetContent(pic2);
					Receipt_userBook.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

					UIManager.ShowReceipt();
					UIManager.HideConfirmation();
				}));
			}));
		}));
	}
	public void DisclaimOffer() {
		StartCoroutine(DisclaimOfferAPI(numberOfTransaction.ToString(), action=> {
			UIManager.HideConfirmation();
		}));
	}
	public void OnClick_Search() {
		var TemList = new List<Transform>();
		int x = MarketPage.childCount;
		for (int i = 0; i < x; i++) {
			TemList.Add(MarketPage.GetChild(i));
		}
		for (int i = 0; i < x; i++) {
			Destroy(TemList[i].gameObject);
		}
		StartCoroutine(SearchAPI(SearchField.text, request => {
			print(request.downloadHandler.text);
			var books = JsonUtility.FromJson<Books>(request.downloadHandler.text);
			if (books != null) {
				if (books.list.Count > 6) {
					var y = books.list.Count - 5;
					y /= 2;
					MarketPage.GetComponent<RectTransform>().sizeDelta = new Vector2(MarketPage.GetComponent<RectTransform>().sizeDelta.x, 3560 + 1200 * y);
				}
				foreach (var book in books.list) {
					var item = Instantiate(BookItem);
					item.transform.SetParent(MarketPage, false);

					var TempList = new List<Transform>();
					for (int i = 0; i < 3; i++) {
						TempList.Add(item.transform.GetChild(i));
					}
					foreach (var listObj in TempList) {
						switch (listObj.name) {
							case "Image:BookPhoto":
								StartCoroutine(LoadBookPicture(book.image, action => {
									var texture = DownloadHandlerTexture.GetContent(action);
									listObj.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
								}));
								break;
							case "Text:Title":
								listObj.GetComponent<Text>().text = $"{book.book_name}";
								break;
							case "Text:Detail":
								listObj.GetComponent<Text>().text = book.description;
								break;
						}
					}
					item.GetComponent<Button>().onClick.AddListener(() => OnClick_BookItem(int.Parse(book.book_id)));
				}
			}
		}));
	}
	public void OnClick_Logout() {
		PlayerPrefs.DeleteAll();
		OneSignal.ClearOneSignalNotifications();
		SceneManager.LoadScene("Initialization");
	}
	private IEnumerator AcceptOfferAPI(string number, Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/transaction/accept", "POST");
		var bodyRaw = new UTF8Encoding().GetBytes($"{{\"number\":\"{number}\"}}");
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("x-auth", PlayerPrefs.GetString("token"));
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
	private IEnumerator DisclaimOfferAPI(string number, Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/transaction/decline", "POST");
		var bodyRaw = new UTF8Encoding().GetBytes($"{{\"number\":\"{number}\"}}");
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("x-auth", PlayerPrefs.GetString("token"));
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
	private IEnumerator SearchAPI(string query, Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/search", "POST");
		var bodyRaw = new UTF8Encoding().GetBytes($"{{\"query\":\"{query}\"}}");
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("x-auth", PlayerPrefs.GetString("token"));
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
	private IEnumerator DeleteBookAPI(string bookId, Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/books/{bookId}", "DELETE");
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("x-auth", PlayerPrefs.GetString("token"));
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
	private IEnumerator SeeOfferAPI(string bookId, Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/transaction/confirmation", "POST");
		var bodyRaw = new UTF8Encoding().GetBytes($"{{\"book_offer\":\"{bookId}\"}}");
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("x-auth", PlayerPrefs.GetString("token"));
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
	private IEnumerator AddBookAPI(Action<UnityWebRequest> callBack) {
		var form = new BookUpload(){
			bookImage = AddNew_Photo.sprite.texture.GetRawTextureData(),
			book_name = AddNew_BookName.text,
			description = AddNew_Description.text
		};

		var request = new UnityWebRequest($"{Config.URL()}/books/upload", "POST");
		var bodyRaw = new UTF8Encoding().GetBytes(JsonUtility.ToJson(form));
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("x-auth", PlayerPrefs.GetString("token"));
		request.SetRequestHeader("Content-Type", "application/form-data");
		yield return request.SendWebRequest();
		callBack(request);
	}
	private IEnumerator SentOfferAPI(offerData data, Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/offer", "POST");
		var bodyRaw = new UTF8Encoding().GetBytes(JsonUtility.ToJson(data));
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("x-auth", PlayerPrefs.GetString("token"));
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
	private IEnumerator LoadMarketAPI(Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/books", "GET");
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("x-auth", PlayerPrefs.GetString("token"));
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
	private IEnumerator LoadMyBookAPI(Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/books/me", "GET");
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("x-auth", PlayerPrefs.GetString("token"));
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
	private IEnumerator LoadNotificationAPI(Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/transaction/offer", "GET");
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("x-auth", PlayerPrefs.GetString("token"));
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
	private IEnumerator LoadBookPicture(string pictureName, Action<UnityWebRequest> action) {
		UnityWebRequest www = UnityWebRequestTexture.GetTexture($"{Config.HostName}/{pictureName}");
		yield return www.SendWebRequest();
		action(www);
	}
	private IEnumerator LoadBookDetail(int bookId, Action<UnityWebRequest> action) {
		var request = new UnityWebRequest($"{Config.URL()}/books/{bookId}", "GET");
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		action(request);
	}
	private IEnumerator LoadUserProfile(int UserId, Action<UnityWebRequest> action) {
		var request = new UnityWebRequest($"{Config.URL()}/users/{UserId}", "GET");
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		action(request);
	}
}
