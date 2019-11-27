using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class Token
    { 
         
        /// <summary>
        /// AccessToken
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 有效期时长，单位秒
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 有效期，Utc时间
        /// </summary>
        [JsonProperty("expires_On")]
        public DateTime ExpiresOn { get; set; }

        /// <summary>
        /// Token类型
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; } 


    }
}
