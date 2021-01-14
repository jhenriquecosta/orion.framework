using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Orion.Framework.Helpers;
using Orion.Framework.Validations;

namespace Orion.Framework.Domains {
  
    public abstract class DomainBase<T> : IDomainObject, ICompareChange<T> where T : IDomainObject
    {

        private StringBuilder _description;
      
        private readonly List<IValidationRule> _rules;
      
        private IValidationHandler _handler;
      
        private ChangeValueCollection _changeValues;

       
        protected DomainBase() 
        {
            _rules = new List<IValidationRule>();
            _handler = new ThrowHandler();
        }

       

        public virtual void SetValidationHandler( IValidationHandler handler ) {
            if( handler == null )
                return;
            _handler = handler;
        }

      

        public virtual void AddValidationRules( IEnumerable<IValidationRule> rules ) {
            if( rules == null )
                return;
            foreach( var rule in rules )
                AddValidationRule( rule );
        }

 

        public virtual void AddValidationRule( IValidationRule rule ) {
            if( rule == null )
                return;
            _rules.Add( rule );
        }

       

    
        public virtual ValidationResultCollection Validate()
        {
            var result = GetValidationResults();
            //HandleValidationResults( result );
            return result;
        }

   
        public virtual ValidationResultCollection GetValidationResults() {
            var result = DataAnnotationValidation.Validate( this );
            Validate( result );
            foreach( var rule in _rules )
                result.Add( rule.Validate() );
            return result;
        }

        protected virtual void Validate( ValidationResultCollection results ) {
        }

       
        protected virtual void HandleValidationResults( ValidationResultCollection results ) {
            if( results.IsValid )
                return;
            _handler.Handle( results );
        }

        public virtual ChangeValueCollection GetChanges( T newEntity ) {
            _changeValues = new ChangeValueCollection();
            if( Equals( newEntity, null ) )
                return _changeValues;
            AddChanges( newEntity );
            return _changeValues;
        }

     
        protected virtual void AddChanges( T newEntity ) {
        }

      
        protected virtual void AddChange<TProperty, TValue>( Expression<Func<T, TProperty>> expression, TValue newValue ) {
            var member = Orion.Framework.Helpers.Lambda.GetMemberExpression( expression );
            var name = Orion.Framework.Helpers.Lambda.GetMemberName( member );
            var description = Orion.Framework.Helpers.Reflection.GetDisplayNameOrDescription( member.Member );
            var value = member.Member.GetPropertyValue( this );
            AddChange( name, description, Orion.Framework.Helpers.TypeConvert.To<TValue>( value ), newValue );
        }

      
        protected virtual void AddChange<TValue>( string propertyName, string description, TValue oldValue, TValue newValue ) {
            if( Equals( oldValue, newValue ) )
                return;
            string oldValueString = oldValue.SafeString().ToLower().Trim();
            string newValueString = newValue.SafeString().ToLower().Trim();
            if( oldValueString == newValueString )
                return;
            _changeValues.Add( propertyName, description, oldValueString, newValueString );
        }

      
        protected virtual void AddChange<TDomainObject>( ICompareChange<TDomainObject> oldObject, TDomainObject newObject ) where TDomainObject : IDomainObject {
            if( Equals( oldObject, null ) )
                return;
            if( Equals( newObject, null ) )
                return;
            _changeValues.AddRange( oldObject.GetChanges( newObject ) );
        }

       
        protected virtual void AddChange<TDomainObject>( IEnumerable<ICompareChange<TDomainObject>> oldObjects, IEnumerable<TDomainObject> newObjects ) where TDomainObject : IDomainObject {
            if( Equals( oldObjects, null ) )
                return;
            if( Equals( newObjects, null ) )
                return;
            var oldList = oldObjects.ToList();
            var newList = newObjects.ToList();
            for( int i = 0; i < oldList.Count; i++ ) {
                if( newList.Count <= i )
                    return;
                AddChange( oldList[i], newList[i] );
            }
        }

   

        protected virtual void AddDescriptions() {
        }

       
        protected virtual void AddDescription( string description ) {
            if( string.IsNullOrWhiteSpace( description ) )
                return;
            _description.Append( description );
        }


        protected virtual void AddDescription<TValue>( string name, TValue value ) {
            if( string.IsNullOrWhiteSpace( value.SafeString() ) )
                return;
            _description.AppendFormat( "{0}:{1},", name, value );
        }

       
        protected virtual void AddDescription<TProperty>( Expression<Func<T, TProperty>> expression ) {
            var member = Orion.Framework.Helpers.Lambda.GetMember( expression );
            var description = Orion.Framework.Helpers.Reflection.GetDisplayNameOrDescription( member );
            var value = member.GetPropertyValue( this );
            if ( Reflection.IsBool( member ) )
                value = Orion.Framework.Helpers.TypeConvert.ToBool( value ).Description();
            AddDescription( description, value );
        }


    
        public override string ToString() {
            _description = new StringBuilder();
            AddDescriptions();
            return _description.ToString().TrimEnd().TrimEnd( ',' );
        }

    }
}