
using System;
using System.Collections.Generic;

[Serializable]
public class Books {
	public List<BookData> list;
}

[Serializable]
public class BookData {
	public string book_id;
	public string book_name;
	public string description;
	public string user_creator;
	public string image;
}

[Serializable]
public class BookUpload {
	public byte[] bookImage;
	public string book_name;
	public string description;
}
