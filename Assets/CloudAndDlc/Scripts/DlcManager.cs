namespace CloudAndDlc
{
    using CloudAndDlc.Models;

    using UnityEngine;

    public class DlcManager : MonoBehaviour
    {
        public string BaseUrl = "http://localhost:50050/api/";

        public string Dlc = "office";

        private WWW _webRequestTexture;
        private WWW _webRequestAudio;

        #region methods

        // Use this for initialization
        private void Start()
        {
            _webRequestTexture = new WWW(BaseUrl + "dlc?name=" + Dlc + "&resource=texture.png");
            _webRequestAudio = new WWW(BaseUrl + "dlc?name=" + Dlc + "&resource=sound.ogg");
        }

        // Update is called once per frame
        private void Update()
        {
            if (_webRequestTexture != null && _webRequestTexture.isDone)
            {
                GetComponent<Renderer>().material.mainTexture = _webRequestTexture.texture;
                _webRequestTexture = null;
            }

            if (_webRequestAudio != null && _webRequestAudio.isDone)
            {
                var audio = GetComponent<AudioSource>();
                audio.clip = _webRequestAudio.GetAudioClip(false, true);
                audio.Play();
                _webRequestAudio = null;
            }
        }

        #endregion
    }
}