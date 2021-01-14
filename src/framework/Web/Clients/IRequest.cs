using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Web.Clients {
    /// <summary>
    /// 
    /// </summary>
    public interface IRequest<out TRequest> where TRequest : IRequest<TRequest> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding"></param>
        TRequest Encoding( Encoding encoding );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding">2</param>
        TRequest Encoding( string encoding );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        TRequest ContentType( HttpContentType contentType );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType">容类型</param>
        TRequest ContentType( string contentType );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expiresDate"></param>
        TRequest Cookie( string name, string value, double expiresDate );
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresDate">到期时间</param>
        TRequest Cookie( string name, string value, DateTime expiresDate );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <param name="domain"></param>
        /// <param name="expiresDate"></param>
        TRequest Cookie( string name, string value, string path = "/", string domain = null, DateTime? expiresDate = null );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookie">cookie</param>
        TRequest Cookie( Cookie cookie );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        TRequest BearerToken( string token );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout">,：</param>
        TRequest Timeout( int timeout );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        TRequest Header<T>( string key, T value );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        TRequest Data( IDictionary<string, object> parameters );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        TRequest Data( string key, object value );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        TRequest JsonData<T>( T value );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        TRequest XmlData( string value );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action">,</param>
        TRequest OnFail( Action<string> action );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action">,，</param>
        TRequest OnFail( Action<string, HttpStatusCode> action );
        /// <summary>
        /// 
        /// </summary>
        TRequest IgnoreSsl();
        /// <summary>
        /// 
        /// </summary>
        Task<string> ResultAsync();
    }
}
