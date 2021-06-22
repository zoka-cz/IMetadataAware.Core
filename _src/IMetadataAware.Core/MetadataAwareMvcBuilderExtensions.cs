using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Zoka.IMetadataAwareCore
{
	/// <summary>Configuring Mvc for using IMetadataAware functionality</summary>
	/// <remarks>http://blog.emikek.com/reinstating-imetadataaware-in-asp-net-5-vnext-mvc-6/</remarks>
	public static class MetadataAwareMvcBuilderExtensions
	{
		/// <summary>Will register MetadataAwareProvider into MVC</summary>
		public static IMvcBuilder							UseModelMetadataAwareAttributes(this IMvcBuilder _mvc_builder, bool _scan_all_loaded_dlls)
		{
			_mvc_builder.AddMvcOptions(o => {
				o.ModelMetadataDetailsProviders.Add(new MetadataAwareProvider());
			});

			if (_scan_all_loaded_dlls)
			{
				foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies().Union(AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies()))
				{
					foreach (Type t in a.GetTypes())
					{
						if (typeof(IMetadataAware).IsAssignableFrom(t) && t != typeof(IMetadataAware))
						{
							MetadataAwareProvider.RegisterMetadataAwareAttribute(t);
						}
					}
				}
			}

			return _mvc_builder;
		}
	}
}
