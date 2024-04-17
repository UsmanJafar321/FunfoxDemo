using System;
using System.Web;

namespace Manager
{

    [Serializable]
    public class EmailMessage
    {
        private string _fromemail;
        private string _fromname;
        private string[] _mailto;
        private string _subject;
        private string _body;
        private string[] _ccemail;
        private string[] _bccemail;

        public string FromEmail
        {
            get { return _fromemail; }
            set { _fromemail = value; }
        }


        public string[] CCEmail
        {
            get { return _ccemail; }
            set { _ccemail = value; }
        }
        public string[] BCCEmail
        {
            get { return _bccemail; }
            set { _bccemail = value; }
        }

        public string FromName
        {
            get { return _fromname; }
            set { _fromname = value; }
        }

        public string[] MailTo
        {
            get { return _mailto; }
            set { _mailto = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }
    }
}