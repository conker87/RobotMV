using UnityEngine;
using System.Collections;
using SmartLocalization;

public class _DEBUG_CHANGE_LANG : MonoBehaviour {

	public void _DEBUG_TOGGLE_LANG() {

		if (LanguageManager.Instance.CurrentlyLoadedCulture.languageCode == "de") {

			LanguageManager.Instance.ChangeLanguage ("en");

		} else if (LanguageManager.Instance.CurrentlyLoadedCulture.languageCode == "en") {

			LanguageManager.Instance.ChangeLanguage ("fr");

		} else if (LanguageManager.Instance.CurrentlyLoadedCulture.languageCode == "fr") {

			LanguageManager.Instance.ChangeLanguage ("de");

		}

	}

}