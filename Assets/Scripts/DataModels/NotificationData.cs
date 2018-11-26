
using System;
using System.Collections.Generic;

[Serializable]
public class Notifications {
	public List<NotificationData> list;
}

[Serializable]
public class NotificationData {
	public string image;
	public string username;
	public string book_id;
}

