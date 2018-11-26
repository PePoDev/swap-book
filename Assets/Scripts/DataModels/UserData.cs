
using System;

[Serializable]
public class UserData {
	public string user_id;
	public string username;
	public string name;
	public string email;
	public string telephone;
}

[Serializable]
public class UserProfile{
	public string name;
	public string image;
	public string telephone;
}

[Serializable]
public class Receipt{
	public UserProfile other_user;
	public BookData other_book;
	public UserProfile user_me;
	public BookData book_me;
}