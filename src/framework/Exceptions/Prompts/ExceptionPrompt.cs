using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using Orion.Framework.Helpers;
using Orion.Framework.Properties;

namespace Orion.Framework.Exceptions.Prompts
{

    public static class ExceptionPrompt {
     
        private static readonly List<IExceptionPrompt> Prompts = new List<IExceptionPrompt>();

      
        public static bool IsShowSystemException { get; set; }

       
        public static void AddPrompt( IExceptionPrompt prompt ) {
            if( prompt == null )
                throw new ArgumentNullException( nameof( prompt ) );
            if( Prompts.Contains( prompt ) )
                return;
            Prompts.Add( prompt );
        }

        public static string GetPrompt( Exception exception ) {
            if ( exception == null )
                return null;
            exception = exception.GetRawException();
            var prompt = GetExceptionPrompt( exception );
            if( string.IsNullOrWhiteSpace( prompt ) == false )
                return prompt;
            if( exception is Warning warning )
                return warning.Message;
            if (WebHttp.Environment.IsDevelopment() || IsShowSystemException )
                return exception.Message;
            return R.SystemError;
        }

        private static string GetExceptionPrompt( Exception exception ) {
            foreach( var prompt in Prompts ) {
                var result = prompt.GetPrompt( exception );
                if( string.IsNullOrWhiteSpace( result ) == false )
                    return result;
            }
            return string.Empty;
        }
    }
}
