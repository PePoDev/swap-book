using System;
using System.Collections;
using UnityEngine.Networking;

namespace PePo.Utilities {
	static class Config {

		static public readonly string HostName = "http://10.37.5.84:8080";
		static public readonly string PortNumber = "8080";

		public static string URL() => $"{HostName}";	}
}
