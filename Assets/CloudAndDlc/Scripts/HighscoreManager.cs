namespace CloudAndDlc
{
    using System.Collections.Generic;
    using System.Text;

    using CloudAndDlc.Models;

    using UnityEngine;

    public class HighscoreManager : MonoBehaviour
    {
        public string BaseUrl = "http://localhost:50050/api/";
        private WWW _webRequest;

        private static readonly Dictionary<string, string> Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" },
            { "Authorization", "Token S0jieghJHFcddWHE4jP9OsjoP4Y1XTue" },
        };

        #region methods

        // Use this for initialization
        private void Start()
        {
            _webRequest = new WWW(BaseUrl + "highscore");
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Add");
                AddItem(new HighscoreItem
                {
                    Points = 1337,
                    Name = "Freddy"
                });
            }

            if (_webRequest == null)
            {
                return;
            }

            if (_webRequest.isDone)
            {
                var highscore = JsonUtility.FromJson<HighscoreList>(_webRequest.text);
                Debug.Log(highscore.Items.Length + ": " + highscore.Items[0].Name + " (" + highscore.Items[0].Points + ")");
                _webRequest = null;
            }
        }

        public void AddItem(HighscoreItem item)
        {
            _webRequest = new WWW(BaseUrl + "highscore", Encoding.UTF8.GetBytes(JsonUtility.ToJson(item)), Headers);
        }

        #endregion
    }
}