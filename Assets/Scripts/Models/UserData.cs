
using System;

[Serializable]
public class UserData {
	public string id;
	public string username;
	public string email;
	public string password;
	public Tokens tokens;
}

[Serializable]
public class Tokens {
	public string access;
	public string token;
}

