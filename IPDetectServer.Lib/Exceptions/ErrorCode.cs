#region Header

/**
 * <pre>
 *
 *  Accela Mobile
 *  File: ErrorCodes.cs
 *
 *  Accela, Inc.
 *  Copyright (C): 2011
 *
 *  Description:
 *
 *  Notes:
 * $Id: ErrorCodes.cs 142354 2011-11-04 02:19:45Z ACHIEVO\jackie.yu $.
 *  Revision History
 *  &lt;Date&gt;,    &lt;Who&gt;,    &lt;What&gt;
 *  2011-11-04       Jackie Yu       Init.
 * </pre>
 */

#endregion Header

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Lib.Exceptions
{
   
    public class Errors
    {
        public const string bad_request_400 = "bad_request";
        public const string data_validation_error_400 = "data_validation_error";
        public const string invalid_uri_400 = "invalid_uri";
        public const string record_has_existed_400 = "record_has_existed";

        public const string unauthorized_401 = "unauthorized";
        public const string no_token_401 = "no_token";
        public const string token_expired_401 = "token_expired";
        public const string invalid_token_401 = "invalid_token";
        public const string unauthorized_app_401 = "unauthorized_app";
        public const string no_app_credential_401 = "no_app_credential";
        public const string invalid_access_key_401 = "invalid_access_key";

        public const string forbidden_403 = "forbidden";

        public const string not_found_404 = "resource_not_found";

        public const string internal_server_error_500 = "internal_server_error";

        public const string require_mannual_link_account_500 = "require_mannual_link_account";

        public const string create_aca_user_failed_500 = "create_aca_user_failed";
        
    }
}
