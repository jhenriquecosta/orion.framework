using System;
using AutoMapper;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper.Configuration;
using System.Linq;
using System.Linq.Expressions;

namespace Orion.Framework.Maps
{
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly object Sync = new object();
        /// <summary>
        /// 
        /// </summary>
        private static IConfigurationProvider _config;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return MapTo<TDestination>(source, destination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        public static TDestination MapTo<TDestination>(this object source) where TDestination : new()
        {
            return MapTo(source, new TDestination());
        }

        /// <summary>
        /// 
        /// </summary>
        private static TDestination MapTo<TDestination>(object source, TDestination destination)
        {
            if (source == null)
                return default(TDestination);
            if (destination == null)
                return default(TDestination);
            var sourceType = GetType(source);
            var destinationType = GetType(destination);
            if (Exists(sourceType, destinationType))
                return GetResult(source, destination);
            lock (Sync)
            {
                if (Exists(sourceType, destinationType))
                    return GetResult(source, destination);
                Init(sourceType, destinationType);
            }
            return GetResult(source, destination);
        }

        /// <summary>
        /// 
        /// </summary>
        private static Type GetType(object obj)
        {
            var type = obj.GetType();
            if ((obj is System.Collections.IEnumerable) == false)
                return type;
            if (type.IsArray)
                return type.GetElementType();
            var genericArgumentsTypes = type.GetTypeInfo().GetGenericArguments();
            if (genericArgumentsTypes == null || genericArgumentsTypes.Length == 0)
                throw new ArgumentException("");
            return genericArgumentsTypes[0];
        }

        /// <summary>
        /// 
        /// </summary>
        private static bool Exists(Type sourceType, Type destinationType)
        {
            return _config?.FindTypeMapFor(sourceType, destinationType) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Init(Type sourceType, Type destinationType)
        {
            if (_config == null)
            {
                _config = new MapperConfiguration(t => t.CreateMap(sourceType, destinationType));
                return;
            }
            var maps = _config.GetAllTypeMaps();
            _config = new MapperConfiguration(t => t.CreateMap(sourceType, destinationType));
            foreach (var map in maps)
                _config.RegisterTypeMap(map);
        }

        /// <summary>
        /// 
        /// </summary>
        private static TDestination GetResult<TDestination>(object source, TDestination destination)
        {
            return new Mapper(_config).Map(source, destination);
        }
        //private static TDestination ProjectTo<TDestination>(object source, TDestination destination)
        //{
        //   // return new Mapper(_config).ProjectTo<TDestination>(source)
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDestination">：Sample,</typeparam>
        /// <param name="source"></param>
        public static List<TDestination> MapToList<TDestination>(this System.Collections.IEnumerable source)
        {
            return MapTo<List<TDestination>>(source);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    //public static class Extensions
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private static readonly object Sync = new object();

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <typeparam name="TSource"></typeparam>
    //    /// <typeparam name="TDestination"></typeparam>
    //    /// <param name="source"></param>
    //    /// <param name="destination">目标对象</param>
    //    public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
    //    {
    //        return MapTo<TDestination>(source, destination);
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <typeparam name="TDestination"></typeparam>
    //    /// <param name="source"></param>
    //    public static TDestination MapTo<TDestination>(this object source) where TDestination : new()
    //    {
    //        return MapTo(source, new TDestination());
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private static TDestination MapTo<TDestination>(object source, TDestination destination)
    //    {
    //        if (source == null)
    //            return default(TDestination);
    //        if (destination == null)
    //            return default(TDestination);
    //        var sourceType = GetType(source);
    //        var destinationType = GetType(destination);
    //        var map = GetMap(sourceType, destinationType);
    //        if (map != null)
    //            return Mapper.Map(source, destination);
    //        lock (Sync)
    //        {
    //            map = GetMap(sourceType, destinationType);
    //            if (map != null)
    //                return Mapper.Map(source, destination);
    //            InitMaps(sourceType, destinationType);
    //        }
    //        return Mapper.Map(source, destination);
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private static Type GetType(object obj)
    //    {
    //        var type = obj.GetType();
    //        if ((obj is System.Collections.IEnumerable) == false)
    //            return type;
    //        if (type.IsArray)
    //            return type.GetElementType();
    //        var genericArgumentsTypes = type.GetTypeInfo().GetGenericArguments();
    //        if (genericArgumentsTypes == null || genericArgumentsTypes.Length == 0)
    //            throw new ArgumentException("Erro - Automapper");
    //        return genericArgumentsTypes[0];
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private static TypeMap GetMap(Type sourceType, Type destinationType)
    //    {
    //        try
    //        {
    //            return Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
    //        }
    //        catch (InvalidOperationException)
    //        {

    //            lock (Sync)
    //            {
    //                try
    //                {
    //                    return Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
    //                }
    //                catch (InvalidOperationException)
    //                {
    //                    InitMaps(sourceType, destinationType);
    //                }
    //                return Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private static void InitMaps(Type sourceType, Type destinationType)
    //    {
    //        try
    //        {
    //            var maps = Mapper.Configuration.GetAllTypeMaps();
    //            Mapper.Reset();
    //            InitMapper(sourceType, destinationType);
    //            foreach (var map in maps)
    //                Mapper.Configuration.RegisterTypeMap(map);
    //        }
    //        catch (InvalidOperationException)
    //        {
    //            InitMapper(sourceType, destinationType);
    //        }
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private static void InitMapper(Type sourceType, Type destinationType)
    //    {
    //        var config = new MapperConfigurationExpression();
    //        config.CreateMap(sourceType, destinationType);
    //        Mapper.Initialize(config);
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <typeparam name="TDestination"></typeparam>
    //    /// <param name="source"></param>
    //    public static List<TDestination> MapToList<TDestination>(this System.Collections.IEnumerable source)
    //    {
    //        return MapTo<List<TDestination>>(source);
    //    }

    //    /// <summary>
    //    /// UNFLATTEN
    //    /// </summary>
    //    /// <typeparam name="TDestination"></typeparam>
    //    /// <param name="source"></param>
    //    public static void Unflatten<TSource, TDestination, TMember>(this IMemberConfigurationExpression<TSource, TDestination, TMember> opt)
    //    {
    //        var prefix = opt.DestinationMember.Name;
    //        var memberProps = typeof(TMember).GetProperties();
    //        var props = typeof(TSource).GetProperties().Where(p => p.Name.StartsWith(prefix))
    //            .Select(sourceProp => new
    //            {
    //                SourceProp = sourceProp,
    //                MemberProp = memberProps.FirstOrDefault(memberProp => prefix + memberProp.Name == sourceProp.Name)
    //            })
    //            .Where(x => x.MemberProp != null);
    //        var parameter = Expression.Parameter(typeof(TSource));

    //        var bindings = props.Select(prop => Expression.Bind(prop.MemberProp, Expression.Property(parameter, prop.SourceProp)));
    //        var resolver = Expression.Lambda<Func<TSource, TMember>>(
    //            Expression.MemberInit(Expression.New(typeof(TMember)), bindings),
    //            parameter);

    //       // opt.AddTransform(resolver.Compile());
    //    }
    //}


}
