using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zeus.DataLayer.EntityFramework.Core;
using Zeus.DataLayer.UnitOfWorks;
using Zeus.Helpers;
using Zeus.Logs;
using Zeus.Logs.Extensions;

namespace Zeus.DataLayer.NHibernate.Logs
{

    public class EfLog : ILogger {
      
        public const string TraceLogName = "EfTraceLog";

       
        public void Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter ) {
            var config = GetConfig();
            var log = GetLog();
            if( IsEnabled( eventId, config ) == false )
                return;
            log.Caption( $"：" )
                .Content( $": {GetUnitOfWork()?.TraceId}" )
                .Content( $": {eventId.Id}" )
                .Content( $": {eventId.Name}" );
            AddContent( state, config, log );
            log.Exception( exception ).Trace();
        }

       
        private EfConfig GetConfig() {
            try {
                var options = Ioc.Create<IOptionsSnapshot<EfConfig>>();
                return options.Value;
            }
            catch {
                return new EfConfig { EfLogLevel = EfLogLevel.Sql };
            }
        }

       
        protected virtual ILog GetLog() {
            try {
                return Zeus.Logs.Log.GetLog( TraceLogName );
            }
            catch {
                return Zeus.Logs.Log.Null;
            }
        }

      
        protected virtual UnitOfWorkBase GetUnitOfWork() {
            try {
                var unitOfWork = Ioc.Create<IUnitOfWork>();
                return unitOfWork as UnitOfWorkBase;
            }
            catch {
                return null;
            }
        }

       
        private bool IsEnabled( EventId eventId, EfConfig config ) {
            if( config.EfLogLevel == EfLogLevel.Off )
                return false;
            if( config.EfLogLevel == EfLogLevel.All )
                return true;
            if( eventId.Name == "Microsoft.EntityFrameworkCore.Database.Command.CommandExecuted" )
                return true;
            return false;
        }

      
        private void AddContent<TState>( TState state, EfConfig config, ILog log ) {
            if( config.EfLogLevel == EfLogLevel.All )
                log.Content( "：" ).Content( state.SafeString() );
            if( !( state is IEnumerable list ) )
                return;
            var dictionary = new Dictionary<string, string>();
            foreach( KeyValuePair<string, object> item in list )
                dictionary.Add( item.Key, item.Value.SafeString() );
            AddDictionary( dictionary, log );
        }

     
        private void AddDictionary( IDictionary<string, string> dictionary, ILog log ) {
            AddElapsed( GetValue( dictionary, "elapsed" ), log );
            var sqlParams = GetValue( dictionary, "parameters" );
            AddSql( GetValue( dictionary, "commandText" ), log );
            AddSqlParams( sqlParams, log );
        }

       
        private string GetValue( IDictionary<string, string> dictionary, string key ) {
            if( dictionary.ContainsKey( key ) )
                return dictionary[key];
            return string.Empty;
        }

     
        private void AddElapsed( string value, ILog log ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return;
            log.Content( $": {value} " );
        }

      
        private void AddSql( string sql, ILog log ) {
            if( string.IsNullOrWhiteSpace( sql ) )
                return;
            log.Sql( $"{sql}{Common.Line}" );
        }

       
        private void AddSqlParams( string value, ILog log ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return;
            log.SqlParams( value );
        }

      
        public bool IsEnabled( LogLevel logLevel ) {
            return true;
        }

      
        public IDisposable BeginScope<TState>( TState state ) {
            return null;
        }
    }
}
