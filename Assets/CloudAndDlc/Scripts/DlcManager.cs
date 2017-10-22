namespace CloudAndDlc
{
    using System.IO;

    using CloudAndDlc.Models;

    using UnityEngine;

    public class DlcManager : MonoBehaviour
    {
        public string BaseUrl = "http://localhost:50050/api/";

        public string Dlc = "office";
        public bool UpdateResources = false;

        private WWW _webRequestTexture;
        private WWW _webRequestAudio;

        private string _path;

        #region methods

        // Use this for initialization
        private void Start()
        {
            _path = Path.Combine(Application.persistentDataPath, Path.Combine("DLCs", Dlc));
            string requestUrl;
            if (!Directory.Exists(_path) || UpdateResources)
            {
                Directory.CreateDirectory(_path);
                requestUrl = BaseUrl + "dlc?name=" + Dlc + "&resource=";
            }
            else
            {
                requestUrl = _path + "/";
            }

            _webRequestTexture = new WWW(requestUrl + "texture.png");
            _webRequestAudio = new WWW(requestUrl + "sound.ogg");
        }

        private void LoadTexture(Texture texture)
        {
            var renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
        }

        private void LoadSound(AudioClip clip)
        {
            var audio = GetComponent<AudioSource>();
            audio.clip = clip;
            audio.Play();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_webRequestTexture != null && _webRequestTexture.isDone)
            {
                LoadTexture(_webRequestTexture.texture);
                File.WriteAllBytes(Path.Combine(_path, "texture.png"), _webRequestTexture.bytes);
                _webRequestTexture = null;
            }

            if (_webRequestAudio != null && _webRequestAudio.isDone)
            {
                LoadSound(_webRequestAudio.GetAudioClip(false, true));
                File.WriteAllBytes(Path.Combine(_path, "sound.ogg"), _webRequestAudio.bytes);
                _webRequestAudio = null;
            }
        }

        #endregion
    }
}