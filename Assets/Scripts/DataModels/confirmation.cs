using System;

[Serializable]
public class confirmation {
	public offer offer;
	public me me;
}

[Serializable]
public class offer {
	public string book_name;
	public string description;
	public string image;
	public string user_image;
	public string name;
}
[Serializable]
public class me {
	public string number;
	public string book_name;
	public string description;
	public string image;
}
