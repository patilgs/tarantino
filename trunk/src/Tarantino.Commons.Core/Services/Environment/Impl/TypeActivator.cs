using System;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class TypeActivator : ITypeActivator
	{
		public T ActivateType<T>(string typeDescriptor)
		{
			Type type = Type.GetType(typeDescriptor);
			T instance = (T)Activator.CreateInstance(type);

			return instance;
		}
	}
}