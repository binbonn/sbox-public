namespace SystemTest;

[TestClass]
public class TexturePathNormalizationTest
{
	[TestMethod]
	public void NormalizeLookupPath_PreservesRemoteUrlCasing()
	{
		var url = "https://Example.com/Path/Image.JPG?Token=AbCd";
		var normalized = Texture.NormalizeLookupPath( url );

		Assert.AreEqual( url, normalized );
	}

	[TestMethod]
	public void NormalizeLookupPath_NormalizesRemoteUrlSchemeCase()
	{
		var url = "HTTPS://Example.com/Path/Image.JPG";
		var normalized = Texture.NormalizeLookupPath( url );

		Assert.AreEqual( "https://Example.com/Path/Image.JPG", normalized );
	}

	[TestMethod]
	public void NormalizeLookupPath_LowercasesLocalPaths()
	{
		var localPath = "assets\\MyImage.PNG";
		var normalized = Texture.NormalizeLookupPath( localPath );

		Assert.AreEqual( "assets/myimage.png", normalized );
	}

	[TestMethod]
	public void NormalizeLookupPath_PreservesDataUriPayloadCasing()
	{
		var dataUri = "DATA:IMAGE/PNG;BASE64,AbCdEf0123+/=";
		var normalized = Texture.NormalizeLookupPath( dataUri );

		Assert.AreEqual( "data:IMAGE/PNG;BASE64,AbCdEf0123+/=", normalized );
	}

	[TestMethod]
	public void ImageDataUri_IsAppropriate_IgnoresPrefixCase()
	{
		Assert.IsTrue( TextureLoader.ImageDataUri.IsAppropriate( "DATA:IMAGE/PNG;BASE64,AA==" ) );
		Assert.IsTrue( TextureLoader.ImageDataUri.IsAppropriate( "Data:Image/Jpeg;Base64,AA==" ) );
	}
}
