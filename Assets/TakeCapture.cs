using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCapture : MonoBehaviour
{
    [SerializeField]
    GameObject blink;
    [SerializeField]
    Transform blParent;

    public List<GameObject> Kids;

    private void Awake()
    {
        blParent = GameObject.Find("Canvas").GetComponent<Transform>();
        
        TakeShotWithKids(Kids, false);
    }
    public void TakeShot()
    {
        //StartCoroutine(CaptureIt());
        
        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        TakeShotWithKids(Kids, true);

        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        GameObject bl = Instantiate(blink) as GameObject;
        bl.transform.SetParent(blParent.transform, false);

        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;

        TakeShotWithKids(Kids, true);

        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, "GalleryTest", "My img{0}.png"));
        Destroy(ss);
    }

    public void TakeShotWithKids(List<GameObject> _kidsList, bool isShow)
    {
        if (_kidsList == null)
            return;

        for (int i = 0; i < _kidsList.Count; i++)
        {
            _kidsList[i].SetActive(isShow);
        }

    }

    protected const string MEDIA_STORE_IMAGE_MEDIA = "android.provider.MediaStore$Images$Media";
    protected static AndroidJavaObject m_Activity;

    protected static string SaveImageToGallery(Texture2D a_Texture, string a_Title, string a_Description)
    {
        using (AndroidJavaClass mediaClass = new AndroidJavaClass(MEDIA_STORE_IMAGE_MEDIA))
        {
            using (AndroidJavaObject contentResolver = Activity.Call<AndroidJavaObject>("getContentResolver"))
            {
                AndroidJavaObject image = Texture2DToAndroidBitmap(a_Texture);
                return mediaClass.CallStatic<string>("insertImage", contentResolver, image, a_Title, a_Description);
            }
        }
    }

    protected static AndroidJavaObject Texture2DToAndroidBitmap(Texture2D a_Texture)
    {
        byte[] encodedTexture = a_Texture.EncodeToPNG();
        using (AndroidJavaClass bitmapFactory = new AndroidJavaClass("android.graphics.BitmapFactory"))
        {
            return bitmapFactory.CallStatic<AndroidJavaObject>("decodeByteArray", encodedTexture, 0, encodedTexture.Length);
        }
    }

    protected static AndroidJavaObject Activity
    {
        get
        {
            if (m_Activity == null)
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                m_Activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return m_Activity;
        }
    }

    public void OpenAndroidGallery()
    {
        #region [ Intent intent = new Intent(); ]
        //instantiate the class Intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //instantiate the object Intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        #endregion [ Intent intent = new Intent(); ]
        #region [ intent.setAction(Intent.ACTION_VIEW); ]
        //call setAction setting ACTION_SEND as parameter
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_VIEW"));
        #endregion [ intent.setAction(Intent.ACTION_VIEW); ]
        #region [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
        //instantiate the class Uri
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        //instantiate the object Uri with the parse of the url's file
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "content://media/internal/images/media");
        //call putExtra with the uri object of the file
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
        #endregion [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
        //set the type of file
        intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
        #region [ startActivity(intent); ]
        //instantiate the class UnityPlayer
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //instantiate the object currentActivity
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //call the activity with our Intent
        currentActivity.Call("startActivity", intentObject);
        #endregion [ startActivity(intent); ]
    }
}
