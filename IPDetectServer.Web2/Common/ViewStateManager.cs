using System.Collections.Generic;
using System.Text;
using System.Web;
using IPDetectServer.Lib.Common;

namespace IPDetectServer.Web
{
    public class ViewStateManager
    {
        private HttpRequestBase _request;
        private dynamic _viewBag;
        private string _viewStateInHtmlName;

        public ViewStateManager(HttpRequestBase request, dynamic viewBag, string viewStateInHtmlName)
        {
            _request = request;
            _viewBag = viewBag;
            _viewStateInHtmlName = viewStateInHtmlName;
            this.InitViewState();
        }

        /// <summary>
        /// Encodes to the base64 string.
        /// </summary>
        /// <param name="str">The STR string.</param>
        /// <returns>encoded string to base64</returns>
        private static string Base64Encode(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }
            else
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                return HttpServerUtility.UrlTokenEncode(bytes);
            }
        }

        /// <summary>
        /// Decodes from the base64 string.
        /// </summary>
        /// <param name="str">The STR string.</param>
        /// <returns>decoded string from base64</returns>
        private static string Base64Decode(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }
            else
            {
                byte[] bytes = HttpServerUtility.UrlTokenDecode(str);
                return Encoding.UTF8.GetString(bytes);
            }
        }

        private Dictionary<string, string> _viewState = null;
        private void InitViewState()
        {
            if (_viewState == null)
            {
                string viewStateRowData = _request[_viewStateInHtmlName];
                if (!string.IsNullOrWhiteSpace(viewStateRowData))
                {
                    string dictionaryData = Base64Decode(viewStateRowData);
                    _viewState = JsonConverter.FromJsonTo<Dictionary<string, string>>(dictionaryData);
                }

                if (_viewState == null)
                {
                    _viewState = new Dictionary<string, string>();
                }

                _viewBag.ViewState = viewStateRowData;
            }
        }

        private void SetViewBag()
        {
            string tem = JsonConverter.ToJson(this._viewState);
            _viewBag.ViewState = Base64Encode(tem);
        }

        //view state maintain
        public T GetViewState<T>(string name)
        {
            InitViewState();

            if (_viewState.ContainsKey(name))
            {
                string val = _viewState[name];
                if (!string.IsNullOrWhiteSpace(val))
                {
                    return JsonConverter.FromJsonTo<T>(val);
                }
            }

            return default(T);

        }

        public void SetViewState(string name, object obj)
        {
            string val = JsonConverter.ToJson(obj);
            if (_viewState.ContainsKey(name))
            {
                this._viewState[name] = val;
            }
            else
            {
                this._viewState.Add(name, val);
            }

            SetViewBag();
        }
    }
}