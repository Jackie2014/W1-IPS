using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace IPDetectServer.Lib
{
    public static class Context
    {
        /// <summary>
        /// Adds a name value pair to the thread context. Pass a null value to remove an entry.
        /// </summary>
        /// <param name="key">the property key.</param>
        /// <param name="value">the property value.</param>
        private static void SetProperty(string key, object value)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return;
            }

            System.Web.HttpContext ctx = System.Web.HttpContext.Current;

            if (ctx == null)
            {
                CallContext.LogicalSetData(key,value);
            }
            else
            {
               ctx.Items[key] = value;
            }
        }

        /// <summary>
        /// Retrieves a value from the thread context by its key name
        /// </summary>
        /// <param name="key">the property key.</param>
        /// <returns>propery value.</returns>
        private static object GetProperty(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            System.Web.HttpContext ctx = System.Web.HttpContext.Current;

            if (ctx == null)
            {
                return CallContext.LogicalGetData(key);
            }
            else
            {
                return ctx.Items[key];
            }
        }

        /// <summary>
        /// Get invoking times.
        /// </summary>
        public static int Index
        {
            get
            {
                return ThreadContext.Index;
            }
            set
            {
                ThreadContext.Index = value;
            }
        }

        /// <summary>
        /// Get token of current context.
        /// </summary>
        public static string Token
        {
            get
            {
                return ThreadContext.Token;
            }
            set
            {
                ThreadContext.Token = value;
            }
        }

        /// <summary>
        /// Get language of current context.
        /// </summary>
        public static string Language
        {
            get
            {
                string lang = ThreadContext.Language;

                if (String.IsNullOrWhiteSpace(lang))
                {
                    lang = "en-US";
                    ThreadContext.Language = lang;
                }

                return lang;
            }
            set
            {
                ThreadContext.Language = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicates whether current user is authed.
        /// </summary>
        public static bool IsAuthed
        {
            get
            {
                return ThreadContext.IsAuthed;
            }
            set
            {
                ThreadContext.IsAuthed = value;
            }
        }

        /// <summary>
        /// Get user name of current context.
        /// </summary>
        public static string LoginName
        {
            get
            {
                return ThreadContext.LoginName;
            }
            set
            {
                ThreadContext.LoginName = value;
            }
        }

        /// <summary>
        /// Get ClientIP of current context.
        /// </summary>
        public static string ClientIP
        {
            get
            {
                return ThreadContext.ClientIP;
            }
            set
            {
                ThreadContext.ClientIP = value;
            }
        }

        /// <summary>
        /// Get Request URI of current context.
        /// </summary>
        public static string RequestUri
        {
            get
            {
                return ThreadContext.RequestUri;
            }
            set
            {
                ThreadContext.RequestUri = value;
            }
        }

        /// <summary>
        /// Get Request Uri template of current context.
        /// </summary>
        public static string RequestUriTemplate
        {
            get
            {
                return ThreadContext.RequestUriTemplate;
            }
            set
            {
                ThreadContext.RequestUriTemplate = value;
            }
        }

        /// <summary>
        /// Get request http method of current context.
        /// </summary>
        public static string HttpMethod
        {
            get
            {
                return ThreadContext.HttpMethod;
            }
            set
            {
                ThreadContext.HttpMethod = value;
            }
        }

        /// <summary>
        /// Get request http method of current context.
        /// </summary>
        public static string HttpHeader
        {
            get
            {
                return ThreadContext.HttpHeader;
            }
            set
            {
                ThreadContext.HttpHeader = value;
            }
        }

        /// <summary>
        /// Get Request content type of current context.
        /// </summary>
        public static string ContentType
        {
            get
            {
                return ThreadContext.ContentType;
            }
            set
            {
                ThreadContext.ContentType = value;
            }
        }

        /// <summary>
        /// Get Request content length of current context.
        /// </summary>
        public static int ContentLength
        {
            get
            {
                return ThreadContext.ContentLength;
            }
            set
            {
                ThreadContext.ContentLength = value;
            }
        }
       
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public static IDictionary<string,object> Items
        {
            get
            {
                return ThreadContext.Items;
            }
            set
            {
                ThreadContext.Items = value;
            }
        }

        /// <summary>
        /// Gets or sets CotextEntity object.
        /// </summary>
        private static ContextEntity ThreadContext
        {
            get
            {
                var result = GetProperty("ContextEntity") as ContextEntity;
                if (result == null)
                {
                    result = new ContextEntity();
                    SetProperty("ContextEntity", result);
                }

                return result;
            }
        }

        /// <summary>
        /// Clone Context entity.
        /// </summary>
        /// <returns>new an instance of ContextEntity.</returns>
        public static ContextEntity Clone()
        {
            ContextEntity cloned = ThreadContext.Clone();

            return cloned;
        }

        /// <summary>
        /// Set context in a new thread.
        /// </summary>
        /// <param name="contextEntity">ContextEntity object.</param>
        public static void SetContext(ContextEntity contextEntity)
        {
            if (contextEntity == null) return;

            Index = contextEntity.Index - 1;
         
            ClientIP = contextEntity.ClientIP;
            HttpMethod = contextEntity.HttpMethod;
            HttpHeader = contextEntity.HttpHeader;
            RequestUri = contextEntity.RequestUri;
            RequestUriTemplate = contextEntity.RequestUriTemplate;
            IsAuthed = contextEntity.IsAuthed;
            Language = contextEntity.Language;
            Token = contextEntity.Token;
            LoginName = contextEntity.LoginName;

            Items = contextEntity.Items;
        }
    }


    /// <summary>
    /// Thread Context entity.
    /// </summary>
    [Serializable]
    public class ContextEntity
    {
        public int Index
        {
            get;
            set;
        }
        /// <summary>
        /// Get token of current context.
        /// </summary>
        public string Token
        {
            get;
            set;
        }

        /// <summary>
        /// Get language of current context.
        /// </summary>
        public string Language
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicates whether current user is authed.
        /// </summary>
        public bool IsAuthed
        {
            get;
            set;
        }

        /// <summary>
        /// Get login name of current context.
        /// </summary>
        public string LoginName
        {
            get;
            set;
        }

     
        public string ClientIP
        {
            get;
            set;
        }

        public string RequestUri
        {
            get;
            set;
        }

        public string RequestUriTemplate
        {
            get;
            set;
        }

        public string HttpMethod
        {
            get;
            set;
        }

        public string HttpHeader
        {
            get;
            set;
        }

        public string ContentType
        {
            get;
            set;
        }

        public int ContentLength
        {
            get;
            set;
        }
        private IDictionary<string, object> _items;
        public IDictionary<string,object> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new Dictionary<string, object>();
                }

                return _items;
            }
            set
            {
                _items = value;
            }
        }
        /// <summary>
        /// Clone a ContextEntity object.
        /// </summary>
        /// <returns></returns>
        public ContextEntity Clone()
        {
            ContextEntity cloned = new ContextEntity();

            cloned.ClientIP = this.ClientIP;
            cloned.RequestUri = this.RequestUri;
            cloned.RequestUriTemplate = this.RequestUriTemplate;
            cloned.HttpMethod = this.HttpMethod;
            cloned.HttpHeader = this.HttpHeader;
            cloned.ContentType = this.ContentType;
            cloned.ContentLength = this.ContentLength;
            cloned.IsAuthed = this.IsAuthed;
            cloned.Language = this.Language;
            cloned.Token = this.Token;
            cloned.LoginName = this.LoginName;

            // just support simple type for items
            foreach ( KeyValuePair<string, object> kvp in this.Items ) 
            {
                cloned.Items.Add(kvp.Key,kvp.Value);
            }

            return cloned;
        }
    }
}
