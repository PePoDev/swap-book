using System;
using System.Collections;
using UnityEngine.Networking;

namespace PePo.Utilities {
	static class API_Config {

		static public readonly string HostName = "424b5bc0.ngrok.io";
		static public readonly string PortNumber = "8080";

		public static string URL() => $"{HostName}";
	}
}
