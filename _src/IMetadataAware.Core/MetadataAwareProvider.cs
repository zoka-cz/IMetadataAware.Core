using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Zoka.IMetadataAwareCore
{
	/// <summary>Metadata provider for IMetadaAwareProvider</summary>
	/// <remarks>
	///		Usage:
	///			1) register all the IModelMetadataAware interfaces
	///				you may use helper method MetadataAwareMvcBuilderExtensions.UseModelMetadataAwareAttributes
	///			2) register this MetadataAwareProvider in StartUp
	///				or use the helper method MetadataAwareMvcBuilderExtensions.UseModelMetadataAwareAttributes
	/// </remarks>
	public class MetadataAwareProvider : IDisplayMetadataProvider
	{
		private static List<Type>							Registry = new List<Type>();

		/// <summary>Will register the IMetadataAware attribute</summary>
		public static void									RegisterMetadataAwareAttribute(Type _attribute)
		{
			if (_attribute == null)
				throw new ArgumentNullException(nameof(_attribute));

			if (!typeof(IMetadataAware).IsAssignableFrom(_attribute))
				throw new ArgumentException("_attribute is not IModelMetadataAware", nameof(_attribute));

			Registry.Add(_attribute);
		}

		/// <inheritdoc />
		public void CreateDisplayMetadata(DisplayMetadataProviderContext _context)
		{
			if (_context == null)
				throw new ArgumentNullException(nameof(_context));

			foreach (var type in Registry)
			{
				IMetadataAware attribute = _context
					.Attributes
					.Where(a => a.GetType() == type)
					.FirstOrDefault() as IMetadataAware;
				
				if (attribute != null)
					attribute.GetDisplayMetadata(_context);
			}
		}
	}
}
