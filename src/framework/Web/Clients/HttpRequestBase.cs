using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Orion.Framework.Json;

namespace Orion.Framework.Web.Clients {
 
    public abstract class HttpRequestBase<TRequest> where TRequest : IRequest<TRequest> {

       

        private readonly string _url;
       
        private readonly HttpMethod _httpMethod;
       
        private IDictionary<string, object> _params;
       
        private string _data;
       
        private Encoding _encoding;
     
        private string _contentType;
       
        private readonly CookieContainer _cookieContainer;
       
        private TimeSpan _timeout;
       
        private readonly Dictionary<string, string> _headers;
      
        private Action<string> _failAction;
       
        private Action<string, HttpStatusCode> _failStatusCodeAction;
  
        private Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> _serverCertificateCustomValidationCallback;
        
        private string _token;

        
        protected HttpRequestBase( HttpMethod httpMethod, string url ) {
            if( string.IsNullOrWhiteSpace( url ) )
                throw new ArgumentNullException( nameof( url ) );
            System.Text.Encoding.RegisterProvider( CodePagesEncodingProvider.Instance );
            _url = url;
            _httpMethod = httpMethod;
            _params = new Dictionary<string, object>();
            _contentType = HttpContentType.FormUrlEncoded.Description();
            _cookieContainer = new CookieContainer();
            _timeout = new TimeSpan( 0, 0, 30 );
            _headers = new Dictionary<string, string>();
            _encoding = System.Text.Encoding.UTF8;
        }

     

        public TRequest Encoding( Encoding encoding ) {
            _encoding = encoding;
            return This();
        }

        public TRequest Encoding( string encoding ) {
            return Encoding( System.Text.Encoding.GetEncoding( encoding ) );
        }

       
        public TRequest ContentType( HttpContentType contentType ) {
            return ContentType( contentType.Description() );
        }

       
        public TRequest ContentType( string contentType ) {
            _contentType = contentType;
            return This();
        }

   
        private TRequest This() {
            return (TRequest)(object)this;
        }

       
        public TRequest Cookie( string name, string value, double expiresDate ) {
            return Cookie( name, value, null, null, DateTime.Now.AddDays( expiresDate ) );
        }

        
        public TRequest Cookie( string name, string value, DateTime expiresDate ) {
            return Cookie( name, value, null, null, expiresDate );
        }

      
        public TRequest Cookie( string name, string value, string path = "/", string domain = null, DateTime? expiresDate = null ) {
            return Cookie( new Cookie( name, value, path, domain ) {
                Expires = expiresDate ?? DateTime.Now.AddYears( 1 )
            } );
        }

    
        public TRequest Cookie( Cookie cookie ) {
            _cookieContainer.Add( new Uri( _url ), cookie );
            return This();
        }

   
        public TRequest Timeout( int timeout ) {
            _timeout = new TimeSpan( 0, 0, timeout );
            return This();
        }

       
        public TRequest Header<T>( string key, T value ) {
            _headers.Add( key, value.SafeString() );
            return This();
        }

        
        public TRequest Data( IDictionary<string, object> parameters ) {
            _params = parameters ?? throw new ArgumentNullException( nameof( parameters ) );
            return This();
        }


        public TRequest Data( string key, object value ) {
            if( string.IsNullOrWhiteSpace( key ) )
                throw new ArgumentNullException( nameof( key ) );
            if( string.IsNullOrWhiteSpace( value.SafeString() ) )
                return This();
            _params.Add( key, value );
            return This();
        }

     
        public TRequest JsonData<T>( T value ) {
            ContentType( HttpContentType.Json );
            _data = Json.Json.ToJson( value );
            return This();
        }

       
        public TRequest XmlData( string value ) {
            ContentType( HttpContentType.Xml );
            _data = value;
            return This();
        }

        
        public TRequest OnFail( Action<string> action ) {
            _failAction = action;
            return This();
        }

       
        public TRequest OnFail( Action<string, HttpStatusCode> action ) {
            _failStatusCodeAction = action;
            return This();
        }
      
        public TRequest IgnoreSsl() {
            _serverCertificateCustomValidationCallback = ( a, b, c, d ) => true;
            return This();
        }

        public TRequest BearerToken( string token ) {
            _token = token;
            return This();
        }

      

        

      
        public async Task<string> ResultAsync() {
            SendBefore();
            var response = await SendAsync();
            var result = await response.Content.ReadAsStringAsync();
            SendAfter( result, response );
            return result;
        }

      

        
        protected virtual void SendBefore() {
        }

       
       

        protected async Task<HttpResponseMessage> SendAsync() {
            var client = CreateHttpClient();
            InitHttpClient( client );
            return await client.SendAsync( CreateRequestMessage() );
        }

      
        protected virtual HttpClient CreateHttpClient() {
            return new HttpClient( new HttpClientHandler {
                CookieContainer = _cookieContainer,
                ServerCertificateCustomValidationCallback = _serverCertificateCustomValidationCallback
            } ) { Timeout = _timeout };
        }

     
        protected virtual void InitHttpClient( HttpClient client ) {
          //  client.SetBearerToken( _token );
        }

     
        protected virtual HttpRequestMessage CreateRequestMessage() {
            var message = new HttpRequestMessage {
                Method = _httpMethod,
                RequestUri = new Uri( _url ),
                Content = CreateHttpContent()
            };
            foreach( var header in _headers )
                message.Headers.Add( header.Key, header.Value );
            return message;
        }

      
        private HttpContent CreateHttpContent() {
            var contentType = _contentType.SafeString().ToLower();
            switch( contentType ) {
                case "application/x-www-form-urlencoded":
                    return new FormUrlEncodedContent( _params.ToDictionary( t => t.Key, t => t.Value.SafeString() ) );
                case "application/json":
                    return CreateJsonContent();
                case "text/xml":
                    return CreateXmlContent();
            }
            throw new NotImplementedException( "ContentType" );
        }

     
        private HttpContent CreateJsonContent() {
            if( string.IsNullOrWhiteSpace( _data ) )
                _data = Json.Json.ToJson( _params );
            return new StringContent( _data, _encoding, "application/json" );
        }

        
        private HttpContent CreateXmlContent() {
            return new StringContent( _data, _encoding, "text/xml" );
        }

     

    
        protected virtual void SendAfter( string result, HttpResponseMessage response ) {
            var contentType = GetContentType( response );
            if( response.IsSuccessStatusCode ) {
                SuccessHandler( result, response.StatusCode, contentType );
                return;
            }
            FailHandler( result, response.StatusCode, contentType );
        }

        
        private string GetContentType( HttpResponseMessage response ) {
            return response?.Content?.Headers?.ContentType == null ? string.Empty : response.Content.Headers.ContentType.MediaType;
        }

        protected virtual void SuccessHandler( string result, HttpStatusCode statusCode, string contentType ) {
        }

       
        protected virtual void FailHandler( string result, HttpStatusCode statusCode, string contentType ) {
            _failAction?.Invoke( result );
            _failStatusCodeAction?.Invoke( result, statusCode );
        }

      
    }
}
