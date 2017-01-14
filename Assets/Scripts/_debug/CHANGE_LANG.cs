using UnityEngine;
using System.Collections;
using SmartLocalization;

public class CHANGE_LANG : MonoBehaviour {

	public void TOGGLE_LANG() {

		if (LanguageManager.Instance.CurrentlyLoadedCulture.languageCode == "de") {

			LanguageManager.Instance.ChangeLanguage ("en");

		} else if (LanguageManager.Instance.CurrentlyLoadedCulture.languageCode == "en") {

			LanguageManager.Instance.ChangeLanguage ("fr");

		} else if (LanguageManager.Instance.CurrentlyLoadedCulture.languageCode == "fr") {

			LanguageManager.Instance.ChangeLanguage ("de");

		}

	}

}