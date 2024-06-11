using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamworkTest : MonoBehaviour
{
	void Start()
	{
		if (SteamManager.Initialized)
		{
			string name = SteamFriends.GetPersonaName();
			Debug.Log(name);
		}
	}
}
