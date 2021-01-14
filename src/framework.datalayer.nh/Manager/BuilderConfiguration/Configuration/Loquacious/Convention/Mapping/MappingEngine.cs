using Framework.DataLayer.NHibernate.Loquacious.Convention.Config;
using Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Extensions;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Framework.DataLayer.NHibernate.Loquacious.Convention.Naming;
using Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.ConfigExt;
using NHibernate.Dialect;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using Framework.DataLayer.NHibernate.Reflections;
using Framework.DataLayer.Models.Mappings.Attributes;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Mapping
{
    /// <summary>
    /// Builds the mappings configuration from a given settings
    /// </summary>
    internal class MappingEngine
    {
        #region Private Members

        private const string MapSuffix = "Map";
        private ConventionMapConfig rootConfig;
        private Configuration nhConfig;
        private ConventionModelMapper modelMapper;
        private INamingEngine namingEngine;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingEngine"/> class
        /// </summary>
        /// <param name="rootConfig">The root configuration</param>
        /// <param name="nhConfig">The current NH configuration</param>
        /// <param name="modelMapper">The model mapper</param>
        public MappingEngine(ConventionMapConfig rootConfig, Configuration nhConfig, ConventionModelMapper modelMapper)
        {
            this.rootConfig = rootConfig;
            this.nhConfig = nhConfig;
            this.modelMapper = modelMapper;
            this.namingEngine = new NamingEngine(rootConfig.NamingConventionsConfig);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds the mappings configuration from the root config by using an instance of NH configuration
        /// </summary>
        public void BuildConfiguration()
        {
            SetNamingStrategy();
            SetEntityDefintions();
            AddCustomMappings();
            SetMappingConventions();
            InitalizeExtensions();
            CompileMappingsForEntities(true);
        }
        
        /// <summary>
        /// Compiles the mappings without building them
        /// </summary>
        public HbmMapping CompileMappings()
        {
            SetNamingStrategy();
            SetEntityDefintions();
            AddCustomMappings();
            SetMappingConventions();

            return CompileMappingsForEntities(false);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the default naming strategy
        /// </summary>
        private void SetNamingStrategy()
        {
            var dialect = Dialect.GetDialect(nhConfig.Properties);
            if ((dialect != null) && (dialect.GetType().InheritsFrom(typeof(MsSql2008Dialect))))
            {
                this.nhConfig.SetNamingStrategy(new ConventionNamingStrategy());
            }
        }

        /// <summary>
        /// Indicates the Model Mapper how the Entities and Root Entities are identified
        /// </summary>
        private void SetEntityDefintions()
        {
            this.modelMapper.IsEntity((type, declared) => IsEntity(type));
            this.modelMapper.IsRootEntity((type, declared) => IsRootEntity(type));
        }

        /// <summary>
        /// Adds the custom mappings list to the ModelMapper
        /// </summary>
        private void AddCustomMappings()
        {
            this.rootConfig.MappingsConfig.AddMappingFilter(t => t.Name.EndsWith(MapSuffix) || t.InheritsFrom(typeof(ClassMapping<>)) || t.InheritsFrom(typeof(ComponentMapping<>))
                || t.InheritsFrom(typeof(JoinedSubclassMapping<>)) || t.InheritsFrom(typeof(SubclassMapping<>)) || t.InheritsFrom(typeof(UnionSubclassMapping<>)));
            var mappingsList = this.rootConfig.MappingsConfig.GetMappings(AppDomain.CurrentDomain.GetAssemblies());

            this.modelMapper.AddMappings(mappingsList);
        }

        /// <summary>
        /// Compiles the mappings for the entities and adds the mappings to the configuration
        /// </summary>
        /// <param name="buildMappings">A value indicating if the mappings should be built</param>
        private HbmMapping CompileMappingsForEntities(bool buildMappings)
        {
            IList<Type> entityTypes = this.rootConfig.EntityDefinitionsConfig.GetSourceAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .SelectMany(a => a.GetTypes())
                .Where(IsEntity)
                .Distinct()
                .ToList();

            entityTypes = entityTypes.Concat(this.rootConfig.EntityDefinitionsConfig.EntityTypes.Where(e => IsEntity(e) && !entityTypes.Contains(e))).ToList();

            this.rootConfig.Extensions.ToList().ForEach(e => e.BeforeCompileMappings(entityTypes));
            var hbmMapping = this.modelMapper.CompileMappingFor(entityTypes);

         

            this.rootConfig.Extensions.ToList().ForEach(e => e.AfterCompileMappings(entityTypes, hbmMapping));

            if (buildMappings)
            {
                this.rootConfig.Extensions.ToList().ForEach(e => e.BeforeBuildMappings(hbmMapping));
                this.nhConfig.AddMapping(hbmMapping);
                this.nhConfig.BuildMappings();
                this.rootConfig.Extensions.ToList().ForEach(e => e.AfterBuildMappings(hbmMapping));
            }

            XmlSerializer ser = new XmlSerializer(hbmMapping.GetType());
            Stream fs = new FileStream("C:\\usr\\maps.xml", FileMode.Create);
            XmlWriter writer = new XmlTextWriter(fs, Encoding.UTF8);
            // Serialize using the XmlTextWriter.
            ser.Serialize(writer,hbmMapping);
            writer.Close();


            return hbmMapping;
        }

        /// <summary>
        /// Initializes the registered extensions
        /// </summary>
        private void InitalizeExtensions()
        {
            this.rootConfig.Extensions.ToList().ForEach(e => e.Init(this.nhConfig, this.modelMapper, new NamingConventionsSettings(this.rootConfig.NamingConventionsConfig), this.namingEngine));
        }

        /// <summary>
        /// Sets the mapping rules and conventions
        /// </summary>
        private void SetMappingConventions()
        {
            

            // Naming Mapping
            this.modelMapper.AfterMapClass += MapClass;
            this.modelMapper.AfterMapProperty += MapProperty;

            // Relationships mapping
            this.modelMapper.AfterMapManyToOne += MapManyToOne;
            this.modelMapper.AfterMapManyToMany += MapManyToMany;

           

            //Collection mappings
            this.modelMapper.AfterMapBag += MapCollection;
            this.modelMapper.AfterMapIdBag += MapCollection;
            this.modelMapper.AfterMapList += MapCollection;
            this.modelMapper.AfterMapSet += MapCollection;
            this.modelMapper.AfterMapMap += MapCollection;
            this.modelMapper.AfterMapElement += MapElement;
            this.modelMapper.AfterMapMapKey += MapMapKey;

            // Inheritance/composite mappings
            this.modelMapper.AfterMapUnionSubclass += MapUnionSubclass;
            this.modelMapper.AfterMapJoinedSubclass += MapJoinedSubclass;
        }

       
        private void MapClass(IModelInspector modelInspector, Type classType, IClassAttributesMapper mapper)
        {
            Type entityType = classType.UnderlyingSystemType;

            string schemaName = namingEngine.ToSchemaName(entityType) ?? mapper.GetSchema();
            string tableName = namingEngine.ToTableName(entityType);
            var idProperty = modelInspector.GetIdentifierMember(entityType);
            var versionProperty = modelInspector.GetVersionMember(entityType);
            string primaryKeyColumnName = namingEngine.ToPrimaryKeyColumnName(entityType, idProperty);

            // Mapping
            mapper.Schema(schemaName);
            mapper.Table(tableName);
            mapper.Id(id => id.Column(primaryKeyColumnName));

            // Version mapping
            if (versionProperty != null)
            {
                string versionColumnName = namingEngine.ToColumnName(versionProperty);
                mapper.Version(versionProperty, m => m.Column(versionColumnName));
            }
        }

        /// <summary>
        /// Maps a property according the naming conventions configuration
        /// </summary>
        /// <param name="modelInspector">The model inspector</param>
        /// <param name="property">The property</param>
        /// <param name="mapper">The property mapper</param>
        private void MapProperty(IModelInspector modelInspector, PropertyPath property, IPropertyMapper mapper)
        {

            var attrib = ReflectionHelper.GetSingleAttributeOrDefault<XTFieldMappingAttribute>(property.LocalMember);
            if (attrib != null)
            {
                if (attrib.Ignore) return;
            }
            if (MatchOneToOneComponent(property, modelInspector))
            {
                mapper.Column(this.namingEngine.ToComponentColumnName(property.LocalMember, property.PreviousPath.LocalMember));
            }
            else
            {
                mapper.Column(this.namingEngine.ToColumnName(property.LocalMember));
            }
        }

        /// <summary>
        /// Maps a collection of components or entities
        /// </summary>
        /// <param name="modelInspector">The model inspector</param>
        /// <param name="property">The property to map</param>
        /// <param name="mapper">The collections mapper</param>
        private void MapCollection(IModelInspector modelInspector, PropertyPath property, ICollectionPropertiesMapper mapper)
        {
            Type sourceType = property.GetContainerEntity(modelInspector);
            Type targetType = property.LocalMember.GetPropertyOrFieldType().DetermineCollectionElementType();

            var primaryKeyProperty = modelInspector.GetIdentifierMember(sourceType);
            var foreignKeyProperty = property.LocalMember;
            string foreignKeyColumnName = null;
            string foreignKeyName = null;
            string tableName = null;
            string schemaName = null;

            if (modelInspector.IsEntity(targetType))
            {
                // Entity Relationship Mapping
                if (modelInspector.IsManyToManyItem(property.LocalMember))
                {
                    // Many to many
                    foreignKeyColumnName = namingEngine.ToManyToManyForeignKeyColumnName(sourceType, primaryKeyProperty);
                    foreignKeyName = namingEngine.ToManyToManyForeignKeyName(sourceType, targetType, sourceType, primaryKeyProperty);
                    tableName = namingEngine.ToManyToManyTableName(sourceType, targetType);
                    schemaName = namingEngine.ToSchemaName(sourceType, targetType);
                }
                else
                {
                  
                    // One to Many
                    foreignKeyColumnName = namingEngine.ToForeignKeyColumnName(sourceType, primaryKeyProperty);
                    foreignKeyName = namingEngine.ToForeignKeyName(targetType, sourceType, sourceType, primaryKeyProperty);
                }
            }
            else if (IsElement(targetType))
            {
                // Element mapping
                foreignKeyColumnName = namingEngine.ToForeignKeyColumnName(sourceType, primaryKeyProperty);
                foreignKeyName = namingEngine.ToComponentForeignKeyName(targetType, sourceType, foreignKeyProperty, primaryKeyProperty);
                tableName = namingEngine.ToElementTableName(sourceType, targetType, property.LocalMember);
                schemaName = namingEngine.ToSchemaName(sourceType, targetType);
            }
            else
            {
                // Component Relationship Mapping
                foreignKeyColumnName = namingEngine.ToForeignKeyColumnName(sourceType, primaryKeyProperty);
                foreignKeyName = namingEngine.ToComponentForeignKeyName(targetType, sourceType, foreignKeyProperty, primaryKeyProperty);
                tableName = namingEngine.ToComponentTableName(sourceType, targetType, property.LocalMember);
                schemaName = namingEngine.ToSchemaName(sourceType, targetType);
            }

            // Mapping
            mapper.Schema(schemaName);
            mapper.Table(tableName);
            mapper.Key(k =>
            {
                k.Column(foreignKeyColumnName);
                k.ForeignKey(foreignKeyName);
            });
        }

        /// <summary>
        /// Maps a many to many relationship
        /// </summary>
        /// <param name="modelInspector">The model inspector</param>
        /// <param name="property">The property to map</param>
        /// <param name="mapper">The property mapper</param>
        private void MapManyToMany(IModelInspector modelInspector, PropertyPath property, IManyToManyMapper mapper)
        {
            Type sourceType = property.LocalMember.DeclaringType;
            Type targetType = property.LocalMember.GetPropertyOrFieldType().DetermineCollectionElementType();

            var primaryKeyProperty = modelInspector.GetIdentifierMember(targetType);
            var foreignKeyProperty = property.LocalMember;
            string foreignKeyColumnName = namingEngine.ToManyToManyForeignKeyColumnName(targetType, primaryKeyProperty);
            string foreignKeyName = namingEngine.ToManyToManyForeignKeyName(sourceType, targetType, targetType, primaryKeyProperty);

            mapper.Column(foreignKeyColumnName);
            mapper.ForeignKey(foreignKeyName);
        }

        /// <summary>
        /// Maps a many to one relationship
        /// </summary>
        /// <param name="modelInspector">The model inspector</param>
        /// <param name="property">The property to map</param>
        /// <param name="mapper">The property mapper</param>
        private void MapManyToOne(IModelInspector modelInspector, PropertyPath property, IManyToOneMapper mapper)
        {
            Type targetEntityType = property.LocalMember.GetPropertyOrFieldType();
            Type sourceEntityType = property.GetContainerEntity(modelInspector);
            MemberInfo member = property.PreviousPath != null ? property.PreviousPath.LocalMember : property.LocalMember;

            var targetEntityIDProperty = modelInspector.GetIdentifierMember(targetEntityType);
            var foreignKeyProperty = property.LocalMember;

            string columnName = null;
            string foreignKeyName = null;
            var one = modelInspector.IsOneToOne(property.LocalMember);


            if (MatchOneToOneComponent(property, modelInspector))
            {
                columnName = namingEngine.ToComponentForeignKeyColumnName(foreignKeyProperty, member, targetEntityIDProperty);
                foreignKeyName = namingEngine.ToForeignKeyName(sourceEntityType, targetEntityType, member, targetEntityIDProperty);
            }
            else
            {
                columnName = namingEngine.ToForeignKeyColumnName(property.LocalMember, targetEntityIDProperty);
                foreignKeyName = namingEngine.ToForeignKeyName(sourceEntityType, targetEntityType, foreignKeyProperty, targetEntityIDProperty);
            }
          
            mapper.Column(columnName);
            mapper.ForeignKey(foreignKeyName);
        }

       
        private void MapElement(IModelInspector modelInspector, PropertyPath member, IElementMapper mapper)
        {
            string columName = namingEngine.ToElementValueColumnName(member.LocalMember);

            mapper.Column(columName);
        }

        private void MapMapKey(IModelInspector modelInspector, PropertyPath member, IMapKeyMapper mapper)
        {
            string columName = namingEngine.ToElementKeyColumnName(member.LocalMember);

            mapper.Column(columName);
        }

        /// <summary>
        /// Maps a joined subclass inheritance hierarchy
        /// </summary>
        /// <param name="modelInspector">The model inspector</param>
        /// <param name="type">The entity type</param>
        /// <param name="mapper">The joined subclass mapper</param>
        private void MapJoinedSubclass(IModelInspector modelInspector, Type type, IJoinedSubclassAttributesMapper mapper)
        {
            Type entityType = type.UnderlyingSystemType;
            Type baseType = type.GetBaseTypes().FirstOrDefault(t => IsEntity(t));

            string schemaName = namingEngine.ToSchemaName(entityType);
            string tableName = namingEngine.ToTableName(entityType);
            var idProperty = modelInspector.GetIdentifierMember(entityType);
            string foreignKeyColumnName = namingEngine.ToForeignKeyColumnName(baseType, idProperty);
            string foreignKeyName = namingEngine.ToForeignKeyName(entityType, baseType, entityType, idProperty);

            // Mapping
            mapper.Schema(schemaName);
            mapper.Table(tableName);
            mapper.Key(k =>
                {
                    k.Column(foreignKeyColumnName);
                    k.ForeignKey(foreignKeyName);
                });
        }

        /// <summary>
        /// Maps an union subclass inheritance hierarchy
        /// </summary>
        /// <param name="modelInspector">The model inspector</param>
        /// <param name="type">The entity type</param>
        /// <param name="mapper">The union subclass mapper</param>
        private void MapUnionSubclass(IModelInspector modelInspector, Type type, IUnionSubclassAttributesMapper mapper)
        {
            Type entityType = type.UnderlyingSystemType;

            string schemaName = namingEngine.ToSchemaName(entityType);
            string tableName = namingEngine.ToTableName(entityType);

            // Mapping
            mapper.Schema(schemaName);
            mapper.Table(tableName);
        }

        /// <summary>
        /// Indicates if a given property matches a one to one component relationship
        /// </summary>
        /// <param name="property">The property</param>
        /// <param name="modelInspector">An instance of the current model inspector</param>
        /// <returns>True if the property matches a one to one component relationship, false if not</returns>
        private bool MatchOneToOneComponent(PropertyPath property, IModelInspector modelInspector)
        {
            bool result = false;
            var one = modelInspector.IsOneToOne(property.LocalMember.DeclaringType);

            if (modelInspector.IsComponent(property.LocalMember.DeclaringType))
            {
                result = (property.PreviousPath != null) && !property.PreviousPath.LocalMember.GetPropertyOrFieldType().IsGenericCollection();
            }

            return result;
        }

        /// <summary>
        /// Determines if a given type is an entity type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if the type is an entity type, false if not</returns>
        private bool IsEntity(Type type)
        {
            var result = !this.rootConfig.EntityDefinitionsConfig.RootEntityTypes.Contains(type) && this.rootConfig.EntityDefinitionsConfig.RootEntityTypes.Any(t => t.IsAssignableFrom(type) );
          
            return result;
        }

        /// <summary>
        /// Determines if a given type is a root entity type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if the type is a root entity type, false if not</returns>
        private bool IsRootEntity(Type type)
        {
            var result =  this.rootConfig.EntityDefinitionsConfig.RootEntityTypes.Contains(type.BaseType) && !this.rootConfig.EntityDefinitionsConfig.RootEntityTypes.Contains(type);
            return result;
        }

        /// <summary>
        /// Determines if a given type is an element type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if the type is an element type, false if not</returns>
        private bool IsElement(Type type)
        {
            return type.IsValueType || type.IsPrimitive ||
               new Type[] { 
				typeof(String),
				typeof(Decimal),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(Guid)
			}.Contains(type) || Convert.GetTypeCode(type) != TypeCode.Object;
        }


        #endregion
    }
}
