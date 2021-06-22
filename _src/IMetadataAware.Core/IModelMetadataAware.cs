using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Zoka.IMetadataAwareCore
{
	/// <summary>Interface, which mimics the IMetadataAware from the .NET Frameworks</summary>
	public interface IMetadataAware
	{
		/// <summary></summary>
		void GetDisplayMetadata(DisplayMetadataProviderContext context);
	}
}
