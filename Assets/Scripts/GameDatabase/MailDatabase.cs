using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameDatabase
{
    [CreateAssetMenu(fileName = "MailDatabase", menuName = "Game/Database/Mail")]
    public class MailDatabase : ScriptableObject
    {
        public Mail[] mails;
        public Mail GetRandomMail()
        {
            int randomIndex = Random.Range(0, mails.Length + 1);
            Mail mail = mails[randomIndex];
            return mail;
        }
    }
    
    [Serializable]
    public class Mail
    {
        public string title;
        public string content;
        public Sprite icon;
    }
}
