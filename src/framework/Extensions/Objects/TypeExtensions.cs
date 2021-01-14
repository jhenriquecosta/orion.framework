using System;
using System.Reflection;

namespace Orion.Framework
{
	public static class TypeExtensions
	{
		public static Assembly GetAssembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}
	}
}
